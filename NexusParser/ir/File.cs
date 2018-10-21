using System.Collections.Generic;
using Nexus.ir.stmt;

namespace Nexus.ir
{
    public class File
    {
        public string Path { get; set; }
        public IList<Class> Classes { get; set; }
        public IList<ExtensionFunction> ExtensionFunctions { get; set; }
    }
}