using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class AssignmentStatement : Statement
    {
        public IExpression Left { private get; set; }
        public IExpression Right { private get; set; }

        public override void Check(Context context)
        {
            
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType
            {
                Line = Line,
                Column = Column,
                Name = PrimitiveType.Void.ToString()
            };

        public override void Print(PrintType type, Printer printer)
        {
            Left.Print(type, printer);
            printer.Write(" = ");
            Right.Print(type, printer);
            printer.WriteLine(";");
        }
    }
}