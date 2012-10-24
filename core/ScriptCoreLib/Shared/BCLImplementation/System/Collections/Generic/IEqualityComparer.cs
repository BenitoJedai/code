using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(IEqualityComparer<>))]
    internal interface __IEqualityComparer<T>
    {

        bool Equals(T x, T y);
     
        int GetHashCode(T obj);
    }
}
