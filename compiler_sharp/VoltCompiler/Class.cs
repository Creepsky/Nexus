using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Volt
{
    public class Class : IPrintable
    {
        public string Name;
        public List<IClassMember> Members;

        public void ToHeader(Printer printer)
        {
            printer.WriteLine("#pragma once");
            printer.WriteLine();
            printer.WriteLine($"class {Name}");
            printer.WriteLine("{");
            printer.WriteLine("public:");
            printer.Push();
            foreach (var i in Members.Where(i => i.GetType() == typeof(Variable)))
            {
                var variable = (Variable)i;
                
                if (variable.Getter)
                {
                    var returnValue = variable.IsPrimitive() ? variable.TypeToCpp() : $"const {variable.TypeToCpp()}&";
                    printer.WriteLine($"{returnValue} get_{variable.Name}() const;");
                }
            }
            printer.Pop();
            printer.WriteLine();
            printer.WriteLine("private:");
            printer.Push();
            foreach (var i in Members.Where(i => i.GetType() == typeof(Variable)))
                i.ToHeader(printer);
            printer.Pop();
            printer.WriteLine("};");

        }

        public void ToSource(Printer printer)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
