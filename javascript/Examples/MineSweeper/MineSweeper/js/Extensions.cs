using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace MineSweeper.js
{
    [Script]
    static class Extensions
    {
        public static int GetDigit(this int value, int offset)
        {
            var x0 = Math.Pow(10, offset + 0);
            var x1 = Math.Pow(10, offset + 1);

            var y0 = (int)Math.Floor(value / x0);
            var y1 = (int)Math.Floor(value / x1);

            return y0 - (y1 * 10);
        }

        public static IEnumerable<T> Random<T>(this IEnumerable<T> source, int count)
        {
            var r = new List<T>();
            var x = source.Randomize().ToArray();

            if (count > x.Length)
                count = x.Length;

            for (int i = 0; i < count; i++)
            {
                x[i].AddTo(r);
            }

            return r;
        }

        public static int Abs(this int e)
        {
            return Math.Abs(e);
        }

        public static int ToInt32(this double e)
        {
            return Convert.ToInt32(e);
        }

        public static T AddTo<T>(this T e, IList<T> list)
        {
            list.Add(e);

            return e;
        }

        public static bool NoMouseButton(this IEvent e)
        {
            return e.MouseButton == IEvent.MouseButtonEnum.Unknown;
        }

        public static void replaceWith(this IHTMLElement e, IHTMLElement x)
        {
            e.parentNode.insertBefore(x, e);
            e.parentNode.removeChild(e);
        }

        public static void SetBackground(this IStyle s, string src)
        {
            s.SetBackground(src, false);
        }

        public static int Random(this int i)
        {
            return new Random().Next(i);
        }
    }
}
