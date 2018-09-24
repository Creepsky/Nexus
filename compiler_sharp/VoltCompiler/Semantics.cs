using System;
using System.Collections.Generic;
using Nexus;

namespace Nexus
{
    public class Semantics
    {
        private readonly SymbolTable _globalSymbolTable = new SymbolTable();
        private readonly IList<TypeDefinition> _types = new List<TypeDefinition>();
        private readonly IDictionary<Type, INodeSemantic> _compiler = new Dictionary<Type, INodeSemantic>()
        {
            {typeof(Class), new ClassNodeSemantic()},
            {typeof(Variable), new VariableNodeSemantic()},
            {typeof(Function), new FunctionNodeSemantic()}
        };

        public void Compile(Class c)
        {
            Compile(c, _globalSymbolTable);
        }

        public void Compile<T>(T instance, SymbolTable outerSymbolTable)
        {
            if (!_compiler.ContainsKey(typeof(T)))
                throw new ArgumentOutOfRangeException(nameof(instance), instance, "Unable to compile type");

            _compiler[typeof(T)].Check(instance, this, outerSymbolTable);
        }

        public void AddType(TypeDefinition type)
        {
            _types.Add(type);
        }
    }

    public interface INodeSemantic
    {
        void Check(object node, Semantics semantics, SymbolTable outerSymbolTable);
    }

    public abstract class NodeSemantic<T> : INodeSemantic
    {
        public void Check(object node, Semantics semantics, SymbolTable outerSymbolTable)
        {
            if (node.GetType() != typeof(T))
                throw new ArgumentOutOfRangeException(nameof(node), node, "Unable to compile instance");

            var symbolTable = new SymbolTable(outerSymbolTable);

            Check((T)node, semantics, symbolTable);
        }

        protected abstract void Check(T node, Semantics semantics, SymbolTable symbolTable);
    }

    public class ClassNodeSemantic : NodeSemantic<Class>
    {
        protected override void Check(Class node, Semantics semantics, SymbolTable symbolTable)
        {
            // create the symbol table
            foreach (var i in node.Members)
            {
                if (i.GetType() == typeof(Variable))
                {
                    var variable = (Variable) i;
                    symbolTable.AddSymbol(variable.Name, variable);
                    semantics.AddType(variable.Type);
                }
                else if (i.GetType() == typeof(Function))
                {
                    var function = (Function) i;
                    symbolTable.AddSymbol(function.Name, function);
                    semantics.AddType(function.ReturnType);
                    foreach (var j in function.Parameters)
                        semantics.AddType(j.Type);
                }
            }

            foreach (var i in node.Members)
            {
                if (i.GetType() == typeof(Variable))
                    semantics.Compile((Variable) i, symbolTable);
                else if (i.GetType() == typeof(Function))
                    semantics.Compile((Function) i, symbolTable);
            }
        }
    }

    public class VariableNodeSemantic : NodeSemantic<Variable>
    {
        protected override void Check(Variable node, Semantics semantics, SymbolTable symbolTable)
        {
            if (node.Initialization.IsDefined)
            {
            }
        }
    }

    public class FunctionNodeSemantic : NodeSemantic<Function>
    {
        protected override void Check(Function node, Semantics semantics, SymbolTable symbolTable)
        {
            
        }
    }
}