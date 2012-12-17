using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Diagnostics
{
    [Script(Implements = typeof(global::System.Diagnostics.StackTrace))]
    internal class __StackTrace
    {
        public override string ToString()
        {
            return "<__StackTrace>";
        }
    }
}
