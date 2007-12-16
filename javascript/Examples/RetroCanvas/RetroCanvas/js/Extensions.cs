using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;

namespace RetroCanvas.js
{
    [Script]
    static class Extensions
    {
        public static void AtInterval(this int x, Action<Timer> h, Func<bool> stop, Action done)
        {
            new Timer(
                t =>
                {
                    h(t);

                    if (stop())
                    {
                        t.Stop();
                        done();
                    }

                }, x, x);
        }

        public static void AtInterval(this int x, Action<Timer> h)
        {
            new Timer(t => h(t), x, x);
        }

        public static void AtTimeout(this int x, Action<Timer> h)
        {
            new Timer(t => h(t), x, 0);
        }

        public static T cloneNode<T>(this T x) where T : INode
        {
            return (T)x.cloneNode(false);
        }

        public static T[] ToArray<T>(params T[] e)
        {
            return e;
        }

        public static T[] ToArray<TSource, T>(this IEnumerable<TSource> e, Func<TSource, T> f)
        {
            return e.Select(f).ToArray();
        }

        public static void ToWindowText(this Type e, string s)
        {
            if (string.IsNullOrEmpty(s))
                Native.Window.document.title = e.Name;
            else
                Native.Window.document.title = e.Name + " - " + s;
        }

        public static void ToWindowText(this Type e)
        {
            ToWindowText(e, null);
        }

        public static int Random(this int i)
        {
            return new Random().Next(i);
        }
    }
}
