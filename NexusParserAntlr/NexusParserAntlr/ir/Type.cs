﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace NexusParserAntlr.ir
{
    public interface IType
    { }

    public enum PrimitiveType
    {
        I8, I16, I32, I64,
        U8, U16, U32, U64,
        USize,
        F32, F64,
        String
    }

    public class SimpleType : IType
    {
        public string Name;
        public int Array;

        public bool IsPrimitive()
        {
            if (Name.ToLower() == "string")
                return false;

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

        public override string ToString()
        {
            return $"{Name}{string.Concat(Enumerable.Repeat("[]", Array))}";
        }
    }

    public class TupleType : IType
    {
        public IList<IType> Types;
        public int Array;

        public override string ToString()
        {
            return $"({string.Join(',', Types)}) {string.Concat(Enumerable.Repeat("[]", Array))}";
        }
    }

    public class MapType : IType
    {
        public IType KeyType;
        public IType ValueType;
        public int Array;

        public override string ToString()
        {
            return $"[{KeyType} -> {ValueType}] {string.Concat(Enumerable.Repeat("[]", Array))}";
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
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static PrimitiveType ToType(string type)
        {
            switch (type.ToLower())
            {
                case "i8": return PrimitiveType.I8;
                case "i16": return PrimitiveType.I16;
                case "i32": return PrimitiveType.I32;
                case "i64": return PrimitiveType.I64;
                case "u8": return PrimitiveType.U8;
                case "u16": return PrimitiveType.U16;
                case "u32": return PrimitiveType.U32;
                case "u64": return PrimitiveType.U64;
                case "usize": return PrimitiveType.USize;
                case "f32": return PrimitiveType.F32;
                case "f64": return PrimitiveType.F64;
                case "string": return PrimitiveType.String;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static readonly IList<PrimitiveType> Integer = new List<PrimitiveType>
        {
            PrimitiveType.I8, PrimitiveType.I16, PrimitiveType.I32, PrimitiveType.I64,
            PrimitiveType.U8, PrimitiveType.U16, PrimitiveType.U32, PrimitiveType.U64
        };

        public static readonly IList<PrimitiveType> Signed = new List<PrimitiveType>
        {
            PrimitiveType.I8, PrimitiveType.I16, PrimitiveType.I32, PrimitiveType.I64
        };

        public static readonly IList<PrimitiveType> Unsigned = new List<PrimitiveType>
        {
            PrimitiveType.U8, PrimitiveType.U16, PrimitiveType.U32, PrimitiveType.U64, PrimitiveType.USize
        };

        public static readonly IList<PrimitiveType> Real = new List<PrimitiveType>
        {
            PrimitiveType.F32, PrimitiveType.F64
        };
    }
}