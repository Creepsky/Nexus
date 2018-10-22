using Nexus.common;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ExtensionFunctionCall : Expression
    {
        public string Variable { get; set; }
        public FunctionCall FunctionCall { get; set; }
        
        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            FunctionCall.Generate(context, phase);
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return context.Get($"{Variable}.{FunctionCall.Name}").GetResultType(context);
        }

        public override void Check(Context context)
        {
            var name = $"{Variable}.{FunctionCall.Name}";
            var function = context.Get(name);

            if (function == null)
            {
                throw new NotFoundException(this, "extension function", nameof(name));
            }
        }

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.Write($"{Variable}.");
                FunctionCall.Print(type, printer);
            }
        }
    }
}