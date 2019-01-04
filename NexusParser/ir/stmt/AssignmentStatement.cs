using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class AssignmentStatement : Statement
    {
        public IExpression Left { private get; set; }
        public IExpression Right { private get; set; }

        public override void Check(Context context)
        {
            Left.Check(context);
            Right.Check(context);
        }

        public override SimpleType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void)
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column
            };

        public override void ForwardDeclare(Context upperContext)
        {
            Left.ForwardDeclare(upperContext);
            Right.ForwardDeclare(upperContext);
        }

        public override void Declare()
        {
            Left.Declare();
            Right.Declare();
        }

        public override void Define()
        {
            Left.Define();
            Right.Define();
        }

        public override void Remove()
        {
            Left.Remove();
            Right.Remove();
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            Left.Template(context, concreteElement);
            Right.Template(context, concreteElement);
        }

        public override bool Print(PrintType type, Printer printer)
        {
            Left.Print(type, printer);
            printer.Write(" = ");
            Right.Print(type, printer);
            printer.WriteLine(";");
            return true;
        }

        public override object Clone()
        {
            return new AssignmentStatement
            {
                Left = (IExpression) Left.CloneDeep(),
                Right = (IExpression) Right.CloneDeep()
            };
        }
    }
}