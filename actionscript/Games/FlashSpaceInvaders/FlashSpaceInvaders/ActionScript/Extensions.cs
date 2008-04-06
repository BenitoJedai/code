using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashSpaceInvaders.ActionScript
{
    [Script]
    static class MyExtensions
    {
        public static T AnimateAt<T>(this T c, DisplayObject[] e, int interval)  where T:DisplayObjectContainer
        {
            var i = 0;

            c.addChild(e[i]);

            var t = new Timer(interval);

            t.timer +=
                delegate
                {
                    c.removeChild(e[i]);
                    
                    i = (i + 1) % e.Length;

                    c.addChild(e[i]);
                    e[i].x = -e[i].width / 2;
                    e[i].y = -e[i].height / 2;
                };

            t.start();

            return c;
        }
    }
}
