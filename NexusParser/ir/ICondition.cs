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

        public override void Print(PrintType type, Printer printer)
        {
            Left.Print(type, printer);

            switch (Type)
            {
                case ComparisonType.Equals:
                    printer.Write(" == ");
                    break;
                case ComparisonType.NotEquals:
                    printer.Write(" != ");
                    break;
                case ComparisonType.Less:
                    printer.Write(" < ");
                    break;
                case ComparisonType.Greater:
                    printer.Write(" > ");
                    break;
                case ComparisonType.LessEquals:
                    printer.Write(" <= ");
                    break;
                case ComparisonType.GreaterEquals:
                    printer.Write(" >= ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Right.Print(type, printer);
        }
    }
}