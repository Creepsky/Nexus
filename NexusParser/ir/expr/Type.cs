using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public interface IType : IGenerationElement
    {
        bool IsPrimitive();
        bool IsAuto();
        int Array { get; }
    }

    public enum PrimitiveType
    {
        I8, I16, I32, I64,
        U8, U16, U32, U64,
        USize,
        F32, F64,
        String,
        Void,
        Bool
    }

    public class SimpleType : Expression, IType, IEquatable<SimpleType>
    {
        public int Array { get; }

        public SimpleType(string name)
            : this(name, 0)
        {
        }

        public SimpleType(string name, int array)
            : this(name, array, 0, 0)
        {
        }

        public SimpleType(string name, int array, int line, int column)
        {
            Name = name;
            Array = array;
            Line = line;
            Column = column;

            if (TypesExtension.Aliases.ContainsKey(name))
            {
                Name = TypesExtension.Aliases[Name];
            }
        }

        public bool IsPrimitive()
        {
            try
            {
                TypesExtension.ToType(Name);
                return true;
            }
            catch (Exception)
            {
                // class
                return false;
            }
        }

        public bool IsAuto()
        {
            return Name == "auto";
        }

        public PrimitiveType ToPrimitiveType() => TypesExtension.ToType(Name);

        public bool Equals(SimpleType other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }

        public override bool Equals(object other)
        {
            return Equals(other as SimpleType);
        }

        public override int GetHashCode()
        {
            return Array;
        }

        public override string ToString()
        {
            return $"{Name}{string.Concat(Enumerable.Repeat("[]", Array))}";
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.PrintWithModifiers(Name, type);
        }

        public override IType GetResultType(Context context)
        {
            return this;
        }

        public override void Check(Context context)
        {
            try
            {
                // first check if its a primitive type
                ToPrimitiveType();
            }
            catch (Exception)
            {
                // if not, it could also be a class
                context.Get<Class>(Name, this);
            }
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }
    }

    public static class TypesExtension
    {
        public static PrimitiveType ToType(string type)
        {
            switch (type.ToLowerInvariant())
            {
                case "i8": return PrimitiveType.I8;
                case "short":
                case "i16": return PrimitiveType.I16;
                case "int":
                case "i32": return PrimitiveType.I32;
                case "long":
                case "i64": return PrimitiveType.I64;
                case "u8": return PrimitiveType.U8;
                case "ushort":
                case "u16": return PrimitiveType.U16;
                case "uint":
                case "u32": return PrimitiveType.U32;
                case "ulong":
                case "u64": return PrimitiveType.U64;
                case "usize": return PrimitiveType.USize;
                case "f32": return PrimitiveType.F32;
                case "f64": return PrimitiveType.F64;
                case "string": return PrimitiveType.String;
                case "void": return PrimitiveType.Void;
                case "bool":
                case "boolean": return PrimitiveType.Bool;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static readonly IReadOnlyCollection<PrimitiveType> Integer = new List<PrimitiveType>
        {
            PrimitiveType.I8, PrimitiveType.I16, PrimitiveType.I32, PrimitiveType.I64,
            PrimitiveType.U8, PrimitiveType.U16, PrimitiveType.U32, PrimitiveType.U64
        };

        public static readonly IReadOnlyCollection<PrimitiveType> Signed = new List<PrimitiveType>
        {
            PrimitiveType.I8, PrimitiveType.I16, PrimitiveType.I32, PrimitiveType.I64
        };

        public static readonly IReadOnlyCollection<PrimitiveType> Unsigned = new List<PrimitiveType>
        {
            PrimitiveType.U8, PrimitiveType.U16, PrimitiveType.U32, PrimitiveType.U64, PrimitiveType.USize
        };

        public static readonly IReadOnlyCollection<PrimitiveType> Real = new List<PrimitiveType>
        {
            PrimitiveType.F32, PrimitiveType.F64
        };

        public static string ToArray(this string cppString, int array)
        {
            if (array == 0)
            {
                return cppString;
            }

            return string.Concat(Enumerable.Repeat("std::vector<", array)) +
                   cppString +
                   string.Concat(Enumerable.Repeat('>', array));
        }

        public static void PrintWithModifiers(this Printer printer, string toPrint, PrintType printType)
        {
            switch (printType)
            {
                case PrintType.Header:
                case PrintType.Source:
                case PrintType.Parameter:
                case PrintType.FunctionSource:
                case PrintType.ForSource:
                    printer.Write(toPrint);
                    break;
                case PrintType.ParameterRef:
                    printer.Write($"{toPrint}&");
                    break;
                case PrintType.ParameterConstRef:
                    printer.Write($"const {toPrint}&");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(printType), printType, null);
            }
        }

        public static readonly string I8 = "i8";
        public static readonly string I16 = "i16";
        public static readonly string I32 = "i32";
        public static readonly string I64 = "i64";

        public static readonly string Short = "short";
        public static readonly string Int = "int";
        public static readonly string Long = "long";

        public static readonly string U8 = "u8";
        public static readonly string U16 = "u16";
        public static readonly string U32 = "u32";
        public static readonly string U64 = "u64";

        public static readonly string Byte = "byte";
        public static readonly string UShort = "ushort";
        public static readonly string UInt = "uint";
        public static readonly string ULong = "ulong";
        public static readonly string USize = "usize";

        public static readonly string F32 = "f32";
        public static readonly string F64 = "f64";

        public static readonly string Float = "float";
        public static readonly string Double = "double";

        public static readonly string Bool = "bool";
        public static readonly string Boolean = Bool;

        public static readonly string String = "string";

        public static readonly string Void = "void";

        public static readonly IReadOnlyCollection<string> Primitives = new[]
        {
            I8,
            I16,
            I32,
            I64,
            Short,
            Int,
            Long,
            U8,
            U16,
            U32,
            U64,
            Byte,
            UShort,
            UInt,
            ULong,
            F32,
            F64,
            Float,
            Double,
            Bool,
            Boolean,
            String,
            Void,
            USize,
        };

        public static readonly IReadOnlyDictionary<string, string> Aliases = new Dictionary<string, string>
        {
            {Short, I16},
            {Int, I32},
            {Long, I64},
            {Byte, U8},
            {UShort, U16},
            {UInt, U32},
            {ULong, U64},
            {USize, U64},
            {Bool, Boolean},
            {Float, F32},
            {Double, F64},
        };
    }
}