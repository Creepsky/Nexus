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
            Assert.Equal("/main.nx", config.EnumeratePath("/main.nx").First());
            Assert.True(config.EnumeratePath("/std/string").Any());
            Assert.Equal("/std/string/string.nx", config.EnumeratePath("/std/string/string.nx").First());
        }
    }
}