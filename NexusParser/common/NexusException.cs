using System;
using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir;

namespace Nexus.common
{
    public class NexusException : Exception
    {
        protected NexusException(string message)
            : base(message)
        { }
    }

    public class PositionedException : NexusException
    {
        public PositionedException(IPositioned element, string message)
            : base($"{message} at line {element.Line}, col {element.Column}")
        { }
    }

    public class NoScopeException : PositionedException
    {
        public NoScopeException(IPositioned element)
            : base(element, $"expected scope for {element.GetType()}")
        { }
    }

    public class UnexpectedScopeException : PositionedException
    {
        public UnexpectedScopeException(IPositioned element, string got, IEnumerable<string> expected)
            : base(element, $"unexpected scope for {element.GetType().Name}, got {got}, expected {string.Join(" or ", expected)}")
        { }
    }

    public class VariableNotFoundException : PositionedException
    {
        public VariableNotFoundException(IPositioned element, string name)
            : base(element, $"could not find variable {name}")
        { }
    }

    public class FunctionNotFoundException : PositionedException
    {
        public FunctionNotFoundException(IPositioned element, string name)
            : base(element, $"could not find function {name}")
        { }
    }

    public class TypeMismatchException : PositionedException
    {
        public TypeMismatchException(IPositioned element, string expected, string got)
            : this(element, new []{expected}, got)
        { }

        public TypeMismatchException(IPositioned element, IEnumerable<string> expected, string got)
             : base(element, $"expected {string.Join(" or ", expected)} but got {got}")
        { }
    }

    public class RedeclarationException : PositionedException
    {
        public RedeclarationException(IPositioned overwritingElement, IPositioned oldElement, string name)
            : base(overwritingElement, $"first declaration of {name} at line {oldElement.Line}, col {oldElement.Column}, redeclaration")
        { }
    }
}