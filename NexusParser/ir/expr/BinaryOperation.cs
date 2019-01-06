using System;
using System.Collections.Generic;
using Nexus.common;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public enum BinaryOperatorType
    {
        Add,
        Sub,
        Mul,
        Div,
        Assign,
        AddAndAssign,
        SubAndAssign,
        MulAndAssign,
        DivAndAssign,
        Less,
        Greater,
        LessEqual,
        GreaterEqual,
        Equal,
        LogicalOr,
        LogicalAnd,
        BitwiseOr,
        BitwiseAnd
    }

    public class BinaryOperation : Expression
    {
        public IExpression Left { get; set; }
        public BinaryOperatorType Type { get; set; }
        public IExpression Right { get; set; }
        private SimpleType _resultType;
        private FunctionCall _functionCall;

        public override string ToString()
        {
            return $"{Left} {GetOperator()} {Right}";
        }

        private string GetOperator()
        {
            switch (Type)
            {
                case BinaryOperatorType.Add:
                    return "+";
                case BinaryOperatorType.Div:
                    return "/";
                case BinaryOperatorType.Sub:
                    return "-";
                case BinaryOperatorType.Mul:
                    return "*";
                case BinaryOperatorType.Assign:
                    return "assign";
                case BinaryOperatorType.AddAndAssign:
                    return "+=";
                case BinaryOperatorType.SubAndAssign:
                    return "-=";
                case BinaryOperatorType.MulAndAssign:
                    return "*=";
                case BinaryOperatorType.DivAndAssign:
                    return "/=";
                case BinaryOperatorType.Less:
                    return "<";
                case BinaryOperatorType.Greater:
                    return ">";
                case BinaryOperatorType.LessEqual:
                    return "<=";
                case BinaryOperatorType.GreaterEqual:
                    return ">=";
                case BinaryOperatorType.Equal:
                    return "==";
                case BinaryOperatorType.LogicalOr:
                    return "||";
                case BinaryOperatorType.LogicalAnd:
                    return "&&";
                case BinaryOperatorType.BitwiseOr:
                    return "|";
                case BinaryOperatorType.BitwiseAnd:
                    return "&";
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), Type, "unknown binary type");
            }
        }

        public override SimpleType GetResultType(Context context) =>
            _resultType ?? (_resultType = GetFunctionCall().GetResultType(context));

        public override void ForwardDeclare(Context upperContext)
        {
            Left.ForwardDeclare(upperContext);
            Right.ForwardDeclare(upperContext);
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
        }

        public override void Remove()
        {
            Left.Remove();
            Right.Remove();
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

            // if the extension type is a c++ type, we can't ensure nothing.. so just return as it is ok
            if (Left.GetResultType(context) is CppType)
            {
                return;
            }

            var functionCall = GetFunctionCall();

            functionCall.Check(context);

            if (Type == BinaryOperatorType.Equal ||
                Type == BinaryOperatorType.Greater ||
                Type == BinaryOperatorType.Less ||
                Type == BinaryOperatorType.LessEqual ||
                Type == BinaryOperatorType.LogicalOr ||
                Type == BinaryOperatorType.LogicalAnd)
            {
                var boolean = new SimpleType(TypesExtension.Boolean);
                var actualReturnType = functionCall.GetResultType(context);

                if (!actualReturnType.Equals(boolean))
                {
                    throw new PositionedException(this, $"Return type of comparison function {functionCall.Name} " +
                                                        $"expected to be {boolean} but was {actualReturnType}");
                }
            }
        }

        private FunctionCall GetFunctionCall() => _functionCall ?? (_functionCall = new FunctionCall
        {
            Name = "operator" + GetOperator(),
            Column = Column,
            Line = Line,
            FilePath = FilePath,
            Object = Left,
            Parameter = new List<IExpression> {Right},
            Static = false
        });

        public override bool Print(PrintType type, Printer printer)
        {
            Left.Print(type, printer);
            printer.Write($" {GetOperator()} ");
            Right.Print(type, printer);
            return true;
        }

        public override object Clone()
        {
            return new BinaryOperation
            {
                Left = (IExpression) Left.CloneDeep(),
                Right = (IExpression) Right.CloneDeep(),
                Type = Type
            };
        }
    }
}