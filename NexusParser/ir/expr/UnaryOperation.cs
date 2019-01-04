using System;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public enum UnaryOperationType
    {
        Minus,
        Plus,
        PreIncrement,
        PostIncrement
    }

    public class UnaryOperation : Expression
    {
        public IExpression Expression { get; private set; }
        public UnaryOperationType Type { get; }

        public UnaryOperation(IExpression expression, UnaryOperationType type)
        {
            Expression = expression;
            Type = type;
        }

        public override SimpleType GetResultType(Context context)
        {
            return Expression.GetResultType(context);
        }

        public override void Check(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override bool Print(PrintType type, Printer printer)
        {
            if (Type == UnaryOperationType.Minus)
            {
                printer.Write("-");
            }

            if (Type == UnaryOperationType.Plus)
            {
                printer.Write("+");
            }

            if (Type == UnaryOperationType.PreIncrement)
            {
                printer.Write("--");
            }

            Expression.Print(type, printer);

            return true;
        }

        public override object Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}