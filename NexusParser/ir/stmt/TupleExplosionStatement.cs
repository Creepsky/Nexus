using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class TupleExplosionStatement : Statement
    {
        public IList<string> Names;
        public IExpression Right;

        public override void Check(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override IGenerationElement Generate(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}