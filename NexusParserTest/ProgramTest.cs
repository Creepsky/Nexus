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
    }
}