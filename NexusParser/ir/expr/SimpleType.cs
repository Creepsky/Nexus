using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public enum PrimitiveType
    {
        I8, I16, I32, I64,
        U8, U16, U32, U64,
        ISize, USize,
        F32, F64,
        String,
        Void,
        Bool,
        Char
    }

    public class SimpleType : Expression, IEquatable<SimpleType>
    {
        public bool Constant { get; set; }
        public bool Reference { get; set; }
        public bool IsVariadic { get; set; }
        public bool IsTemplate { get; set; }
        public TemplateList TemplateList { get; set; }

        public SimpleType(string name)
            : this(name, 0)
        {
        }

        public SimpleType(string name, int array)
            : this(name, null, array)
        {
        }

        public SimpleType(string name, TemplateList templateList, int array)
        {
            if (array > 0)
            {
                Name = "vector";
                TemplateList = CreateVector(array - 1, new SimpleType(name, templateList, 0)).TemplateList;
            }
            else
            {
                Name = name;
                TemplateList = templateList;

                if (TypesExtension.Aliases.ContainsKey(name))
                {
                    Name = TypesExtension.Aliases[Name];
                }
            }
        }

        private static SimpleType CreateVector(int depth, SimpleType type)
        {
            return new SimpleType("vector")
            {
                TemplateList = new TemplateList(new List<SimpleType>
                    {depth > 0 ? CreateVector(depth - 1, type) : type})
            };
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

            // exceptions
            if (IsTemplate ||
                Name == TypesExtension.CppType ||
                other.Name == TypesExtension.CppType)
            {
                return true;
            }

            if (TemplateList == null)
            {
                if (other.TemplateList != null)
                {
                    return false;
                }
            }
            else if (!TemplateList.Equals(other.TemplateList))
            {
                return false;
            }

            return Name == other.Name;
        }

        public override bool Equals(object other)
        {
            return Equals(other as SimpleType);
        }

        public override int GetHashCode()
        {
            var i = Name.GetHashCode() + Constant.GetHashCode() + Reference.GetHashCode();

            if (TemplateList != null)
            {
                i += TemplateList.GetHashCode();
            }

            return i;
        }

        public override string ToString()
        {
            var name = Name;

            if (TemplateList != null)
            {
                name += TemplateList.ToString();
            }

            if (!Constant && Reference)
            {
                name += "&";
            }

            return name;
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write(GetQualifiedName(type != PrintType.WithoutModifiers));
            return true;
        }

        private string GetQualifiedName(bool withModifiers)
        {
            var name = Name;

            if (Constant && withModifiers)
            {
                name = "const " + name;
            }

            if (TemplateList != null)
            {
                name += "Of";

                foreach (var i in TemplateList.Types)
                {
                    name += i.GetQualifiedName(false);
                    
                    if (i != TemplateList.Types.Last())
                    {
                        name += "And";
                    }
                }
            }

            if (Reference && withModifiers)
            {
                name += "&";
            }

            return name;
        }

        public override object Clone()
        {
            var i = new SimpleType(Name, 0)
            {
                Constant = Constant,
                Reference = Reference,
                IsTemplate = IsTemplate,
                IsVariadic = IsVariadic
            };

            if (TemplateList != null)
            {
                i.TemplateList = (TemplateList) TemplateList.CloneDeep();
            }
            return i;
        }

        public override SimpleType GetResultType(Context context)
        {
            return this;
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            if (concreteElement == null)
            {
                var tmp1 = context.LookupTemplateType(this);
                Name = tmp1.Name;
                TemplateList = tmp1.TemplateList;
                return;
            }

            // get the resulting type of the concrete element
            var concreteSimpleType = concreteElement.GetResultType(context.CallerContext);

            if (concreteSimpleType == null)
            {
                throw new TemplateGenerationException(this, $"Could not get the result type of {concreteElement}");
            }

            // check the template list of both types
            if (TemplateList == null)
            {
                if (concreteSimpleType.TemplateList != null)
                {
                    throw new TemplateGenerationException(this, $"Expected an empty template list ({this}), but got {concreteSimpleType}");
                }
            }
            // if the concrete type has a template list, we have to check if it fits to the template type template list
            else if (!TemplateList.Equals(concreteSimpleType.TemplateList))
            {
                throw new TemplateGenerationException(this, $"Expected the template list to be {TemplateList} but got {concreteSimpleType.TemplateList}");
            }

            TemplateList?.Template(context, concreteSimpleType.TemplateList);

            var tmp2 = context.RegisterTemplateType(this, concreteSimpleType);
            Name = tmp2.Name;
            TemplateList = tmp2.TemplateList;
        }

        public override void Check(Context context)
        {
            // if not, it could also be a class
            if (context.Get(Name) is Class c)
            {
                c = c.GetVariant(this, context);

                if (c != null)
                {
                    return;
                }
            }

            throw new ClassVariantNotFoundException(this, Name, TemplateList?.Types.Select(i => i.ToString()).ToArray());
        }

        //public override IGenerationElement Generate(Context context, GenerationPhase phase)
        //{
        //    if (phase == GenerationPhase.TemplatingExtensionBase)
        //    {
        //        var templateContext = (TemplateContext) context;
        //        templateContext.DeriveTemplates(this, templateContext.ExtensionBaseType);
        //    }

        //    if (phase == GenerationPhase.Templating)
        //    {
        //        if (TemplateList != null)
        //        {
        //            TemplateList.Generate(context, GenerationPhase.Templating);
        //        }
        //        else
        //        {
        //            var templateContext = (TemplateContext) context;
        //            var concreteType = templateContext.TemplateTypeToConcrete(Name);

        //            if (concreteType != null)
        //            {
        //                Name = concreteType.Name;
        //                Array += concreteType.Array;
        //            }
        //        }
        //    }

        //    return this;
        //}
    }

    public class CppType : SimpleType
    {
        private readonly CppBlock _cppBlock;

        public CppType(CppBlock cppBlock)
            : base(cppBlock.Block)
        {
            _cppBlock = cppBlock;
        }

        public override void Check(Context context)
        {
        }

        public override string ToString()
        {
            return _cppBlock.ToString();
        }

        public override bool Print(PrintType type, Printer printer)
        {
            return _cppBlock.Print(type, printer);
        }

        public override object Clone()
        {
            return new CppType((CppBlock) _cppBlock.CloneDeep());
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            if (concreteElement == null)
            {
                _cppBlock.Template(context, null);
            }
        }
    }

    public class SimpleVariadicType : SimpleType
    {
        private readonly IList<SimpleType> _types = new List<SimpleType>();

        public SimpleVariadicType(string name)
            : base(name)
        {
            IsVariadic = true;
            IsTemplate = true;
        }

        public void Add(SimpleType type)
        {
            _types.Add(type);
        }

        public int Count()
        {
            return _types.Count;
        }

        public IList<SimpleType> Expand()
        {
            return _types;
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
                case "isize": return PrimitiveType.ISize;
                case "usize": return PrimitiveType.USize;
                case "f32": return PrimitiveType.F32;
                case "f64": return PrimitiveType.F64;
                case "string": return PrimitiveType.String;
                case "void": return PrimitiveType.Void;
                case "char":
                case "character": return PrimitiveType.Char;
                case "bool":
                case "boolean": return PrimitiveType.Bool;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static string ToString(this PrimitiveType type)
        {
            switch (type)
            {
                case PrimitiveType.I8:
                    return I8;
                case PrimitiveType.I16:
                    return I16;
                case PrimitiveType.I32:
                    return I32;
                case PrimitiveType.I64:
                    return I64;
                case PrimitiveType.U8:
                    return U8;
                case PrimitiveType.U16:
                    return U16;
                case PrimitiveType.U32:
                    return U32;
                case PrimitiveType.U64:
                    return U64;
                case PrimitiveType.ISize:
                    return ISize;
                case PrimitiveType.USize:
                    return USize;
                case PrimitiveType.F32:
                    return F32;
                case PrimitiveType.F64:
                    return F64;
                case PrimitiveType.Char:
                    return Char;
                case PrimitiveType.String:
                    return String;
                case PrimitiveType.Void:
                    return Void;
                case PrimitiveType.Bool:
                    return Bool;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static readonly IReadOnlyCollection<PrimitiveType> Integer = new List<PrimitiveType>
        {
            PrimitiveType.I8, PrimitiveType.I16, PrimitiveType.I32, PrimitiveType.I64,
            PrimitiveType.U8, PrimitiveType.U16, PrimitiveType.U32, PrimitiveType.U64
        };

        public static readonly IReadOnlyCollection<PrimitiveType> Signed = new List<PrimitiveType>
        {
            PrimitiveType.I8, PrimitiveType.I16, PrimitiveType.I32, PrimitiveType.I64, PrimitiveType.ISize
        };

        public static readonly IReadOnlyCollection<PrimitiveType> Unsigned = new List<PrimitiveType>
        {
            PrimitiveType.U8, PrimitiveType.U16, PrimitiveType.U32, PrimitiveType.U64, PrimitiveType.USize
        };

        public static readonly IReadOnlyCollection<PrimitiveType> Real = new List<PrimitiveType>
        {
            PrimitiveType.F32, PrimitiveType.F64
        };

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
        public static readonly string ISize = "isize";

        public static readonly string F32 = "f32";
        public static readonly string F64 = "f64";

        public static readonly string Float = "float";
        public static readonly string Double = "double";

        public static readonly string Bool = "bool";
        public static readonly string Boolean = "boolean";

        public static readonly string Char = "char";
        public static readonly string Character = "character";
        public static readonly string String = "string";

        public static readonly string Unit = "unit";
        public static readonly string Void = "void";

        public static readonly string CppType = "cppType";

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
            String,
            Void,
            ISize,
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
            {Bool, Boolean},
            {Float, F32},
            {Double, F64},
            {Char, Character},
            {Void, Unit}
        };
    }
}