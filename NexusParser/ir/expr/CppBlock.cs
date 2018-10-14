using Nexus.gen;

namespace Nexus.ir.expr
{
    public class CppBlock : Expression
    {
        public string Block { get; }
        
        public CppBlock(string block, int line, int column)
        {
            Block = block;
            Line = line;
            Column = column;
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return new SimpleType(TypesExtension.Void, 0);
        }

        public override void Check(Context context)
        {
            // TODO
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write(Block);
        }
    }
}