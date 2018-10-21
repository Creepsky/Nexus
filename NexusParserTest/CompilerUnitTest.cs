using System.IO;
using Nexus;
using Xunit;
using Zio;

namespace NexusParserTest
{
    public static class CompilerUnitTest
    {
        [Fact]
        public static void TestExampleProject()
        {
            const string examplePath = "../../../../example";
            var configuration = new Configuration(examplePath, Path.Combine(examplePath, "bin"));
            configuration.Read();
            var files = FileParser.ParseProject(configuration);
            var units = Compiler.Compile(files);
            FileParser.WriteFiles(configuration, units);
        }
    }
}