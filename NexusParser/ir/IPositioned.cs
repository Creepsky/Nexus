namespace Nexus.ir
{
    public interface IPositioned
    {
        string FilePath { set; get; }
        int Line { set; get; }
        int Column { set; get; }
    }
}