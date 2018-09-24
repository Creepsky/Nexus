using System.Collections.Generic;
using Sprache;

namespace Nexus
{
    public class Variable : IClassMember
    {
        public TypeDefinition Type;
        public string Name;
        public bool Setter;
        public bool Getter;
        public IOption<IExpression> Initialization;

        public void Validate()
        {
            
        }

        public override string ToString()
        {
            return $"{Type} {Name}";
        }

        public void ToHeader(Printer printer)
        {
        }

        public void ToSource(Printer printer)
        {
        }

        public bool IsPrimitive() => Type.IsPrimitive();

        public IEnumerable<TypeDefinition> UsedTypes()
        {
            yield return Type;
        }

        // TODO: ??
        //public void MakeGetterHeader(Printer printer)
        //{
        //    if (Getter)
        //    {
        //        var returnValue = IsPrimitive() ? TypeToCpp() : $"const {TypeToCpp()}&";
        //        printer.WriteLine($"{returnValue} get_{Name}() const;");
        //    }
        //}

        //public void MakeSetterHeader(Printer printer)
        //{
        //    if (Setter)
        //    {
        //        printer.WriteLine($"void set_{Name}({TypeToCpp()} value);");
        //    }
        //}
    }
}
