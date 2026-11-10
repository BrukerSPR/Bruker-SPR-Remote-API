using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BrukerSprRemoteAPI.Python.Generator
{
    /// <summary>
    /// Roslyn-based code generator that produces a typed Python client class
    /// from a C# remote service interface using a Python template.
    /// </summary>
    public class ApiGenerator
    {
        public const string TemplateFile = "BrukerSprRemoteAPI.py.template";

        private const string Indent4 = "    ";
        private const string Indent8 = "        ";

        /// <summary>
        /// Generates the Python class code from the given C# interface code and Python template code.
        /// </summary>
        /// <param name="interfaceCode">C# source code containing the interface definition.</param>
        /// <param name="templateCode">Python template code.</param>
        /// <returns>Generated Python source code.</returns>
        public static string GenerateCode(string interfaceCode, string templateCode)
        {
            var generator = new ApiGenerator();
            return generator.Generate(interfaceCode, templateCode);
        }

        /// <summary>
        /// Generates the Python class code from files and writes the output to the specified file path.
        /// </summary>
        /// <param name="interfaceFilePath">Path to the C# interface file.</param>
        /// <param name="templateFilePath">Path to the Python template file.</param>
        /// <param name="outputFilePath">Path to write the generated Python file.</param>
        public static void GenerateFile(string interfaceFilePath, string templateFilePath, string outputFilePath)
        {
            string interfaceCode = File.ReadAllText(interfaceFilePath);
            string templateCode = File.ReadAllText(templateFilePath);
            string generatedCode = GenerateCode(interfaceCode, templateCode);
            File.WriteAllText(outputFilePath, generatedCode, new UTF8Encoding(false));
        }

        /// <summary>
        /// Generates the Python class code from the given C# interface code and Python template code.
        /// </summary>
        /// <param name="interfaceCode">C# source code containing the interface definition.</param>
        /// <param name="templateCode">Python template code.</param>
        /// <returns>Generated Python source code.</returns>
        public string Generate(string interfaceCode, string templateCode)
        {
            if (string.IsNullOrWhiteSpace(interfaceCode))
            {
                throw new ArgumentException("Interface code cannot be null or empty.", nameof(interfaceCode));
            }

            if (string.IsNullOrWhiteSpace(templateCode))
            {
                throw new ArgumentException("Template code cannot be null or empty.", nameof(templateCode));
            }

            var syntaxTree = CSharpSyntaxTree.ParseText(interfaceCode);
            var root = syntaxTree.GetCompilationUnitRoot();

            var interfaceDecl = root.DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                .FirstOrDefault() ?? throw new InvalidOperationException("No interface declaration found in the provided C# source code.");
            var methods = interfaceDecl.Members
                .OfType<MethodDeclarationSyntax>()
                .ToList();

            var sb = new StringBuilder();
            sb.Append(templateCode.TrimEnd());
            sb.AppendLine();
            sb.AppendLine();

            for (int i = 0; i < methods.Count; i++)
            {
                var method = methods[i];
                string pythonMethod = GenerateMethod(method);
                sb.Append(pythonMethod);

                if (i < methods.Count - 1)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private string GenerateMethod(MethodDeclarationSyntax method)
        {
            string methodName = method.Identifier.Text;
            string csharpReturnType = method.ReturnType.ToString().Trim();
            string pythonReturnType = MapToPythonType(csharpReturnType);

            var parameters = method.ParameterList.Parameters
                .Select(p => new ParameterInfo(
                    p.Identifier.Text,
                    p.Type?.ToString().Trim() ?? "object",
                    MapToPythonType(p.Type?.ToString().Trim() ?? "object")))
                .ToList();

            var docInfo = ParseDocumentation(method);

            var sb = new StringBuilder();

            // Method signature: def MethodName(self, param1: type1, param2: type2) -> return_type:
            sb.Append(Indent4);
            sb.Append("def ");
            sb.Append(methodName);
            sb.Append("(self");

            foreach (var param in parameters)
            {
                sb.Append(", ");
                sb.Append(param.Name);
                sb.Append(": ");
                sb.Append(param.PythonType);
            }

            sb.Append(") -> ");
            sb.Append(pythonReturnType);
            sb.AppendLine(" :");

            // Docstring
            string docstring = FormatDocstring(docInfo);
            if (!string.IsNullOrWhiteSpace(docstring))
            {
                sb.AppendLine(docstring);
            }

            // Type checks
            foreach (var param in parameters)
            {
                string typeCheck = GenerateTypeCheck(param);
                sb.AppendLine(typeCheck);
            }

            // API Call
            string callStatement = GenerateApiCall(methodName, parameters);
            sb.AppendLine(callStatement);

            return sb.ToString();
        }

        private static DocumentationInfo ParseDocumentation(MethodDeclarationSyntax method)
        {
            var docInfo = new DocumentationInfo();

            var leadingTrivia = method.GetLeadingTrivia();
            var docCommentTrivia = leadingTrivia
                .Select(t => t.GetStructure())
                .OfType<DocumentationCommentTriviaSyntax>()
                .FirstOrDefault();

            if (docCommentTrivia == null)
            {
                return docInfo;
            }

            string rawXml = docCommentTrivia.ToString();
            var lines = rawXml.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
            var cleanedLines = new List<string>();

            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (trimmed.StartsWith("///"))
                {
                    trimmed = trimmed.Substring(3).Trim();
                }

                cleanedLines.Add(trimmed);
            }

            string cleanedXml = $"<root>{string.Join(" ", cleanedLines)}</root>";

            // Replace <see cref="X"/> or <see cref="T:X"/> with just X
            cleanedXml = Regex.Replace(cleanedXml, @"<see\s+cref=""(?:[A-Za-z]:)?([^""]+)""\s*/>", "$1");

            try
            {
                var doc = XDocument.Parse(cleanedXml);
                var root = doc.Root;
                if (root != null)
                {
                    var summaryNode = root.Element("summary");
                    if (summaryNode != null)
                    {
                        docInfo.Summary = CleanText(summaryNode.Value);
                    }

                    var returnsNode = root.Element("returns");
                    if (returnsNode != null)
                    {
                        docInfo.Returns = CleanText(returnsNode.Value);
                    }

                    foreach (var paramNode in root.Elements("param"))
                    {
                        string name = paramNode.Attribute("name")?.Value ?? string.Empty;
                        string desc = CleanText(paramNode.Value);
                        if (!string.IsNullOrEmpty(name))
                        {
                            docInfo.Parameters[name] = desc;
                        }
                    }

                    foreach (var excNode in root.Elements("exception"))
                    {
                        string cref = excNode.Attribute("cref")?.Value ?? "Exception";
                        string desc = CleanText(excNode.Value);
                        docInfo.Exceptions.Add(new ExceptionInfo(cref, desc));
                    }
                }
            }
            catch
            {
                // Fallback: regex extraction if XML parsing fails
                var summaryMatch = Regex.Match(cleanedXml, @"<summary>(.*?)</summary>", RegexOptions.Singleline);
                if (summaryMatch.Success)
                {
                    docInfo.Summary = CleanText(summaryMatch.Groups[1].Value);
                }

                var returnsMatch = Regex.Match(cleanedXml, @"<returns>(.*?)</returns>", RegexOptions.Singleline);
                if (returnsMatch.Success)
                {
                    docInfo.Returns = CleanText(returnsMatch.Groups[1].Value);
                }
            }

            return docInfo;
        }

        private static string CleanText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            // Strip any remaining XML tags
            text = Regex.Replace(text, @"<[^>]+>", string.Empty);
            // Normalize whitespace
            text = Regex.Replace(text, @"\s+", " ");
            return text.Trim();
        }

        private string FormatDocstring(DocumentationInfo docInfo)
        {
            var lines = new List<string>();

            if (!string.IsNullOrWhiteSpace(docInfo.Summary))
            {
                lines.Add(docInfo.Summary);
            }

            bool hasEntries = docInfo.Parameters.Count > 0 || !string.IsNullOrWhiteSpace(docInfo.Returns) || docInfo.Exceptions.Count > 0;
            if (hasEntries && lines.Count > 0)
            {
                lines.Add(string.Empty);
            }

            foreach (var kvp in docInfo.Parameters)
            {
                lines.Add($":param {kvp.Key}: {kvp.Value}");
            }

            if (!string.IsNullOrWhiteSpace(docInfo.Returns))
            {
                lines.Add($":return: {docInfo.Returns}");
            }

            foreach (var exc in docInfo.Exceptions)
            {
                lines.Add($":raises {exc.Cref}: {exc.Description}");
            }

            if (lines.Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.Append(Indent8);
            sb.AppendLine("\"\"\"");

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    sb.AppendLine();
                }
                else
                {
                    sb.Append(Indent8);
                    sb.AppendLine(line);
                }
            }

            sb.Append(Indent8);
            sb.Append("\"\"\"");

            return sb.ToString();
        }

        private static string GenerateTypeCheck(ParameterInfo param)
        {
            string name = param.Name;
            return param.PythonType switch
            {
                "str" => $"{Indent8}if not isinstance({name}, str):\n{Indent8}    raise TypeError(f\"Parameter '{name}' must be of type str, but got {{type({name}).__name__}}\")",
                "int" => $"{Indent8}if not isinstance({name}, int) or isinstance({name}, bool):\n{Indent8}    raise TypeError(f\"Parameter '{name}' must be of type int, but got {{type({name}).__name__}}\")",
                "bool" => $"{Indent8}if not isinstance({name}, bool):\n{Indent8}    raise TypeError(f\"Parameter '{name}' must be of type bool, but got {{type({name}).__name__}}\")",
                "float" => $"{Indent8}if not isinstance({name}, (int, float)) or isinstance({name}, bool):\n{Indent8}    raise TypeError(f\"Parameter '{name}' must be of type float, but got {{type({name}).__name__}}\")",
                "list[str]" => $"{Indent8}if not isinstance({name}, list) or not all(isinstance(x, str) for x in {name}):\n{Indent8}    raise TypeError(f\"Parameter '{name}' must be of type list[str], but got {{type({name}).__name__}}\")",
                "list[int]" => $"{Indent8}if not isinstance({name}, list) or not all(isinstance(x, int) and not isinstance(x, bool) for x in {name}):\n{Indent8}    raise TypeError(f\"Parameter '{name}' must be of type list[int], but got {{type({name}).__name__}}\")",
                "list[bool]" => $"{Indent8}if not isinstance({name}, list) or not all(isinstance(x, bool) for x in {name}):\n{Indent8}    raise TypeError(f\"Parameter '{name}' must be of type list[bool], but got {{type({name}).__name__}}\")",
                _ => $"{Indent8}if {name} is None:\n{Indent8}    raise TypeError(f\"Parameter '{name}' cannot be None\")",
            };
        }

        private static string GenerateApiCall(string methodName, List<ParameterInfo> parameters)
        {
            if (parameters.Count == 0)
            {
                return $"{Indent8}return self.CallApi(\"{methodName}\")";
            }

            bool hasListParam = parameters.Any(p => p.PythonType.StartsWith("list["));

            if (hasListParam)
            {
                var queryParts = new List<string>();
                var sb = new StringBuilder();

                sb.AppendLine($"{Indent8}query_parts = []");
                foreach (var param in parameters)
                {
                    if (param.PythonType.StartsWith("list["))
                    {
                        sb.AppendLine($"{Indent8}query_parts.extend([f\"{param.Name}={{self.__replaceSpace__(str(item))}}\" for item in {param.Name}])");
                    }
                    else if (param.PythonType == "str")
                    {
                        sb.AppendLine($"{Indent8}query_parts.append(f\"{param.Name}={{self.__replaceSpace__({param.Name})}}\")");
                    }
                    else if (param.PythonType == "bool")
                    {
                        sb.AppendLine($"{Indent8}query_parts.append(f\"{param.Name}={{str({param.Name}).lower()}}\")");
                    }
                    else
                    {
                        sb.AppendLine($"{Indent8}query_parts.append(f\"{param.Name}={{{param.Name}}}\")");
                    }
                }

                sb.Append($"{Indent8}return self.CallApi(f\"{methodName}?\" + \"&\".join(query_parts))");
                return sb.ToString();
            }

            var querySegments = new List<string>();
            foreach (var param in parameters)
            {
                if (param.PythonType == "str")
                {
                    querySegments.Add($"{param.Name}={{self.__replaceSpace__({param.Name})}}");
                }
                else if (param.PythonType == "bool")
                {
                    querySegments.Add($"{param.Name}={{str({param.Name}).lower()}}");
                }
                else
                {
                    querySegments.Add($"{param.Name}={{{param.Name}}}");
                }
            }

            string queryString = string.Join("&", querySegments);
            return $"{Indent8}return self.CallApi(f\"{methodName}?{queryString}\")";
        }

        private static string MapToPythonType(string csharpType)
        {
            csharpType = csharpType.Trim();

            return csharpType switch
            {
                "void" => "None",
                "string" => "str",
                "int" or "long" or "short" or "byte" => "int",
                "bool" => "bool",
                "float" or "double" or "decimal" => "float",
                "string[]" => "list[str]",
                "int[]" or "long[]" or "short[]" or "byte[]" => "list[int]",
                "bool[]" => "list[bool]",
                "float[]" or "double[]" or "decimal[]" => "list[float]",
                _ => csharpType.EndsWith("[]")
                    ? $"list[{MapToPythonType(csharpType[..^2])}]"
                    : csharpType
            };
        }

        private class ParameterInfo(string name, string csharpType, string pythonType)
        {
            public string Name { get; } = name;

            public string CsharpType { get; } = csharpType;

            public string PythonType { get; } = pythonType;
        }

        private class DocumentationInfo
        {
            public string Summary { get; set; } = string.Empty;

            public string Returns { get; set; } = string.Empty;

            public Dictionary<string, string> Parameters { get; } = [];

            public List<ExceptionInfo> Exceptions { get; } = [];
        }

        private class ExceptionInfo(string cref, string description)
        {
            public string Cref { get; } = cref;

            public string Description { get; } = description;
        }
    }
}
