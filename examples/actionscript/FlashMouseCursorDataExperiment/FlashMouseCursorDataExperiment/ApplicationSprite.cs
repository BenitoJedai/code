using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.Extensions;
using System;

namespace FlashMouseCursorDataExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // http://www.adobe.com/devnet/flash/articles/avoiding-mistakes-assets.html

            // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/ui/MouseCursorData.html

            //Returns a Vector containing 8 cursor images
            Func<Vector<BitmapData>> makeCursorImages = () =>
            {
                var cursorData = new Vector<BitmapData>();

                var cursorShape = new Shape();
                cursorShape.graphics.beginFill(0xff5555, .75);
                cursorShape.graphics.lineStyle(1);

                //Graphics path data for an arrow
                var cursorPoints = new double[] { 0, 8, 16, 8, 16, 0, 24, 12, 16, 24, 16, 16, 0, 16, 0, 8 };
                var cursorDrawCommands = new int[] { 1, 2, 2, 2, 2, 2, 2, 2 };



                cursorShape.graphics.drawPath(
                    cursorDrawCommands,
                    cursorPoints
                );

                cursorShape.graphics.endFill();
                var transformer = new Matrix();

                //Rotate and draw the arrow shape to a BitmapData object for each of 8 frames 
                for (var i = 0; i < 8; i++)
                {
                    var cursorFrame = new BitmapData(32, 32, true, 0);
                    cursorFrame.draw(cursorShape, transformer);
                    cursorData.push(cursorFrame);

                    transformer.translate(-15, -15);
                    transformer.rotate(0.785398163);
                    transformer.translate(15, 15);
                }
                return cursorData;
            };


            // wtf? missing api? not defined in air?
            var mouseCursorData = new MouseCursorData();
            mouseCursorData.data = makeCursorImages();
            mouseCursorData.frameRate = 1;

            Mouse.registerCursor("spinningArrow", mouseCursorData);
            Mouse.cursor = "spinningArrow";
        }




    }
}
