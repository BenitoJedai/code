using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.Comparer<>))]
    internal abstract class __Comparer<T> : __IComparer<T>
    {
        public abstract int Compare(T x, T y);

    }
}
