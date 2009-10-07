using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WavePlayer.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Math))]
	internal class __Math
	{
		public static int Sign(double e)
		{
			if (e == 0)
				return 0;

			if (e < 0)
				return -1;

			return 1;
		}

		public static double Sin(double e)
		{
			return math_h.sin(e);
		}

		public static double Cos(double e)
		{
			return math_h.cos(e);
		}

		public static short Abs(short e)
		{
			if (e < 0)
				return (short)-e;

			return e;
		}
	}

}
