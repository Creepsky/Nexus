using System.Collections.Generic;
using NexusParserAntlr.ir;

namespace NexusParserAntlr.Generation
{
    public class ClassVariable
    {
        private readonly CompilationUnit _unit;
        private readonly CompilationVariable _variable;

        public ClassVariable(CompilationUnit unit, Variable variable)
        {
            _unit = unit;
            _variable = new CompilationVariable(unit, variable);

            unit.Private.Variables.Add(this);

            if (variable.Getter)
                unit.Public.Functions.Add(new ClassFunction(unit, new Function
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
                unit.Public.Functions.Add(new ClassFunction(unit, new Function
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

            unit.Public.Types.Add(new CompilationType(unit, variable.Type));
        }

        public void PrintHeaderDeclaration(Printer printer)
        {
            _variable.Type.Print(printer);
            //printer.WriteLine($" {_variable.Name};");
        }

        public void PrintSourceInitialization(Printer printer)
        {
           // printer.Write($"{_variable.Name}{{{_variable.Initialization?.ToString() ?? string.Empty}}}");
        }

        public static void Compile(CompilationUnit unit, Variable variable)
        {
            
        }
    }
}