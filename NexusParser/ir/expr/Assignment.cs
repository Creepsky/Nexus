using System;
using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public enum AssignmentType
    {
        Move,
        Copy
    }

    public class Assignment : Expression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }
        public AssignmentType Type { get; }

        private Context _context;
        private SimpleType _resultType;
        private FunctionCall _functionCall;

        public Assignment(IExpression left, IExpression right, AssignmentType type)
        {
            Left = left;
            Right = right;
            Type = type;
        }

        protected virtual string GetOperatorName()
        {
            switch (Type)
            {
                case AssignmentType.Move:
                    return Function.OperatorToName("<-");
                case AssignmentType.Copy:
                    return Function.OperatorToName("=");
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), Type, "Unknown assignment type");
            }
        }

        public override SimpleType GetResultType(Context context)
        {
            return _resultType ?? (_resultType = context.Get<Function>(GetOperatorName(), this)
                       .GetOverload(GetFunctionCall(), context)
                       .GetResultType(context));
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            Left.Template(context, concreteElement);
            Right.Template(context, concreteElement);
        }

        public override void Check(Context context)
        {
            Left.Check(context);
            Right.Check(context);
            GetFunctionCall().Check(context);
        }

        public override bool Print(PrintType type, Printer printer)
        {
            var isVariable = Left is Variable;

            if (isVariable)
            {
                Left.Print(type, printer);
                printer.WriteLine(";");
                printer.Write($"assign({Left.Name}, ");
            }

            printer.Write($"{GetOperatorName()}(");
            
            if (isVariable)
            {
                printer.Write(Left.Name);
            }
            else
            {
                Left.Print(type, printer);
            }

            printer.Write(", ");
            Right.Print(type, printer);
            printer.Write(")");

            if (isVariable)
            {
                printer.Write(")");
            }

            return true;
        }

        public override object Clone()
        {
            return new Assignment((IExpression)Left.CloneDeep(), (IExpression)Right.CloneDeep(), Type);
        }

        public override void ForwardDeclare(Context upperContext)
        {
            _context = upperContext;
            Left.ForwardDeclare(_context);
            Right.ForwardDeclare(_context);
        }

        public override void Declare()
        {
            Left.Declare();
            Right.Declare();
        }

        public override void Define()
        {
            Left.Define();
            Right.Define();

            var function = _context.Get(Function.OperatorToName("=")) as Function;

            function = function?.GetOverload(GetFunctionCall(), _context);

            if (function == null)
            {
                var lhsClass = _context.Get<Class>(Left.GetResultType(_context).Name, this);

                function = new Function
                {
                    Line = Line,
                    Column = Column,
                    FilePath = FilePath,
                    ExtensionBase = (SimpleType)Left.GetResultType(_context).CloneDeep(),
                    Body = new List<IStatement>(),
                    Name = GetOperatorName(),
                    ReturnType = (SimpleType)Left.GetResultType(_context).CloneDeep(),
                    Parameter = new List<Variable>
                    {
                        new Variable
                        {
                            Line = Line,
                            Column = Column,
                            FilePath = FilePath,
                            Type = (SimpleType)Right.GetResultType(_context).CloneDeep(),
                            Name = "rhs"
                        }
                    }
                };

                function.ExtensionBase.Reference = true;
                function.Parameter[0].Type.Constant = true;
                function.Parameter[0].Type.Reference = true;
                
                foreach (var i in lhsClass.Variables)
                {
                    function.Body.Add(new ExpressionStatement
                    {
                        Line = Line,
                        Column = Column,
                        FilePath = FilePath,
                        Expression =
                            new Assignment(
                                new MemberAccess(
                                        new VariableLiteral
                                            {Line = Line, Column = Column, FilePath = FilePath, Name = "this"},
                                        i.Name)
                                    {Line = Line, Column = Column, FilePath = FilePath},
                                new MemberAccess(
                                        new VariableLiteral
                                            {Line = Line, Column = Column, FilePath = FilePath, Name = "rhs"},
                                        i.Name)
                                    {Line = Line, Column = Column, FilePath = FilePath},
                                Type)
                            {
                                Line = Line,
                                Column = Column,
                                FilePath = FilePath
                            }
                    });
                }

                function.Body.Add(new ReturnStatement
                {
                    Line = Line,
                    Column = Column,
                    FilePath = FilePath,
                    Value = new VariableLiteral
                    {
                        Line = Line,
                        Column = Column,
                        FilePath = FilePath,
                        Name = "this"
                    }
                });

                function.ForwardDeclare(_context.UpperContext);
                function.Declare();
                function.Define();
            }
        }

        public override void Remove()
        {
            Left.Remove();
            Right.Remove();
        }

        public override string ToString()
        {
            return $"{Left} = {Right}";
        }
        
        private FunctionCall GetFunctionCall() =>
            _functionCall ?? (_functionCall = new FunctionCall
            {
                Column = Column,
                Line = Line,
                FilePath = FilePath,
                Name = GetOperatorName(),
                Object = Left,
                Parameter = new List<IExpression> {Right},
                Static = false
            });
    }
}