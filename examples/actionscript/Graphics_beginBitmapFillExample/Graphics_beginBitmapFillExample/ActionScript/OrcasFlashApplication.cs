using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.geom;
using System;
using ScriptCoreLib.ActionScript.flash.net;

namespace Graphics_beginBitmapFillExample.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class Graphics_beginBitmapFillExample : Sprite
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Graphics_beginBitmapFillExample()
        {
            // http://livedocs.adobe.com/flex/3/langref/flash/display/Graphics.html#beginBitmapFill()

            var request = new URLRequest(url);

            loader.load(request);

            loader.contentLoaderInfo.complete += drawImage;
            loader.contentLoaderInfo.ioError += ioErrorHandler;

        }

        private string url = "assets/Graphics_beginBitmapFillExample/image1.png";
        private Loader loader = new Loader();


        private void drawImage(Event e)
        {

            var mySprite = new Sprite();
            var myBitmap = new BitmapData((int)loader.width, (int)loader.height, false);

            myBitmap.draw(loader, new Matrix());


            Action<Matrix> update =
                matrix =>
                {


                    mySprite.graphics.beginBitmapFill(myBitmap, matrix, true);
                    mySprite.graphics.drawRect(100, 50, 200, 90);
                    mySprite.graphics.endFill();
                };

            this.mouseMove +=
                ev =>
                {
                    var matrix = new Matrix();

                    matrix.rotate(Math.PI / 4 * ev.stageX / this.stage.stageWidth);
                    update(matrix);
                };

            update(new Matrix());

            addChild(mySprite);
        }

        private void ioErrorHandler(IOErrorEvent e)
        {
            throw new Exception("Unable to load image: " + url + "\n" + e.text);
        }
    }
}