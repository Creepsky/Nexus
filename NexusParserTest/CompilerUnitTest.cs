using System.IO;
using Xunit;

namespace NexusParserTest
{
    public static class CompilerUnitTest
    {
        [Fact]
        public static void TestExampleProject()
        {
            const string examplePath = "../../../../example";
            var files = Nexus.FileParser.ParseDirectory(examplePath);
            Nexus.Compiler.Compile(files);
        }
    }
}