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
        private Function _createFunction;
        private FunctionCall _createFunctionCall;
        private readonly IList<IExpression> _sortedParameter = new List<IExpression>();

        public override SimpleType GetResultType(Context context)
        {
            return Type.GetResultType(context);
        }

        public override void ForwardDeclare(Context upperContext)
        {
            _context = upperContext;

            foreach (var i in Parameter)
            {
                i.Value.ForwardDeclare(_context);
            }
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
            foreach (var i in Parameter)
            {
                i.Value.Declare();
            }

            // get the under laying class for the new keyword
            _class = _context.Get<Class>(Type.GetResultType(_context).Name, this);

            _class = _class.GetVariant(Type.GetResultType(_context), _context);

            _sortedParameter.Clear();

            _createFunctionCall = new FunctionCall
            {
                Column = Column,
                Line = Line,
                FilePath = FilePath,
                Name = "create",
                Object = Type,
                Parameter = Parameter.Values.ToList(),
                Static = true
            };

            _createFunction = (_context.Get("create") as Function)?.GetOverload(_createFunctionCall, _context);

            if (_createFunction != null)
            {
                // we found a create function, so we use it instead of building an automatic constructor
                return;
            }

            // first, reserve a place for every parameter in the array
            foreach (var v in _class.Variables)
            {
                if (v.GetResultType(_context) is CppType)
                {
                    throw new PositionedException(this, $"Can not automatically create a constructor for '{_class}', " +
                                                        $"because it uses the c++ variable '{v.Name}'");
                }

                _sortedParameter.Add(null);
            }
            
            foreach (var (parameterName, parameterValue) in Parameter)
            {
                for (var j = 0; j < _class.Variables.Count; ++j)
                {
                    if (_class.Variables[j].Name == parameterName)
                    {
                        // save the parameter in the sorted array
                        _sortedParameter[j] = parameterValue;
                    }
                }
            }

            for (var i = 0; i < _sortedParameter.Count; ++i)
            {
                if (_sortedParameter[i] == null)
                {
                    _sortedParameter[i] = new New
                    {
                        Column = Column,
                        Line = Line,
                        FilePath = FilePath,
                        Type = _class.Variables[i].Type,
                        Parameter = new Dictionary<string, IExpression>()
                    };

                    _sortedParameter[i].ForwardDeclare(_context);
                    _sortedParameter[i].Declare();
                }
            }

            _createFunctionCall = new FunctionCall
            {
                Column = Column,
                Line = Line,
                FilePath = FilePath,
                Name = "create",
                Object = Type,
                Parameter = _sortedParameter,
                Static = true
            };

            _createFunctionCall.Declare();
        }

        public override void Define()
        {
            if (SingleParameter != null)
            {
                return;
            }

            var createFunction = _context.Get("create") as Function;

            // no T::create function found, create one if possible
            if (createFunction != null)
            {
                _createFunction = createFunction.GetOverload(_createFunctionCall, _context);
            }

            // no valid overload for T::create found, let's create it
            if (_createFunction == null)
            {
                _createFunction = new Function
                {
                    Column = Column,
                    Line = Line,
                    FilePath = FilePath,
                    ExtensionBase = Type,
                    ReturnType = Type,
                    Name = "create",
                    Parameter = _class.Variables.Select(i => (Variable)i.CloneDeep()).ToList(),
                    Body = new List<IStatement>(),
                    Static = true
                };

                foreach (var i in _createFunction.Parameter)
                {
                    i.Type.Reference = true;
                }

                // create the new instance
                _createFunction.Body.Add(new ExpressionStatement
                {
                    Column = Column,
                    Line = Line,
                    FilePath = FilePath,
                    Expression = new Variable
                    {
                        Column = Column,
                        Line = Line,
                        FilePath = FilePath,
                        Name = "instance",
                        Type = Type
                    }
                });

                // assign the parameter
                foreach (var i in _class.Variables)
                {
                    _createFunction.Body.Add(new ExpressionStatement
                    {
                        Column = Column,
                        Line = Line,
                        FilePath = FilePath,
                        Expression =
                            new Assignment(
                                new MemberAccess(
                                        new VariableLiteral {Column = Column, Line = Line, FilePath = FilePath, Name = "instance"}, i.Name)
                                    {Column = Column, Line = Line, FilePath = FilePath},
                                new VariableLiteral {Column = Column, Line = Line, FilePath = FilePath, Name = i.Name},
                                AssignmentType.Move)
                            {
                                Column = Column,
                                Line = Line,
                                FilePath = FilePath,
                            }
                    });
                }

                // return it
                _createFunction.Body.Add(new ReturnStatement
                {
                    Column = Column,
                    Line = Line,
                    FilePath = FilePath,
                    Value = new VariableLiteral
                    {
                        Column = Column,
                        Line = Line,
                        FilePath = FilePath,
                        Name = "instance"
                    }
                });

                _createFunction.ForwardDeclare(_context.UpperContext);
                _createFunction.Declare();
                _createFunction.Define();
            }

            _createFunctionCall.Define();
        }

        public override void Check(Context context)
        {
            Type.Check(context);

            _createFunction.Check(context);
            _createFunctionCall.Check(context);

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
            return _createFunctionCall.Print(type, printer);
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
            return $"new {Type}({SingleParameter?.ToString() ?? string.Join(", ", Parameter.Select(i => $"{i.Key} = {i.Value}"))})";
        }
    }
}