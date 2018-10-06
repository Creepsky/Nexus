using System.Collections.Generic;

namespace Nexus.ir
{
    public class Class : Statement
    {
        public string Name;
        public IList<Variable> Variables;
        public IList<Function> Functions;

        public override string ToString()
        {
            return $"class {Name}";
        }
    }
}