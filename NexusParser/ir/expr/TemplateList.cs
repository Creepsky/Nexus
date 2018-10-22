using System.Collections.Generic;
using System.Linq;
using Nexus.common;
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
            if (Types.Distinct().Count() != Types.Count)
            {
                var duplicateItems =
                    from x in Types
                    group x by x into grouped
                    where grouped.Count() > 1
                    select grouped.Key;
                
                throw new PositionedException(this, $"Duplicated template variable names: {string.Join(", ", duplicateItems)}");
            }

            if (!Types.Any())
            {
                throw new PositionedException(this, "Empty template list");
            }
        }

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.WriteLine($"template <typename {string.Join(", typename ", Types)}>");
            }
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
            if (type == PrintType.Header)
            {
                printer.WriteLine("template <typename... T>");
            }
        }
    }
}