using System.Collections.Generic;
using Nexus.common;
using Nexus.ir;
using Nexus.ir.stmt;

namespace Nexus.gen
{
    public class Context
    {
        public readonly Context UpperContext;
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
                throw new RedeclarationException(element, Get(name), name);

            _symbols.Add(name, element);
        }

        public T Get<T>(string name, IPositioned caller) where T : IGenerationElement
        {
            var element = Get(name);
            
            if (element == null)
                throw new NotFoundException(caller, nameof(T), name);
            
            if (element.GetType() != typeof(T))
                throw new TypeMismatchException(caller, nameof(T), element.GetType().Name);

            return (T) element;
        }

        public IGenerationElement Get(string name)
        {
            if (Contains(name))
                return _symbols[name];

            if (UpperContext != null)
                return UpperContext.Get(name);

            return null;
        }

        public Context StackNewContext(IGenerationElement element) => new Context(this, element);

        public IEnumerable<IGenerationElement> GetElements()
        {
            return _symbols.Values;
        }

        public T GetElementAs<T>(IPositioned element)
        {
            if (Element.GetType() != typeof(T))
                throw new TypeMismatchException(element, typeof(T).Name, Element.GetType().Name);
            
            return (T)Element;
        }
    }
}