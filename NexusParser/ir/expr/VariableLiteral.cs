using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class VariableLiteral : Expression
    {
        public string Name { get; set; }

        public override string ToString() => Name;

        public override IType GetResultType(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override void Check(Context context)
        {
            // does the variable exist in this or some higher context?
            context.Get<Variable>(Name, this);
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write(Name);
        }
    }
}