using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormsAvalonAnimation
{
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
	}
}
