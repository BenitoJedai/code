using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.media;

namespace FlashTowerDefense.ActionScript
{

    [Script]
    static class MyExtensions
    {
        public static int ToInt32(this double e)
        {
            return Convert.ToInt32(e);
        }

        public static T AddTo<T>(this T e, List<T> a)
        {
            a.Add(e);

            return e;
        }

        public static T RemoveFrom<T>(this T e, List<T> a)
        {
            a.Remove(e);

            return e;
        }

        public static Action ToAction(this Sound c)
        {
            return delegate { c.play(); };
        }

        public static Timer AtDelay(this int e, Action<Timer> a)
        {
            var t = new Timer(e, 1);

            t.timer += delegate { a(t); };

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

        public static void Dipsose(this DisplayObject e)
        {
            if (e.parent != null)
                e.parent.removeChild(e);

        }

        [Script(OptimizedCode = "return new c();")]
        public static object CreateType(this Class c)
        {
            return default(object);
        }

        public static SoundAsset ToSoundAsset(this Class c)
        {
            return (SoundAsset)c.CreateType();
        }

        public static BitmapAsset ToBitmapAsset(this Class c)
        {
            return (BitmapAsset)c.CreateType();
        }

        public static DisplayObject SetCenteredPosition(this DisplayObject e, double x, double y)
        {
            e.x = x - e.width / 2;
            e.y = y - e.height / 2;

            return e;
        }

        public static double Random(this int e)
        {
            return (Math.Round(new Random().NextDouble() * e));
        }

        public static void Times(this double e, Action h)
        {
            e.Round().Times(h);
        }

        public static void Times(this int e, Action h)
        {
            for (int i = 0; i < e; i++)
            {
                h();
            }
        }

        public static int Round(this double e)
        {
            return (int)(Math.Round(e));
        }
    }
}
