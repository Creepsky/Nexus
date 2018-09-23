using System.Collections.Generic;
using System.IO;

namespace Volt
{
    public class Function : IClassMember
    {
        public string ReturnType;
        public string Name;
        public IList<Variable> Parameters;

        public override string ToString()
        {
            return $"{ReturnType} {Name}({string.Join(", ", Parameters)})";
        }

        public void ToHeader(Printer printer)
        {
            throw new System.NotImplementedException();
        }

        public void ToSource(Printer printer)
        {
            throw new System.NotImplementedException();
        }

        public void Validate()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> UsedTypes()
        {
            yield return ReturnType;
            foreach (var parameter in Parameters)
            foreach (var usedType in parameter.UsedTypes())
                yield return usedType;
        }
    }
}
