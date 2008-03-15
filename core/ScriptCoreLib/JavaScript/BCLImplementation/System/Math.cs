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
        public static double Ceiling(double d) { return m.ceil(d); }
        public static double Atan(double d) { return m.atan(d); }
        public static double Tan(double d) { return m.tan(d); }
        public static double Cos(double d) { return m.cos(d); }
        public static double Sin(double d) { return m.sin(d); }

        public static double Abs(double e) { return m.abs(e); }
        public static double Sqrt(double e) { return m.sqrt(e); }
        public static int Abs(int e) { return (int)m.abs(e); }
        public static double Round(double e) { return m.round(e); }

        public static byte Max(byte e, byte x) { return (byte)m.max(e, x); }
        public static int Max(int e, int x) { return m.max(e, x); }
        public static double Max(double e, double x) { return m.max(e, x); }

        public static byte Min(byte e, byte x) { return (byte)m.min(e, x); }
        public static int Min(int e, int x) { return m.min(e, x); }
        public static double Min(double e, double x) { return m.min(e, x); }

        public static int Sign(double d)
        {
            if (d == 0) return 0;
            if (d < 0) return -1;
            return 1;
        }

        public static double Pow(double e, double x)
        {
            return m.pow(e, x);
        }
    }
}
