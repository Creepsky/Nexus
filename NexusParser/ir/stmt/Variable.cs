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
            throw new System.NotImplementedException();
        }

        public override IGenerationElement Generate(Context upperContext)
        {
            if (upperContext.Element == null)
                throw new NoScopeException(this);

            if (upperContext.Element.GetType() == typeof(Class))
                return GenerateClassVariable((Class)upperContext.Element, upperContext);

            if (upperContext.Element.GetType() == typeof(Function))
                return GenerateParameter((Function)upperContext.Element, upperContext);

            throw new UnexpectedScopeException(this, upperContext.Element.GetType(),
                new[] {typeof(Class), typeof(Function)});
        }

        private IGenerationElement GenerateClassVariable(Class c, Context context)
        {
            c.Private.Variables.Add(this);

            if (Getter)
                c.Public.Functions.Add(new Function
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
                    }
                }.Generate<Function>(context));

            if (Setter)
                c.Public.Functions.Add(new Function
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
                }.Generate<Function>(context));

            c.Public.Types.Add(Type.Generate<IType>(context));

            return this;
        }

        private IGenerationElement GenerateParameter(Function f, Context context)
        {
            context.Add(Name, this);
            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}