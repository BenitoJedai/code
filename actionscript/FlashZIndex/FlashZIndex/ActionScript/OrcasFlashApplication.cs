using System.Linq;

using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using System;

namespace FlashZIndex.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashZIndex : Sprite
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashZIndex()
        {
            var c = new Sprite
            {
            };

            c.graphics.beginFill((uint)0xffffff.Random());
            c.graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight);
            c.graphics.endFill();


            c.AttachTo(this);

            c.doubleClickEnabled = true;

            Action Reorder =
                delegate
                {
                    var a = new DisplayObject[this.numChildren];

                    for (int i = 0; i < a.Length; i++)
                    {
                        a[i] = this.getChildAt(i);
                    }


                    a.OrderBy(i => i.y).ForEach(
                        (k, i) =>
                            this.setChildIndex(k, i)
                    );
                };

            c.doubleClick +=
                q =>
                {
                    var s = new Sprite
                    {
                        filters = new[] { new DropShadowFilter() }
                    };

                    s.graphics.beginFill((uint)0xffffff.Random());
                    s.graphics.drawRect(-16, -16, 32, 32);
                    s.graphics.endFill();

                    var drag = default(Point);

                    s.mouseDown +=
                        e =>
                        {
                            drag = new Point { x = e.localX, y = e.localY };
                        };

                    s.mouseUp +=
                        e =>
                        {
                            drag = null;
                        };

                    stage.mouseMove +=
                        e =>
                        {
                            if (drag == null)
                                return;

                            var p = new Point { x = e.stageX, y = e.stageY };

                            s.MoveTo(p - drag);

                            Reorder();
                        };

                    s.MoveTo(q.stageX, q.stageY).AttachTo(this);
                };

        }


    }
}