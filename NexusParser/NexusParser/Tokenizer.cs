using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace NexusParser
{
    public class UnknownTokenException : Exception
    {
        public readonly int Line;
        public readonly int Column;
        public readonly string Token;

        public UnknownTokenException(int line, int column, string token)
            : base("unknown token")
        {
            Line = line;
            Column = column;
            Token = token;
        }
    }

    public class Tokenizer
    {
        private readonly IList<TokenDefinition> _tokenDefinitions;

        public Tokenizer()
        {
            _tokenDefinitions = new List<TokenDefinition>
            {
                new TokenDefinition(TokenType.Plus, @"^\+"),
                new TokenDefinition(TokenType.Minus, @"^\-"),
                new TokenDefinition(TokenType.Multiply, @"^\*"),
                new TokenDefinition(TokenType.Divide, @"^/"),
                new TokenDefinition(TokenType.BraceOpen, @"^{"),
                new TokenDefinition(TokenType.BraceClose, @"^}"),
                new TokenDefinition(TokenType.SquareBracketOpen, @"^\["),
                new TokenDefinition(TokenType.SquareBracketClose, @"^\]"),
                new TokenDefinition(TokenType.Real, @"^\d+\.\d*"),
                new TokenDefinition(TokenType.Integer, @"^(\d,?)+"),
                new TokenDefinition(TokenType.ParenthesesOpen, @"^\("),
                new TokenDefinition(TokenType.ParenthesesClose, @"^\)"),
                new TokenDefinition(TokenType.Class, @"^class"),
                new TokenDefinition(TokenType.Equal, @"^="),
                new TokenDefinition(TokenType.Less, @"^<"),
                new TokenDefinition(TokenType.LessEqual, @"^<="),
                new TokenDefinition(TokenType.Greater, @"^>"),
                new TokenDefinition(TokenType.GreaterEqual, @"^>="),
                new TokenDefinition(TokenType.Colon, @"^;"),
                new TokenDefinition(TokenType.Comma, @"^,"),
                new TokenDefinition(TokenType.Get, @"^get"),
                new TokenDefinition(TokenType.Set, @"^set"),
                new TokenDefinition(TokenType.Range, @"^\.\."),
                new TokenDefinition(TokenType.Dot, @"^\."),
                new TokenDefinition(TokenType.String, "^\".*\""),
                new TokenDefinition(TokenType.Identifier, @"^[a-zA-Z_-][a-zA-Z0-9_-]*"),
            };
        }

        public List<Token> Tokenize(string input)
        {
            //new Regex("^");
            var tokens = new List<Token>();
            var remaining = input;
            var match = new TokenMatch();
            var errorHandler = new InvalidTokenDefinition();
            var line = 1;
            var column = 1;

            while (!string.IsNullOrWhiteSpace(remaining))
            {
                if (FindMatch(remaining, ref match))
                {
                    tokens.Add(new Token(match.Type, match.Value, line, column));
                    column += match.Value.Length;
                    remaining = match.RemainingText;
                }
                else
                {
                    if (remaining.First() == ' ')
                    {
                        // skip
                        ++column;
                        remaining = remaining.Substring(1);
                    }
                    else if (remaining.First() == '\r')
                    {
                        // skip
                        remaining = remaining.Substring(1);
                    }
                    else if (remaining.First() == '\n')
                    {
                        ++line;
                        column = 1;
                        remaining = remaining.Substring(1);
                    }
                    else if (errorHandler.Match(remaining, ref match))
                    {
                        // invalid
                        tokens.Add(new Token(match.Type, match.Value.Trim(), line, column));
                        column += match.Value.Length;
                        remaining = match.RemainingText;
                        throw new UnknownTokenException(line, column, match.Value);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(remaining), remaining, "unknown syntax");
                    }
                }
            }

            tokens.Add(new Token(TokenType.Terminator, string.Empty, line, column));

            return tokens;
        }

        private bool FindMatch(string input, ref TokenMatch match)
        {
            foreach (var i in _tokenDefinitions)
                if (i.Match(input, ref match))
                    return true;

            return false;
        }
    }
}