using System.Collections.Generic;
using Nexus.ir;

namespace Nexus.gen
{
    public class ClassVariable : IGenerationElement
    {
        private readonly ir.Variable _variable;
        private readonly Variable _classVariable;

        public ClassVariable(ir.Variable variable)
        {
            _variable = variable;
            _classVariable = new Variable(_variable);
        }

        public void PrintHeaderDeclaration(Printer printer)
        {
            _classVariable.Type.Print(printer);
            //printer.WriteLine($" {_variable.Name};");
        }

        public void PrintSourceInitialization(Printer printer)
        {
           // printer.Write($"{_variable.Name}{{{_variable.Initialization?.ToString() ?? string.Empty}}}");
        }

        public void Check(Context upperContext)
        {
            throw new System.NotImplementedException();
        }

        public IGenerationElement Generate(Context upperContext)
        {
            var c = (Class)upperContext.Element;

            c.Private.Variables.Add(this);

            if (_variable.Getter)
                c.Public.Functions.Add(new Function(new ir.Function
                {
                    Name = $"get_{_variable.Name}",
                    Type = _variable.Type,
                    Parameter = new List<ir.Variable>(),
                    Statements = new List<IStatement>
                    {
                        new ReturnStatement
                        {
                            Value = new VariableLiteral
                            {
                                Name = _variable.Name
                            }
                        }
                    }
                }).Generate<Function>(upperContext));

            if (_variable.Setter)
                c.Public.Functions.Add(new Function(new ir.Function
                {
                    Name = $"set_{_variable.Name}",
                    Type = c.Type,
                    Parameter = new List<ir.Variable>(new [] { new ir.Variable
                    {
                        Getter = false,
                        Setter = false,
                        Type = _variable.Type,
                        Name = "newValue",
                        Initialization = null
                    }}),
                    Statements = new List<IStatement>
                    {
                        new AssignmentStatement
                        {
                            Left = new VariableLiteral
                            {
                                Name = _variable.Name
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
                }).Generate<Function>(upperContext));

            c.Public.Types.Add(new Type(_variable.Type).Generate<Type>(upperContext));

            return this;
        }
    }
}