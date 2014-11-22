using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibNative.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.EqualityComparer<>))]
    internal class __EqualityComparer<T>
    {
        // for other languages we dont have it?

        public static EqualityComparer<T> Default { get { return (EqualityComparer<T>)(object)new __EqualityComparer<T>(); } }

        // TestFunc.exe.c(109) : error C2065: 'false' : undeclared identifier
        // Error	1	Operator '==' cannot be applied to operands of type 'T' and 'T'	X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Collections\Generic\EqualityComparer.cs	17	47	ScriptCoreLibNative
        public bool Equals(T x, T y) { return (object)x == (object)y; }
        public int GetHashCode(T obj) { return 0; }
    }
}
