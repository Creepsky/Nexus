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
            // TODO
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void, 0, Line, Column);

        public override void Print(PrintType type, Printer printer)
        {
            Left.Print(type, printer);
            printer.Write(" = ");
            Right.Print(type, printer);
            printer.WriteLine(";");
        }
    }
}