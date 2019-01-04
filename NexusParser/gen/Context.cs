using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Nexus.common;
using Nexus.ir;
using Nexus.ir.stmt;

namespace Nexus.gen
{
    public class Context : ICloneable
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
            var current = Get(name);

            if (current != null)
            {
                if (current is Function extensionFunction)
                {
                    extensionFunction.AddOverload((Function)element);
                }
                else if (current is Class c)
                {
                    c.AddVariant((Class)element);
                }
                else
                {
                    throw new RedeclarationException(element, current, name);
                }
            }
            else
            {
                symbols.Add(name, element);
            }
        }

        public void Remove(string name, IPositioned caller)
        {
            _symbols.Remove(name);
        }

        public T Get<T>(string name, IPositioned caller) where T : class, IGenerationElement
        {
            var element = Get(name, caller);

            if (!(element is T elementCast))
            {
                throw new TypeMismatchException(caller, typeof(T).Name, element.GetType().Name);
            }

            return elementCast;
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
            Debug.Assert(!string.IsNullOrEmpty(name), "Empty name given");
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

        public object Clone()
        {
            return new Context(
                (Context) UpperContext?.Clone(),
                Element,
                new Dictionary<string, IGenerationElement>(_symbols)
            );
        }
    }
}