using Nexus.gen;
using Nexus.ir.stmt;

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
            if (phase == GenerationPhase.Declaration)
            {
            }

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

    public class CppBlockStatement : Statement
    {
        private CppBlock _cppBlock;

        public CppBlockStatement(CppBlock cppBlock)
        {
            _cppBlock = cppBlock;
        }

        public override void Check(Context context)
        {
            _cppBlock.Check(context);
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            _cppBlock.Generate(context, phase);
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return _cppBlock.GetResultType(context);
        }

        public override void Print(PrintType type, Printer printer)
        {
            _cppBlock.Print(type, printer);
            printer.WriteLine(";");
        }
    }
}