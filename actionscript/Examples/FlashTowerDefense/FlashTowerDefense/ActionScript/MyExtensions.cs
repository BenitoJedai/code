using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    static class MyExtensions
    {
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
