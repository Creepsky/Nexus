using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using NexusParserAntlr.ir;

namespace NexusParserAntlr.Generation
{
    public interface IPrintable
    {
        void ToHeader(Printer printer);
        void ToSource(Printer printer);
    }

    public class CompilationUnitMember : IPrintable
    {
        public readonly Variable Variable;

        public CompilationUnitMember(Variable variable)
        {
            Variable = variable;
        }

        public void ToHeader(Printer printer)
        {
            printer.WriteLine($"{Variable.Type.ToCpp()} {Variable.Name};");
        }

        public void ToSource(Printer printer)
        { }

        public static void Compile(CompilationUnit unit, Variable variable)
        {
            unit.Private.Members.Add(new CompilationUnitMember(variable));

            if (variable.Getter)
                unit.Public.Functions.Add(new CompilationUnitFunction(new Function
                {
                    Name = $"get_{variable.Name}",
                    Type = variable.Type,
                    Parameter = new List<Variable>(),
                    Statements = new List<IStatement>
                    {
                        new ReturnStatement
                        {
                            Value = new VariableLiteral
                            {
                                Name = variable.Name
                            }
                        }
                    }
                }));

            if (variable.Setter)
                unit.Public.Functions.Add(new CompilationUnitFunction(new Function
                {
                    Name = $"set_{variable.Name}",
                    Type = unit.Type,
                    Parameter = new List<Variable>(new [] { new Variable
                    {
                        Getter = false,
                        Setter = false,
                        Type = variable.Type,
                        Name = "newValue",
                        Initialization = null
                    }}),
                    Statements = new List<IStatement>
                    {
                        new AssignmentStatement
                        {
                            Left = new VariableLiteral
                            {
                                Name = variable.Name
                            },
                            Right = new VariableLiteral
                            {
                                Name = "newValue"
                            }
                        },
                        new ReturnStatement
                        {
                            Value = new VariableLiteral
                            {
                                // TODO: implement this keyword
                                Name = "this"
                            }
                        }
                    }
                }));

            unit.Public.Types.Add(variable.Type);
        }
    }

    public class CompilationUnitFunction : IPrintable
    {
        public readonly Function Function;

        public CompilationUnitFunction(Function function)
        {
            Function = function;
        }

        public void ToHeader(Printer printer)
        {
            printer.Write(Function.Type.IsPrimitive()
                ? Function.Type.ToCpp()
                : $"const {Function.Type.ToCpp()}&");

            printer.Write($" {Function.Name}(");

            if (Function.Parameter.Any())
            {
                var last = Function.Parameter.Last();

                foreach (var i in Function.Parameter)
                {
                    printer.Write(i.Type.IsPrimitive() ? $"{i.Type.ToCpp()}" : $"const {i.Type.ToCpp()}&");
                    printer.Write($" {i.Name}");

                    if (last != i)
                        printer.Write(", ");
                }
            }

            printer.WriteLine(");");
        }

        public void ToSource(Printer printer)
        { }

        public static void Compile(CompilationUnit unit, Function function)
        {
            // TODO: compile
        }
    }

    public class CompilationUnitSection
    {
        public readonly IList<CompilationUnitMember> Members = new List<CompilationUnitMember>();
        public readonly IList<CompilationUnitFunction> Functions = new List<CompilationUnitFunction>();
        public readonly IList<IType> Types = new List<IType>();
    }

    public class CompilationUnit : IPrintable
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
                CompilationUnitMember.Compile(this, i);

            foreach (var i in ast.Functions)
                CompilationUnitFunction.Compile(this, i);
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
            foreach (var i in Private.Members)
                i.ToHeader(printer);
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
            printer.WriteLine(":");
            foreach (var i in Private.Members)
            {
                printer.Write($"{i.Variable.Name}{{{i.Variable.Initialization?.ToString() ?? string.Empty}}}");
                if (i != Private.Members.Last())
                    printer.WriteLine(",");
            }
            printer.WriteLine();
            printer.Pop();
            printer.WriteLine("{}");
            printer.WriteLine();
        }
    }
}
