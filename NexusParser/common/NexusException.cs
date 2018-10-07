using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        public UnexpectedScopeException(IPositioned element, Type got, IEnumerable<Type> expected)
            : base(element, $"unexpected scope for {element.GetType().Name}, got {got.Name}, expected {string.Join(" or ", expected.Select(i => i.Name))}")
        { }
    }
}