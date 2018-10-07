using Nexus.gen;

namespace Nexus.ir.expr
{
    public interface IExpression : ICheckable
    { }

    public abstract class Expression : IExpression, IPositioned
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public abstract void Check(Context context);
    }
}