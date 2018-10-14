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

        string ToCpp();

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

    public class SimpleType : Expression, IType
    {
        public string Name { get; }
        public int Array { get; }

        public SimpleType(string name, int array, int line = 0, int column = 0)
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

        public string ToCpp()
        {
            try
            {
                return TypesExtension.ToType(Name).ToCpp().ToArray(Array);
            }
            catch (Exception)
            {
                return Name;
            }
        }

        public PrimitiveType ToPrimitiveType() => TypesExtension.ToType(Name);

        public override string ToString()
        {
            return $"{Name}{string.Concat(Enumerable.Repeat("[]", Array))}";
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.PrintWithModifiers(ToCpp(), type);
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
        public static string ToCpp(this PrimitiveType type)
        {
            switch (type)
            {
                case PrimitiveType.I8: return "int8_t";
                case PrimitiveType.I16: return "int16_t";
                case PrimitiveType.I32: return "int32_t";
                case PrimitiveType.I64: return "int64_t";
                case PrimitiveType.U8: return "uint8_t";
                case PrimitiveType.U16: return "uint16_t";
                case PrimitiveType.U32: return "uint32_t";
                case PrimitiveType.U64: return "uint64_t";
                case PrimitiveType.USize: return "size_t";
                case PrimitiveType.F32: return "float";
                case PrimitiveType.F64: return "double";
                case PrimitiveType.String: return "std::string";
                case PrimitiveType.Void: return "void";
                case PrimitiveType.Bool: return "bool";
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

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
                return cppString;

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

        public const string I8 = "i8";
        public const string I16 = "i16";
        public const string I32 = "i32";
        public const string I64 = "i64";

        public const string Short = "short";
        public const string Int = "int";
        public const string Long = "long";

        public const string U8 = "u8";
        public const string U16 = "u16";
        public const string U32 = "u32";
        public const string U64 = "u64";

        public const string Byte = "byte";
        public const string UShort = "ushort";
        public const string UInt = "uint";
        public const string ULong = "ulong";
        public const string USize = "usize";

        public const string F32 = "f32";
        public const string F64 = "f64";

        public const string Float = "float";
        public const string Double = "double";

        public const string Bool = "bool";
        public const string Boolean = Bool;

        public const string String = "string";

        public const string Void = "void";

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

        public static readonly IDictionary<string, string> Aliases = new Dictionary<string, string>
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