using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

using System.Linq;

namespace MyWebCamera.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = 400, height = 300)]
    public class MyWebCamera : Sprite
    {
        public const string Path_Buzzer = "/assets/MyWebCamera/buzzer.mp3";

        [Embed(Path_Buzzer)]
        static Class Asset_Buzzer;

        public MyWebCamera()
        {
            var cam1 = Camera.getCamera();
            var snd = Asset_Buzzer.ToSoundAsset();

            var shadow = new DropShadowFilter();

            var status = new TextField
            {
                x = 20,
                y = 140,
                width = 200,
                text = "no motion yet",
                textColor = 0xffffff,
                filters = new[] { shadow }
            };

            if (cam1 != null)
            {
                var vid1 = new Video(400, 300);
                
                cam1.setMode(640, 480, 1000 / 24);
                cam1.activity +=
                    delegate
                    {
                        snd.play();

                        status.text = "last motion at " + new Date().toTimeString();
                    };


                
                vid1.attachCamera(cam1);
                vid1.x = 0;
                vid1.y = 0;

                vid1.AttachTo(this);
            }

            stage.scaleMode = StageScaleMode.NO_BORDER;


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


            var t = new TextField
            {
                text = "powered by jsc - " + Camera.names.Aggregate("",
                    (e, i) =>
                    {
                        if (!string.IsNullOrEmpty(e))
                            e += ", ";

                        return e + i;
                    })
                ,
                x = 20,
                y = 40,
                width = 400,
                selectable = false,
                sharpness = -400,
                textColor = 0xffffff,
                filters = new[] { shadow }
            }.AttachTo(this);

            t.doubleClickEnabled = true;
            t.doubleClick +=
                delegate
                {
                    this.stage.SetFullscreen(true);
                };

            // http://manfred.dschini.org/2007/06/12/as3-loading-external-images/
            var url = new URLRequest("assets/MyWebCamera/Preview.png");
            var loader = new Loader
                {
                    x = 320, y = 0, alpha = 0.6, filters = new [] { shadow }
                };

            loader.load(url);
            
            addChild(loader);

            status.AttachTo(this);
        }
    }

}
