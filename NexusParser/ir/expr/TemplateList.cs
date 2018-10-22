using System.Collections.Generic;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TemplateList : Expression
    {
        public IList<string> Types { get; }

        public TemplateList(IList<string> types)
        {
            Types = types;
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            throw new System.NotImplementedException();
        }

        public override IType GetResultType(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override void Check(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}