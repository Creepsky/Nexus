using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir
{
    public class File
    {
        public string Path { get; set; }
        public IList<Class> Classes { get; set; }
        public IList<ExtensionFunction> ExtensionFunctions { get; set; }

        public IEnumerable<IGenerationElement> Elements =>
            Classes.Select(i => (IGenerationElement) i).Concat(ExtensionFunctions);
    }
}