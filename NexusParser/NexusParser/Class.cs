using System.Collections.Generic;
using NexusParser;

namespace NexusParser
{
    public class Class
    {
        public string Name;
        public List<IStatement> Members;

        private void ToSource(Printer printer, List<string> usedTypes)
        {
            printer.WriteLine("#include \"\"");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
