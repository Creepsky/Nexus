using System;
using System.Collections.Generic;
using System.Linq;

namespace NexusParser
{
    public class UnexpectedTokenException : Exception
    {
        public UnexpectedTokenException(TokenType expected, TokenType got)
            : base($"expected {expected.ToString()} but got {got.ToString()}")
        { }
    }

    public class Parser
    {
        private readonly Stack<Token> _tokenStream;

        public Parser(IEnumerable<Token> tokenStream)
        {
            _tokenStream = new Stack<Token>(tokenStream.Reverse());
        }

        private Token Accept() => _tokenStream.Pop();

        private bool Accept(out Token token, TokenType tokenType)
        {
            if (_tokenStream.Peek().Type == tokenType)
            {
                token = Accept();
                return true;
            }

            token = null;
            return false;
        }
        
        private bool Accept(out IList<Token> tokens, params TokenType[] tokenTypes)
        {
            var stream = _tokenStream;
            tokens = null;

            foreach (var token in tokenTypes)
                if (stream.Pop().Type != token)
                    return false;

            tokens = new List<Token>();

            for (var i = 0; i < tokenTypes.Length; ++i)
                tokens.Add(Accept());

            return true;
        }

        private Token Expect(TokenType tokenType)
        {
            if (Accept(out var token, tokenType))
                return token;

            var currentToken = _tokenStream.Peek();
            throw new Exception($"line {currentToken.Line}, column {currentToken.Column}: expected {tokenType} but got '{currentToken.Value}'");
        }
        
        public Class Parse()
        {
            var c = ParseClass();
            Expect(TokenType.Terminator);
            return c;
        }

        private Class ParseClass()
        {
            var c = new Class();
            Expect(TokenType.Class);
            c.Name = Expect(TokenType.Identifier).Value;
            Expect(TokenType.BraceOpen);
            if (ParseVariable(out var variable))
                c.Members.Add(variable);
            else if (ParseFunction(out var function))
                c.Members.Add(function);
            Expect(TokenType.BraceClose);
            return c;
        }

        private bool ParseType(out Type type)
        {
            var 

            // type
            if (Accept(TokenType.Identifier, ))
            {
                if ()
            }

            return false;
        }

        private bool ParseVariable(out Variable variable)
        {
            variable = null;

            // i32 i = ... ;
            if (Accept(out var tokens, TokenType.Identifier, TokenType.Identifier, TokenType.Equal))
            {
                if (ParseExpression(out var init))
                {
                    variable = new Variable
                    {
                        Initialization = init
                    };

                    Expect(TokenType.Colon);
                    return true;
                }

                return false;
            }

            // i32 i ;
            if (Accept(out var tokens, TokenType.Identifier, TokenType.Identifier, TokenType.Colon))
                return true;

            return false;
        }

        private bool ParseFunction(out Function function)
        {
            var Accept(TokenType.Identifier, );
        }

        private bool ParseExpression(out IExpression expression)
        {
            expression = null;
            return false;
        }

        private void ParseTerm()
        {

        } 

        private void ParseFactor()
        {
            //if (_lookAhead.Type == TokenType.ParenthesesOpen)
            //{
            //    try
            //    {

            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //        throw;
            //    }
            //}
        }
    }
}