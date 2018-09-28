using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexus
{
    public enum Types
    {
        I8, I16, I32, I64,
        U8, U16, U32, U64,
        USize,
        F32, F64,
        String
    }

    public class TypeDefinition
    {
        public string OriginalType { get; private set; }
        public string Type { get; private set; }
        public int Array { get; private set; }

        public TypeDefinition(string type, int array = 0)
        {
            OriginalType = type;
            Type = type;
            Array = array;
        }

        public bool IsArray() => Array > 0;

        public bool IsInferred() => OriginalType != Type;

        public bool IsTuple() => Type.StartsWith('(') && Type.EndsWith(')');

        public void Infer()
        {
            // TODO: infer auto type
            Type = null;
        }

        public string TypeToCpp()
        {
            try
            {
                // primitive
                if (Array > 0)
                    return string.Concat(Enumerable.Repeat("std::vector<", Array)) +
                           TypesExtension.ToType(Type).ToCpp() +
                           new string('>', Array);

                return TypesExtension.ToType(Type).ToCpp();

            }
            catch (Exception)
            {
                // class
                return Type;
            }
        }

        public bool IsPrimitive()
        {
            if (Type.ToLower() == "string")
                return false;

            try
            {
                TypesExtension.ToType(Type);
                return true;
            }
            catch (Exception)
            {
                // class
                return false;
            }
        }

        public override string ToString()
        {
            return $"{Type}{string.Concat(Enumerable.Repeat("[]", Array))}";
        }
    }

    public static class TypesExtension
    {
        public static string ToCpp(this Types type)
        {
            switch (type)
            {
                case Types.I8: return "int8_t";
                case Types.I16: return "int16_t";
                case Types.I32: return "int32_t";
                case Types.I64: return "int64_t";
                case Types.U8: return "uint8_t";
                case Types.U16: return "uint16_t";
                case Types.U32: return "uint32_t";
                case Types.U64: return "uint64_t";
                case Types.USize: return "size_t";
                case Types.F32: return "float";
                case Types.F64: return "double";
                case Types.String: return "std::string";
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static Types ToType(string type)
        {
            switch (type.ToLower())
            {
                case "i8": return Types.I8;
                case "i16": return Types.I16;
                case "i32": return Types.I32;
                case "i64": return Types.I64;
                case "u8": return Types.U8;
                case "u16": return Types.U16;
                case "u32": return Types.U32;
                case "u64": return Types.U64;
                case "usize": return Types.USize;
                case "f32": return Types.F32;
                case "f64": return Types.F64;
                case "string": return Types.String;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static readonly IList<Types> Integer = new List<Types>
        {
            Types.I8, Types.I16, Types.I32, Types.I64,
            Types.U8, Types.U16, Types.U32, Types.U64
        };

        public static readonly IList<Types> Signed = new List<Types>
        {
            Types.I8, Types.I16, Types.I32, Types.I64
        };

        public static readonly IList<Types> Unsigned = new List<Types>
        {
            Types.U8, Types.U16, Types.U32, Types.U64, Types.USize
        };

        public static readonly IList<Types> Real = new List<Types>
        {
            Types.F32, Types.F64
        };

        public static TypeDefinition CreateTupleTypeDefinition(IEnumerable<TypeDefinition> types, int array) =>
            new TypeDefinition('(' + string.Join(',', types) + ')', array);
    }
}