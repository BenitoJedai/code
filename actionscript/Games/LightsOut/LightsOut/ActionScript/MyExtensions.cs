using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;

namespace LightsOut.ActionScript
{
    [Script]
    static class MyExtensions
    {
        public static void Times(this int e, Action h)
        {
            for (int i = 0; i < e; i++)
            {
                h();
            }
        }

        public static IEnumerable<T> Do<T>(this IEnumerable<T> s)
        {
            foreach (var v in s);

            return s;
        }

        public static T Aggregate<T>(this T e, Action<T> h)
        {
            h(e);
            return e;
        }

        public static Action TimerLoop<T>(this T e, double delay, Action<T> h)
        {
            var t = new Timer(delay);

            t.timer += ev => h(e);
            t.start();

            // compiler bug when writing ldtoken as string for native types
            return t.stop;
        }
    }
}
