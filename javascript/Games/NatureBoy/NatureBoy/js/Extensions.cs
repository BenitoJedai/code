using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;

namespace NatureBoy.js
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

        public static double Random(this double e)
        {
            return new System.Random().NextDouble() * e;
        }

        public static int Random(this int e)
        {
            return new System.Random().Next(e);
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
                return System.Math.PI / 2;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += System.Math.PI;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }

        public static int ToInt32(this double e)
        {
            var dummy = 0;

            return System.Convert.ToInt32(e);
        }

        public static string ToCSSImage(this string url)
        {
            return "url(" + url + ")";
        }

        public static Timer AutoRotate(this Dude e, double multiplier)
        {
            return new Timer(
                t => e.Rotation16 = System.Convert.ToInt32(t.Counter * multiplier), 0, 100
            );
        }

        public static void AutoRotateToCursor(this Dude e, IHTMLElement stage)
        {
            stage.onmousemove +=
                delegate(IEvent ev)
                {

                    e.LookAt(ev.CursorPosition);

                };
        }

        public static IHTMLImage[] ToImages(this string[] e)
        {
            return e.Select(src => new IHTMLImage( src) ).ToArray();
        }

        public static IHTMLDiv AttachToDocument(this string e)
        {
            var r = new IHTMLDiv(e);
            
            r.attachToDocument();

            return r;
        }
    }
}
