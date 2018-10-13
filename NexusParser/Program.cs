#pragma warning disable CS3021

namespace Nexus
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var files = FileParser.ParseDirectory(args[0]);
            var units = Compiler.Compile(files);
            FileParser.WriteFiles(args[1], units);
            return 0;
        }
    }
}
