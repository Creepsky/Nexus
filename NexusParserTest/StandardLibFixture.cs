using System;
using System.Linq;
using Nexus;
using Nexus.gen;

namespace NexusParserTest
{
    public class StandardLibFixture : IDisposable
    {
        public Context Context { get; private set; }

        private Generator _generator;

        public StandardLibFixture()
        {
            var config = new Configuration("../../../../std_lib", "std_lib/bin");
            config.Read();
            var files = FileParser.ParseDirectory("/", config);
            _generator = new Generator(files.ToList());
            Context = _generator.globalContext;
        }

        public void Dispose()
        {
            Context = null;
        }

        public void Generate()
        {
            _generator.Generate();
        }
    }
}