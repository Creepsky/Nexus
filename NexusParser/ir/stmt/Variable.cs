using System;
using System.Collections.Generic;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Variable : Statement
    {
        public IType Type { get; set; }
        public IExpression Initialization { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}";
        }

        public void PrintSourceInitialization(Printer printer)
        {
           // printer.Write($"{_variable.Name}{{{_variable.Initialization?.ToString() ?? string.Empty}}}");
        }

        public override void Check(Context upperContext)
        {
            if (Type.IsAuto())
            {
                //throw new Exception();
            }

            if (Initialization != null)
            {
                //throw new Exception();
            }
        }

        public override IGenerationElement Generate(Context upperContext, GenerationPhase phase)
        {
            if (upperContext.Element == null)
            {
                throw new NoScopeException(this);
            }

            switch (phase)
            {
                case GenerationPhase.Declaration when upperContext.Element.GetType() == typeof(Class):
                    return GenerateClassVariable((Class)upperContext.Element, upperContext);
                case GenerationPhase.Declaration when upperContext.Element.GetType() == typeof(Function):
                case GenerationPhase.Declaration when upperContext.Element.GetType() == typeof(ExtensionFunction):
                    return GenerateParameter(upperContext);
                case GenerationPhase.Declaration:
                    throw new UnexpectedScopeException(this, upperContext.Element.GetType().Name,
                        new[] {nameof(Class), nameof(Function)});
                default:
                    break;
            }

            return this;
        }

        public override IType GetResultType(Context context) => Type;

        private IGenerationElement GenerateClassVariable(Class c, Context context)
        {
            return this;
        }

        private IGenerationElement GenerateParameter(Context context)
        {
            context.Add(Name, this);
            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                Type.Print(type, printer);
                printer.WriteLine($" {Name};");
            }
            else if (type == PrintType.Source)
            {
                printer.Write($"{Name}");
                printer.Write("{");
                Initialization?.Print(type, printer);
                printer.Write("}");
            }
            else if (type == PrintType.Parameter ||
                     type == PrintType.ParameterRef ||
                     type == PrintType.ParameterConstRef)
            {
                Type.Print(type, printer);
                printer.Write(' ' + Name);
            }
            else if (type == PrintType.FunctionSource ||
                     type == PrintType.ForSource)
            {
                Type.Print(type, printer);
                printer.Write(" ");
                printer.Write(Name);
                if (Initialization != null)
                {
                    printer.Write(" = ");
                    Initialization.Print(type, printer);
                }

                if (type == PrintType.ForSource)
                {
                    printer.Write(";");
                }
                else
                {
                    printer.WriteLine(";");
                }
            }
        }
    }
}