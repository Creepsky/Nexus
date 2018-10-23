using System;
using System.IO;
using Nexus;
using Xunit;

namespace NexusParserTest
{
    public static class ProgramTest
    {
        [Theory]
        [InlineData("in", "--output out")]
        [InlineData("--input in", "out")]
        public static void WrongParameters(string param1, string param2)
        {
            Assert.ThrowsAny<Exception>(() => Program.Main(new []{param1, param2}));
        }

        [Theory]
        [InlineData("-h")]
        [InlineData("--help")]
        public static void Help(string input)
        {
            var (sw, swError) = RedirectConsole();
            Assert.Equal(0, Program.Main(new []{input}));
            Assert.StartsWith("Usage:", sw.ToString());
            Assert.Empty(swError.ToString());
        }

        private static (StringWriter, StringWriter) RedirectConsole()
        {
            var sw = new StringWriter();
            var swError = new StringWriter();
            Console.SetOut(sw);
            Console.SetError(swError);
            return (sw, swError);
        }
    }
}