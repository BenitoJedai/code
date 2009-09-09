using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace MatrixTransformCExample.js
{
	[Script]
	static class Extensions
	{
		public static double DegreesToRadians(this int Degrees)
		{
			return (Math.PI * 2) * Degrees / 360;
		}

		public static int RadiansToDegrees(this double Arc)
		{
			return System.Convert.ToInt32(360 * Arc / (Math.PI * 2));
		}

		public static int Random(this int i)
		{
			return new Random().Next(i);
		}
	}
}
