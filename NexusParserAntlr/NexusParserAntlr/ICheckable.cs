using NexusParserAntlr.Generation;

namespace NexusParserAntlr
{
    public interface ICheckable
    {
        void Check(Context context);
    }
}