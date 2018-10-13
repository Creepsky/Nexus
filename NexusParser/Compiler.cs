using System.Collections.Generic;
using Nexus.gen;

namespace Nexus
{
    public static class Compiler
    {
        public static IEnumerable<CompilationUnit> Compile(IList<ir.File> files)
        {
            var generator = new Generator(files);
            generator.Generate();
            generator.Check();
            return generator.Compile();
        }
    }
}