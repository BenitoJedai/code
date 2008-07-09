using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Lambda;

namespace FlashZIndex.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class MyExtensions
    {
        public static double Random(this double e)
        {
            return new Random().NextDouble() * e;
        }

        public static double Random(this double e, double from, double to)
        {
            return (from * e) + ((to - from) * e).Random();
        }

        public static Action<T, int> ForEach<T>(this Action<T, int> e, IEnumerable<T> s)
        {
            LambdaExtensions.ForEach(s, e);
            return e;
        }


        // http://community.bartdesmet.net/blogs/bart/archive/2008/07/05/1-to-3-ruby-style-internal-iterators-in-c.aspx
        public static Action<Action<int>> To(this int from, int to)
        {
            return a => { for (int i = from; i <= to; i++) a(i); };
        }

         

        public static Timer AtDelayDo(this int e, Action a)
        {
            var t = new Timer(e, 1);

            t.timer += delegate { a(); };

            t.start();

            return t;
        }

        public static Action ThrottleTo(this Action a, int ms)
        {
            var f = false;
            var r = default(Action);
            var t = ms.AtDelayDo(
                 delegate
                 {
                     if (f)
                     {
                         f = false;
                         r();
                     }
                 }
             );

            r = delegate
            {
                if (!t.running)
                {
                    t.start();
                    a();
                }
                else
                {
                    f = true;
                }
            };

            return r;
        }

        public static double Random(this int e)
        {
            return (Math.Round(new Random().NextDouble() * e));
        }

        public static int ToInt32(this double e)
        {
            return Convert.ToInt32(e);
        }
    }
}
