namespace BrukerSprRemoteAPI.JavaScript.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Roslyn-based code generator that produces a typed JavaScript client class
    /// from a C# remote service interface using a JavaScript template.
    /// </summary>
    public class ApiGenerator
    {
        /// <summary>
        /// Default template file name.
        /// </summary>
        public const string TemplateFile = "BrukerSprRemoteAPI.js.template";

        private const string Indent4 = "    ";
        private const string Indent8 = "        ";

        /// <summary>
        /// Generates the JavaScript class code from the given C# interface code and JavaScript template code.
        /// </summary>
        /// <param name="interfaceCode">C# source code containing the interface definition.</param>
        /// <param name="templateCode">JavaScript template code.</param>
        /// <returns>Generated JavaScript source code.</returns>
        public static string GenerateCode(string interfaceCode, string templateCode)
        {
            var generator = new ApiGenerator();
            return generator.Generate(interfaceCode, templateCode);
        }

        /// <summary>
        /// Generates the TypeScript declaration file (.d.ts) from the given C# interface code.
        /// </summary>
        /// <param name="interfaceCode">C# source code containing the interface definition.</param>
        /// <returns>Generated TypeScript definition source code.</returns>
        public static string GenerateTypeScriptDefinition(string interfaceCode)
        {
            var generator = new ApiGenerator();
            return generator.GenerateDts(interfaceCode);
        }

        /// <summary>
        /// Generates the JavaScript class code from files and writes the output to the specified file paths.
        /// </summary>
        /// <param name="interfaceFilePath">Path to the C# interface file.</param>
        /// <param name="templateFilePath">Path to the JavaScript template file.</param>
        /// <param name="outputJsFilePath">Path to write the generated JavaScript file.</param>
        /// <param name="outputDtsFilePath">Optional path to write the generated TypeScript definitions file.</param>
        public static void GenerateFile(string interfaceFilePath, string templateFilePath, string outputJsFilePath, string? outputDtsFilePath = null)
        {
            string interfaceCode = File.ReadAllText(interfaceFilePath);
            string templateCode = File.ReadAllText(templateFilePath);
            string generatedJs = GenerateCode(interfaceCode, templateCode);
            File.WriteAllText(outputJsFilePath, generatedJs, new UTF8Encoding(false));

            if (!string.IsNullOrEmpty(outputDtsFilePath))
            {
                string generatedDts = GenerateTypeScriptDefinition(interfaceCode);
                File.WriteAllText(outputDtsFilePath, generatedDts, new UTF8Encoding(false));
            }
        }

        /// <summary>
        /// Generates the JavaScript class code from the given C# interface code and template code.
        /// </summary>
        /// <param name="interfaceCode">C# source code containing the interface definition.</param>
        /// <param name="templateCode">JavaScript template code.</param>
        /// <returns>Generated JavaScript source code.</returns>
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
                string jsMethod = this.GenerateMethod(method);
                sb.Append(jsMethod);

                if (i < methods.Count - 1)
                {
                    sb.AppendLine();
                }
            }

            sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine("// Universal Module Definition / CommonJS / Browser global export");
            sb.AppendLine("if (typeof module !== 'undefined' && module.exports) {");
            sb.AppendLine("    module.exports = { BrukerSprRemoteAPI };");
            sb.AppendLine("}");
            sb.AppendLine("if (typeof window !== 'undefined') {");
            sb.AppendLine("    window.BrukerSprRemoteAPI = BrukerSprRemoteAPI;");
            sb.AppendLine("}");
            sb.AppendLine("if (typeof globalThis !== 'undefined') {");
            sb.AppendLine("    globalThis.BrukerSprRemoteAPI = BrukerSprRemoteAPI;");
            sb.AppendLine("}");

            return sb.ToString();
        }

        /// <summary>
        /// Generates TypeScript declarations (.d.ts) for the client class.
        /// </summary>
        /// <param name="interfaceCode">C# source code containing the interface definition.</param>
        /// <returns>TypeScript definition string.</returns>
        public string GenerateDts(string interfaceCode)
        {
            if (string.IsNullOrWhiteSpace(interfaceCode))
            {
                throw new ArgumentException("Interface code cannot be null or empty.", nameof(interfaceCode));
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
            sb.AppendLine("/**");
            sb.AppendLine(" * Bruker SPR Remote API JavaScript Client.");
            sb.AppendLine(" */");
            sb.AppendLine("export class BrukerSprRemoteAPI {");
            sb.AppendLine("    /**");
            sb.AppendLine("     * Initializes a new instance of the BrukerSprRemoteAPI client.");
            sb.AppendLine("     * @param url The base REST service URL.");
            sb.AppendLine("     */");
            sb.AppendLine("    constructor(url: string);");
            sb.AppendLine();
            sb.AppendLine("    /** Base URL of the REST service. */");
            sb.AppendLine("    readonly url: string;");
            sb.AppendLine();
            sb.AppendLine("    /** Executes an API call. */");
            sb.AppendLine("    CallApi(call: string): Promise<any>;");
            sb.AppendLine("    callApi(call: string): Promise<any>;");
            sb.AppendLine();

            foreach (var method in methods)
            {
                string methodName = method.Identifier.Text;
                string csharpReturnType = method.ReturnType.ToString().Trim();
                string tsReturnType = MapToTypeScriptType(csharpReturnType);

                var parameters = method.ParameterList.Parameters
                    .Select(p => new ParameterInfo(
                        p.Identifier.Text,
                        p.Type?.ToString().Trim() ?? "object",
                        MapToTypeScriptType(p.Type?.ToString().Trim() ?? "object")))
                    .ToList();

                var docInfo = ParseDocumentation(method);

                sb.AppendLine("    /**");
                if (!string.IsNullOrWhiteSpace(docInfo.Summary))
                {
                    sb.AppendLine($"     * {docInfo.Summary}");
                }

                foreach (var param in parameters)
                {
                    string desc = docInfo.Parameters.TryGetValue(param.Name, out string? paramDesc) ? paramDesc : string.Empty;
                    sb.AppendLine($"     * @param {param.Name} {desc}".TrimEnd());
                }

                if (!string.IsNullOrWhiteSpace(docInfo.Returns))
                {
                    sb.AppendLine($"     * @returns {docInfo.Returns}");
                }

                sb.AppendLine("     */");

                string paramList = string.Join(", ", parameters.Select(p => $"{p.Name}: {p.Type}"));
                sb.AppendLine($"    {methodName}({paramList}): Promise<{tsReturnType}>;");
                sb.AppendLine();
            }

            sb.AppendLine("}");
            sb.AppendLine("export default BrukerSprRemoteAPI;");

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
                        string cref = excNode.Attribute("cref")?.Value ?? "Error";
                        string desc = CleanText(excNode.Value);
                        docInfo.Exceptions.Add(new ExceptionInfo(cref, desc));
                    }
                }
            }
            catch
            {
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

            text = Regex.Replace(text, @"<[^>]+>", string.Empty);
            text = Regex.Replace(text, @"\s+", " ");
            return text.Trim();
        }

        private static string GenerateTypeCheck(ParameterInfo param)
        {
            string name = param.Name;
            return param.Type switch
            {
                "string" => $"{Indent8}if (typeof {name} !== 'string') {{\n{Indent8}    throw new TypeError(\"Parameter '{name}' must be of type string, but got \" + (typeof {name}));\n{Indent8}}}",
                "number" => $"{Indent8}if (typeof {name} !== 'number' || Number.isNaN({name})) {{\n{Indent8}    throw new TypeError(\"Parameter '{name}' must be of type number, but got \" + (typeof {name}));\n{Indent8}}}",
                "boolean" => $"{Indent8}if (typeof {name} !== 'boolean') {{\n{Indent8}    throw new TypeError(\"Parameter '{name}' must be of type boolean, but got \" + (typeof {name}));\n{Indent8}}}",
                "string[]" => $"{Indent8}if (!Array.isArray({name}) || !{name}.every(x => typeof x === 'string')) {{\n{Indent8}    throw new TypeError(\"Parameter '{name}' must be an array of strings.\");\n{Indent8}}}",
                "number[]" => $"{Indent8}if (!Array.isArray({name}) || !{name}.every(x => typeof x === 'number' && !Number.isNaN(x))) {{\n{Indent8}    throw new TypeError(\"Parameter '{name}' must be an array of numbers.\");\n{Indent8}}}",
                "boolean[]" => $"{Indent8}if (!Array.isArray({name}) || !{name}.every(x => typeof x === 'boolean')) {{\n{Indent8}    throw new TypeError(\"Parameter '{name}' must be an array of booleans.\");\n{Indent8}}}",
                _ => $"{Indent8}if ({name} === undefined || {name} === null) {{\n{Indent8}    throw new TypeError(\"Parameter '{name}' cannot be null or undefined.\");\n{Indent8}}}",
            };
        }

        private static string GenerateApiCall(string methodName, List<ParameterInfo> parameters)
        {
            if (parameters.Count == 0)
            {
                return $"{Indent8}return this.CallApi('{methodName}');";
            }

            bool hasArrayParam = parameters.Any(p => p.Type.EndsWith("[]"));

            if (hasArrayParam)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"{Indent8}const queryParts = [];");
                foreach (var param in parameters)
                {
                    if (param.Type.EndsWith("[]"))
                    {
                        sb.AppendLine($"{Indent8}queryParts.push(...{param.Name}.map(item => '{param.Name}=' + encodeURIComponent(item)));");
                    }
                    else if (param.Type == "string")
                    {
                        sb.AppendLine($"{Indent8}queryParts.push('{param.Name}=' + encodeURIComponent({param.Name}));");
                    }
                    else
                    {
                        sb.AppendLine($"{Indent8}queryParts.push('{param.Name}=' + {param.Name});");
                    }
                }

                sb.Append($"{Indent8}return this.CallApi('{methodName}?' + queryParts.join('&'));");
                return sb.ToString();
            }

            var querySegments = new List<string>();
            foreach (var param in parameters)
            {
                if (param.Type == "string")
                {
                    querySegments.Add($"{param.Name}=' + encodeURIComponent({param.Name}) + '");
                }
                else
                {
                    querySegments.Add($"{param.Name}=' + {param.Name} + '");
                }
            }

            string queryString = string.Join("&", querySegments);
            string callExpr = $"'{methodName}?{queryString}'";

            // Simplify concatenated strings like `'Method?param=' + param + ''` -> `'Method?param=' + param`
            if (callExpr.EndsWith(" + ''"))
            {
                callExpr = callExpr.Substring(0, callExpr.Length - 5);
            }

            return $"{Indent8}return this.CallApi({callExpr});";
        }

        private static string MapToTypeScriptType(string csharpType)
        {
            csharpType = csharpType.Trim();

            return csharpType switch
            {
                "void" => "any",
                "string" => "string",
                "int" or "long" or "short" or "byte" or "float" or "double" or "decimal" => "number",
                "bool" => "boolean",
                "string[]" => "string[]",
                "int[]" or "long[]" or "short[]" or "byte[]" or "float[]" or "double[]" or "decimal[]" => "number[]",
                "bool[]" => "boolean[]",
                _ => csharpType.EndsWith("[]")
                    ? $"{MapToTypeScriptType(csharpType[..^2])}[]"
                    : csharpType
            };
        }

        private string GenerateMethod(MethodDeclarationSyntax method)
        {
            string methodName = method.Identifier.Text;
            string csharpReturnType = method.ReturnType.ToString().Trim();
            string tsReturnType = MapToTypeScriptType(csharpReturnType);

            var parameters = method.ParameterList.Parameters
                .Select(p => new ParameterInfo(
                    p.Identifier.Text,
                    p.Type?.ToString().Trim() ?? "object",
                    MapToTypeScriptType(p.Type?.ToString().Trim() ?? "object")))
                .ToList();

            var docInfo = ParseDocumentation(method);

            var sb = new StringBuilder();

            // JSDoc
            string jsDoc = this.FormatJsDoc(docInfo, parameters, tsReturnType);
            if (!string.IsNullOrWhiteSpace(jsDoc))
            {
                sb.AppendLine(jsDoc);
            }

            // Method signature: async MethodName(param1, param2) {
            sb.Append(Indent4);
            sb.Append("async ");
            sb.Append(methodName);
            sb.Append("(");
            sb.Append(string.Join(", ", parameters.Select(p => p.Name)));
            sb.AppendLine(") {");

            // Type checks
            foreach (var param in parameters)
            {
                string typeCheck = GenerateTypeCheck(param);
                sb.AppendLine(typeCheck);
            }

            // API Call
            string callStatement = GenerateApiCall(methodName, parameters);
            sb.AppendLine(callStatement);

            sb.Append(Indent4);
            sb.AppendLine("}");

            return sb.ToString();
        }

        private string FormatJsDoc(DocumentationInfo docInfo, List<ParameterInfo> parameters, string returnType)
        {
            var lines = new List<string>();

            if (!string.IsNullOrWhiteSpace(docInfo.Summary))
            {
                lines.Add(docInfo.Summary);
            }

            bool hasEntries = parameters.Count > 0 || !string.IsNullOrWhiteSpace(docInfo.Returns) || docInfo.Exceptions.Count > 0;
            if (hasEntries && lines.Count > 0)
            {
                lines.Add(string.Empty);
            }

            foreach (var param in parameters)
            {
                string desc = docInfo.Parameters.TryGetValue(param.Name, out string? paramDesc) ? $"- {paramDesc}" : string.Empty;
                lines.Add($"@param {{{param.Type}}} {param.Name} {desc}".TrimEnd());
            }

            string retDesc = !string.IsNullOrWhiteSpace(docInfo.Returns) ? $"- {docInfo.Returns}" : string.Empty;
            lines.Add($"@returns {{Promise<{returnType}>}} {retDesc}".TrimEnd());

            foreach (var exc in docInfo.Exceptions)
            {
                lines.Add($"@throws {{{exc.Cref}}} {exc.Description}".TrimEnd());
            }

            var sb = new StringBuilder();
            sb.Append(Indent4);
            sb.AppendLine("/**");

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    sb.Append(Indent4);
                    sb.AppendLine(" *");
                }
                else
                {
                    sb.Append(Indent4);
                    sb.Append(" * ");
                    sb.AppendLine(line);
                }
            }

            sb.Append(Indent4);
            sb.Append(" */");

            return sb.ToString();
        }

        private class ParameterInfo(string name, string csharpType, string type)
        {
            public string Name { get; } = name;

            public string CsharpType { get; } = csharpType;

            public string Type { get; } = type;
        }

        private class DocumentationInfo
        {
            public string Summary { get; set; } = string.Empty;

            public string Returns { get; set; } = string.Empty;

            public Dictionary<string, string> Parameters { get; } = new();

            public List<ExceptionInfo> Exceptions { get; } = new();
        }

        private class ExceptionInfo(string cref, string description)
        {
            public string Cref { get; } = cref;

            public string Description { get; } = description;
        }
    }
}
