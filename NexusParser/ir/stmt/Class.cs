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
        public int Line { get; set; }
        public int Column { get; set; }

        public void Check(Context context)
        {
            foreach (var i in Variables)
                i.Check(context);

            foreach (var i in Functions)
                i.Check(context);
        }

        public IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            if (phase == GenerationPhase.Definition)
            {
                foreach (var i in Variables)
                    i.Generate(context, phase);

                foreach (var i in Functions)
                    i.Generate(context, phase);
            }

            return this;
        }

        public void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Class : Statement
    {
        public string Name { get; }
        public IList<Variable> Variables { get; }
        public IList<Function> Functions { get; }

        public readonly SimpleType Type;
        public readonly ClassSection Public;
        public readonly ClassSection Private;
        public readonly IList<IType> UsedTypes;
        private Context _context;

        public Class(string name, IList<Variable> variables, IList<Function> functions)
        {
            Name = name;
            Variables = variables;
            Functions = functions;
            UsedTypes = new List<IType>();

            Type = new SimpleType
            {
                Line = Line,
                Column = Column,
                Name = Name
            };

            Public = new ClassSection();
            Private = new ClassSection();
        }

        public override string ToString()
        {
            return $"class {Name}";
        }

        public override IGenerationElement Generate(Context upperContext, GenerationPhase phase)
        {
            if (phase == GenerationPhase.ForwardDeclaration)
            {
                upperContext.Add(Name, this);
                _context = upperContext.StackNewContext(this);
            }

            foreach (var i in Variables)
                i.Generate<Variable>(_context, phase);

            foreach (var i in Functions)
                i.Generate<Function>(_context, phase);

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
            if (type == PrintType.Header)
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
                    i.Print(type, printer);
                printer.Pop();
                printer.WriteLine();
                printer.WriteLine("private:");
                printer.Push();
                foreach (var i in Private.Variables)
                    i.Print(PrintType.Header, printer);
                printer.Pop();
                printer.WriteLine("};");
            }
            else if (type == PrintType.Source)
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
                        i.Print(PrintType.Source, printer);
                        if (i != Private.Variables.Last())
                            printer.WriteLine(",");
                    }
                    printer.Pop();
                    printer.WriteLine();
                }
                printer.WriteLine("{ }");
                printer.WriteLine();

                // functions
                foreach (var i in Public.Functions)
                {
                    i.Print(type, printer);
                    printer.WriteLine();
                }
            }
        }

        public override void Check(Context context)
        {
            Public.Check(context);
        }
    }
}