using System.Collections.Generic;
using System.Linq;

namespace NexusParserAntlr.ir
{
    public interface IStatement
    { }

    public class File
    {
        public IList<Class> Classes;
        public IList<ExtensionFunction> ExtensionFunctions;
    }

    public class AssignmentStatement : IStatement
    {
        public IExpression Left;
        public IExpression Right;
    }

    public class ReturnStatement : IStatement
    {
        public IExpression Value;
    }

    public class IfStatement : IStatement
    {
        public ICondition Condition;
        public IList<IStatement> Then;
        public IList<IStatement> Else;
    }

    public class WhileStatement : IStatement
    {
        public ICondition Condition;
        public IList<IStatement> Body;
    }

    public class ForStatement : IStatement
    {
        public IStatement Start;
        public ICondition Stop;
        public IExpression Step;
        public IList<IStatement> Body;
    }

    public class ExtensionFunction : IStatement
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

    public class TupleExplosionStatement : IStatement
    {
        public IList<string> Names;
        public IExpression Right;
    }

    public class Function : IStatement
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

    public class FunctionCallStatement : IStatement
    {
        public FunctionCall FunctionCall;
    }
}