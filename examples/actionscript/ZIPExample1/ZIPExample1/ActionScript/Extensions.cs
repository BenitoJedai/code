using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace ZIPExample1.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    public static class MyExtensions
    {


        public static Func<Bitmap> ToBitmap(this ByteArray e, Action<Bitmap> done)
        {
            var loader = new Loader();

            loader.contentLoaderInfo.complete +=
                delegate
                {
                    done((Bitmap)loader.content);
                };

            loader.loadBytes(e
                , new LoaderContext(false, ApplicationDomain.currentDomain, null)
                );

            return () => (Bitmap)loader.content;
        }

        public static Timer AtInterval(this int e, Action<Timer, Action<Action>> a)
        {
            var t = new Timer(e);

            var h = new List<Action>();

            t.timer += delegate
            {
                foreach (var v in h)
                    v();

                h.Clear();

                a(t, i => h.Add(i));
            };

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

        public static void Orphanize(this DisplayObject e)
        {
            if (e.parent != null)
                e.parent.removeChild(e);

        }
    }
}
