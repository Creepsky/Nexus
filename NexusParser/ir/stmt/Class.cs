using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ClassSection : IGenerationElement
    {
        public readonly IList<Variable> Variables = new List<Variable>();
        public readonly IList<Function> Functions = new List<Function>();
        public readonly IList<IType> Types = new List<IType>();

        public void Check(Context context)
        {
            foreach (var i in Variables)
                i.Check(context);

            foreach (var i in Functions)
                i.Check(context);

            foreach (var i in Types)
                i.Check(context);
        }

        public IGenerationElement Generate(Context context)
        {
            throw new System.NotImplementedException();
        }

        public void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Class : Statement
    {
        public string Name;
        public IList<Variable> Variables;
        public IList<Function> Functions;

        public SimpleType Type;
        public ClassSection Public;
        public ClassSection Private;

        public override string ToString()
        {
            return $"class {Name}";
        }

        public void ToHeader(Printer printer)
        {
            printer.WriteLine("#pragma once");
            printer.WriteLine();
            // TODO: add #include function
            //if (Public.Types.Any(i => i.IsArray()))
            //    printer.WriteLine("#include <vector>");
            //if (Public.Types.Any(i => i.Type == "string"))
            //    printer.WriteLine("#include <string>");
            printer.WriteLine();
            printer.WriteLine($"class {Name}");
            printer.WriteLine("{");
            printer.WriteLine("public:");
            printer.Push();
            //foreach (var i in Public.Functions)
            //    i.ToHeader(printer);
            printer.Pop();
            printer.WriteLine();
            printer.WriteLine("private:");
            printer.Push();
            //foreach (var i in Private.Variables)
            //    i.PrintHeaderDeclaration(printer);
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
            //foreach (var i in Public.Functions)
            //    i.ToSource(printer);
        }

        public override IGenerationElement Generate(Context upperContext)
        {
            var context = upperContext.StackNewContext(this);

            Type = new SimpleType
            {
                Line = Line,
                Column = Column,
                Name = Name
            };

            Private = new ClassSection();
            Public = new ClassSection();

            foreach (var i in Variables)
                Private.Variables.Add(i.Generate<Variable>(context));

            foreach (var i in Functions)
                Public.Functions.Add(i.Generate<Function>(context));

            //foreach (var i in _extensionFunctions)
            //{
            //    Public.Functions.Add(new Function
            //    {
            //        Name = i.Name,
            //        Type = i.ReturnType,
            //        Parameter = i.Parameter,
            //        Statements = i.Body
            //    }.Generate<Function>(context));
            //}

            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }

        public override void Check(Context context)
        {
            Public.Check(context);
        }
    }
}