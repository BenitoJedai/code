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
    [Script]
    public class ViewInfo
    {
        public double Left;
        public double Right;
        public double Target;

        public bool IsInView;

        public void Update()
        {
            IsInView = true;

            var w = 360.DegreesToRadians();


            if (Right < Left)
            {
                Right += w;

                if (Target < Right)
                    if (Left < Target)
                        return;

                Left -= w;
                Right -= w;


            }

            if (Target < Right)
                if (Left < Target)
                    return;

            IsInView = false;

            return;
        }
    }


    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class MyExtensions
    {
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

        public static Func<T> LoadBytes<T>(this ByteArray e, Action<T> done) where T : DisplayObject
        {
            var loader = new Loader();

            loader.contentLoaderInfo.complete +=
                delegate
                {
                    done((T)loader.content);
                };

            loader.loadBytes(e
                , new LoaderContext(false, ApplicationDomain.currentDomain, null)
                );

            return () => (T)loader.content;
        }

       

        public static bool IsOutOfView(this double e, double left, double right)
        {


            if (right < left)
            {
                right += DegreesToRadians(360);

                if (e < right)
                    if (left < e)
                        return true;

                left -= DegreesToRadians(360);
                right -= DegreesToRadians(360);


            }

            if (e < right)
                if (left < e)
                    return true;

            return false;
        }

        public static double DegreesToRadians(this int Degrees)
        {
            return (Math.PI * 2) * Degrees / 360;
        }

        public static int RadiansToDegrees(this double Arc)
        {
            return (int)(360 * Arc / (Math.PI * 2));
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

        public static double GetRotation(this Point p)
        {
            var x = p.x;
            var y = p.y;

            if (x == 0)
                if (y < 0)
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
}
