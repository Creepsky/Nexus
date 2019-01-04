﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.common;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TemplateList : Expression, IEquatable<TemplateList>
    {
        public IList<SimpleType> Types { get; }

        //public bool IsVariadic { get; private set; }

        public TemplateList(IList<SimpleType> types)
        {
            Types = types;
        }

        //public override IGenerationElement Generate(Context context, GenerationPhase phase)
        //{
        //    if (phase == GenerationPhase.Templating)
        //    {
        //        foreach (var i in Types)
        //        {
        //            i.Generate(context, phase);
        //        }
        //    }

        //    return this;
        //}

        public override SimpleType GetResultType(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            var templateList = concreteElement as TemplateList;

            if (templateList == null)
            {
                throw new TemplateGenerationException(concreteElement,
                    $"TemplateList got {concreteElement.GetType().Name} as concrete element, expected TemplateList");
            }

            if (templateList.Types.Count != Types.Count)
            {
                throw new TemplateGenerationException(concreteElement,
                    $"Concrete TemplateList has {Types.Count} template parameter, expected {templateList.Types.Count}");
            }

            //if (IsVariadic)
            //{
            //    // append as much variadic types to the end of the list as needed
            //    var toAdd = templateList.Types.Count - Types.Count;

            //    for (var i = 0; i < toAdd; ++i)
            //    {
            //        Types.Add(SimpleType.CreateVariadicType());
            //    }
            //}

            for (var i = 0; i < templateList.Types.Count; ++i)
            {
                var j = i;

                //if (IsVariadic && i >= Types.Count)
                //{
                //    j = Types.Count - 1;
                //}

                Types[i] = context.RegisterTemplateType(Types[j], templateList.Types[i]);
            }
        }

        public override void Check(Context context)
        {
            if (Types.Distinct().Count() != Types.Count)
            {
                var duplicateItems =
                    from x in Types
                    group x by x into grouped
                    where grouped.Count() > 1
                    select grouped.Key;
                
                throw new PositionedException(this, $"Duplicated template variable names: {string.Join(", ", duplicateItems)}");
            }

            if (!Types.Any())
            {
                throw new PositionedException(this, "Empty template list");
            }
        }

        public override bool Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.Write("template <");

                foreach (var i in Types)
                {
                    printer.Write("typename ");
                    i.Print(type, printer);
                    if (!i.Equals(Types.Last()))
                    {
                        printer.Write(", ");
                    }
                }

                printer.WriteLine(">");
                return true;
            }

            if (type == PrintType.ClassDefinition)
            {
                printer.Write("<");
                foreach (var i in Types)
                {
                    i.Print(type, printer);
                    if (!i.Equals(Types.Last()))
                    {
                        printer.Write(", ");
                    }
                }
                printer.Write(">");
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TemplateList);
        }

        public bool Equals(TemplateList other)
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

            return true;
        }

        public override int GetHashCode()
        {
            return (Types != null ? Types.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return '<' + string.Join(", ", Types) + '>';
        }

        public override object Clone()
        {
            IList<SimpleType> types = new List<SimpleType>(Types.Count);

            foreach (var i in Types)
            {
                types.Add((SimpleType) i.Clone());
            }

            return new TemplateList(types);
        }
    }
}