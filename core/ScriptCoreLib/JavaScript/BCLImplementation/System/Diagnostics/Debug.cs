using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics
{
    [Script(Implements = typeof(global::System.Diagnostics.Debug))]
    internal class __Debug
    {

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
