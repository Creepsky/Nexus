using System;
using System.IO;
using Sprache;

namespace Volt
{
    public class Variable : IClassMember
    {
        public string Type;
        public string Name;
        public bool Setter;
        public bool Getter;
        public IOption<dynamic> Initialization;

        public virtual void Validate()
        {
            
        }

        public override string ToString()
        {
            return $"{Type} {Name}";
        }

        public void ToHeader(Printer printer)
        {
            printer.WriteLine($"{TypeToCpp()} {Name};");
        }

        public void ToSource(Printer printer)
        {
        }

        public string TypeToCpp()
        {
            try
            {
                // primitive
                return TypesExtension.ToType(Type).ToCpp();
            }
            catch (Exception)
            {
                // class
                return Type;
            }
        }

        public bool IsPrimitive()
        {
            if (Type.ToLower() == "string")
                return false;

            try
            {
                TypesExtension.ToType(Type);
                return true;
            }
            catch (Exception)
            {
                // class
                return false;
            }
        }
    }
}
