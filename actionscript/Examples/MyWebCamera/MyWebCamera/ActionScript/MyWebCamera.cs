using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.filters;


namespace MyWebCamera.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class MyWebCamera : Sprite
    {
        public MyWebCamera()
        {
            var cam1 = Camera.getCamera();

            if (cam1 != null)
            {
                var vid1 = new Video();

                vid1.attachCamera(cam1);
                vid1.x = 40;
                vid1.y = 40;

                addChild(vid1);
            }


            var circle1 = new Sprite();

            for (var j = 0.0; j < 1; j += 0.1)
            {
                circle1.graphics.beginFill(0xff0000, j);
                circle1.graphics.drawCircle(40, 40, 40 * (1.0 - j));
                circle1.graphics.endFill();
            }

            addChild(circle1);


            var circle2 = new Sprite();

            for (var j = 0.0; j < 1; j += 0.1)
            {
                circle2.graphics.beginFill(0x00ff00, j);
                circle2.graphics.drawCircle(40 + 320, 40 + 240, 40 * (1.0 - j));
                circle2.graphics.endFill();
            }

            addChild(circle2);

            var shadow = new DropShadowFilter();

            addChild(
                new TextField
                {
                    text = "powered by jsc",
                    x = 20,
                    y = 40,
                    selectable = false,
                    sharpness = -400,
                    textColor = 0xffffff,
                    filters = new [] { shadow }
                }
            );

            // http://manfred.dschini.org/2007/06/12/as3-loading-external-images/
            var url = new URLRequest("assets/MyWebCamera/Preview.png");
            var loader = new Loader
                {
                    x = 320, y = 0, alpha = 0.6, filters = new [] { shadow }
                };

            loader.load(url);
            
            addChild(loader);
        }
    }

}
