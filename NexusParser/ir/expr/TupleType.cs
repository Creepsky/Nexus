using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TupleType : Expression, IType
    {
        public IList<IType> Types { get; set; }
        public int Array { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(TupleType))
            {
                var rhs = (TupleType) obj;

                if (Types.Count != rhs.Types.Count)
                {
                    return false;
                }

                for (var i = 0; i < Types.Count; ++i)
                {
                    if (!Types[i].Equals(rhs.Types[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Types != null ? Types.GetHashCode() : 0) * 397) ^ Array;
            }
        }

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
            return this;
        }

        public override void Check(Context context)
        {
            foreach (var i in Types)
                i.Check(context);
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }
    }
}