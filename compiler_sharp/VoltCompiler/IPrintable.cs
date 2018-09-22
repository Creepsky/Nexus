using System.IO;

namespace Volt
{
    public interface IPrintable
    {
        void ToHeader(Printer printer);

        void ToSource(Printer printer);
    }
}