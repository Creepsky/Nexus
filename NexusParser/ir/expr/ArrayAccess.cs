using System;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class ArrayAccess : Expression
    {
        public string Name { get; set; }
        public IExpression Index { get; set; }

        public override string ToString() => $"{Name}[{Index}]";

        public override IType GetResultType(Context context)
        {
            throw new NotImplementedException();
        }

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