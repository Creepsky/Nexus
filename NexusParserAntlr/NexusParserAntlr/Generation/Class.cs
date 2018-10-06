using System.Collections.Generic;
using System.Linq;
using NexusParserAntlr.ir;

namespace NexusParserAntlr.Generation
{
    public class ClassSection : IGenerationElement
    {
        public readonly IList<ClassVariable> Variables = new List<ClassVariable>();
        public readonly IList<Function> Functions = new List<Function>();
        public readonly IList<Type> Types = new List<Type>();

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
    }

    public class Class : IGenerationElement
    {
        public readonly string Name;
        public readonly SimpleType Type;
        public readonly ClassSection Public;
        public readonly ClassSection Private;
        private readonly IEnumerable<ir.ExtensionFunction> _extensionFunctions;
        private readonly ir.Class _ast;

        public Class(Context upperContext, ir.Class ast, IEnumerable<ir.ExtensionFunction> extensionFunctions)
        {
            _ast = ast;
            _extensionFunctions = extensionFunctions;
            Name = ast.Name;
            Type = new SimpleType {Name = Name, Array = 0};
            Public = new ClassSection();
            Private = new ClassSection();
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

        public IGenerationElement Generate(Context upperContext)
        {
            var context = upperContext.StackNewContext(this);

            foreach (var i in _ast.Variables)
            {
                Private.Variables.Add(new ClassVariable(i).Generate<ClassVariable>(context));
            }

            foreach (var i in _ast.Functions)
                Public.Functions.Add(new Function(i).Generate<Function>(context));

            foreach (var i in _extensionFunctions)
            {
                Public.Functions.Add(new Function(new ir.Function
                {
                    Name = i.Name,
                    Type = i.ReturnType,
                    Parameter = i.Parameter,
                    Statements = i.Body
                }).Generate<Function>(context));
            }

            return this;
        }

        public void Check(Context context)
        {
            Public.Check(context);
        }
    }
}
