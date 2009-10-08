using System.Linq;

using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using System;
using ScriptCoreLib.ActionScript;

namespace FlashZIndex.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(backgroundColor = BackgroundColor)]
    public class FlashZIndex : Sprite
    {
		// upgrade to wpf: http://blogs.msdn.com/wpfsdk/archive/2006/06/13/Controlling-zOrder-using-the-ZIndex-Property.aspx

        public const uint BackgroundColor = 0xffffff;// (uint)0xffffff.Random();
        public const uint ForegroundColor = 0x202020;//(uint)(0xffffff ^ BackgroundColor);

        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashZIndex()
        {
            var c = new Sprite
            {
            };


            c.graphics.beginFill(BackgroundColor);
            c.graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight);
            c.graphics.endFill();

            Func<string, string, string> ToSize =
                (e, size) => "<font size='" + size + "'>" + e + "</font>";

            Func<string, string, string> ToColor =
                (e, color) => "<font color='#" + color + "'>" + e + "</font>";

            var keywords = new[] { "from", "this", " in", "orderby", " is", " descending", "select" };

            Func<string, string> Colorize =
                e =>
                {
                    foreach (var keyword in keywords.OrderByDescending(i => i.Length))
                    {
                        e = e.Replace(keyword, ToColor(keyword, "0000ff"));
                    }

                    return e;
                };

            var t = new TextField
            {
                autoSize = TextFieldAutoSize.LEFT,
                defaultTextFormat = new TextFormat
                {
                    size = 15,
                    color = ForegroundColor
                },
                //mouseEnabled = false,
                //filters = new[] { new DropShadowFilter() },
                multiline = true,
                condenseWhite = true,
                x = 8,
                htmlText =

                Colorize(

                    ToSize("flash zIndex example powered by <b>jsc</b>", "24") + "<br />"
                    + @"click on the background to create<br /> or drag to see zIndex in effect<br /><br />"
                    + ToColor(@"
                        from v in this.Children() <br />
                        orderby v is TextField descending, v.y, v.x <br />
                        select v
                    ", "000000")
                )
            };

            c.AttachTo(this);
            t.AttachTo(this);

            c.doubleClickEnabled = true;

            Action Reorder =
                delegate
                {
                    Action<DisplayObject, int> SetZIndex =
                        (k, i) => k.parent.setChildIndex(k, i);

                    SetZIndex.ForEach(
                        from v in this.Children()
                        orderby v is TextField descending, v.y, v.x
                        select v
                    );



                };

            Action ReorderThrottle = Reorder.ThrottleTo(500);

            c.click +=
                q =>
                {
                    var x = q.stageX;
                    var y = q.stageY;

                    AddSprite(ReorderThrottle, x, y);

                    ReorderThrottle();
                };

            10.To(90)(i => AddSprite(ReorderThrottle, stage.stageWidth * i / 100, stage.height.Random(0.4, 0.9)));

            Reorder();
        }

        private void AddSprite(Action Reorder, double x, double y)
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
                    foreach (InteractiveObject item in this.Children().Where(i => i is TextField))
                    {
                        item.mouseEnabled = false;
                    }

                    drag = new Point { x = e.localX, y = e.localY };
                };

            s.mouseUp +=
                e =>
                {
                    foreach (InteractiveObject item in this.Children().Where(i => i is TextField))
                    {
                        item.mouseEnabled = true;
                    }

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

            s.mouseWheel +=
                e =>
                {
                    s.scaleX = (s.scaleX + 0.5 * Math.Sign(e.delta)).Max(1).Min(4);
                    s.scaleY = (s.scaleY + 0.5 * Math.Sign(e.delta)).Max(1).Min(4);
                };

            s.MoveTo(x, y).AttachTo(this);
        }


    }
}