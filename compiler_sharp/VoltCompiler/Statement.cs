namespace Nexus
{
    public interface IStatement
    { }

    public class AssignmentStatement : IStatement
    {
        public IExpression LValue;
        public IExpression RValue;
    }

    public class FunctionCallStatement : IStatement
    {
        public string Name;
        public IList<IExpression> Parameter;
    }

    public class ReturnStatement : IStatement
    {
        public IExpression Value;
    }

    public class WhileStatement : IStatement
    {
        public IExpression Condition;
        public IList<IStatement> Body;
    }

    public class IfStatement : IStatement
    {
        public IExpression Condition;
        public IList<IStatement> Then;
        public IList<IStatement> Else;
    }
}