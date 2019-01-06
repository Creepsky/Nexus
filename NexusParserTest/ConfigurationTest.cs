using System.IO;
using System.Linq;
using Nexus;
using Xunit;

namespace NexusParserTest
{
    public static class ConfigurationTest
    {
        [Fact]
        public static void LoadConfig()
        {
            var config = new Configuration("../../../../example", "../../../../example/bin");
            config.Read();
            Assert.Equal("/MyClass.nx", config.EnumerateSourceFiles("/MyClass.nx").First());
            Assert.True(config.EnumerateSourceFiles("/std/string").Any());
            Assert.Equal("/std/string/string.nx", config.EnumerateSourceFiles("/std/string/string.nx").First());
        }
    }
}