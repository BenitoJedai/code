using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib;

namespace NatureBoy.js
{
    [Script]
    static class Extensions
    {
        public static double GetAngle(this ScriptCoreLib.Shared.Drawing.Point p, int _x, int _y)
        {
            var x = p.X - _x;
            var y = p.Y - _y;

            if (x == 0)
                return System.Math.PI / 2;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += System.Math.PI;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }

        public static string ToCSSImage(this string url)
        {
            return "url(" + url + ")";
        }

        public static Timer AutoRotate(this Dude e, double multiplier)
        {
            return new Timer(
                t => e.Rotation = System.Convert.ToInt32(t.Counter * multiplier), 0, 100
            );
        }

    }
}
