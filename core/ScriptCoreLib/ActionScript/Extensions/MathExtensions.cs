using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	public static class MathExtensions
	{
		public static double DegreesToRadians(this int Degrees)
		{
			return (Math.PI * 2) * Degrees / 360;
		}

		public static int RadiansToDegrees(this double Arc)
		{
			return (int)(360 * Arc / (Math.PI * 2));
		}

		public static double GetRotation(this Point p)
        {
            var x = p.x;
            var y = p.y;

			const double _180 = System.Math.PI ;
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



	}
}
