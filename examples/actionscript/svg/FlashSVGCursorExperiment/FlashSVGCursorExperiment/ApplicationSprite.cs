using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.Extensions;

namespace FlashSVGCursorExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {

            var c = new ActionScript.Images.MyCursor();

            //Error: Error #2071: The Stage class does not implement this property or method.
            //    at Error$/throwError()
            //    at flash.display::Stage/set contextMenu()
            //    at FlashSVGCursorExperiment::ApplicationSprite()[V:\web\FlashSVGCursorExperiment\ApplicationSprite.as:36]

            //this.stage.contextMenu = new ContextMenu();
            this.stage.showDefaultContextMenu = false;


            // http://stackoverflow.com/questions/16896432/mouseevent-right-mouse-down-rightmousedown-doesnt-work-in-flex4-6-web-runs
            this.stage.rightMouseDown +=
              e =>
              {
                  c.alpha = 0.3;
              };

            this.stage.rightMouseUp +=
              e =>
              {
                  c.alpha = 0.9;
              };


            //public static function remove_rightClick_4ebbe596_06001096(that:InteractiveObject, value:__Action_1):void
            //{
            //    CommonExtensions.RemoveDelegate_4ebbe596_060021d9((EventDispatcher(that)), value, MouseEvent.CLICK);
            //}

            // we are getting wrong bindings?
            // because of older playerglobal?

            // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/events/MouseEvent.html#RIGHT_CLICK
            // X:\jsc.svn\examples\actionscript\svg\FlashSVGCursorExperiment\FlashSVGCursorExperiment\ApplicationSprite.cs
            CommonExtensions.CombineDelegate(
                this.stage,
                new System.Action<MouseEvent>(
                    e =>
                    {
                        e.preventDefault();

                    }
                )
                ,
                "rightClick"
            );


            this.stage.rightClick +=
                e =>
                {
                    c.alpha = 1.0;
                };

            // http://stackoverflow.com/questions/377033/remove-the-right-click-menu-in-flash-9

            c.AttachTo(this);



            new ActionScript.Images.MyCursor().ToMouseCursor();

        }

    }


    public static class X
    {
        public static void ToMouseCursor(this Sprite c)
        {
            // http://www.kirupa.com/forum/showthread.php?274754-bitmapData-transparency

            var u = new BitmapData(32, 32, transparent: true, fillColor: 0x00FFFFFFu);

            var adjustAlpha = new ColorTransform();

            //adjustAlpha.alphaMultiplier = 0.5;

            var m = new Matrix();

            u.draw(c, m, adjustAlpha, BlendMode.NORMAL);


            var data = new BitmapData[] {
                    u
                };

            // X:\jsc.svn\examples\actionscript\FlashMouseCursorDataExperiment\FlashMouseCursorDataExperiment\ApplicationSprite.cs
			// partial build?
            var cursor = new MouseCursorData
            {
                data = data
            };

            // http://stackoverflow.com/questions/16004940/error-2136-swf-contains-invalid-data
            //Error: Error #2136: The SWF file file:///X|/jsc.svn/examples/actionscript/svg/FlashSVGCursorExperiment/FlashSVGCursorExperiment/bin/Debug/staging/FlashSVGCursorExperiment.ApplicationSprite/web/FlashSVGCursorExperiment.ApplicationSprite.swf contains invalid data.
            //    at FlashSVGCursorExperiment::ApplicationSprite()[V:\web\FlashSVGCursorExperiment\ApplicationSprite.as:34]


            Mouse.registerCursor("c", cursor);
            Mouse.cursor = "c";
        }
    }
}
