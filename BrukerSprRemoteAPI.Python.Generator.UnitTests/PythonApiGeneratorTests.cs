namespace BrukerSprRemoteAPI.Python.Generator.UnitTests
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using BrukerSprRemoteAPI;
    using BrukerSprRemoteAPI.Python.Generator;
    using NUnit.Framework;

    [TestFixture]
    public class PythonApiGeneratorTests
    {
        private string interfacePath = null!;
        private string templatePath = null!;
        private string outputPath = null!;

        [SetUp]
        public void SetUp()
        {
            // Determine project root path
            string baseDir = TestContext.CurrentContext.TestDirectory;
            string remotingDir = Path.GetFullPath(Path.Combine(baseDir, @$"..\..\..\..\{nameof(BrukerSprRemoteAPI)}"));
            this.interfacePath = Path.Combine(remotingDir, $"{nameof(IBrukerSprRemoteService)}.cs");
            this.templatePath = Path.Combine(baseDir, ApiGenerator.TemplateFile);
            this.outputPath = Path.Combine(baseDir, $"{nameof(BrukerSprRemoteAPI)}.py");
        }

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

            const string sampleTemplate = @"import json

class SampleAPI:
    def __init__(self, url):
        self.__url = url

    def __replaceSpace__(self, value: str) -> str:
        return value.replace(' ', '%20')

    def CallApi(self, call):
        return {}
";

            string code = ApiGenerator.GenerateCode(sampleInterface, sampleTemplate);

            Assert.That(code, Does.Contain("def GetStatus(self) -> str :"));
            Assert.That(code, Does.Contain("def SetMode(self, index: int, mode: str, enabled: bool) -> bool :"));
            Assert.That(code, Does.Contain("def ProcessItems(self, items: list[str]) -> list[str] :"));

            // Docstrings
            Assert.That(code, Does.Contain("Gets the current status name."));
            Assert.That(code, Does.Contain(":param index: index of the item"));
            Assert.That(code, Does.Contain(":param mode: mode name"));
            Assert.That(code, Does.Contain(":param enabled: flag whether enabled"));
            Assert.That(code, Does.Contain(":return: true if successful"));
            Assert.That(code, Does.Contain(":raises System.ArgumentException: Thrown if invalid mode."));

            // Type checks
            Assert.That(code, Does.Contain("if not isinstance(index, int) or isinstance(index, bool):"));
            Assert.That(code, Does.Contain("if not isinstance(mode, str):"));
            Assert.That(code, Does.Contain("if not isinstance(enabled, bool):"));
            Assert.That(code, Does.Contain("if not isinstance(items, list) or not all(isinstance(x, str) for x in items):"));
        }

        [Test]
        public void GenerateCode_WithIBrukerSprRemoteService_GeneratesAllInterfaceMethods()
        {
            Assert.That(File.Exists(this.interfacePath), Is.True, $"Interface file not found at {this.interfacePath}");
            Assert.That(File.Exists(this.templatePath), Is.True, $"Template file not found at {this.templatePath}");

            string interfaceCode = File.ReadAllText(this.interfacePath);
            string templateCode = File.ReadAllText(this.templatePath);

            string code = ApiGenerator.GenerateCode(interfaceCode, templateCode);

            // Verify all interface methods are present in generated code using reflection
            MethodInfo[] methods = typeof(IBrukerSprRemoteService).GetMethods();
            Assert.That(methods, Is.Not.Empty);

            foreach (MethodInfo method in methods)
            {
                Assert.That(code, Does.Contain($"def {method.Name}("), $"Generated Python API should contain method {method.Name}.");
            }

            // Verify XML Documentation is included
            Assert.That(code, Does.Contain("Returns an array of all method names that are loaded in the current runset library."));
            Assert.That(code, Does.Contain(":param assayType: The assay type (the list of assay types can be obtained using GetAssayTypesOfAllMethods)."));
            Assert.That(code, Does.Contain(":raises NotSupportedException: Thrown if not MASS-1 platform."));
            Assert.That(code, Does.Contain(":raises ArgumentException: Thrown if location does not exist on deck."));
        }

        [Test]
        public void GenerateFile_ProducesBrukerSprRemoteAPI_AndValidatesInPython()
        {
            ApiGenerator.GenerateFile(this.interfacePath, this.templatePath, this.outputPath);

            Assert.That(File.Exists(this.outputPath), Is.True);
            string generatedContent = File.ReadAllText(this.outputPath);
            Assert.That(generatedContent.Length, Is.GreaterThan(1000));

            // Execute Python verification test
            string pyTestScript = @"
import sys
import unittest

from BrukerSprRemoteAPI import BrukerSprRemoteAPI

class TestGeneratedAPI(unittest.TestCase):
    def setUp(self):
        self.api = BrukerSprRemoteAPI('http://localhost:9001/bruker-spr/json/')

    def test_method_existence(self):
        self.assertTrue(hasattr(self.api, 'GetNamesOfMethods'))
        self.assertTrue(hasattr(self.api, 'GetNamesOfMethodsOfAssayType'))
        self.assertTrue(hasattr(self.api, 'CreateRunset'))
        self.assertTrue(hasattr(self.api, 'SetPlateId'))
        self.assertTrue(hasattr(self.api, 'SetStandbyAfterFinish'))
        self.assertTrue(hasattr(self.api, 'GetOperationMode'))

    def test_type_checking_str(self):
        with self.assertRaises(TypeError):
            self.api.GetNamesOfMethodsOfAssayType(123)

        with self.assertRaises(TypeError):
            self.api.GetNamesOfMethodsOfAssayType(None)

    def test_type_checking_int(self):
        with self.assertRaises(TypeError):
            self.api.StartSelectedRunsetFrom('0')

        with self.assertRaises(TypeError):
            self.api.StartSelectedRunsetFrom(True) # bool should not be accepted as int

    def test_type_checking_bool(self):
        with self.assertRaises(TypeError):
            self.api.SetStandbyAfterFinish(1)

        with self.assertRaises(TypeError):
            self.api.SetStandbyAfterFinish('True')

    def test_type_checking_list_str(self):
        with self.assertRaises(TypeError):
            self.api.CreateRunset('Method1') # string instead of list[str]

        with self.assertRaises(TypeError):
            self.api.CreateRunset([1, 2, 3]) # list of ints instead of list of str

    def test_docstrings(self):
        doc = self.api.SetPlateId.__doc__
        self.assertIsNotNone(doc)
        self.assertIn(':param methodIndex:', doc)
        self.assertIn(':param plateId:', doc)
        self.assertIn(':param deckLocation:', doc)
        self.assertIn(':return:', doc)

if __name__ == '__main__':
    unittest.main()
";

            string testScriptPath = Path.Combine(Path.GetDirectoryName(this.outputPath)!, "test_generated_api.py");
            File.WriteAllText(testScriptPath, pyTestScript);

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{testScriptPath}\"",
                    WorkingDirectory = Path.GetDirectoryName(this.outputPath)!,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                using var process = Process.Start(processInfo);
                Assert.That(process, Is.Not.Null);

                string stdout = process!.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();
                process.WaitForExit();

                TestContext.Out.WriteLine("Python Test Stdout: " + stdout);
                TestContext.Out.WriteLine("Python Test Stderr: " + stderr);

                Assert.That(process.ExitCode, Is.Zero, $"Python unit test failed. Error: {stderr}");
            }
            finally
            {
                if (File.Exists(testScriptPath))
                {
                    File.Delete(testScriptPath);
                }
            }
        }
    }
}
