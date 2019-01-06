using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="element">The throwing element.</param>
        /// <param name="type">The name of the type of the not found element.</param>
        /// <param name="name">The name that could not be found.</param>
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

    public class ClassVariantNotFoundException : PositionedException
    {
        public ClassVariantNotFoundException(IPositioned element, string className)
            : base(element, CreateErrorMessage(className, new string[]{}))
        { }

        public ClassVariantNotFoundException(IPositioned element, string className, string[] types)
            : base(element, CreateErrorMessage(className, types))
        { }

        private static string CreateErrorMessage(string className, string[] types)
        {
            var errorMessage = $"Could not find a matching variant of the class {className}";

            if (types != null)
            {
                errorMessage += $" with the template types {string.Join(", ", types)}";
            }

            return errorMessage;
        }
    }

    public class OverloadingFunctionNotFound : PositionedException
    {
        public OverloadingFunctionNotFound(IPositioned element, string className, string functionName)
            : base(element, CreateErrorMessage(className, functionName, new string[]{}))
        { }

        public OverloadingFunctionNotFound(IPositioned element, string functionName, string[] parameterTypes)
            : base(element, CreateErrorMessage(string.Empty, functionName, parameterTypes))
        { }

        public OverloadingFunctionNotFound(IPositioned element, string className, string functionName, string[] parameterTypes)
            : base(element, CreateErrorMessage(className, functionName, parameterTypes))
        { }

        private static string CreateErrorMessage(string className, string functionName, string[] parameterTypes)
        {
            var errorMessage = $"Could not find a matching variant of the function {functionName}";

            if (!string.IsNullOrWhiteSpace(className))
            {
                errorMessage += $" for the class {className}";
            }

            if (parameterTypes.Any())
            {
                errorMessage += $" and the parameter types {string.Join(", ", parameterTypes)}";
            }

            return errorMessage;
        }
    }

    public class TemplateGenerationException : PositionedException
    {
        public TemplateGenerationException(IGenerationElement element, string message)
            : base(element, $"Could not instantiate template for {element.Name}: {message}")
        { }
    }

    public class CircularReferenceException : NexusException
    {
        public CircularReferenceException(string className, string circularReferencedByClassName)
            : base($"{className} is circular referenced by {circularReferencedByClassName}")
        {}
    }
}