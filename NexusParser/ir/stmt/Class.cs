using System.Collections.Generic;
using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Class : Statement, IGenerationElementGenerator
    {
        public TemplateList TemplateList { get; private set; }
        public IList<Variable> Variables { get; }
        public IList<CppBlockStatement> CppBlocks { get; }
        public IList<Include> Includes { get; }
        public readonly IList<Class> Variants = new List<Class>();

        public SimpleType Type { get; }
        private Context _context;

        public Class(string name, TemplateList templateList, IList<Variable> variables, IList<CppBlockStatement> cppBlocks, IList<Include> includes)
        {
            Name = name;
            Variables = variables;
            CppBlocks = cppBlocks;
            Type = new SimpleType(Name, TemplateList, 0)
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column
            };
            TemplateList = templateList;
            Includes = includes;
        }

        public override string ToString()
        {
            return $"class {Type}";
        }

        public override SimpleType GetResultType(Context context) =>
            new SimpleType(Name)
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column
            };

        public override void ForwardDeclare(Context upperContext)
        {
            upperContext.AddGlobal(Name, this);
            _context = upperContext.StackNewContext(this);

            foreach (var i in GetElements())
            {
                i.ForwardDeclare(_context);
            }
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

            _context.Remove(Name, this);
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            if (!(concreteElement is SimpleType type))
            {
                throw new TemplateGenerationException(this, $"Can not create a class from a {concreteElement.GetType().Name}");
            }

            _context = context;

            Type.Template(context, type);

            foreach (var i in GetElements())
            {
                i.Template(context, null);
            }
        }

        public override bool Print(PrintType type, Printer printer)
        {
            if (TemplateList != null)
            {
                return false;
            }

            if (type == PrintType.ForwardDeclaration)
            {
                printer.Write("struct ");
                Type.Print(type, printer);
                printer.WriteLine(";");
                return true;
            }

            if (type == PrintType.Declaration)
            {
                printer.WriteLine($"// {FilePath} @ {Line}");
                type = PrintType.ClassDefinition;
                printer.Write("struct ");
                Type.Print(type, printer);
                printer.WriteLine();
                printer.WriteLine("{");
                printer.Push();
                foreach (var i in Variables)
                {
                    i.Print(type, printer);
                    printer.WriteLine(";");
                }
                foreach (var i in CppBlocks)
                {
                    i.Print(type, printer);
                }
                printer.Pop();
                printer.WriteLine("};");
                return true;
            }

            return false;
        }

        private IEnumerable<IGenerationElement> GetElements()
        {
            return new[]
            {
                Includes.OfType<IGenerationElement>(),
                Variables,
                CppBlocks
            }.SelectMany(i => i);
        }

        public override object Clone()
        {
            return new Class(new string(Name),
                (TemplateList) TemplateList?.CloneDeep(),
                Variables.Select(i => (Variable) i.CloneDeep()).ToList(),
                CppBlocks.Select(i => (CppBlockStatement) i.CloneDeep()).ToList(),
                Includes.Select(i => (Include) i.CloneDeep()).ToList());
        }

        public override void Check(Context context)
        {
            foreach (var i in Variables)
            {
                i.Check(context);
            }
        }

        public void AddVariant(Class c)
        {
            Variants.Add(c);
        }

        public Class GetVariant(SimpleType type, Context context)
        {
            var variant = Variants.Prepend(this)
                .Where(i => i.TemplateList == null)
                .Select(i => i.Matches(type, _context))
                .FirstOrDefault(match => match != null);

            if (variant != null)
            {
                return variant;
            }

            return Variants.Prepend(this)
                .Where(i => i.TemplateList != null)
                .Select(i => i.Generate(type, context))
                .FirstOrDefault(match => match != null);
        }

        protected virtual Class Matches(SimpleType type, Context context)
        {
            return Type.Equals(type) ? this : null;
        }

        public IGenerationElement Generate()
        {
            if (TemplateList == null)
            {
                throw new TemplateGenerationException(this, "Can not generate concrete class from non template class");
            }

            var c = (Class) CloneDeep();

            c.Type.TemplateList = TemplateList;
            c.TemplateList = null;

            return c;
        }

        public Class Generate(SimpleType type, Context context)
        {
            if (TemplateList == null)
            {
                throw new TemplateGenerationException(this, "Can not generate class from non template class");
            }

            var templateContext = new TemplateContext(_context, context, this);

            return templateContext.Generate(type) ? templateContext.Element as Class : null;
        }
    }
}