using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class New : Expression
    {
        public SimpleType Type { get; set; }
        public IDictionary<string, IExpression> Parameter { get; private set; } = new Dictionary<string, IExpression>();
        public IExpression SingleParameter { get; set; }

        private Context _context;
        private Class _class;
        private FunctionCall _functionCall;
        private readonly IList<IExpression> _sortedParameter = new List<IExpression>();

        public override SimpleType GetResultType(Context context)
        {
            return Type.GetResultType(context);
        }

        public override void ForwardDeclare(Context upperContext)
        {
            _context = upperContext;
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            _context = context;

            Type.Template(context, concreteElement);

            SingleParameter?.Template(context, concreteElement);

            foreach (var i in Parameter)
            {
                i.Value.Template(context, concreteElement);
            }
        }

        public override void Declare()
        {
            // get the under laying class for the new keyword
            _class = _context.Get<Class>(Type.GetResultType(_context).Name, this);

            if (SingleParameter != null)
            {
                return;
            }

            _sortedParameter.Clear();

            // first, reserve a place for every parameter in the array
            for (var i = 0; i < Parameter.Count; ++i)
            {
                _sortedParameter.Add(null);
            }
            
            foreach (var (parameterName, parameterValue) in Parameter)
            {
                // find the position of the parameter in the class
                var parameterIndex = _class.Variables
                    .Select((value, index) => new {value, index})
                    .Where(p => p.value.Name == parameterName)
                    .Select((value, index) => index);

                for (var j = 0; j < _class.Variables.Count; ++j)
                {
                    if (_class.Variables[j].Name == parameterName)
                    {
                        // save the parameter in the sorted array
                        _sortedParameter[j] = parameterValue;
                    }
                }
            }
        }

        public override void Define()
        {
            _functionCall = new FunctionCall
            {
                Column = Column,
                Line = Line,
                FilePath = FilePath,
                Name = Type.Name,
                Object = Type,
                Parameter = new List<IExpression>(),
                Static = true
            };

            var createFunction = _context.Get(Type.Name) as Function;

            if (createFunction == null)
            {
                createFunction = new Function
                {
                    Column = Column,
                    Line = Line,
                    FilePath = FilePath,
                    ExtensionBase = Type,
                    ReturnType = Type,
                    Name = "create",
                    Parameter = _class.Variables,
                    Body = new List<IStatement>(),
                    Static = true
                };
            }
        }

        public override void Check(Context context)
        {
            Type.Check(context);

            SingleParameter?.Check(context);

            var type = context.Get<Class>(Type.GetResultType(context).Name, this);

            if (SingleParameter == null)
            {
                foreach (var i in Parameter)
                {
                    i.Value.Check(context);

                    // also check if the member is available
                    if (type.Variables.All(v => v.Name != i.Key))
                    {
                        throw new PositionedException(this, $"Class {Type} has no member {i.Key}");
                    }
                }
            }
            else if (type.Variables.Count != 1)
            {
                throw new PositionedException(this, $"Could not create an instance of the class {Type} " +
                                                    $"with the single constructor parameter {SingleParameter.GetResultType(context)}, " +
                                                    $"because the class has {type.Variables.Count} member variables");
            }
        }

        public override bool Print(PrintType type, Printer printer)
        {
            Type.Print(type, printer);

            printer.Write("{");

            if (SingleParameter != null)
            {
                SingleParameter.Print(type, printer);
            }
            else
            {
                foreach (var i in Parameter)
                {
                    if (i.Value.Print(type, printer) && !Equals(i, Parameter.Last()))
                    {
                        printer.Write(", ");
                    }
                }
            }

            printer.Write("}");

            return true;
        }

        public override object Clone()
        {
            return new New
            {
                Type = (SimpleType)Type.Clone(),
                SingleParameter = (IExpression)SingleParameter?.Clone(),
                Parameter = Parameter?.Select(i => (i.Key, (IExpression)i.Value.Clone())).ToDictionary(x => x.Key, x => x.Item2)
            };
        }

        public override string ToString()
        {
            return $"new {Type}({SingleParameter.ToString() ?? string.Join(", ", Parameter.Select(i => $"{i.Key} = {i.Value}"))})";
        }
    }
}