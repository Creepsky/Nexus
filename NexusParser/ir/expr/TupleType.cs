using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TupleType : Expression, IType, IEquatable<TupleType>
    {
        public IList<IType> Types { get; set; }
        public int Array { get; set; }

        public bool Equals(TupleType other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Types.Count != other.Types.Count)
            {
                return false;
            }

            for (var i = 0; i < Types.Count; ++i)
            {
                if (!Types[i].Equals(other.Types[i]))
                {
                    return false;
                }
            }

            return true;
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
            {
                i.Check(context);
            }
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }
    }
}