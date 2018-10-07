using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ExtensionFunction : Statement
    {
        public IType ReturnType;
        public string Class;
        public string Name;
        public IList<Variable> Parameter;
        public IList<IStatement> Body;

        public override string ToString()
        {
            return $"{ReturnType} {Class}.{Name}()";
        }

        public override void Check(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            throw new System.NotImplementedException();
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}