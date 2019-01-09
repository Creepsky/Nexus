using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TupleLiteral : Expression
    {
        public IList<IExpression> Values { get; set; }

        private Context _context;
        private SimpleType _resultType;
        private New _newFunction;

        public override string ToString() => $"tupleLiteral({string.Join(", ", Values)})>";

        public override SimpleType GetResultType(Context context) =>
            _resultType ?? (_resultType = new SimpleType($"tuple{Values.Count}", new TemplateList(Values.Select(i => i.GetResultType(context)).ToList()), 0)
            {
                Line = Line,
                Column = Column,
                FilePath = FilePath
            });

        public override void ForwardDeclare(Context upperContext)
        {
            _context = upperContext;

            _newFunction = new New
            {
                Column = Column,
                Line = Line,
                FilePath = FilePath
            };

            _newFunction.ForwardDeclare(_context);
        }

        public override void Declare()
        {
            var templateTypes = new TemplateList(Values.Select(i => i.GetResultType(_context)).ToList());

            _newFunction.Type = new SimpleType($"tuple{Values.Count}", templateTypes, 0);

            for (var i = 0; i < Values.Count; ++i)
            {
                _newFunction.Parameter.Add($"_{i}", Values[i]);
            }

            _newFunction.Declare();
        }

        public override void Define()
        {
            _newFunction.Define();
        }

        public override void Check(Context context)
        {
            _newFunction.Check(context);
        }

        public override bool Print(PrintType type, Printer printer)
        {
            return _newFunction.Print(type, printer);
        }

        public override object Clone()
        {
            return new TupleLiteral
            {
                Values = Values.Select(i => (IExpression) i.CloneDeep()).ToList()
            };
        }
    }
}