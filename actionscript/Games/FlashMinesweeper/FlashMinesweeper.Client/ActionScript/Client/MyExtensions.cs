using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashMinesweeper.ActionScript.Client
{
    [Script]
    public static class MyExtensions
    {


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

        public static Timer AtDelayDo(this int e, Action a)
        {
            var t = new Timer(e, 1);

            t.timer += delegate { a(); };

            t.start();

            return t;
        }

    }
}
