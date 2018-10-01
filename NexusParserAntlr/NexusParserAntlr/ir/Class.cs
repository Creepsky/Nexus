using System.Collections.Generic;

namespace NexusParserAntlr.ir
{
    public class Class
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