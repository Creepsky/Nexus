using System;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir
{
    public interface ICondition : IExpression
    { }

    public enum ComparisonType
    {
        Equals,
        NotEquals,
        Less,
        Greater,
        LessEquals,
        GreaterEquals
    }

    public class Comparison : Expression, ICondition
    {
        public ComparisonType Type;
        public IExpression Left;
        public IExpression Right;

        public override IType GetResultType(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Check(Context context)
        {
        }
    }
}