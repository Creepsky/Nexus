using System.Collections.Generic;
using Nexus.ir.stmt;

namespace Nexus.ir
{
    public class File
    {
        public IList<Class> Classes { get; set; }
        public IList<ExtensionFunction> ExtensionFunctions { get; set; }
        public IList<string> Includes { get; set; }
    }
}