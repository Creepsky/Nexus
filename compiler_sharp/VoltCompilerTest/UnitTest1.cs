using System;
using System.Linq;
using Sprache;
using Volt;
using Xunit;

namespace VoltTest
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
        public void IdentifierOk(string id, string expected) => Assert.Equal(expected, VoltParser.Identifier.Parse(id));

        [Theory]
        [InlineData("1ABC")]
        [InlineData("1ABC ")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("1")]
        [InlineData(" ABC")]
        [InlineData(" ABC  ")]
        public void IdentifierFail(string id) => Assert.Throws<ParseException>(() => VoltParser.Identifier.Parse(id));

        [Theory]
        [InlineData("\"\"")]
        [InlineData("\"test\"")]
        [InlineData("\"123\"")]
        [InlineData("\"123test\"")]
        [InlineData("\" abc \"")]
        [InlineData("\" a b c \"")]
        public void QuotedTextOk(string text) => Assert.Equal(text.Substring(1, text.Length - 2),
            VoltParser.QuotedText.Parse(text));

        [Theory]
        [InlineData("abc \"")]
        [InlineData("\" a b c ")]
        [InlineData("abc")]
        [InlineData("   a b c   ")]
        [InlineData("   abc   ")]
        [InlineData("   abc")]
        [InlineData("abc   ")]
        public void QuotedTextFail(string text) => Assert.Throws<ParseException>(() => VoltParser.QuotedText.Parse(text));

        [Theory]
        [InlineData("string abc", "string", "abc")]
        [InlineData("u32 num", "u32", "num")]
        [InlineData("MyClass myObject", "MyClass", "myObject")]
        public void Variables(string input, string type, string name)
        {
            var output = (Variable)VoltParser.Variable.Parse(input);
            Assert.Equal(type, output.Type);
            Assert.Equal(name, output.Name);
        }

        [Theory]
        [InlineData("string")]
        [InlineData("test")]
        [InlineData("string 123")]
        [InlineData("123 test")]
        [InlineData("string 1 2 3")]
        [InlineData(" test test")]
        public void VariablesFail(string input) => Assert.Throws<ParseException>(() => VoltParser.Variable.Parse(input));

        [Theory]
        [InlineData("i32 function1() { }", "i32", "function1")]
        [InlineData("i32     function1()   {    }    ", "i32", "function1")]
        [InlineData("i32   function1(i32 param1)   {    }    ", "i32", "function1")]
        [InlineData("i32            function1(i32 param1, usize param2)   {    }    ", "i32", "function1")]
        public void Functions(string input, string returnType, string name)
        {
            var function = (Function)VoltParser.Function.Parse(input);
            Assert.Equal(returnType, function.ReturnType);
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
        [InlineData("i32 member", typeof(Variable))]
        [InlineData("i32    member       ", typeof(Variable))]
        [InlineData("i32 member() {}", typeof(Function))]
        [InlineData("i32    member (  )     {     }  ", typeof(Function))]
        public void ClassMember(string input, Type type)
        {
            var result = VoltParser.ClassMembers.Parse(input);
            Assert.IsType(type, result);
        }

        [Fact]
        public void Class()
        {
            const string input = "class Test       { " +
                                 " i32    " +
                                 " testInt    " +
                                 "}";

            var result = VoltParser.Class.Parse(input);
        }
    }
}
