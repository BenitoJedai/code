using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using DOM;

    [Script(Implements = typeof(global::System.Math))]
    internal class __Math
    {
        // inline static methods optimization needed

        [Script(ExternalTarget = "Math")]
        readonly static IMath m;

        public static double Floor(double d) { return m.floor(d); }
        public static double Atan(double d) { return m.atan(d); }
        public static double Cos(double d) { return m.cos(d); }
        public static double Sin(double d) { return m.sin(d); }
        public static double Abs(double e) { return m.abs(e); }
    }
}
