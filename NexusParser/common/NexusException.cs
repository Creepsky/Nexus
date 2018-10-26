using System;
using System.Collections.Generic;
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
            : this(element.FilePath, element.Line, element.Column, message)
        {
        }

        public PositionedException(string filePath, int line, int column, string message)
            : base($"{message} in file {filePath} at line {line}, col {column}")
        {
        }
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

    public class NotFoundException : PositionedException
    {
        public NotFoundException(IPositioned element, string type, string name)
            : base(element, $"could not find the {type} with the name {name}")
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
            : base(overwritingElement, $"first declaration of {name} in file {oldElement.FilePath} " +
                                       $"at line {oldElement.Line}, col {oldElement.Column}, redeclaration")
        { }
    }

    public class UnexpectedCallException : PositionedException
    {
        public UnexpectedCallException(IPositioned element, string className, string functionName)
            : base(element, $"Unexpected call of function '{functionName}' of class {className}")
        { }
    }
}