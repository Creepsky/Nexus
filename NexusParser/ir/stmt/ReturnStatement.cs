using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ReturnStatement : Statement
    {
        public IExpression Value { get; set; }

        public override void Check(Context context)
        {
            if (context.Element == null)
            {
                throw new PositionedException(this, "return statement without parent scope");
            }

            // TODO: check compatiblity of types
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            Value.Generate(context, phase);
            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void, 0, Line, Column);

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("return ");
            Value.Print(type, printer);
            printer.WriteLine(";");
        }
    }
}