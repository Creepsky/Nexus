using System.Collections.Generic;
using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ExtensionFunction : Statement
    {
        public IType ReturnType { get; set; }
        public IType ExtensionBase { get; set; }
        public ITemplateList ExtensionBaseTemplateList { get; set; }
        public ITemplateList TemplateList { get; set; }
        public IList<Variable> Parameter { get; set; }
        public IList<IStatement> Body { get; set; }
        public IList<ExtensionFunction> Overloadings { get; } = new List<ExtensionFunction>();
        private Context _context;

        public void AddOverload(ExtensionFunction overload)
        {
            Overloadings.Add(overload);
        }

        public override string ToString()
        {
            return $"{ReturnType} {ExtensionBase}.{Name}()";
        }

        public override void Check(Context context)
        {
            var extensionBase =  context.Get(ExtensionBase.Name);

            if (extensionBase.GetType() != typeof(Class))
            {
                throw new TypeMismatchException(this, typeof(Class).Name, extensionBase.GetType().Name);
            }

            ExtensionBase.Check(_context);
            TemplateList?.Check(context);

            foreach (var i in Parameter)
            {
                i.Check(_context);
            }

            foreach (var i in Body)
            {
                i.Check(_context);
            }
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            if (phase == GenerationPhase.ForwardDeclaration)
            {
                context.AddGlobal($"{ExtensionBase.Name}.{Name}", this);
                _context = context.StackNewContext(this);
            }

            ReturnType.Generate(_context, phase);

            foreach (var i in Parameter)
            {
                i.Generate(_context, phase);
            }

            foreach (var i in Body)
            {
                i.Generate(_context, phase);
            }

            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void, 0, Line, Column);

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                TemplateList?.Print(type, printer);
                ReturnType.Print(type, printer);
                printer.Write($" {Name}(");
                
                var thisVariable = new Variable
                {
                    Line = Line,
                    Column = Column,
                    Name = "__this",
                    Type = ExtensionBase
                };

                thisVariable.Print(PrintType.ParameterRef, printer);

                if (Parameter.Any())
                {
                    printer.Write(", ");
                }

                foreach (var i in Parameter)
                {
                    i.Print(PrintType.ParameterConstRef, printer);
                    if (i != Parameter.Last())
                    {
                        printer.Write(", ");
                    }
                }

                printer.WriteLine(")");
                printer.WriteLine("{");
                printer.Push();
                foreach (var i in Body.Where(i => i.GetType() != typeof(Include)))
                {
                    i.Print(type, printer);
                }
                printer.Pop();
                printer.WriteLine("}");
            }
        }
    }
}