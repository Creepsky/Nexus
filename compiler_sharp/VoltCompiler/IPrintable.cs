using System.IO;

namespace Nexus
{
    public interface IPrintable
    {
        void ToHeader(Printer printer);

        void ToSource(Printer printer);
    }
}