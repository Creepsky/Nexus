using Nexus.gen;

namespace Nexus.ir.stmt
{
    public interface IStatement : IPositioned, IGenerationElement
    { }

    public abstract class Statement : IStatement
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public abstract void Check(Context context);
        public abstract IGenerationElement Generate(Context context);
        public abstract void Print(PrintType type, Printer printer);
    }
}