using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nexus.common;
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
        public Context GlobalContext { get; } = new Context();
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
                {"forward declaration", f => f.ForwardDeclare(GlobalContext)},
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
                i.Check(GlobalContext);
            }
        }

        public IEnumerable<CompilationUnit> Compile()
        {
            var sourceStringWriter = new StringWriter();
            var sourcePrinter = new Printer(sourceStringWriter);
            var any = false;

            // includes
            foreach (var i in GlobalContext.GetElements().OfType<Include>())
            {
                any = any | i.Print(PrintType.Header, sourcePrinter);
            }

            if (any)
            {
                sourcePrinter.WriteLine();
                any = false;
            }

            // forward declaration of all classes
            foreach (var i in GlobalContext.GetElements().OfType<Class>())
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
            foreach (var i in GlobalContext.GetElements().OfType<Function>())
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

            foreach (var i in GetSortedClasses())
            {
                var printed = i.Print(PrintType.Declaration, sourcePrinter);

                if (printed)
                {
                    sourcePrinter.WriteLine();
                }

                any = any || printed;
            }

            // definition of all functions
            foreach (var i in GlobalContext.GetElements().OfType<Function>())
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

            yield return new CompilationUnit("main", "/main.cpp", sourceStringWriter.ToString());
        }

        private IEnumerable<Class> GetSortedClasses()
        {
            // definition of all classes
            var dependencies = new Dictionary<Class, List<Class>>();

            foreach (var i in GlobalContext.GetElements().OfType<Class>())
            {
                foreach (var j in i.Variants.Append(i).Where(v => v.TemplateList == null))
                {
                    dependencies.Add(j, new List<Class>());

                    foreach (var d in j.Variables
                        .Where(v => v.Type.GetType() == typeof(SimpleType))
                        .Select(v => (GlobalContext.Get(v.Type.Name) as Class)?.GetVariant(v.Type, GlobalContext))
                        .Where(v => v != null))
                    {
                        dependencies[j].Add(d);
                    }
                }
            }

            // find circular dependencies
            foreach (var (key, value) in dependencies)
            {
                foreach (var j in value)
                {
                    if (dependencies[j].Contains(key))
                    {
                        throw new CircularReferenceException(key.ToString(), j.ToString());
                    }
                }
            }

            // sort the classes according to it dependencies
            var sortedClasses = new List<Class>();

            // create a node list for all classes with empty edges at first
            var allClasses = dependencies
                .Select(i => new ClassNode {Marked = false, Class = i.Key, Edges = new List<ClassNode>()})
                .ToList();

            // now also fill the edges
            foreach (var i in allClasses)
            {
                foreach (var j in dependencies[i.Class])
                {
                    i.Edges.Add(allClasses.First(e => e.Class == j));
                }
            }

            // find the first not marked node
            var node = allClasses.FirstOrDefault(i => i.Marked == false);

            // visit all nodes until all are marked
            // we don't need to check for circular dependencies, it was already done before
            while (node != null)
            {
                VisitClassNode(node, sortedClasses);
                node = allClasses.FirstOrDefault(i => i.Marked == false);
            }

            // we have to reverse the whole list, because right now it is sorted like this: class -> dependency -> ... -> ...
            sortedClasses.Reverse();

            return sortedClasses;
        }

        private class ClassNode
        {
            public bool Marked;
            public Class Class;
            public List<ClassNode> Edges;
        }

        private static void VisitClassNode(ClassNode node, IList<Class> sortedClasses)
        {
            if (node.Marked)
            {
                return;
            }

            foreach (var i in node.Edges)
            {
                VisitClassNode(i, sortedClasses);
            }

            node.Marked = true;
            sortedClasses.Insert(0, node.Class);
        }
    }
}