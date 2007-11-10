using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls.Effects;

namespace NatureBoy.js
{
    [Script]
    static class Extensions
    {
        public static IHTMLElement AttachTo(this IHTMLElement e, IHTMLElement c)
        {
            c.appendChild(e);

            return e;
        }

        public static TweenDataDouble ToOpacityTween(this IHTMLElement e)
        {
            var t = new TweenDataDouble();

            t.ValueChanged +=
                delegate
                {
                    e.style.Opacity = 1 - t.Value;
                };

            return t;
        }

        public static void KeepInCenter(this IHTMLElement e)
        {
            Action MoveToCenter =
                delegate
                {
                    var w = Native.Window.Width;
                    var h = Native.Window.Height;


                    e.SetCenteredLocation(Native.Document.body.scrollLeft + w / 2, Native.Document.body.scrollTop + h / 2);

                };

            Native.Window.onresize += delegate { MoveToCenter(); };

            MoveToCenter();
        }
        public static string SerializeToJSON<T>(this T e)
            where T : class, new()
        {
            return Expando.Of(e).ToJSON();
        }

        public static IXMLDocument SerializeToXML<T>(this T e)
            where T : class, new()
        {
            return new IXMLSerializer<T>().Serialize(e);
        }

        public static T Deserialize<T>(this IXMLDocument e, object[] k)
            where T : class, new()
        {
            return new IXMLSerializer<T>(k).Deserialize(e);
        }

        public static void DownloadToXML(this string url, Action<IXMLHttpRequest> done)
        {
            new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET, url, i => done(i));
        }

        public static void DownloadToXML<T>(this string url, object[] KnownTypes, Action<T> done)
            where T : class, new()
        {
            url.DownloadToXML(
                r => done(r.responseXML.Deserialize<T>(KnownTypes))
            );

        }

        public static void SpawnTo(this string alias, Action<IHTMLElement> h)
        {
            ScriptCoreLib.JavaScript.Native.Spawn(alias, i => h(i));
        }


        public static IEnumerable<T> Range<T>(this int count, Func<int, T> s)
        {
            return count.Range().Select(s);
        }


        public static IEnumerable<int> Range(this int count)
        {
            return Enumerable.Range(0, count);
        }

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
            return e.Select(src => new IHTMLImage(src)).ToArray();
        }

        public static IHTMLDiv AttachToDocument(this string e)
        {
            var r = new IHTMLDiv(e);

            r.attachToDocument();

            return r;
        }


    }
}
