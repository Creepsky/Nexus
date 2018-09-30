using System.Collections.Generic;

namespace NexusParser
{
    public class Function : IStatement
    {
        public TypeDefinition ReturnType;
        public string Name;
        public IList<Variable> Parameters;
        public IList<IStatement> Body;

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

        public IEnumerable<TypeDefinition> UsedTypes()
        {
            yield return ReturnType;
            foreach (var parameter in Parameters)
            foreach (var usedType in parameter.UsedTypes())
                yield return usedType;
        }
    }
}
