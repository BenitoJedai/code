using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashKeyboard.ActionScript
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

        public static Action<Event> ToEvent(this SoundAsset c)
        {
            return delegate { c.play(); };
        }

        public static Action<MouseEvent> ToMouseEvent(this SoundAsset c)
        {
            return delegate { c.play(); };
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
    }
}
