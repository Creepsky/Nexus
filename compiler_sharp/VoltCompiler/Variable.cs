using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sprache;

namespace Volt
{
    public class Variable : IClassMember
    {
        public string Type;
        public int Array;
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
                if (Array > 0)
                    return string.Concat(Enumerable.Repeat("std::vector<", Array)) +
                           TypesExtension.ToType(Type).ToCpp() +
                           new string('>', Array);

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
