using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Math))]
    internal class __Math
    {
		public static double Floor(double a)
		{
			return global::java.lang.Math.floor(a);
		}

        public static double Sin(double a)
        {
            return global::java.lang.Math.sin(a);
        }

        public static double Cos(double a)
        {
            return global::java.lang.Math.cos(a);
        }

        public static double Tan(double a)
        {
            return global::java.lang.Math.tan(a);
        }
    }
}
