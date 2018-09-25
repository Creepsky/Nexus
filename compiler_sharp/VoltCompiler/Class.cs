using System.Collections.Generic;
using System.Linq;

namespace Nexus
{
    public class Class
    {
        public string Name;
        public List<IStatement> Members;

        public void Compile(Printer header, Printer source)
        {
            var compilationUnit = new Semantics();
            compilationUnit.Compile(this);
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
                // TODO: ??
                //variable.MakeGetterHeader(printer);
                //variable.MakeSetterHeader(printer);
            }
            printer.Pop();
            printer.WriteLine();
            printer.WriteLine("private:");
            printer.Push();
            //foreach (var i in Members.Where(i => i.GetType() == typeof(Variable)))
            //    i.ToHeader(printer);
            printer.Pop();
            printer.WriteLine("};");

        }

        private void ToSource(Printer printer, List<string> usedTypes)
        {
            printer.WriteLine("#include \"\"");
        }

        public override string ToString()
        {
            return Name;
        }

        private IEnumerable<TypeDefinition> GatherTypes()
        {
            //foreach (var member in Members)
            //foreach (var usedType in member.UsedTypes())
            //    yield return usedType;
            return null;
        }
    }
}
