using System.Linq;
using Nexus.common;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ExtensionFunctionCall : Expression
    {
        public VariableLiteral Variable { get; set; }
        public FunctionCall FunctionCall { get; set; }
        
        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            if (phase == GenerationPhase.Declaration)
            {
                if (Variable.Name == "this")
                {
                    FunctionCall.Parameter.Insert(0, Variable);
                }
            }

            FunctionCall.Generate(context, phase);
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return context.Get($"{Variable}.{FunctionCall.Name}").GetResultType(context);
        }

        public override void Check(Context context)
        {
            Variable.Check(context);

            var name = $"{Variable}.{FunctionCall.Name}";
            var function = context.Get(name);

            if (function == null)
            {
                throw new NotFoundException(this, "extension function", name);
            }

            FunctionCall.Check(context);
        }

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                if (Variable.Name != "this")
                {
                    Variable.Print(type, printer);
                    printer.Write(".");
                }
                FunctionCall.Print(type, printer);
            }
        }
    }
}