using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics
{
    [Script(Implements = typeof(global::System.Diagnostics.Debugger))]
    internal class __Debugger
    {
        public static bool IsAttached
        {
            get
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131224

                // would we know if the inspector is opened yet?

                return true;
            }
        }

        [Script(OptimizedCode = "debugger;")]
        public static void Break()
        {

        }


    }
}
