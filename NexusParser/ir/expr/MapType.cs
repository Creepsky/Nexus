using System;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class MapType : Expression, IType
    {
        public IType KeyType;
        public IType ValueType;
        public int Array { get; set; }

        public override string ToString()
        {
            return $"[{KeyType} -> {ValueType}] {string.Concat(Enumerable.Repeat("[]", Array))}";
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.PrintWithModifiers(ToCpp(), type);
        }

        public bool IsPrimitive() => false;

        public bool IsAuto() => false;

        public string ToCpp()
        {
            return $"std::map<{KeyType.ToCpp().ToArray(Array)}, {ValueType.ToCpp().ToArray(Array)}>";
        }

        public override IType GetResultType(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Check(Context context)
        {
            KeyType.Check(context);
            ValueType.Check(context);
        }

        public IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }
    }
}