using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TupleType : Expression, IType
    {
        public IList<IType> Types;
        public int Array { get; set; }

        public override string ToString()
        {
            return $"({string.Join(',', Types)}) {string.Concat(Enumerable.Repeat("[]", Array))}";
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.PrintWithModifiers(ToCpp(), type);
        }

        public bool IsPrimitive() => false;

        public bool IsAuto() => false;

        public string ToCpp()
        {
            return $"std::tuple<{string.Join(", ", Types.Select(i => i.ToCpp().ToArray(Array)))}>";
        }

        public override IType GetResultType(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Check(Context context)
        {
            foreach (var i in Types)
                i.Check(context);
        }

        public IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }
    }
}