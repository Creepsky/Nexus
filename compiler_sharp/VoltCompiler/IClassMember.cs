using System;
using System.Collections.Generic;
using System.IO;

namespace Volt
{
    public interface IClassMember : IPrintable
    {
        void Validate();

        IEnumerable<string> UsedTypes();
    }
}
