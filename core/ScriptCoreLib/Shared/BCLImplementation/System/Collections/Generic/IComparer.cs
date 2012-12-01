using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.IComparer<>))]
    public interface __IComparer<T>
    {
        int Compare(T x, T y);

    }
}
