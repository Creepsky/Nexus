using System.Collections.Generic;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public interface ITemplateList : IExpression
    { }
    
    public class TemplateList : Expression, ITemplateList
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

    public class VariadicTemplateList : Expression, ITemplateList
    {
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