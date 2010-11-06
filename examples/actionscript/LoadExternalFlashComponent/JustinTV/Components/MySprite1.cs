using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;
using System;

namespace JustinTV.Components
{
    internal static class DynamicExtensions
    {
        // dynamic keyword please!!

        [Script(OptimizedCode = "content.api.play_live(channel);")]
        public static void api_play_live(this object content, string channel = "apidemo")
        {

        }
    }

    public sealed class MySprite1 : Sprite
    {
        public const int DefaultWidth = 640;
        public const int DefaultHeight = 480;

        public Sprite sprite2;

        public MySprite1()
        {
            // http://apiwiki.justin.tv/mediawiki/index.php/Live_Video_SWF_Documentation
            Security.allowDomain("*");
            Security.allowInsecureDomain("*");

            // http://apiwiki.justin.tv/mediawiki/index.php/Live_Video_SWF_Documentation

            //var TargetContent = "http://www.justin.tv/widgets/live_api_player.swf?video_height=480&video_width=640&consumer_key=YOUR_API_KEY";

            var TargetContent = "http://www.justin.tv/widgets/live_api_player.swf?video_height=480&video_width=640";

            var ldr = new Loader();
            var urlReq = new URLRequest(TargetContent);

            var ctx_app = ApplicationDomain.currentDomain;
            var ctx_sec = SecurityDomain.currentDomain;

            ctx_sec = null;
            ctx_app = null;

            __api_play_live = delegate
            {
            };

            ldr.contentLoaderInfo.complete +=
                delegate
                {
                    __api_play_live =
                        channel =>
                        {
                            ldr.content.api_play_live(channel);
                        };

                    __api_play_live("nitro301");
                    //(ldr.content as dynamic).api.play_live("apidemo");
                };

            var ctx = new LoaderContext(true, ctx_app, ctx_sec);
            sprite2 = new Sprite { z = 0.02 }.AttachTo(this);

            sprite2.mouseChildren = false;

            ldr.AttachTo(sprite2);

            var t = new Timer(1000 / 60);

            t.timer +=
                delegate
                {
                    var x = sprite2.x;
                    var y = sprite2.y;

                    sprite2.transform.matrix3D.appendTranslation(-x, -y, 0);
                    sprite2.transform.matrix3D.appendRotation(0.01, Vector3D.Y_AXIS);
                    sprite2.transform.matrix3D.appendRotation(0.02, Vector3D.X_AXIS);
                    sprite2.transform.matrix3D.appendTranslation(x, y, 0);

                };

            t.start();

            try
            {
                ldr.load(urlReq, ctx);

            }
            catch
            {
            }
        }


        Action<string> __api_play_live;
        public void api_play_live(string e)
        {
            try
            {
                __api_play_live(e);
            }
            catch
            {
            }
        }
    }
}
