using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics
{
    // http://referencesource.microsoft.com/#System/compmod/system/diagnostics/Debug.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/System/System/Diagnostics/Debug.cs

    [Script(Implements = typeof(global::System.Diagnostics.Debug))]
    internal class __Debug
    {
        // used by ?

        [Conditional("DEBUG")]
        public static void Assert(bool condition)
        {
            if (!condition)
                throw new Exception("Assert failed");
        }

        [Conditional("DEBUG")]
        public static void Assert(bool condition, string message)
        {
            if (!condition)
                throw new Exception("Assert failed: " + message);
        }
    }
}
