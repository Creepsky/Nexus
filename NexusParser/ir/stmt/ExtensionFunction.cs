using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ExtensionFunction : Statement
    {
        public IType ReturnType { get; set; }
        public IType ExtensionBase { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public IList<Variable> Parameter { get; set; }
        public IList<IStatement> Body { get; set; }

        public override string ToString()
        {
            return $"{ReturnType} {ExtensionBase}.{Name}()";
        }

        public override void Check(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            throw new System.NotImplementedException();
        }

        public override IType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void, Line, Column);

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}