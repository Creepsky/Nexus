using Sprache;

namespace Nexus.Parser
{
    public static class Factor
    {
        public static Parser<string> Identifier =>
            from start in Parse.Letter.Once().Text()
            from rest in Parse.LetterOrDigit.Or(Parse.Chars("_-")).Many().Text()
            select start + rest;

        public static IExpression ParseString(string input)
        {
            return null;
        }
    }
}