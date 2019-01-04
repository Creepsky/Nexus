using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nexus.ir;
using Nexus.ir.expr;
using Nexus.ir.stmt;
using NLog;

namespace Nexus.gen
{
    public struct CompilationUnit : IEquatable<CompilationUnit>
    {
        public string Name { get; }
        public string Path { get; }
        public string Source { get; }

        public CompilationUnit(string name, string path, string source)
        {
            Name = name;
            Path = path;
            Source = source;
        }

        public bool Equals(CompilationUnit other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Path, other.Path) && string.Equals(Source, other.Source);
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
                hashCode = (hashCode * 397) ^ (Source != null ? Source.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    public class Generator
    {
        public readonly Context globalContext = new Context();
        private readonly IList<ir.File> _files;
        private readonly Logger _logger;

        public Generator(IList<ir.File> files)
        {
            _files = files;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Generate()
        {
            var phases = new Dictionary<string, Action<ir.File>>
            {
                {"forward declaration", f => f.ForwardDeclare(globalContext)},
                {"declaration", f => f.Declare()},
                {"definition", f => f.Define()}
            };

            foreach (var phase in phases)
            {
                _logger.Info($"Generating data, {phase.Key} phase");

                foreach (var file in _files)
                {
                    phase.Value(file);
                }
            }
        }

        public void Check()
        {
            foreach (var i in _files)
            {
                i.Check(globalContext);
            }
        }

        public IEnumerable<CompilationUnit> Compile()
        {
            var sourceStringWriter = new StringWriter();
            var sourcePrinter = new Printer(sourceStringWriter);
            var any = false;

            // includes
            foreach (var i in globalContext.GetElements().OfType<Include>())
            {
                any = any | i.Print(PrintType.Header, sourcePrinter);
            }

            if (any)
            {
                sourcePrinter.WriteLine();
                any = false;
            }

            // forward declaration of all classes
            foreach (var i in globalContext.GetElements().OfType<Class>())
            {
                foreach (var j in i.Variants.Append(i))
                {
                    any = any | j.Print(PrintType.ForwardDeclaration, sourcePrinter);
                }
            }

            if (any)
            {
                sourcePrinter.WriteLine();
                any = false;
            }

            // forward declaration of all functions
            foreach (var i in globalContext.GetElements().OfType<Function>())
            {
                foreach (var j in i.Overloads.Append(i))
                {
                    any = any | j.Print(PrintType.ForwardDeclaration, sourcePrinter);
                }
            }

            if (any)
            {
                sourcePrinter.WriteLine();
                any = false;
            }

            // definition of all classes
            foreach (var i in globalContext.GetElements().OfType<Class>())
            {
                foreach (var j in i.Variants.Append(i))
                {
                    var printed = j.Print(PrintType.Declaration, sourcePrinter);

                    if (printed)
                    {
                        sourcePrinter.WriteLine();
                    }

                    any = any || printed;
                }
            }

            // definition of all functions
            foreach (var i in globalContext.GetElements().OfType<Function>())
            {
                foreach (var j in i.Overloads.Append(i))
                {
                    var printed = j.Print(PrintType.Definition, sourcePrinter);
                    
                    if (printed)
                    {
                        sourcePrinter.WriteLine();
                    }

                    any = any || printed;
                }
            }

            //foreach (var i in _files)
            //{
            //    headerStringWriter.GetStringBuilder().Clear();

            //    if (i.Print(PrintType.Header, headerPrinter))
            //    {
            //        compilationUnits.Add(new CompilationUnit(
            //            i.Name,
            //            i.FilePath,
            //            headerStringWriter.ToString(),
            //            ""
            //        ));
            //    }
            //}

            yield return new CompilationUnit("main", "/main.cpp", sourceStringWriter.ToString());
        }
    }
}