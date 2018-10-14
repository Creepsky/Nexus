using System;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class ArrayAccess : Expression
    {
        public string Name { get; set; }
        public IExpression Index { get; set; }
        
        public override string ToString() => $"{Name}[{Index}]";

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            context.Get(Name).GetResultType(context);

        public override void Check(Context context)
        {
            context.Get(Name);
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write($"{Name}[");
            Index.Print(type, printer);
            printer.Write("]");
        }
    }
}