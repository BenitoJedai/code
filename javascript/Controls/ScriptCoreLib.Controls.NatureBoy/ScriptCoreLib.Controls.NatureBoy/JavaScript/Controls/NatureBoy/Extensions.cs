using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.Controls.NatureBoy
{
    [Script]
    internal static class Extensions
    {
        public static IEnumerable<T> Range<T>(this int count, Func<int, T> s)
        {
            return count.Range().Select(s);
        }


        public static IEnumerable<int> Range(this int count)
        {
            return Enumerable.Range(0, count);
        }


        public static int Random(this int i)
        {
            return new Random().Next(i);
        }

        public static double GetDistance(this Point a, Point b)
        {
            return GetDistance(new Point<double> { X = a.X, Y = a.Y }, b.X, b.Y);
        }

        public static double GetDistance(this Point<double> a)
        {
            return GetDistance(a, 0, 0);
        }

        public static double GetDistance(this Point<double> a, Point<double> z)
        {
            return GetDistance(a, z.X, z.Y);
        }

        public static double GetDistance(this Point<double> a, double _x, double _y)
        {
            var dx = a.X - _x;
            var dy = a.Y - _y;

            return System.Math.Sqrt(dx * dx + dy * dy);
        }

        public static double GetRotation(this Point a, double _x, double _y)
        {
            return GetRotation(new Point<double> { X = a.X, Y = a.Y }, _x, _y);

        }

        public static double GetRotation(this Point<double> p, Point<double> z)
        {
            return GetRotation(p, z.X, z.Y);
        }

        public static double GetRotation(this Point<double> p, double _x, double _y)
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

        public static Timer Swap<T>(this T[] e, int interval, Action<T> h)
        {
            return new ScriptCoreLib.JavaScript.Runtime.Timer(
                t =>
                {
                    h(e[t.Counter % e.Length]);


                }, interval.Random(), interval);
        }

        public static int ToInt32(this int e)
        {
            return (int)Math.Round((double)e);
        }

        public static int ToInt32(this double e)
        {
            return (int)Math.Round((double)e);
        }

    }
}
