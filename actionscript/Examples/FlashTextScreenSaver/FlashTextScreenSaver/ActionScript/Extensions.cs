using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.events;

namespace FlashTextScreenSaver.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class Extensions
    {
        public static int WheelDirection(this MouseEvent e)
        {
            return Math.Sign(e.delta);
        }

        public static Timer AtIntervalDo(this int e, Action a)
        {
            var t = new Timer(e);

            t.timer += delegate { a(); };

            t.start();

            return t;
        }

        public static Timer AtInterval(this int e, Action<Timer> a)
        {
            var t = new Timer(e);

            t.timer += delegate { a(t); };

            t.start();

            return t;
        }


        public static void FadeOutAndOrphanize(this DisplayObject e)
        {
            e.FadeOutAndOrphanize(200, 0.1);
        }

        public static void FadeOutAndOrphanize(this DisplayObject e, int timeout, double step)
        {
            timeout.AtInterval(
               t =>
               {
                   if (e.alpha < 0.1)
                   {
                       t.stop();
                       e.Orphanize();
                   }
                   else
                   {
                       e.alpha -= step;
                   }
               }
           );
        }

        public static Timer AtDelay(this int e, Action<Timer> a)
        {
            var t = new Timer(e, 1);

            t.timer += delegate { a(t); };

            t.start();

            return t;
        }

        public static int ToInt32(this string e)
        {
            return int.Parse(e);
        }


        public static int ToInt32(this double e)
        {
            return Convert.ToInt32(e);
        }


        public static double GetOffsetRight(this DisplayObject e)
        {
            return e.x + e.width;
        }

        public static double Random(this double e)
        {
            return new Random().NextDouble() * e;
        }

        public static double Random(this int e)
        {
            return (Math.Round(new Random().NextDouble() * e));
        }
    }
}
