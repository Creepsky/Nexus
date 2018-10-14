using System;
using System.Globalization;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public abstract class NumberLiteral : Expression
    {
        public static NumberLiteral Parse(string integerPart, string decimalPart, string suffix, int line, int column)
        {
            var sign = integerPart.First() == '-' ? '-' : '+';
            integerPart = integerPart.Replace(",", "");
            decimalPart = decimalPart?.Replace(",", "");

            PrimitiveType? type = null;

            if (string.IsNullOrEmpty(integerPart))
                throw new ArgumentNullException(nameof(integerPart), "not a number");

            if (!string.IsNullOrEmpty(suffix))
            {
                switch (suffix.ToLower())
                {
                    case "_i8":
                        type = PrimitiveType.I8;
                        break;
                    case "_i16":
                        type = PrimitiveType.I16;
                        break;
                    case "_i32":
                        type = PrimitiveType.I32;
                        break;
                    case "_i64":
                        type = PrimitiveType.I64;
                        break;
                    case "_u8":
                        type = PrimitiveType.U8;
                        break;
                    case "_u16":
                        type = PrimitiveType.U16;
                        break;
                    case "_u32":
                        type = PrimitiveType.U32;
                        break;
                    case "_u64":
                        type = PrimitiveType.U64;
                        break;
                    case "_usize":
                        type = PrimitiveType.USize;
                        break;
                    case "_f32":
                    case "f":
                        type = PrimitiveType.F32;
                        break;
                    case "_f64":
                    case "d":
                        type = PrimitiveType.F64;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(suffix), suffix, "unknown suffix");
                }
            }

            // check sign
            if (type.HasValue && sign == '-')
            {
                if (!TypesExtension.Signed.Contains(type.Value))
                {
                    throw new ArgumentOutOfRangeException(nameof(sign), sign, "");
                }
            }

            // f32/f64 with decimal part
            if (!string.IsNullOrEmpty(decimalPart))
            {
                if (type.HasValue && !TypesExtension.Real.Contains(type.Value))
                {
                    throw new ArgumentOutOfRangeException(nameof(suffix), suffix, "number has decimal part, but is not a real");
                }

                var real = $"{sign}{integerPart}.{decimalPart}";

                if (type.HasValue)
                {
                    switch (type)
                    {
                        case PrimitiveType.F32:
                            return new F32(ParseFloat(real)) {Line = line, Column = column};
                        case PrimitiveType.F64:
                            return new F64(ParseDouble(real)) {Line = line, Column = column};
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, "invalid type");
                    }
                }

                if (UIntPtr.Size == 4)
                {
                    if (Try(ParseFloat, real, out var f32))
                    {
                        return new F32(f32) {Line = line, Column = column};
                    }

                    if (Try(ParseDouble, real, out var f64))
                    {
                        return new F64(f64) {Line = line, Column = column};
                    }
                }
                else if (UIntPtr.Size == 8)
                {
                    if (Try(ParseDouble, real, out var f64))
                    {
                        return new F64(f64) {Line = line, Column = column};
                    }

                    if (Try(ParseFloat, real, out var f32))
                    {
                        return new F32(f32) {Line = line, Column = column};
                    }
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
                        case PrimitiveType.I8: return new I8(sbyte.Parse(integerPart)) { Line = line, Column = column  };
                        case PrimitiveType.I16: return new I16(short.Parse(integerPart)) { Line = line, Column = column };
                        case PrimitiveType.I32: return new I32(int.Parse(integerPart)) { Line = line, Column = column };
                        case PrimitiveType.I64: return new I64(long.Parse(integerPart)) { Line = line, Column = column };
                        case PrimitiveType.U8: return new U8(byte.Parse(integerPart)) { Line = line, Column = column };
                        case PrimitiveType.U16: return new U16(ushort.Parse(integerPart)) { Line = line, Column = column };
                        case PrimitiveType.U32: return new U32(uint.Parse(integerPart)) { Line = line, Column = column };
                        case PrimitiveType.U64: return new U64(ulong.Parse(integerPart)) { Line = line, Column = column };
                        // TODO: ulong to bit size
                        case PrimitiveType.USize: return new USize(ulong.Parse(integerPart)) { Line = line, Column = column };
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, "invalid type");
                    }
                }

                if (long.TryParse(integerPart, out var i64))
                {
                    return new I64(i64) {Line = line, Column = column };
                }

                if (ulong.TryParse(integerPart, out var u64))
                {
                    return new U64(u64) {Line = line, Column = column};
                }

                if (sbyte.TryParse(integerPart, out var i8))
                {
                    return new I8(i8) {Line = line, Column = column};
                }

                if (short.TryParse(integerPart, out var i16))
                {
                    return new I16(i16) {Line = line, Column = column};
                }

                if (int.TryParse(integerPart, out var i32))
                {
                    return new I32(i32) {Line = line, Column = column};
                }

                if (byte.TryParse(integerPart, out var u8))
                {
                    return new U8(u8) {Line = line, Column = column};
                }

                if (ushort.TryParse(integerPart, out var u16))
                {
                    return new U16(u16) {Line = line, Column = column};
                }

                if (uint.TryParse(integerPart, out var u32))
                {
                    return new U32(u32) {Line = line, Column = column};
                }
            }

            return null;
        }

        private static NumberLiteral ParseNumberBase(string number, int fromBase, int line, int column)
        {
            try
            {
                return new I64(Convert.ToInt64(number, fromBase))
                {
                    Line = line,
                    Column = column
                };

            }
            catch (Exception)
            {
                try
                {
                    return new U64(Convert.ToUInt64(number, fromBase))
                    {
                        Line = line,
                        Column = column
                    };

                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException(nameof(number), number, "number is too big");
                }
            }
        }

        public static NumberLiteral ParseBinary(string bits, int line, int column) => ParseNumberBase(bits, 2, line, column);

        public static NumberLiteral ParseHex(string hex, int line, int column) => ParseNumberBase(hex, 16, line, column);

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

        public abstract override IType GetResultType(Context context);

        public abstract override void Check(Context context);

        public abstract override void Print(PrintType type, Printer printer);
    }

    public class NumberLiteral<T> : NumberLiteral
    {
        public T Value { get; private set; }
        public PrimitiveType Type { get; private set; }

        public NumberLiteral(T value, PrimitiveType type)
        {
            Value = value;
            Type = type;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return new SimpleType(Type.ToString(), 0, Line, Column);
        }

        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write(Value.ToString());
        }
    }

    public class I8 : NumberLiteral<sbyte> { public I8(sbyte value) : base(value, PrimitiveType.I8) { } }
    public class I16 : NumberLiteral<short> { public I16(short value) : base(value, PrimitiveType.I16) { } }
    public class I32 : NumberLiteral<int> { public I32(int value) : base(value, PrimitiveType.I32) { } }
    public class I64 : NumberLiteral<long> { public I64(long value) : base(value, PrimitiveType.I64) { } }
    public class U8 : NumberLiteral<byte> { public U8(byte value) : base(value, PrimitiveType.U8) { } }
    public class U16 : NumberLiteral<ushort> { public U16(ushort value) : base(value, PrimitiveType.U16) { } }
    public class U32 : NumberLiteral<uint> { public U32(uint value) : base(value, PrimitiveType.U32) { } }
    public class U64 : NumberLiteral<ulong> { public U64(ulong value) : base(value, PrimitiveType.U64) { } }
    public class Byte : NumberLiteral<byte> { public Byte(byte value) : base(value, PrimitiveType.U8) { } }
    public class USize : NumberLiteral<ulong> { public USize(ulong value) : base(value, PrimitiveType.USize) { } }
    public class F32 : NumberLiteral<float> { public F32(float value) : base(value, PrimitiveType.F32) { } }
    public class F64 : NumberLiteral<double> { public F64(double value) : base(value, PrimitiveType.F64) { } }
}