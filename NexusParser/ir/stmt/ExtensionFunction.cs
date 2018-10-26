using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ExtensionFunction : Statement
    {
        public IType ReturnType { get; set; }
        public IType ExtensionBase { get; set; }
        public ITemplateList TemplateList { get; set; }
        public IList<Variable> Parameter { get; set; }
        public IList<IStatement> Body { get; set; }
        private Context _context;

        public virtual void SetName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{ReturnType} {ExtensionBase}.{Name}()";
        }

        public override void Check(Context context)
        {
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
                printer.Write($" {ExtensionBase.Name}_{Name}(");
                
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
                    i.Print(PrintType.Parameter, printer);
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