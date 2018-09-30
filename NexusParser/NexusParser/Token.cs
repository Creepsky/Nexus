using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NexusParser
{
    public enum TokenType
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Real,
        Integer,
        Invalid,
        SquareBracketOpen,
        SquareBracketClose,
        BraceOpen,
        BraceClose,
        ParenthesesOpen,
        ParenthesesClose,
        Identifier,
        Class,
        Equal,
        Less,
        LessEqual,
        Greater,
        GreaterEqual,
        Colon,
        Comma,
        Get,
        Set,
        String,
        Terminator,
        Range,
        Dot
    }

    public struct TokenMatch
    {
        public bool Success;
        public TokenType Type;
        public string Value;
        public string RemainingText;
    }

    public class TokenDefinition
    {
        public readonly TokenType Type;
        public readonly Regex Regex;

        public TokenDefinition(TokenType type, string pattern)
        {
            Type = type;
            Regex = new Regex(pattern);
        }

        public virtual bool Match(string input, ref TokenMatch match)
        {
            var patternMatch = Regex.Match(input);

            if (!patternMatch.Success)
                return false;

            var remainingText = string.Empty;

            if (patternMatch.Length != input.Length)
                remainingText = input.Substring(patternMatch.Length);

            match.Success = true;
            match.RemainingText = remainingText;
            match.Type = Type;
            match.Value = patternMatch.Value;

            return true;
        }
    }

    public class InvalidTokenDefinition : TokenDefinition
    {
        public InvalidTokenDefinition()
            : base(TokenType.Invalid, @"^\S+")
        { }

        public override bool Match(string input, ref TokenMatch match)
        {
            var patternMatch = Regex.Match(input);

            if (!patternMatch.Success)
                return false;

            match.Success = true;
            match.RemainingText = input.Substring(patternMatch.Length);
            match.Type = TokenType.Invalid;
            match.Value = patternMatch.Value.Trim();
            return true;
        }
    }

    public class Token
    {
        public readonly TokenType Type;
        public readonly string Value;
        public readonly int Line;
        public readonly int Column;

        public Token(TokenType type, string value, int line, int column)
        {
            Type = type;
            Value = value;
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }

    //public class OperatorToken : Token {}

    //public class PlusToken : OperatorToken {}
    //public class MinusToken : OperatorToken {}
    //public class MultiplyToken : OperatorToken {}
    //public class DivideToken : OperatorToken {}

    //public class NumberConstantToken : Token
    //{}
}