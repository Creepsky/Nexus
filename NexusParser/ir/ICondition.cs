using System;

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

    public class Comparison : ICondition
    {
        public ComparisonType Type;
        public IExpression Left;
        public IExpression Right;
    }
}