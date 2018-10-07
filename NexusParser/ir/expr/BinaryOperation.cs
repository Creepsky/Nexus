using System;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public enum BinaryOperatorType
    {
        Add,
        Sub,
        Mul,
        Div
    }

    public class BinaryOperation : Expression
    {
        public IExpression Left;
        public BinaryOperatorType Type;
        public IExpression Right;

        public override string ToString()
        {
            char op;

            if (Type == BinaryOperatorType.Add)
                op = '+';
            else if (Type == BinaryOperatorType.Div)
                op = '/';
            else if (Type == BinaryOperatorType.Sub)
                op = '-';
            else if (Type == BinaryOperatorType.Mul)
                op = '*';
            else
                throw new ArgumentOutOfRangeException(nameof(Type), Type, "unknown binary type");

            return $"{Left} {op} {Right}";
        }

        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }
    }
}