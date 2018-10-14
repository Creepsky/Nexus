using Nexus.gen;

namespace Nexus.ir.expr
{    
    public interface IExpression : IGenerationElement
    {
    }

    public abstract class Expression : IExpression
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public abstract IGenerationElement Generate(Context context, GenerationPhase phase);
        public abstract IType GetResultType(Context context);
        public abstract void Check(Context context);
        public abstract void Print(PrintType type, Printer printer);
    }
}