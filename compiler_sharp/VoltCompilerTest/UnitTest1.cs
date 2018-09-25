using System;
using Sprache;
using Xunit;

namespace Nexus.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("test123", "test123")]
        [InlineData("ABC  ", "ABC")]
        [InlineData("abc", "abc")]
        [InlineData("Ab1", "Ab1")]
        [InlineData("ABC", "ABC")]
        [InlineData("A", "A")]
        [InlineData("A-", "A-")]
        [InlineData("A_", "A_")]
        public void IdentifierOk(string id, string expected) => Assert.Equal(expected, NexusParser.Identifier.Parse(id));

        [Theory]
        [InlineData("1ABC")]
        [InlineData("1ABC ")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("1")]
        [InlineData(" ABC")]
        [InlineData(" ABC  ")]
        public void IdentifierFail(string id) => Assert.Throws<ParseException>(() => NexusParser.Identifier.Parse(id));

        [Theory]
        [InlineData("\"\"")]
        [InlineData("\"test\"")]
        [InlineData("\"123\"")]
        [InlineData("\"123test\"")]
        [InlineData("\" abc \"")]
        [InlineData("\" a b c \"")]
        public void QuotedTextOk(string text)
        {
            var quotedText = NexusParser.QuotedText.Parse(text);
            Assert.IsType<Text>(quotedText);
            Assert.Equal(text.Substring(1, text.Length - 2), ((Text) quotedText).Value);
        }

        [Theory]
        [InlineData("abc \"")]
        [InlineData("\" a b c ")]
        [InlineData("abc")]
        [InlineData("   a b c   ")]
        [InlineData("   abc   ")]
        [InlineData("   abc")]
        [InlineData("abc   ")]
        public void QuotedTextFail(string text) => Assert.Throws<ParseException>(() => NexusParser.QuotedText.Parse(text));

        [Theory]
        [InlineData("100", (long) 100)]
        [InlineData("2147483647", (long) 2147483647)]
        public void Integers<T>(string input, T expectedValue)
        {
            var result = NexusParser.Number.Parse(input);
            // TODO: add type check
            //Assert.IsType<>(result);
            var number = (NumberLiteral<T>) result;
            Assert.Equal(expectedValue, number.Value);
        }

        [Theory]
        [InlineData("2147483648_i32")]
        public void I32Fail(string input)
        {
            Assert.ThrowsAny<Exception>(() => NexusParser.Number.Parse(input));
        }

        [Theory]
        [InlineData("string abc", "string", "abc")]
        [InlineData("u32 num", "u32", "num")]
        [InlineData("MyClass myObject", "MyClass", "myObject")]
        public void Variables(string input, string type, string name)
        {
            var output = (Variable)NexusParser.Variable.Parse(input);
            Assert.Equal(type, output.Type.Type);
            Assert.Equal(name, output.Name);
        }

        [Theory]
        [InlineData("string")]
        [InlineData("test")]
        [InlineData("string 123")]
        [InlineData("123 test")]
        [InlineData("string 1 2 3")]
        public void VariablesFail(string input) => Assert.Throws<ParseException>(() => NexusParser.Variable.Parse(input));

        [Theory]
        [InlineData("i32 function1() { }", "i32", "function1")]
        [InlineData("i32     function1()   {    }    ", "i32", "function1")]
        [InlineData("i32   function1(i32 param1)   {    }    ", "i32", "function1")]
        [InlineData("i32            function1(i32 param1, usize param2)   {    }    ", "i32", "function1")]
        public void Functions(string input, string returnType, string name)
        {
            var function = (Function)NexusParser.Function.Parse(input);
            Assert.Equal(returnType, function.ReturnType.Type);
            Assert.Equal(name, function.Name);
        }

        [Theory]
        [InlineData("     token", "token")]
        [InlineData("token", "token")]
        [InlineData("token     ", "token     ")]
        [InlineData("     token     ", "token     ")]
        public void Shifting(string input, string expected)
        {
            Assert.Equal(expected, Parse.AnyChar.Many().Shift().Text().Parse(input));
        }

        [Theory]
        [InlineData("i32 member;", typeof(Variable))]
        [InlineData("i32    member   ;    ", typeof(Variable))]
        [InlineData("i32 member() {}", typeof(Function))]
        [InlineData("i32    member (  )     {     }  ", typeof(Function))]
        public void ClassMember(string input, Type type)
        {
            var result = NexusParser.ClassMembers.Parse(input);
            Assert.IsType(type, result);
        }

        [Fact]
        public void Class()
        {
            const string input = "class Test       { " +
                                 " i32    " +
                                 " testInt;    " +
                                 "}";

            NexusParser.Class.Parse(input);
        }
    }
}
