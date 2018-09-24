using System;
using System.Collections.Generic;
using System.IO;

namespace Nexus
{
    public interface IClassMember : IPrintable
    {
        void Validate();

        IEnumerable<TypeDefinition> UsedTypes();
    }
}
