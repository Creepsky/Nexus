using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Volt
{
    public class Class
    {
        public string Name;
        public List<IClassMember> Members;

        public void Compile(Printer header, Printer source)
        {
            var usedTypes = GatherTypes().ToList();
            ToHeader(header, usedTypes);
            ToSource(source, usedTypes);
        }

        private void ToHeader(Printer printer, List<string> usedTypes)
        {
            printer.WriteLine("#pragma once");
            printer.WriteLine();
            if (usedTypes.Count(i => i.EndsWith("[]")) > 0)
                printer.WriteLine("#include <vector>");
            if (usedTypes.Count(i => i.StartsWith("string")) > 0)
                printer.WriteLine("#include <string>");
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

        private void ToSource(Printer printer, List<string> usedTypes)
        {

        }

        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<string> GatherTypes()
        {
            foreach (var member in Members)
            foreach (var usedType in member.UsedTypes())
                yield return usedType;
        }
    }
}
