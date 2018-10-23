using System.Linq;

namespace Nexus.ir.stmt
{
    public class OperatorFunction : ExtensionFunction
    {
        public string Operator { get; private set; }

        public override void SetName(string name)
        {
            Operator = name;
            Name = "operator" + name;

            if (Parameter.Any())
            {
                Name += "_" + string.Join("_", Parameter.Select(i => i.Type.Name));
            }
        }
    }
}