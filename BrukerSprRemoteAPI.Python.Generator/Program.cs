namespace BrukerSprRemoteAPI.Python.Generator
{
    using System;
    using System.IO;

    /// <summary>
    /// Entry point for the Python API code generator CLI.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point for generating the Python API client file.
        /// </summary>
        /// <param name="args">Command-line arguments: [interfaceFilePath] [templateFilePath] [outputFilePath].</param>
        /// <returns>Exit code (0 on success, non-zero on failure).</returns>
        public static int Main(string[] args)
        {
            try
            {
                string interfacePath;
                string templateFile;
                string outputPath;

                if (args.Length < 2)
                {
                    Console.Error.WriteLine("Not enough arguments given. You need to call this with 2 (path to interface and output path) or 3 arguments (path to interface, path to template and output path).");
                    return 1;
                }

                if (args.Length == 2)
                {
                    interfacePath = args[0];
                    outputPath = args[1];
                    templateFile = Path.Combine(AppContext.BaseDirectory, ApiGenerator.TemplateFile);
                }
                else if (args.Length == 3)
                {
                    interfacePath = args[0];
                    templateFile = args[1];
                    outputPath = args[2];
                }
                else
                {
                    Console.Error.WriteLine($"Too many arguments given ({args.Length}). You need to call this with 2 arguments: path to interface, output path (or 3 arguments: path to interface, template path, output path).");
                    return 1;
                }

                if (!File.Exists(templateFile))
                {
                    string fallbackTemplate = Path.Combine(Path.GetDirectoryName(interfacePath) ?? string.Empty, "..", "BrukerSprRemoteAPI.Python.Generator", ApiGenerator.TemplateFile);
                    if (File.Exists(fallbackTemplate))
                    {
                        templateFile = Path.GetFullPath(fallbackTemplate);
                    }
                }

                if (!File.Exists(interfacePath))
                {
                    Console.Error.WriteLine($"Interface file not found at: {interfacePath}");
                    return 1;
                }

                if (!File.Exists(templateFile))
                {
                    Console.Error.WriteLine($"Template file not found at: {templateFile}");
                    return 1;
                }

                string? outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                Console.WriteLine("Generating Python API...");
                Console.WriteLine($"  Interface: {interfacePath}");
                Console.WriteLine($"  Template:  {templateFile}");
                Console.WriteLine($"  Output:    {outputPath}");

                ApiGenerator.GenerateFile(interfacePath, templateFile, outputPath);

                Console.WriteLine("Python API generation completed successfully.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error generating Python API: {ex.Message}");
                Console.Error.WriteLine(ex.StackTrace);
                return 1;
            }
        }
    }
}
