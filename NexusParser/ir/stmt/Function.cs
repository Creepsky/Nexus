using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Function : Statement, IGenerationElementGenerator
    {
        public SimpleType ReturnType { get; set; }
        public SimpleType ExtensionBase { get; set; }
        public IList<Variable> Parameter { get; set; }
        public IList<IStatement> Body { get; set; }
        public TemplateList TemplateList { get; set; }

        public bool Static { get; set; }
        public bool Operator { get; set; } = false;
        public Function GeneratedBy { get; set; } = null;

        public IList<Function> Overloads { get; } = new List<Function>();

        protected Context Context;

        public void AddOverload(Function overload)
        {
            // TODO: check for duplicates
            Overloads.Add(overload);
        }

        public Function GetOverload(FunctionCall functionCall, Context context)
        {
            var extensionBase = functionCall.Object?.GetResultType(context);

            var overload = Overloads.Prepend(this)
                .Where(i => i.TemplateList == null)
                .Where(i => i.ExtensionBase?.Equals(extensionBase) ?? true)
                .Select(i => i.Matches(functionCall, context))
                .FirstOrDefault(match => match != null);

            if (overload != null)
            {
                return overload;
            }

            return Overloads.Prepend(this)
                .Where(i => i.TemplateList != null)
                .Select(i => i.Generate(functionCall, context))
                .FirstOrDefault(match => match != null);
        }

        protected virtual Function Matches(FunctionCall functionCall, Context context)
        {
            // it is an extension function call
            if (functionCall.Object != null &&
                !functionCall.Object.GetResultType(context).Equals(ExtensionBase))
            {
                return null;
            }

            if (Parameter.Count != functionCall.Parameter.Count)
            {
                return null;
            }

            for (var i = 0; i < functionCall.Parameter.Count; ++i)
            {
                if (!functionCall.Parameter[i].GetResultType(context).Equals(Parameter[i].GetResultType(context)))
                {
                    return null;
                }
            }

            return this;
        }

        public override string ToString()
        {
            var name = ReturnType.ToString() + ' ';

            if (ExtensionBase != null)
            {
                name += ExtensionBase.ToString() + '.';
            }

            name += Name;

            if (TemplateList != null)
            {
                name += TemplateList;
            }

            name += $"({string.Join(',', Parameter.Select(i => i.Type))})";

            return name;
        }

        public override void Check(Context upperContext)
        {
            if (TemplateList != null)
            {
                return;
            }

            foreach (var i in GetElements())
            {
                i.Check(Context);
            }
        }

        public override SimpleType GetResultType(Context context) => ReturnType;

        public override void ForwardDeclare(Context upperContext)
        {
            upperContext.AddGlobal(Name, this);
            Context = upperContext.StackNewContext(this);

            if (ExtensionBase != null)
            {
                AddThisPointer();
            }

            foreach (var i in GetElements())
            {
                i.ForwardDeclare(Context);
            }
        }

        private void AddThisPointer()
        {
            Debug.Assert(ExtensionBase != null, "Can not add a this pointer without extension base");

            Context.Add("this", new Variable
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column,
                Type = ExtensionBase,
                Name = "__this"
            });
        }

        public override void Declare()
        {
            foreach (var i in GetElements())
            {
                i.Declare();
            }
        }

        public override void Define()
        {
            foreach (var i in GetElements())
            {
                i.Define();
            }
        }

        public override void Remove()
        {
            foreach (var i in GetElements())
            {
                i.Remove();
            }
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            if (!(concreteElement is FunctionCall functionCall))
            {
                throw new TemplateGenerationException(this, $"Can not create a function from a {concreteElement.GetType().Name}");
            }

            Context = context;

            if (ExtensionBase != null)
            {
                AddThisPointer();
            }

            if (functionCall.Object != null)
            {
                var functionCallExtensionBase = functionCall.Object.GetResultType(context.CallerContext);

                if (ExtensionBase == null)
                {
                    throw new TemplateGenerationException(this, "Generated function has no extension base, " +
                                                                $"but the function call has the extension base {functionCallExtensionBase}");
                }

                ExtensionBase.Template(context, functionCallExtensionBase);

                // TODO: was wollte ich hier tun?
                //var objectType = functionCall.Object.GetResultType(context.CallerContext);

                //if (objectType.IsTemplate)
                //{
                //    ExtensionBase.Template(context, objectType);
                //}
                //else if (!objectType.Equals(ExtensionBase))
                //{
                //    throw new TemplateGenerationException(this, $"Function call is called for class {objectType}," +
                //                                                $" function is written for class {ExtensionBase}");
                //}
            }
            else if (ExtensionBase != null)
            {
                throw new TemplateGenerationException(this, $"Generated function has the extension base {ExtensionBase}," +
                                                             " but the function call has no extension base");
            }

            if (functionCall.Parameter.Count != Parameter.Count)
            {
                throw new TemplateGenerationException(this, $"Expected {functionCall.Parameter.Count} parameters, got {Parameter.Count}");
            }

            for (var i = 0; i < functionCall.Parameter.Count; ++i)
            {
                Parameter[i].Template(context, functionCall.Parameter[i].GetResultType(context.CallerContext));
            }

            ReturnType = context.LookupTemplateType(ReturnType);

            foreach (var i in Body)
            {
                i.Template(context, null);
            }
        }

        public Function Generate(FunctionCall functionCall, Context functionCallContext)
        {
            if (TemplateList == null)
            {
                throw new TemplateGenerationException(this, "Can not generate function from non template function");
            }

            var context = new TemplateContext(Context, functionCallContext, this);

            var function = context.Generate(functionCall) ? context.Element as Function : null;

            // the function has been generated and is inside the context, but not the file
            if (function != null)
            {
                // now we need to add it to the file, too
                File file = null;
                var currentContext = Context;

                while (file == null && currentContext.UpperContext != null)
                {
                    currentContext = currentContext.UpperContext;
                    file = currentContext.Element as File;
                }

                if (file == null)
                {
                    throw new TemplateGenerationException(this, "Could not find file context");
                }

                file.GeneratedFunctions.Add(function);
            }

            return function;
        }

        public override bool Print(PrintType type, Printer printer)
        {
            if (TemplateList != null)
            {
                return false;
            }

            if (type == PrintType.ForwardDeclaration)
            {
                PrintFunctionHeader(type, printer);
                printer.WriteLine(";");
                return true;
            }

            if (type == PrintType.Definition)
            {
                printer.Write($"// {FilePath} @ {Line}: {this}");
                if (GeneratedBy != null)
                {
                    printer.Write($" generated by {GeneratedBy} in {GeneratedBy.FilePath} @ {GeneratedBy.Line}");
                }
                printer.WriteLine();
                PrintFunctionHeader(type, printer);
                printer.WriteLine();
                printer.WriteLine("{");
                printer.Push();
                foreach (var i in Body.Where(i => i.GetType() != typeof(Include)))
                {
                    i.Print(type, printer);
                }
                printer.Pop();
                printer.WriteLine("}");
                return true;
            }

            return false;
        }

        private void PrintFunctionHeader(PrintType type, Printer printer)
        {
            ReturnType.Print(type, printer);
            printer.Write(" ");
            if (Static)
            {
                ExtensionBase.Print(type, printer);
                printer.Write("_");
            }
            printer.Write($"{Name}(");

            if (!Static && ExtensionBase != null)
            {
                var extensionBase = (SimpleType) ExtensionBase.CloneDeep();

                extensionBase.Constant = false;
                extensionBase.Reference = true;
                //extensionBase.Parameter = true;

                var thisVariable = new Variable
                {
                    Line = Line,
                    Column = Column,
                    Name = "__this",
                    Type = extensionBase
                };

                thisVariable.Print(type, printer);

                if (Parameter.Any())
                {
                    printer.Write(", ");
                }
            }

            foreach (var i in Parameter)
            {
                i.Print(type, printer);
                if (i != Parameter.Last())
                {
                    printer.Write(", ");
                }
            }

            printer.Write(")");
        }

        public IGenerationElement Generate()
        {
            if (TemplateList == null)
            {
                throw new TemplateGenerationException(this, "Can not generate concrete function from non template function");
            }

            // create a deep copy of the template function
            var function = (Function) Clone();

            // remove the template list, otherwise it will be again a template function
            function.TemplateList = null;

            return function;
        }

        public override object Clone()
        {
            return new Function
            {
                Column = Column,
                Line = Line,
                FilePath = FilePath,
                ReturnType = (SimpleType) ReturnType.CloneDeep(),
                ExtensionBase = ExtensionBase?.CloneDeep() as SimpleType,
                Parameter = Parameter.Select(i => (Variable) i.CloneDeep()).ToList(),
                Body = Body.Select(i => (IStatement) i.CloneDeep()).ToList(),
                TemplateList = TemplateList?.CloneDeep() as TemplateList,
                Name = Name,
                Static = Static
            };
        }

        protected virtual IEnumerable<IGenerationElement> GetElements()
        {
            return GenerationElementExtensions.GetAllElements(Parameter, Body)
                .Prepend(ExtensionBase)
                .Prepend(ReturnType)
                .Prepend(TemplateList)
                .Where(i => i != null);
        }

        public static string OperatorToName(string op)
        {
            switch (op)
            {
                case "[]":
                    return "at";
                case "+":
                    return "operator+";
                case "-":
                    return "operator-";
                case "*":
                    return "operator*";
                case "/":
                    return "operator/";
                case "+=":
                    return "operator+=";
                case "-=":
                    return "operator-=";
                case "/=":
                    return "operator/=";
                case "<":
                    return "operator<";
                case "<=":
                    return "operator<=";
                case ">":
                    return "operator>";
                case ">=":
                    return "operator>=";
                case "==":
                    return "operator==";
                case "!=":
                    return "operator!=";
                case "=":
                    return "assign";
                case "&&":
                    return "operator&&";
                default:
                    throw new KeyNotFoundException($"Unknown operator {op}");
            }
        }
    }
}