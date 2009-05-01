using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Alchemy.Headers;

namespace ScriptCoreLib.Alchemy.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Math))]
	internal static class __Math
	{
		public static double Sin(double e)
		{
			return math_h.sin(e);
		}

		public static double Cos(double e)
		{
			return math_h.cos(e);
		}

		public static double Sqrt(double e)
		{
			return math_h.sqrt(e);
		}

		public static double Pow(double x, double y)
		{
			return math_h.pow(x, y);
		}
	}

}
