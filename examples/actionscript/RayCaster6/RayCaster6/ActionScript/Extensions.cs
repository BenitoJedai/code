using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.utils;

namespace RayCaster6.ActionScript
{



    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class MyExtensions
    {

        public static void Multiple<T>(this Action<T> h, List<KeyValuePair<int, T>> e)
        {
            foreach (var v in e)
            {
                if (v.Key > 0)
                    for (int i = 0; i < v.Key; i++)
                    {
                        h(v.Value);
                    }
            }
        }

        public static void Do<T>(this T a, Action<T> e)
        {
            e(a);
        }

        static readonly Rectangle fillRect_rect = new Rectangle();

        public static void fillRect(this BitmapData e, int x, int y, int w, int h, uint color)
        {
            fillRect_rect.x = x;
            fillRect_rect.y = y;
            fillRect_rect.width = w;
            fillRect_rect.height = h;

            e.fillRect(fillRect_rect, color);
        }

        public static Point MoveToArc(this Point e, double direction, double distance)
        {
            var p = new Point(e.x, e.y);
            p.x += Math.Cos(direction) * distance;
            p.y += Math.Sin(direction) * distance;

            return p;
        }

        public static void To(this Point p, double x, double y)
        {
            p.x = x;
            p.y = y;
        }

        public static T Take<T>(this IEnumerator<T> e)
        {
            if (e.MoveNext())
                return e.Current;


            throw new Exception("source is empty");
        }

        public static T[] Take<T>(this IEnumerator<T> e, int length)
        {
            var a = new T[length];

            for (int i = 0; i < length; i++)
            {
                a[i] = e.Take();
            }

            return a;
        }

        public static T TakeOrDefault<T>(this IEnumerator<T> e)
        {
            var r = default(T);

            if (e.MoveNext())
                r = e.Current;


            return r;
        }

        public static Timer AtInterval(this int e, Action<Timer> a)
        {
            var t = new Timer(e);

            t.timer += delegate { a(t); };

            t.start();

            return t;
        }







        public static int Max(this int i, int e)
        {
            if (e > i)
                return e;

            return i;
        }

        public static int Min(this int i, int e)
        {
            if (e < i)
                return e;

            return i;
        }

        public static double Min(this double i, double e)
        {
            if (e < i)
                return e;

            return i;
        }

        public static void drawLine(this BitmapData e, uint color, double x, double y, double cx, double cy)
        {
            e.drawLine(color, (int)x, (int)y, (int)cx, (int)cy);
        }
        public static void drawLine(this BitmapData e, uint color, int x, int y, int cx, int cy)
        {
            e.@lock();



            var dx = cx - x;
            var dy = cy - y;


            Action<int, int> setPixel = (mul, div) =>
                e.setPixel32((x + dx * mul / div), (y + dy * mul / div), color);

            var len = new Point { x = dx, y = dy }.length.Floor().Min(64);

            if (len > 2)
            {
                for (int i = 0; i < len + 1; i++)
                {
                    setPixel(i, len);
                }
            }
            else
            {
                setPixel(0, 1);
                setPixel(1, 1);
            }



            e.unlock();
        }


        public static int Floor(this int e)
        {
            return e;
        }

        //[Script(OptimizedCode = "return int(e);")]
        //[Script(IsDebugCode = true)]
        public static int Floor(this double e)
        {
            return (int)e;
        }

    }

    [Script]
    public class KeyValuePairList<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
    {
        public KeyValuePairList()
        {

        }
        
        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

    }
}
