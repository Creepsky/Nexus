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
        public bool Primitive { get; }
        public bool Auto { get; }

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

        public override bool Equals(object other)
        {
            return Equals(other as TupleType);
        }

        public override int GetHashCode()
        {
            return ((Types != null ? Types.GetHashCode() : 0) * 397) ^ Array;
        }

        public override string ToString()
        {
            return $"({string.Join(',', Types)}) {string.Concat(Enumerable.Repeat("[]", Array))}";
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.PrintWithModifiers("tuple", type);
        }

        public bool IsPrimitive() => Primitive;

        public bool IsAuto() => Auto;

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