using Nexus.gen;

namespace Nexus.ir.expr
{
    public class BooleanLiteral : Expression
    {
        public bool Value { get; set; }

        public override string ToString() => Value ? "true" : "false";

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return new SimpleType
            {
                Line = Line,
                Column = Column,
                Name = PrimitiveType.Bool.ToString()
            };
        }

        public override void Check(Context context)
        {
            // TODO
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write(Value ? "true" : "false");
        }
    }
}