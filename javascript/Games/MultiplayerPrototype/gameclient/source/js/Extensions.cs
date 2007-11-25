using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Runtime;

namespace gameclient.source.js
{
    [Script]
    static class Extensions
    {
        public static Timer Swap<T>(this T[] e, int interval, Action<T> h)
        {
            return new ScriptCoreLib.JavaScript.Runtime.Timer(
                t =>
                {
                    h(e[t.Counter % e.Length]);


                }, interval.Random(), interval);
        }
        public static int Random(this int e)
        {
            return new System.Random().Next(e);
        }
        public static double Random(this double e)
        {
            return new System.Random().NextDouble() * e;
        }

        public static double GetRange(this Point a, Point b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;

            return System.Math.Sqrt(dx * dx + dy * dy);
        }

        public static double GetAngle(this Point p, double _x, double _y)
        {
            var x = p.X - _x;
            var y = p.Y - _y;

            if (x == 0)
                if (_y < 0)
                    return System.Math.PI / 2;
                else
                    return (System.Math.PI / 2) * 3;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += System.Math.PI;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }


        public static int ToInt32(this string e)
        {
            var dummy = 0;

            return int.Parse(e);
        }

        public static int ToInt32(this double e)
        {
            var dummy = 0;

            return System.Convert.ToInt32(e);
        }


        public static IEnumerable<T> Range<T>(this int count, Func<int, T> s)
        {
            return count.Range().Select(s);
        }


        public static IEnumerable<int> Range(this int count)
        {
            return Enumerable.Range(0, count);
        }
    }
}
