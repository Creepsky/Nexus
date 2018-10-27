using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace Nexus.gen
{
    public struct CompilationUnit : IEquatable<CompilationUnit>
    {
        public string Name { get; }
        public string Path { get; }
        public string Header { get; }
        public string Source { get; }

        public CompilationUnit(string name, string path, string header, string source)
        {
            Name = name;
            Path = path;
            Header = header;
            Source = source;
        }

        public bool Equals(CompilationUnit other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Path, other.Path) && string.Equals(Header, other.Header) && string.Equals(Source, other.Source);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is CompilationUnit other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Path != null ? Path.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Header != null ? Header.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Source != null ? Source.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    public class Generator
    {
        private readonly Context _globalContext = new Context();
        private readonly IList<ir.File> _files;
        private readonly Logger _logger;

        public Generator(IList<ir.File> files)
        {
            _files = files;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Generate()
        {
            var phases = new[]
            {
                GenerationPhase.ForwardDeclaration,
                GenerationPhase.Declaration,
                GenerationPhase.Definition
            };

            foreach (var phase in phases)
            {
                _logger.Info($"Generating data, phase {phase.ToString()}");
                foreach (var file in _files)
                {
                    file.Generate(_globalContext, phase);
                }
            }
        }

        public void Check()
        {
            foreach (var i in _files)
            {
                i.Check(_globalContext);
            }
        }

        public IEnumerable<CompilationUnit> Compile()
        {
            var compilationUnits = new List<CompilationUnit>();
            var headerStringWriter = new StringWriter();
            var sourceStringWriter = new StringWriter();
            var headerPrinter = new Printer(headerStringWriter);
            var sourcePrinter = new Printer(sourceStringWriter);

            foreach (var i in _files)
            {
                headerStringWriter.GetStringBuilder().Clear();
                sourceStringWriter.GetStringBuilder().Clear();

                i.Print(PrintType.Header, headerPrinter);
                i.Print(PrintType.Source, sourcePrinter);

                compilationUnits.Add(new CompilationUnit(
                    i.Name,
                    i.FilePath,
                    headerStringWriter.ToString(),
                    sourceStringWriter.ToString()
                ));
            }

            return compilationUnits;
        }
    }
}