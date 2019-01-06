using System.Linq;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class CppBlock : Expression
    {
        public string Block { get; private set; }
        private SimpleType _returnType;
        
        public CppBlock(string block, int line, int column)
        {
            Block = block;
            Line = line;
            Column = column;
        }

        public override SimpleType GetResultType(Context context) =>
            _returnType ?? (_returnType =
                new SimpleType(TypesExtension.CppType)
                {
                    FilePath = FilePath,
                    Line = Line,
                    Column = Column
                });

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            foreach (var (key, value) in context.TemplateVariables)
            {
                Block = Block.Replace(key, value.GetResultType(context).ToString());
            }
        }

        public override void Check(Context context)
        {
            // it is a c++ block, we can not check it ...
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write(Block);
            return true;
        }

        public override object Clone()
        {
            return new CppBlock(new string(Block), Line, Column);
        }

        public override string ToString()
        {
            return $"c++ {{|{Block}|}}";
        }
    }

    public class CppBlockStatement : Statement
    {
        private readonly CppBlock _cppBlock;

        public CppBlockStatement(CppBlock cppBlock)
        {
            _cppBlock = cppBlock;
        }

        public override void Check(Context context)
        {
            _cppBlock.Check(context);
        }

        public override SimpleType GetResultType(Context context)
        {
            return _cppBlock.GetResultType(context);
        }

        public override void Declare()
        {
            _cppBlock.Declare();
        }

        public override void Define()
        {
            _cppBlock.Define();
        }

        public override void Remove()
        {
            _cppBlock.Remove();
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            _cppBlock.Template(context, concreteElement);
        }

        public override bool Print(PrintType type, Printer printer)
        {
            if (_cppBlock.Print(type, printer))
            {
                printer.WriteLine();
                return true;
            }
            return false;
        }

        public override object Clone()
        {
            return new CppBlockStatement((CppBlock) _cppBlock.CloneDeep());
        }
    }
}