using System;
using Nexus.gen;

namespace Nexus.ir.expr
{    
    public interface IExpression : IGenerationElement
    {
    }

    public abstract class Expression : IExpression
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public abstract SimpleType GetResultType(Context context);
        public IGenerationElement CloneDeep()
        {
            var i = (Expression) Clone();
            i.Column = Column;
            i.Line = Line;
            i.FilePath = FilePath;
            i.Name = new string(Name);
            return i;
        }

        public virtual void ForwardDeclare(Context upperContext)
        {
        }

        public virtual void Declare()
        {
        }

        public virtual void Define()
        {
        }

        public virtual void Remove()
        {
        }

        public virtual void Template(TemplateContext context, IGenerationElement concreteElement)
        {
        }

        public abstract void Check(Context context);
        public abstract bool Print(PrintType type, Printer printer);
        public abstract object Clone();
    }
}