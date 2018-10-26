namespace Nexus.ir.stmt
{
    public class OperatorFunction : ExtensionFunction
    {
        public string Operator { get; }

        public OperatorFunction(string op)
        {
            Operator = op;
        }
    }
}