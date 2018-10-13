using System.IO;
using Xunit;

namespace NexusParserTest
{
    public class CompilerUnitTest
    {
        [Fact]
        public void TestExampleProject()
        {
            const string examplePath = "../../../../example";
            var files = Nexus.FileParser.ParseDirectory(examplePath);
            Nexus.Compiler.Compile(files);
        }
    }
}