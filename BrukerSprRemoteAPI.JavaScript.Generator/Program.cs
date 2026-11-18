namespace BrukerSprRemoteAPI.JavaScript.Generator
{
    using System;
    using System.IO;

    /// <summary>
    /// Entry point for the JavaScript API code generator CLI.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point for generating the JavaScript API client file.
        /// </summary>
        /// <param name="args">Command-line arguments: [interfaceFilePath] [outputPath] or [interfaceFilePath] [templateFilePath] [outputPath] [dtsOutputPath].</param>
        /// <returns>Exit code (0 on success, non-zero on failure).</returns>
        public static int Main(string[] args)
        {
            try
            {
                string interfacePath;
                string templatePath;
                string outputPath;
                string? dtsOutputPath = null;

                if (args.Length == 2)
                {
                    interfacePath = args[0];
                    outputPath = args[1];
                    templatePath = Path.Combine(AppContext.BaseDirectory, ApiGenerator.TemplateFile);
                }
                else if (args.Length >= 3)
                {
                    interfacePath = args[0];
                    templatePath = args[1];
                    outputPath = args[2];
                    if (args.Length >= 4)
                    {
                        dtsOutputPath = args[3];
                    }
                }
                else
                {
                    string baseDir = Directory.GetCurrentDirectory();
                    string repoRoot = FindRepoRoot(baseDir);

                    interfacePath = Path.Combine(repoRoot, "BrukerSprRemoteAPI", "IBrukerSprRemoteService.cs");
                    templatePath = Path.Combine(repoRoot, "BrukerSprRemoteAPI.JavaScript.Generator", ApiGenerator.TemplateFile);
                    if (!File.Exists(templatePath))
                    {
                        templatePath = Path.Combine(AppContext.BaseDirectory, ApiGenerator.TemplateFile);
                    }

                    outputPath = Path.Combine(repoRoot, "BrukerSprRemoteAPI.JavaScript", "BrukerSprRemoteAPI.js");
                    dtsOutputPath = Path.Combine(repoRoot, "BrukerSprRemoteAPI.JavaScript", "BrukerSprRemoteAPI.d.ts");
                }

                if (!File.Exists(templatePath))
                {
                    string fallbackTemplate = Path.Combine(Path.GetDirectoryName(interfacePath) ?? string.Empty, "..", "BrukerSprRemoteAPI.JavaScript.Generator", ApiGenerator.TemplateFile);
                    if (File.Exists(fallbackTemplate))
                    {
                        templatePath = Path.GetFullPath(fallbackTemplate);
                    }
                }

                if (!File.Exists(interfacePath))
                {
                    Console.Error.WriteLine($"Interface file not found at: {interfacePath}");
                    return 1;
                }

                if (!File.Exists(templatePath))
                {
                    Console.Error.WriteLine($"Template file not found at: {templatePath}");
                    return 1;
                }

                string? outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                if (string.IsNullOrEmpty(dtsOutputPath))
                {
                    string jsDir = Path.GetDirectoryName(outputPath) ?? string.Empty;
                    string jsFileNameWithoutExt = Path.GetFileNameWithoutExtension(outputPath);
                    dtsOutputPath = Path.Combine(jsDir, $"{jsFileNameWithoutExt}.d.ts");
                }

                Console.WriteLine("Generating JavaScript API...");
                Console.WriteLine($"  Interface: {interfacePath}");
                Console.WriteLine($"  Template:  {templatePath}");
                Console.WriteLine($"  Output:    {outputPath}");
                Console.WriteLine($"  Types:     {dtsOutputPath}");

                ApiGenerator.GenerateFile(interfacePath, templatePath, outputPath, dtsOutputPath);

                Console.WriteLine("JavaScript API generation completed successfully.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error generating JavaScript API: {ex.Message}");
                Console.Error.WriteLine(ex.StackTrace);
                return 1;
            }
        }

        private static string FindRepoRoot(string startDir)
        {
            string? current = Path.GetFullPath(startDir);
            while (!string.IsNullOrEmpty(current))
            {
                if (File.Exists(Path.Combine(current, "RemoteAppTestClient.sln")) ||
                    Directory.Exists(Path.Combine(current, "BrukerSprRemoteAPI")))
                {
                    return current;
                }

                string? parent = Directory.GetParent(current)?.FullName;
                if (parent == null || parent == current)
                {
                    break;
                }

                current = parent;
            }

            return startDir;
        }
    }
}
