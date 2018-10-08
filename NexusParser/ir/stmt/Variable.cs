using System;
using System.Collections.Generic;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Variable : Statement
    {
        public IType Type;
        public string Name;
        public bool Setter;
        public bool Getter;
        public IExpression Initialization;
        public bool Reference;
        public bool Const;

        public override string ToString()
        {
            return $"{Type} {Name}";
        }

        public void PrintSourceInitialization(Printer printer)
        {
           // printer.Write($"{_variable.Name}{{{_variable.Initialization?.ToString() ?? string.Empty}}}");
        }

        public override void Check(Context upperContext)
        {
            if (Type.IsAuto())
            {
                //throw new Exception();
            }

            if (Initialization != null)
            {
                //throw new Exception();
            }
        }

        public override IGenerationElement Generate(Context upperContext, GenerationPhase phase)
        {
            if (upperContext.Element == null)
                throw new NoScopeException(this);

            switch (phase)
            {
                case GenerationPhase.ForwardDeclaration:
                    upperContext.Add(Name, this);
                    break;
                case GenerationPhase.Declaration when upperContext.Element.GetType() == typeof(Class):
                    return GenerateClassVariable((Class)upperContext.Element, upperContext);
                case GenerationPhase.Declaration when upperContext.Element.GetType() == typeof(Function):
                    return GenerateParameter((Function)upperContext.Element, upperContext);
                case GenerationPhase.Declaration:
                    throw new UnexpectedScopeException(this, upperContext.Element.GetType().Name,
                        new[] {nameof(Class), nameof(Function)});
                case GenerationPhase.Definition:
                default:
                    break;
            }

            return this;
        }

        private IGenerationElement GenerateClassVariable(Class c, Context context)
        {
            c.Private.Variables.Add(this);

            if (Getter)
                c.Functions.Add(new Function
                {
                    Name = $"get_{Name}",
                    Type = Type,
                    Parameter = new List<Variable>(),
                    Statements = new List<IStatement>
                    {
                        new ReturnStatement
                        {
                            Value = new VariableLiteral
                            {
                                Name = Name
                            },
                            Line = Line,
                            Column = Column
                        }
                    },
                    Const = true
                }.Generate<Function>(context, GenerationPhase.ForwardDeclaration));

            if (Setter)
                c.Functions.Add(new Function
                {
                    Name = $"set_{Name}",
                    Type = c.Type,
                    Parameter = new List<Variable>(new [] { new Variable
                    {
                        Getter = false,
                        Setter = false,
                        Type = Type,
                        Name = "newValue",
                        Initialization = null,
                        Line = Line,
                        Column = Column
                    }}),
                    Statements = new List<IStatement>
                    {
                        new AssignmentStatement
                        {
                            Left = new VariableLiteral
                            {
                                Name = Name
                            },
                            Right = new VariableLiteral
                            {
                                Name = "newValue"
                            },
                            Line = Line,
                            Column = Column
                        },
                        new ReturnStatement
                        {
                            Value = new VariableLiteral
                            {
                                // TODO: implement this keyword
                                Name = "this"
                            },
                            Line = Line,
                            Column = Column
                        }
                    },
                    Line = Line,
                    Column = Column
                }.Generate<Function>(context, GenerationPhase.ForwardDeclaration));

            c.Public.Types.Add(Type.Generate<IType>(context, GenerationPhase.ForwardDeclaration));

            return this;
        }

        private IGenerationElement GenerateParameter(Function f, Context context)
        {
            context.Add(Name, this);
            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                Type.Print(type, printer);
                printer.WriteLine($" {Name};");
            }
            else if (type == PrintType.Source)
            {
                printer.Write($"{Name}");
                printer.Write("{");
                Initialization?.Print(type, printer);
                printer.Write("}");
            }
            else if (type == PrintType.Parameter ||
                     type == PrintType.ParameterRef ||
                     type == PrintType.ParameterConstRef)
            {
                Type.Print(type, printer);
                printer.Write(' ' + Name);
            }
            else if (type == PrintType.FunctionSource ||
                     type == PrintType.ForSource)
            {
                Type.Print(type, printer);
                printer.Write(" ");
                printer.Write(Name);
                if (Initialization != null)
                {
                    printer.Write(" = ");
                    Initialization.Print(type, printer);
                }
                if (type == PrintType.ForSource)
                    printer.Write(";");
                else
                    printer.WriteLine(";");
            }
        }
    }
}