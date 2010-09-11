using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.BCLImplementation.Microsoft.VisualBasic.CompilerServices
{
    [Script(Implements = typeof(global::Microsoft.VisualBasic.CompilerServices.Conversions))]
    internal class __Conversions
    {
        // to be inlined!
        // jsc should be inlineing static onliners.

        public static string ToString(double Value)
        {
            return "" + Value;
        }

        public static string ToString(DateTime Value)
        {
            return Value.ToString();
        }
    }
}
