using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitJeepControl.Library
{
    public static class X
    {
        public static IEnumerable<T> AsEnumerable<T>(this Vector<T> e)
        {
            return Enumerable.Range(0, (int)e.length).Select(i => e[i]);
        }

        //[Script(OptimizedCode = "s.flatten();")]
        //public static void __flatten(this Sprite s)
        //{
        //}

        public static double GetLength(this __vec2 p)
        {
            return Math.Sqrt(p.x * p.x + p.y * p.y);
        }

        public static double DegreesToRadians(this double Degrees)
        {
            return (Math.PI * 2) * Degrees / 360;
        }

        public static double DegreesToRadians(this int Degrees)
        {
            return (Math.PI * 2) * Degrees / 360;
        }

        public static int RadiansToDegrees(this double Arc)
        {
            return (int)(360 * Arc / (Math.PI * 2));
        }

        public static double GetRotation(this __vec2 p)
        {
            var x = p.x;
            var y = p.y;

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
    }

}
