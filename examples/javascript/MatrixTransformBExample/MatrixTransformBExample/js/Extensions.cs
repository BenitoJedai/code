using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace MatrixTransformBExample.js
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

		public static double GetRotation(double x, double y)
		{

			const double _180 = System.Math.PI;
			const double _90 = System.Math.PI / 2;
			const double _270 = System.Math.PI * 3 / 2;

			if (x == 0)
				if (y < 0)
					return _270;
				else if (y == 0)
					return 0;
				else
					return _90;

			if (y == 0)
				if (x < 0)
					return _180;
				else
					return 0;

			var a = System.Math.Atan(y / x);

			if (x < 0)
				a += _180;
			else if (y < 0)
				a += System.Math.PI * 2;


			return a;
		}



		public static void BlinkAt(this IHTMLElement e, int interval)
		{
			interval.AtIntervalWithCounter(
				counter =>
				{
					if (counter % 2 == 0)
					{
						e.style.Opacity = 0.3;
						return;
					}

					e.style.Opacity = 1;
				}
			);

		}

		public static void AtIntervalWithCounter(this int interval, Action<int> h)
		{
			var c = 0;
			new Timer(
				delegate
				{
					h(c);
					c++;
				}
			, 0, interval);
		}

		public static int Random(this int i)
		{
			return new Random().Next(i);
		}
	}
}
