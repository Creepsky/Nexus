using System;
using System.Collections.Generic;

namespace Volt
{
    public class Symbol
    {
        public string Name;
        public TypeDefinition Type;
    }

    public class SymbolTable
    {
        private readonly IDictionary<string, IClassMember> _symbols = new Dictionary<string, IClassMember>();
        private readonly SymbolTable _outerSymbolTable = null;

        public SymbolTable()
        { }

        public SymbolTable(SymbolTable outerSymbolTable)
        {
            _outerSymbolTable = outerSymbolTable;
        }

        public IClassMember GetSymbol(string symbol) => !_symbols.ContainsKey(symbol) ? null : _symbols[symbol];

        public IClassMember FindSymbol(string symbol) => GetSymbol(symbol) ?? _outerSymbolTable?.FindSymbol(symbol);

        public void AddSymbol(string symbol, IClassMember value)
        {
            var overwriting = GetSymbol(symbol);

            if (overwriting != null)
            {
                // warning (?): overwriting
            }
            else
            {
                var shadowing = FindSymbol(symbol);

                if (shadowing != null)
                {
                    // warning: shadowing
                }
            }

            _symbols[symbol] = value;
        }
    }
}