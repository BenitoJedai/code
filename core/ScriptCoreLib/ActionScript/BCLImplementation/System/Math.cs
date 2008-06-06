using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    // 
    [Script(Implements = typeof(global::System.Math))]
    internal class __Math
    {
        public static double Sin(double e)
        {
            return Math.sin(e);
        }

        public static double Cos(double e)
        {
            return Math.cos(e);
        }

        public static double Round(double e)
        {
            return Math.round(e);
        }
    }
}
