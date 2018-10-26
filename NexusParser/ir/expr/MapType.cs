using System;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class MapType : Expression, IType, IEquatable<MapType>
    {
        public IType KeyType { get; set; }
        public IType ValueType { get; set; }
        public int Array { get; set; }
        public bool Primitive { get; }
        public bool Auto { get; }

        public bool Equals(MapType other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(KeyType, other.KeyType) && Equals(ValueType, other.ValueType) && Array == other.Array;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MapType);
        }

        public override int GetHashCode()
        {
            var hashCode = (KeyType != null ? KeyType.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (ValueType != null ? ValueType.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ Array;
            return hashCode;
        }

        public override string ToString()
        {
            return $"[{KeyType} -> {ValueType}] {string.Concat(Enumerable.Repeat("[]", Array))}";
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.PrintWithModifiers("map", type);
        }

        public bool IsPrimitive() => Primitive;

        public bool IsAuto() => Auto;

        public override IType GetResultType(Context context)
        {
            return this;
        }

        public override void Check(Context context)
        {
            KeyType.Check(context);
            ValueType.Check(context);
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }
    }
}