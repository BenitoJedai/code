using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.IComparer<>))]
    internal interface IComparer<T>
    {
        int Compare(T x, T y);

    }
}
