using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using Sprache;

namespace Nexus
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

    public class ArrayAccessLiteral : IExpression
    {
        public string Name;
        public IExpression Index;
    }

    public class RangeLiteral : IExpression
    {
        public IExpression Start;
        public IExpression End;
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
            if (sign == '-')
                integerPart = '-' + integerPart;

            Types? type = null;

            if (string.IsNullOrEmpty(integerPart))
                throw new ArgumentNullException(nameof(integerPart), "not a number");

            if (!string.IsNullOrEmpty(suffix))
            {
                switch (suffix.ToLower())
                {
                    case "_i8":
                        type = Types.I8;
                        break;
                    case "_i16":
                        type = Types.I16;
                        break;
                    case "_i32":
                        type = Types.I32;
                        break;
                    case "_i64":
                        type = Types.I64;
                        break;
                    case "_u8":
                        type = Types.U8;
                        break;
                    case "_u16":
                        type = Types.U16;
                        break;
                    case "_u32":
                        type = Types.U32;
                        break;
                    case "_u64":
                        type = Types.U64;
                        break;
                    case "_usize":
                        type = Types.USize;
                        break;
                    case "_f32":
                    case "f":
                        type = Types.F32;
                        break;
                    case "_f64":
                    case "d":
                        type = Types.F64;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(suffix), suffix, "unknown suffix");
                }
            }

            // check sign
            if (type.HasValue && sign == '-')
                if (!TypesExtension.Signed.Contains(type.Value))
                    throw new ArgumentOutOfRangeException(nameof(sign), sign, "");

            // f32/f64 with decimal part
            if (!string.IsNullOrEmpty(decimalPart))
            {
                if (type.HasValue && !TypesExtension.Real.Contains(type.Value))
                    throw new ArgumentOutOfRangeException(nameof(suffix), suffix, "number has decimal part, but is not a real");

                var real = $"{sign}{integerPart}.{decimalPart}";

                if (type.HasValue)
                {
                    switch (type)
                    {
                        case Types.F32:
                            return new F32 {Value = ParseFloat(real)};
                        case Types.F64:
                            return new F64 {Value = ParseDouble(real)};
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, "invalid type");
                    }
                }

                if (UIntPtr.Size == 4)
                {
                    if (Try(ParseFloat, real, out var f32))
                        return new F32 {Value = f32};

                    if (Try(ParseDouble, real, out var f64))
                        return new F64 {Value = f64};
                }
                else if (UIntPtr.Size == 8)
                {
                    if (Try(ParseDouble, real, out var f64))
                        return new F64 {Value = f64};

                    if (Try(ParseFloat, real, out var f32))
                        return new F32 {Value = f32};
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(UIntPtr.Size), "unknown platform");
                }
            }
            // i/u 8-64
            else
            {
                if (type.HasValue)
                {
                    switch (type)
                    {
                        case Types.I8: return new I8 { Value = char.Parse(integerPart) };
                        case Types.I16: return new I16 { Value = short.Parse(integerPart) };
                        case Types.I32: return new I32 { Value = int.Parse(integerPart) };
                        case Types.I64: return new I64 { Value = long.Parse(integerPart) };
                        case Types.U8: return new U8 { Value = byte.Parse(integerPart) };
                        case Types.U16: return new U16 { Value = ushort.Parse(integerPart) };
                        case Types.U32: return new U32 { Value = uint.Parse(integerPart) };
                        case Types.U64: return new U64 { Value = ulong.Parse(integerPart) };
                        // TODO: ulong to bit size
                        case Types.USize: return new USize { Value = ulong.Parse(integerPart) };
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, "invalid type");
                    }
                }

                if (long.TryParse(integerPart, out var i64))
                    return new I64 {Value = i64};

                if (ulong.TryParse(integerPart, out var u64))
                    return new U64 {Value = u64};

                if (char.TryParse(integerPart, out var i8))
                    return new I8 {Value = i8};

                if (short.TryParse(integerPart, out var i16))
                    return new I16 {Value = i16};

                if (int.TryParse(integerPart, out var i32))
                    return new I32 {Value = i32};

                if (byte.TryParse(integerPart, out var u8))
                    return new U8 {Value = u8};

                if (ushort.TryParse(integerPart, out var u16))
                    return new U16 {Value = u16};

                if (uint.TryParse(integerPart, out var u32))
                    return new U32 {Value = u32};
            }

            return null;
        }

        public static NumberLiteral ParseBinary(string bits)
        {
            try
            {
                return new NumberLiteral<long>
                {
                    Value = Convert.ToInt64(bits, 2)
                };

            }
            catch (Exception)
            {
                try
                {
                    return new NumberLiteral<ulong>
                    {
                        Value = Convert.ToUInt64(bits, 2)
                    };

                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException(nameof(bits), bits, "binary number is too big");
                }
            }
        }

        private static float ParseFloat(string number) =>
            float.Parse(number, NumberStyles.Float, CultureInfo.InvariantCulture);

        private static double ParseDouble(string number) =>
            double.Parse(number, NumberStyles.Float, CultureInfo.InvariantCulture);

        public static bool Try<T>(Func<string, T> lambda, string number, out T result)
        {
            try
            {
                result = lambda(number);
                return true;
            }
            catch (Exception)
            {
                result = default(T);
                return false;
            }
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

    public class Tuple : IExpression
    {
        public IList<IExpression> Values;
    }

    public class BooleanLiteral : IExpression
    {
        public bool Value;
    }

    public class ArrayLiteral : IExpression
    {
        public IList<IExpression> Values;
    }
}