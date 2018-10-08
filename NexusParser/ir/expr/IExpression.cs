using Nexus.gen;

namespace Nexus.ir.expr
{
    public interface IExpression : ICheckable, IPositioned
    {
        IType GetResultType(Context context);
    }

    public abstract class Expression : IExpression
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public abstract IType GetResultType(Context context);
        public abstract void Check(Context context);
    }
}