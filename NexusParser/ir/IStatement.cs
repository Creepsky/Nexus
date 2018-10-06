using System.Collections.Generic;
using System.Linq;

namespace Nexus.ir
{
    public interface IStatement
    { }

    public abstract class Statement : IStatement, IPositioned
    {
        public int Line { get; set; }
        public int Column { get; set; }
    }

    public class File
    {
        public IList<Class> Classes;
        public IList<ExtensionFunction> ExtensionFunctions;
    }

    public class AssignmentStatement : Statement
    {
        public IExpression Left;
        public IExpression Right;
    }

    public class ReturnStatement : Statement
    {
        public IExpression Value;
    }

    public class IfStatement : Statement
    {
        public ICondition Condition;
        public IList<IStatement> Then;
        public IList<IStatement> Else;
    }

    public class WhileStatement : Statement
    {
        public ICondition Condition;
        public IList<IStatement> Body;
    }

    public class ForStatement : Statement
    {
        public IStatement Start;
        public ICondition Stop;
        public IExpression Step;
        public IList<IStatement> Body;
    }

    public class ExtensionFunction : Statement
    {
        public IType ReturnType;
        public string Class;
        public string Name;
        public IList<Variable> Parameter;
        public IList<IStatement> Body;

        public override string ToString()
        {
            return $"{ReturnType} {Class}.{Name}()";
        }
    }

    public class TupleExplosionStatement : Statement
    {
        public IList<string> Names;
        public IExpression Right;
    }

    public class Function : Statement
    {
        public IType Type;
        public string Name;
        public IList<Variable> Parameter;
        public IList<IStatement> Statements;

        public override string ToString()
        {
            return $"{Type} {Name}({string.Join(',', Parameter.Select(i => i.Type))})";
        }
    }

    public class FunctionCallStatement : Statement
    {
        public FunctionCall FunctionCall;
    }

    public class Variable : Statement
    {
        public IType Type;
        public string Name;
        public bool Setter;
        public bool Getter;
        public IExpression Initialization;

        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }
}