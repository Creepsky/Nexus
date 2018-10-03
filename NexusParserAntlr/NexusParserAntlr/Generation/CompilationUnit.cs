using System.Collections.Generic;
using System.Linq;
using NexusParserAntlr.ir;

namespace NexusParserAntlr.Generation
{
    public class CompilationUnitSection
    {
        public readonly IList<ClassVariable> Variables = new List<ClassVariable>();
        public readonly IList<ClassFunction> Functions = new List<ClassFunction>();
        public readonly IList<CompilationType> Types = new List<CompilationType>();
    }

    public class CompilationUnit
    {
        public readonly string Name;
        public readonly SimpleType Type;
        public readonly CompilationUnitSection Public;
        public readonly CompilationUnitSection Private;

        public CompilationUnit(Class ast)
        {
            Name = ast.Name;
            Type = new SimpleType {Name = Name, Array = 0};
            Public = new CompilationUnitSection();
            Private = new CompilationUnitSection();

            foreach (var i in ast.Variables)
                Private.Variables.Add(new ClassVariable(this, i));

            foreach (var i in ast.Functions)
                Public.Functions.Add(new ClassFunction(this, i));
        }

        public void ToHeader(Printer printer)
        {
            printer.WriteLine("#pragma once");
            printer.WriteLine();
            // TODO: add #include function
            //if (Public.Types.Any(i => i.IsArray()))
            //    printer.WriteLine("#include <vector>");
            //if (Public.Types.Any(i => i.CompilationType == "string"))
            //    printer.WriteLine("#include <string>");
            printer.WriteLine();
            printer.WriteLine($"class {Name}");
            printer.WriteLine("{");
            printer.WriteLine("public:");
            printer.Push();
            foreach (var i in Public.Functions)
                i.ToHeader(printer);
            printer.Pop();
            printer.WriteLine();
            printer.WriteLine("private:");
            printer.Push();
            foreach (var i in Private.Variables)
                i.PrintHeaderDeclaration(printer);
            printer.Pop();
            printer.WriteLine("};");
        }

        public void ToSource(Printer printer)
        {
            printer.WriteLine($"#include \"{Name}.hpp\"");
            printer.WriteLine();
            
            // constructor
            printer.WriteLine($"{Name}::{Name}()");
            printer.Push();
            if (Private.Variables.Any())
            {
                printer.WriteLine(":");
                foreach (var i in Private.Variables)
                {
                    if (i != Private.Variables.Last())
                        printer.WriteLine(",");
                }
                printer.WriteLine();
                printer.Pop();
            }
            printer.WriteLine("{ }");
            printer.WriteLine();
            
            // functions
            foreach (var i in Public.Functions)
                i.ToSource(printer);
        }
    }
}
