using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Diagnostics
{
    [Script(Implements = typeof(global::System.Diagnostics.Debugger))]
    internal class __Debugger
    {
        [Script(OptimizedCode = "debugger;")]
        public static void Break()
        {
            
        }

        
    }
}
