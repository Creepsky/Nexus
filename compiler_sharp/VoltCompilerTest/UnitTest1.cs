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
        [InlineData("123_i8", (sbyte)123)]
        [InlineData("12345_i16", (short)12345)]
        [InlineData("1234567_i32", 1234567)]
        [InlineData("123456789_i64", (long)123456789)]
        [InlineData("+123_i8", (sbyte)123)]
        [InlineData("+12,345_i16", (short)12345)]
        [InlineData("+1,234,567_i32", 1234567)]
        [InlineData("+123,456,789_i64", (long)123456789)]
        [InlineData("-123_i8", (sbyte)-123)]
        [InlineData("-12345_i16", (short)-12345)]
        [InlineData("-1234567_i32", -1234567)]
        [InlineData("-123456789_i64", (long)-123456789)]
        [InlineData("123_u8", (byte)123)]
        [InlineData("12345_u16", (ushort)12345)]
        [InlineData("1234567_u32", (uint)1234567)]
        [InlineData("123456789_u64", (ulong)123456789)]
        public void Tokens<T>(string input, T expectedData)
        {
            var tmp = NexusParser.Number.Parse(input);
            Assert.IsAssignableFrom<NumberLiteral<T>>(tmp);
            Assert.Equal(((NumberLiteral<T>)tmp).Value, expectedData);
        }

        [Theory]
        [InlineData("Bx1000", 8)]
        [InlineData("bx1000_0000", 128)]
        [InlineData("Bx1100_1100", 204)]
        [InlineData("bx0111_1111_1111_1111_1111_1111_1111_1111", 2147483647)]
        public void Binary(string input, int expected)
        {
            Assert.Equal(expected, ((I64)NexusParser.Number.Parse(input)).Value);
        }

        [Theory]
        [InlineData("0x8", 8)]
        [InlineData("0x80", 128)]
        [InlineData("0xCC", 204)]
        [InlineData("0x7FFF_FFFF", 2147483647)]
        public void Hex(string input, int expected)
        {
            Assert.Equal(expected, ((I64)NexusParser.Number.Parse(input)).Value);
        }

        [Fact]
        public void Boolean()
        {
            Assert.True(NexusParser.BooleanLiteral.Parse("true").Value);
            Assert.False(NexusParser.BooleanLiteral.Parse("false").Value);
        }

        [Theory]
        [InlineData("[1, 2, 3]", 3)]
        [InlineData("[1, [\"abc\", 0], (a, b, 1)]", 3)]
        public void ArrayInitialization(string input, int size)
        {
            var tmp = NexusParser.Factor.Parse(input);
            Assert.IsType<ArrayLiteral>(tmp);
            Assert.Equal(size, ((ArrayLiteral)tmp).Values.Count);
        }

        [Fact]
        public void Function()
        {
            var function = NexusParser.Function.Parse("bool test_function(i8 parameter1, u8 parameter2 = 500){ return true; }");
            
            Assert.Equal("test_function", function.Name);
            Assert.Equal("bool", function.ReturnType.Type);
            
            Assert.Equal("i8", function.Parameters[0].Type.Type);
            Assert.Equal("parameter1", function.Parameters[0].Name);
            Assert.False(function.Parameters[0].Getter);
            Assert.False(function.Parameters[0].Setter);

            Assert.Equal("u8", function.Parameters[1].Type.Type);
            Assert.Equal("parameter2", function.Parameters[1].Name);
            Assert.False(function.Parameters[1].Getter);
            Assert.False(function.Parameters[1].Setter);
            Assert.IsType<I64>(function.Parameters[1].Initialization);
            Assert.Equal(500, ((I64)function.Parameters[1].Initialization).Value);

            Assert.Equal(1, function.Body.Count);
            Assert.IsType<ReturnStatement>(function.Body[0]);
            Assert.IsType<BooleanLiteral>(((ReturnStatement)function.Body[0]).Value);
            Assert.True(((BooleanLiteral)((ReturnStatement)function.Body[0]).Value).Value);
        }

        [Theory]
        [InlineData("1,2,3,4", 1234)]
        [InlineData("1,000", 1000)]
        [InlineData("10,000", 10000)]
        [InlineData("100,000", 100000)]
        [InlineData("1,000,000", 1000000)]
        [InlineData("10,000,000", 10000000)]
        public void DigitGrouping(string input, int expected)
        {
            var tmp = NexusParser.Number.Parse(input);
            Assert.IsType<I64>(tmp);
            var i64 = (I64) tmp;
            Assert.Equal(expected, i64.Value);
        }

        [Fact]
        public void FunctionFail()
        {
            Assert.ThrowsAny<Exception>(() =>
                NexusParser.Function.Parse(
                    "bool test_function(i8 parameter1 set, u8 parameter2 = 500){ return true; }"));
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
