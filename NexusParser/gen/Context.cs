using System;
using System.Collections.Generic;

namespace Nexus.gen
{
    public class Context
    {
        public Context UpperContext;
        public readonly IGenerationElement Element;
        private readonly IDictionary<string, IGenerationElement> _symbols;

        /// <summary>
        /// Creates a new, empty context instance without any symbols.
        /// </summary>
        public Context()
            : this(null, null, new Dictionary<string, IGenerationElement>())
        { }

        public Context(Context upperContext, IGenerationElement element)
            : this(upperContext, element, new Dictionary<string, IGenerationElement>())
        { }

        public Context(Context upperContext, IGenerationElement element, IDictionary<string, IGenerationElement> symbols)
        {
            UpperContext = upperContext;
            Element = element;
            _symbols = new Dictionary<string, IGenerationElement>(symbols);
        }

        public bool Contains(string name)
        {
            return _symbols.ContainsKey(name);
        }

        public void Add(string name, IGenerationElement element)
        {
            if (Contains(name))
                throw new ArgumentException("element name already exist", nameof(name));

            _symbols.Add(name, element);
        }

        public IGenerationElement Get(string name)
        {
            return Contains(name) ? _symbols[name] : null;
        }

        public Class GetClass(string name)
        {
            if (Contains(name))
                return (Class)_symbols[name];

            if (UpperContext != null)
                return UpperContext.GetClass(name);

            throw new ArgumentOutOfRangeException(nameof(name), name, "element not found");
        }

        public Context StackNewContext(IGenerationElement element) => new Context(this, element);

        public IEnumerable<IGenerationElement> GetElements()
        {
            return _symbols.Values;
        }
    }
}