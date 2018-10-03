using NexusParserAntlr.ir;

namespace NexusParserAntlr.Generation
{
    public class CompilationType
    {
        public readonly CompilationUnit Unit;
        public readonly IType AstType;

        public CompilationType(CompilationUnit unit, IType type)
        {
            Unit = unit;
            AstType = type;
        }

        public void Print(Printer printer)
        {
            printer.Write(AstType.IsPrimitive()
                ? AstType.ToCpp()
                : $"const {AstType.ToCpp()}&");
        }
    }
}