using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace starling.display
{

    public static class StarlingExtensions
    {
        public static IEnumerable<T> Hide<T>(this IEnumerable<T> e) where T : DisplayObject
        {
            e.WithEach(
                x => x.visible = false
            );

            return e;
        }

        public static T AttachTo<T>(this T e, DisplayObjectContainer x) where T : DisplayObject
        {
            if (e == null)
                return e;

            x.addChild(e);

            return e;
        }

        public static T Orphanize<T>(this T e) where T : DisplayObject
        {
            if (e != null)
                if (e.parent != null)
                    e.removeFromParent();


            return e;
        }

        public static T MoveTo<T>(this T e, double x, double y) where T : DisplayObject
        {
            if (e == null)
                return e;

            e.x = x;
            e.y = y;


            return e;
        }


    }

}
