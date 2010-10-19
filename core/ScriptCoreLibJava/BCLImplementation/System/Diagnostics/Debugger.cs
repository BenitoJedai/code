using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Diagnostics
{
    [Script(Implements = typeof(global::System.Diagnostics.Debugger))]
    internal class Debugger
    {
        public static bool IsAttached { get; internal set; }

        public static void Break()
        {
            // debugger? :)
        }

    }
}
