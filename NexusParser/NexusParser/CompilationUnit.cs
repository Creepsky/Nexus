using System.Collections.Generic;
using System.Linq;

namespace NexusParser
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
            printer.WriteLine($"{Variable.Type.TypeToCpp()} {Variable.Name};");
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
                    ReturnType = variable.Type,
                    Parameters = new List<Variable>(),
                    Body = new List<IStatement>
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
                    ReturnType = unit.Type,
                    Parameters = new List<Variable>(new [] { new Variable
                    {
                        Getter = false,
                        Setter = false,
                        Type = variable.Type,
                        Name = "newValue",
                        Initialization = null
                    }}),
                    Body = new List<IStatement>
                    {
                        new AssignmentStatement
                        {
                            LValue = new VariableLiteral
                            {
                                Name = variable.Name
                            },
                            RValue = new VariableLiteral
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
            printer.Write(Function.ReturnType.IsPrimitive()
                ? Function.ReturnType.TypeToCpp()
                : $"const {Function.ReturnType.TypeToCpp()}&");

            printer.Write($" {Function.Name}(");

            if (Function.Parameters.Any())
            {
                var last = Function.Parameters.Last();

                foreach (var i in Function.Parameters)
                {
                    printer.Write(i.IsPrimitive() ? $"{i.Type.TypeToCpp()}" : $"const {i.Type.TypeToCpp()}&");
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
        public readonly IList<TypeDefinition> Types = new List<TypeDefinition>();
    }

    public class CompilationUnit : IPrintable
    {
        public readonly string Name;
        public readonly TypeDefinition Type;
        public readonly CompilationUnitSection Public;
        public readonly CompilationUnitSection Private;

        public CompilationUnit(Class ast)
        {
            Name = ast.Name;
            Type = new TypeDefinition(Name);
            Public = new CompilationUnitSection();
            Private = new CompilationUnitSection();

            foreach (var i in ast.Members)
            {
                if (i.GetType() == typeof(Variable))
                    CompilationUnitMember.Compile(this, (Variable) i);
                else if (i.GetType() == typeof(Function))
                    CompilationUnitFunction.Compile(this, (Function) i);
            }
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
        }
    }
}
