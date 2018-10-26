using System.Collections.Generic;
using Nexus.common;
using Nexus.ir;

namespace Nexus.gen
{
    public class Context
    {
        public Context UpperContext { get; }
        public IGenerationElement Element { get; }
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
            Add(name, element, _symbols);
        }

        public void AddGlobal(string name, IGenerationElement element)
        {
            var current = this;

            while (current.UpperContext != null)
            {
                current = current.UpperContext;
            }

            Add(name, element, current._symbols);
        }

        private void Add(string name, IGenerationElement element, IDictionary<string, IGenerationElement> symbols)
        {
            if (Contains(name))
            {
                throw new RedeclarationException(element, Get(name), name);
            }

            symbols.Add(name, element);
        }

        public T Get<T>(string name, IPositioned caller) where T : IGenerationElement
        {
            var element = Get(name, caller);

            if (element.GetType() != typeof(T))
            {
                throw new TypeMismatchException(caller, nameof(T), element.GetType().Name);
            }

            return (T) element;
        }

        public IGenerationElement Get(string name, IPositioned caller)
        {
            var element = Get(name);

            if (element == null)
            {
                throw new NotFoundException(caller, "element", name);
            }

            return element;
        }

        public IGenerationElement Get(string name)
        {
            return Contains(name) ? _symbols[name] : UpperContext?.Get(name);
        }

        public Context StackNewContext(IGenerationElement element) => new Context(this, element);

        public IEnumerable<IGenerationElement> GetElements()
        {
            return _symbols.Values;
        }

        public T GetElementAs<T>(IPositioned element)
        {
            if (Element.GetType() != typeof(T))
            {
                throw new TypeMismatchException(element, typeof(T).Name, Element.GetType().Name);
            }
            
            return (T)Element;
        }
    }
}