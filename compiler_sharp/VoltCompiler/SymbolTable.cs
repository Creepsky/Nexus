using System;
using System.Collections.Generic;

namespace Nexus
{
    public class Symbol
    {
        public string Name;
        public TypeDefinition Type;
    }

    public class SymbolTable
    {
        private readonly IDictionary<string, IStatement> _symbols = new Dictionary<string, IStatement>();
        private readonly SymbolTable _outerSymbolTable = null;

        public SymbolTable()
        { }

        public SymbolTable(SymbolTable outerSymbolTable)
        {
            _outerSymbolTable = outerSymbolTable;
        }

        public IStatement GetSymbol(string symbol) => !_symbols.ContainsKey(symbol) ? null : _symbols[symbol];

        public IStatement FindSymbol(string symbol) => GetSymbol(symbol) ?? _outerSymbolTable?.FindSymbol(symbol);

        public void AddSymbol(string symbol, IStatement value)
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