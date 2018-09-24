using System;
using Sprache;

namespace Volt
{
    public enum ExpressionType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public interface IExpression
    { }

    public class Expression : IExpression
    {
        public ExpressionType Type;
        public IExpression LeftExpression;
        public IExpression RightExpression;
    }

    public enum ComparisonType
    {
        Equals,
        NotEquals,
        LessEquals,
        Less,
        GreaterEquals,
        Greater
    }

    public class Comparison : IExpression
    {
        public ComparisonType Type;
        public IExpression LeftExpression;
        public IExpression RightExpression;

        public Comparison(IExpression left, string op, IExpression right)
        {
            LeftExpression = left;
            RightExpression = right;
            switch (op)
            {
                case "<":
                    Type = ComparisonType.Less;
                    break;
                case "<=":
                    Type = ComparisonType.LessEquals;
                    break;
                case "==":
                    Type = ComparisonType.Equals;
                    break;
                case "!=":
                    Type = ComparisonType.NotEquals;
                    break;
                case ">=":
                    Type = ComparisonType.GreaterEquals;
                    break;
                case ">":
                    Type = ComparisonType.Greater;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, "Invalid comparison operator");
            }
        }
    }

    public class VariableLiteral : IExpression
    {
        public string Name;
    }

    public class ArrayLiteral : IExpression
    {
        public string Name;
        public IExpression Index;
    }

    public abstract class Constant : IExpression
    { }

    public class Text : Constant
    {
        public string Value;

        public override string ToString()
        {
            return Value;
        }
    }

    public class NumberLiteral : Constant
    {
        public static NumberLiteral Parse(char sign, string integerPart, string decimalPart, string suffix)
        {
            return null;
        }
    }

    public class NumberLiteral<T> : NumberLiteral
    {
        public T Value;

        //public char Sign;
        //public string IntegerPart;
        //public string DecimalPart;
        //public string Suffix;

        //public char AsI8() => IntegerCast(char.Parse);

        //public short AsI16() => IntegerCast(short.Parse);

        //public int AsI32() => IntegerCast(int.Parse);

        //public long AsI64() => IntegerCast(long.Parse);

        //public byte AsU8() => IntegerCast(byte.Parse);

        //public ushort AsU16() => IntegerCast(ushort.Parse);

        //public uint AsU32() => IntegerCast(uint.Parse);

        //public ulong AsU64() => IntegerCast(ulong.Parse);

        //public ulong AsUSize()
        //{
        //    switch (UIntPtr.Size)
        //    {
        //        case 4:
        //            // 32 bits
        //            return IntegerCast(uint.Parse);
        //        case 8:
        //            // 64 bits
        //            return IntegerCast(ulong.Parse);
        //        default:
        //            // unknown bits
        //            throw new ArgumentOutOfRangeException(nameof(UIntPtr.Size), UIntPtr.Size, "Unknown architecture");
        //    }
        //}

        //private T IntegerCast<T>(Func<string, T> lambda)
        //{
        //    if (!string.IsNullOrWhiteSpace(DecimalPart))
        //    {
        //        // TODO: warning, truncation
        //    }

        //    return lambda($"{Sign}{IntegerPart}");
        //}

        //public float AsF32() => (float) AsF64();

        //public double AsF64()
        //{
        //    var f64 = "";

        //    if (Sign != '+')
        //        f64 += Sign;

        //    f64 += IntegerPart;

        //    if (string.IsNullOrWhiteSpace(DecimalPart))
        //        f64 += $".{DecimalPart}";

        //    return double.Parse(f64);
        //}

        //public override string ToString()
        //{
        //    var toString = "";

        //    if (Sign == '-')
        //        toString += "-";

        //    toString += IntegerPart;

        //    if (!string.IsNullOrWhiteSpace(DecimalPart))
        //        toString += $".{DecimalPart}";

        //    return toString;
        //}
    }

    public class I8 : NumberLiteral<char> { }
    public class I16 : NumberLiteral<short> { }
    public class I32 : NumberLiteral<int> { }
    public class I64 : NumberLiteral<long> { }
    public class U8 : NumberLiteral<byte> { }
    public class U16 : NumberLiteral<ushort> { }
    public class U32 : NumberLiteral<uint> { }
    public class U64 : NumberLiteral<ulong> { }
    public class Byte : NumberLiteral<byte> { }
    public class USize : NumberLiteral<ulong> { }
    public class F32 : NumberLiteral<float> { }
    public class F64 : NumberLiteral<double> { }

}