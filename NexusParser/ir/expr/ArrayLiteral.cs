using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ArrayLiteral : Expression
    {
        public IList<IExpression> Values { get; set; }

        public override string ToString() => '{' + string.Join(", ", Values) + '}';

        public override IType GetResultType(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override void Check(Context context)
        { }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("{");
            foreach (var i in Values)
            {
                i.Print(type, printer);

                if (i != Values.Last())
                    printer.Write(", ");
            }
            printer.Write("}");
        }
    }
}