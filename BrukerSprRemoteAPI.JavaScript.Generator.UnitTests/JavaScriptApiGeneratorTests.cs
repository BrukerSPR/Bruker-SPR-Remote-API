namespace BrukerSprRemoteAPI.JavaScript.Generator.UnitTests
{
    using System;
    using System.IO;
    using System.Reflection;
    using BrukerSprRemoteAPI;
    using BrukerSprRemoteAPI.JavaScript.Generator;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the JavaScript API code generator.
    /// </summary>
    [TestFixture]
    public class JavaScriptApiGeneratorTests
    {
        private string interfacePath = null!;
        private string templatePath = null!;
        private string outputPath = null!;
        private string dtsOutputPath = null!;

        /// <summary>
        /// Test setup - locates the interface and template files.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            string baseDir = TestContext.CurrentContext.TestDirectory;
            this.interfacePath = FindInterfacePath(baseDir);
            this.templatePath = Path.Combine(baseDir, ApiGenerator.TemplateFile);
            if (!File.Exists(this.templatePath))
            {
                this.templatePath = Path.Combine(Path.GetDirectoryName(this.interfacePath)!, "..", "BrukerSprRemoteAPI.JavaScript.Generator", ApiGenerator.TemplateFile);
            }

            this.outputPath = Path.Combine(baseDir, $"{nameof(BrukerSprRemoteAPI)}.js");
            this.dtsOutputPath = Path.Combine(baseDir, $"{nameof(BrukerSprRemoteAPI)}.d.ts");
        }

        /// <summary>
        /// Test teardown.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (File.Exists(this.outputPath))
            {
                File.Delete(this.outputPath);
            }

            if (File.Exists(this.dtsOutputPath))
            {
                File.Delete(this.dtsOutputPath);
            }
        }

        /// <summary>
        /// Validates that simple interface code generates correct JS methods, docstrings, type checks, and calls.
        /// </summary>
        [Test]
        public void GenerateCode_WithSimpleInterface_GeneratesExpectedMethodsAndTypes()
        {
            const string sampleInterface = """

namespace TestNamespace
{
    public interface ISampleService
    {
        /// <summary>
        /// Gets the current status name.
        /// </summary>
        /// <returns>status name</returns>
        string GetStatus();

        /// <summary>
        /// Sets the mode for a given index.
        /// </summary>
        /// <param name="index">index of the item</param>
        /// <param name="mode">mode name</param>
        /// <param name="enabled">flag whether enabled</param>
        /// <returns>true if successful</returns>
        /// <exception cref="System.ArgumentException">Thrown if invalid mode.</exception>
        bool SetMode(int index, string mode, bool enabled);

        /// <summary>
        /// Processes multiple items.
        /// </summary>
        /// <param name="items">list of item names</param>
        /// <returns>results list</returns>
        string[] ProcessItems(string[] items);
    }
}
""";

            const string sampleTemplate = @"class SampleAPI {
    constructor(url) {
        this.url = url;
    }
    async CallApi(call) {
        return {};
    }
";

            string code = ApiGenerator.GenerateCode(sampleInterface, sampleTemplate);

            // Method signatures
            Assert.That(code, Does.Contain("async GetStatus() {"));
            Assert.That(code, Does.Contain("async SetMode(index, mode, enabled) {"));
            Assert.That(code, Does.Contain("async ProcessItems(items) {"));

            // JSDoc
            Assert.That(code, Does.Contain("Gets the current status name."));
            Assert.That(code, Does.Contain("@param {number} index - index of the item"));
            Assert.That(code, Does.Contain("@param {string} mode - mode name"));
            Assert.That(code, Does.Contain("@param {boolean} enabled - flag whether enabled"));
            Assert.That(code, Does.Contain("@returns {Promise<boolean>} - true if successful"));
            Assert.That(code, Does.Contain("@throws {System.ArgumentException} Thrown if invalid mode."));

            // Runtime type checks
            Assert.That(code, Does.Contain("if (typeof index !== 'number' || Number.isNaN(index)) {"));
            Assert.That(code, Does.Contain("if (typeof mode !== 'string') {"));
            Assert.That(code, Does.Contain("if (typeof enabled !== 'boolean') {"));
            Assert.That(code, Does.Contain("if (!Array.isArray(items) || !items.every(x => typeof x === 'string')) {"));

            // API calls
            Assert.That(code, Does.Contain("return this.CallApi('GetStatus');"));
            Assert.That(code, Does.Contain("SetMode?index=' + index + '&mode=' + encodeURIComponent(mode) + '&enabled=' + enabled"));
            Assert.That(code, Does.Contain("queryParts.push(...items.map(item => 'items=' + encodeURIComponent(item)));"));
        }

        /// <summary>
        /// Validates that the generator parses the actual IBrukerSprRemoteService interface and generates all its methods.
        /// </summary>
        [Test]
        public void GenerateCode_WithIBrukerSprRemoteService_GeneratesAllInterfaceMethods()
        {
            Assert.That(File.Exists(this.interfacePath), Is.True, $"Interface file not found at {this.interfacePath}");
            Assert.That(File.Exists(this.templatePath), Is.True, $"Template file not found at {this.templatePath}");

            string interfaceCode = File.ReadAllText(this.interfacePath);
            string templateCode = File.ReadAllText(this.templatePath);

            string code = ApiGenerator.GenerateCode(interfaceCode, templateCode);

            // Verify all interface methods from IBrukerSprRemoteService exist in the generated JavaScript
            var interfaceType = typeof(IBrukerSprRemoteService);
            var methods = interfaceType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            Assert.That(methods, Is.Not.Empty);

            foreach (var method in methods)
            {
                string expectedMethodDecl = $"async {method.Name}(";
                Assert.That(code, Does.Contain(expectedMethodDecl), $"Generated JavaScript missing method: {method.Name}");
            }

            // Key methods verification
            Assert.That(code, Does.Contain("async LeaveStandby() {"));
            Assert.That(code, Does.Contain("async GetOperationMode() {"));
            Assert.That(code, Does.Contain("async RunMaintenanceProcedure(procedureName) {"));
            Assert.That(code, Does.Contain("async GetNamesOfMethods() {"));
            Assert.That(code, Does.Contain("async CreateRunset(methodNames) {"));

            // Verify module exports
            Assert.That(code, Does.Contain("module.exports = { BrukerSprRemoteAPI };"));
            Assert.That(code, Does.Contain("window.BrukerSprRemoteAPI = BrukerSprRemoteAPI;"));
        }

        /// <summary>
        /// Validates file generation to disk.
        /// </summary>
        [Test]
        public void GenerateFile_ProducesValidJavaScriptAndTypeScriptFiles()
        {
            ApiGenerator.GenerateFile(this.interfacePath, this.templatePath, this.outputPath, this.dtsOutputPath);

            Assert.That(File.Exists(this.outputPath), Is.True);
            Assert.That(File.Exists(this.dtsOutputPath), Is.True);

            string jsContent = File.ReadAllText(this.outputPath);
            string dtsContent = File.ReadAllText(this.dtsOutputPath);

            Assert.That(jsContent.Length, Is.GreaterThan(5000));
            Assert.That(dtsContent.Length, Is.GreaterThan(3000));
            Assert.That(dtsContent, Does.Contain("export class BrukerSprRemoteAPI"));
            Assert.That(dtsContent, Does.Contain("LeaveStandby(): Promise<boolean>;"));
            Assert.That(dtsContent, Does.Contain("GetOperationMode(): Promise<number>;"));
        }

        private static string FindInterfacePath(string startDir)
        {
            string[] searchRoots =
            [
                startDir,
                AppContext.BaseDirectory,
                Directory.GetCurrentDirectory(),
            ];

            foreach (string root in searchRoots)
            {
                string? current = Path.GetFullPath(root);
                while (!string.IsNullOrEmpty(current))
                {
                    string candidate = Path.Combine(current, nameof(BrukerSprRemoteAPI), $"{nameof(IBrukerSprRemoteService)}.cs");
                    if (File.Exists(candidate))
                    {
                        return Path.GetFullPath(candidate);
                    }

                    candidate = Path.Combine(current, $"{nameof(IBrukerSprRemoteService)}.cs");
                    if (File.Exists(candidate))
                    {
                        return Path.GetFullPath(candidate);
                    }

                    string? parent = Directory.GetParent(current)?.FullName;
                    if (parent == null || parent == current)
                    {
                        break;
                    }

                    current = parent;
                }
            }

            throw new FileNotFoundException($"Could not locate {nameof(IBrukerSprRemoteService)}.cs from test directories.");
        }
    }
}
