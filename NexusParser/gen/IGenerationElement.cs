using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.ir;
using Nexus.ir.expr;

namespace Nexus.gen
{
    public interface IGenerationElement : ICheckable, IPrintable, IPositioned, ICloneable
    {
        string Name { get; set; }
        SimpleType GetResultType(Context context);
        IGenerationElement CloneDeep();
        void ForwardDeclare(Context upperContext);
        void Declare();
        void Define();
        void Remove();
        void Template(TemplateContext context, IGenerationElement concreteElement);
    }

    public interface IGenerationElementGenerator
    {
        TemplateList TemplateList { get; }
        IGenerationElement Generate();
    }

    public static class GenerationElementExtensions
    {
        public static IEnumerable<IGenerationElement> GetAllElements(params IEnumerable<IGenerationElement>[] children)
        {
            return children.SelectMany(i => i);
        }
    }
}