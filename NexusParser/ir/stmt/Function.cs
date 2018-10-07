using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Function : Statement
    {
        public IType Type;
        public string Name;
        public IList<Variable> Parameter;
        public IList<IStatement> Statements;
        private Context _context;

        public override string ToString()
        {
            return $"{Type} {Name}({string.Join(',', Parameter.Select(i => i.Type))})";
        }

        public void ToHeader(Printer printer)
        {
            Type.Print(PrintType.Header, printer);

            printer.Write($" {Name}(");

            if (Parameter.Any())
            {
                var last = Parameter.Last();

                foreach (var i in Parameter)
                {
                    printer.Write(i.Type.IsPrimitive() ? $"{i.Type.ToCpp()}" : $"const {i.Type.ToCpp()}&");
                    printer.Write($" {i.Name}");

                    if (last != i)
                        printer.Write(", ");
                }
            }

            printer.WriteLine(");");
        }

        public void ToSource(Printer printer)
        {
            Type.Print(PrintType.Source, printer);
            //printer.Write($" {((Class)Context.Element).Name}::{_function.Name}");
            printer.WriteLine(string.Join(", ", Parameter.Select(i => $"{i.Type.ToCpp()} {i.Name}")));
            printer.WriteLine("{ }");
        }

        public override void Check(Context upperContext)
        {
            Type.Check(_context);

            foreach (var i in Parameter)
                i.Check(_context);

            foreach (var i in Statements)
                i.Check(_context);
        }

        public override IGenerationElement Generate(Context upperContext, GenerationPhase phase)
        {
            if (phase == GenerationPhase.ForwardDeclaration)
            {
                upperContext.Add(Name, this);
            }
            else if (phase == GenerationPhase.Definition)
            {
                _context = upperContext.StackNewContext(this);

                Type.Generate(_context, phase);

                foreach (var i in Parameter)
                    i.Generate(_context, phase);

                foreach (var i in Statements)
                    i.Generate(_context, phase);

                if (upperContext.Element.GetType() == typeof(Class))
                {
                    var c = (Class) upperContext.Element;
                    c.Public.Functions.Add(this);
                }
            }

            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}