using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public interface IStatement : IGenerationElement
    {
    }

    public abstract class Statement : IStatement
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public abstract void Check(Context context);
        public abstract SimpleType GetResultType(Context context);
        public abstract bool Print(PrintType type, Printer printer);

        public IGenerationElement CloneDeep()
        {
            var cloned = (Statement) Clone();
            cloned.Column = Column;
            cloned.Line = Line;
            cloned.FilePath = new string(FilePath);
            cloned.Name = new string(Name);
            return cloned;
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

        public abstract object Clone();
    }
}