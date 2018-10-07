namespace Nexus.gen
{
    public enum PrintType
    {
        Header,
        Source
    }

    public interface IPrintable
    {
        void Print(PrintType type, Printer printer);
    }
}