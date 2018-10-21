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
            var config = new Configuration(examplePath, Path.Combine(examplePath, "bin"));
            var files = FileParser.ParseProject(config);
            Compiler.Compile(files);
        }
    }
}