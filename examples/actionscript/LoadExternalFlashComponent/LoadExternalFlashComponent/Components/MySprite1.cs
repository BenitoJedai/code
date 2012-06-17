using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.system;
using System.Xml.Linq;
using System.Linq;
using System;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib;

namespace LoadExternalFlashComponent.Components
{
    internal static class DynamicYouTubePlayer
    {
        // dynamic keyword please!!

        [Script(OptimizedCode = "e.playVideo();")]
        public static void playVideo(this object e)
        {

        }

        // ArgumentError: Error #1063: Argument count mismatch on com.google.youtube.application::VideoApplication/resizeApplication(). Expected 2, got 0.
        [Script(OptimizedCode = "e.setSize(width, height);")]
        public static void setSize(this object e, int width, int height)
        {

        }


        [Script(OptimizedCode = "e.loadVideoById(videoId, startSeconds, suggestedQuality);")]
        public static void loadVideoById(this object e, string videoId, double startSeconds, string suggestedQuality)
        {

        }
    }

    internal sealed class MySprite1 : Sprite
    {
        public const int DefaultWidth = 800;
        public const int DefaultHeight = 600;

        public event Action<XElement> Inspecting;

        public event Action VideoPlayerReady;
        public event Action Ready;

        Action<string> InternalLoadTargetContent;

        public void LoadTargetContent(string e = "http://sketch.odopod.com/flash/OdoSketch.swf?sketchURL=/sketches/231498.xml&userURL=/users/21416&bgURL=/images/bigbg.jpg&mode=embed")
        {
            InternalLoadTargetContent(e);
        }

        public MySprite1()
        {
            // http://www.adobe.com/livedocs/flash/9.0/ActionScriptLangRefV3/flash/display/Loader.html


            InternalLoadTargetContent = TargetContent =>
            {
                this.OrphanizeChildren();

                // read more: http://www.senocular.com/flash/tutorials/contentdomains/
                Security.allowDomain("*");
                Security.allowInsecureDomain("*");
                //Security.loadPolicyFile("http://www.youtube.com/crossdomain.xml");

                // http://code.google.com/apis/youtube/flash_api_reference.html
                // http://code.google.com/p/gdata-samples/source/browse/trunk/ytplayer/actionscript3/com/google/youtube/examples/AS3Player.as

                var ldr = new Loader();
                var urlReq = new URLRequest(TargetContent);

                var ctx_app = ApplicationDomain.currentDomain;
                var ctx_sec = SecurityDomain.currentDomain;

                if (TargetContent.StartsWith("http://www.youtube.com/"))
                {
                    // http://www.youtube.com/crossdomain.xml
                    ctx_app = null;
                    ctx_sec = null;

                    // http://www.zedia.net/2010/using-the-actionscript-3-youtube-api/

                    DoplayVideo = delegate
                    {
                        ldr.content.playVideo();
                    };


                    DoloadVideoById = (id, s, q) =>
                    {
                        ldr.content.loadVideoById(id, s, q);
                    };

                    Action<Event> onReady = e =>
                    {
                        if (VideoPlayerReady != null)
                            VideoPlayerReady();

#if JSC_FEATURE_dynamic
                        dynamic player = ldr.content;

                        player.setSize(160, 120);
#endif
                        ldr.content.setSize(160, 120);
                    };

                    ldr.contentLoaderInfo.init +=
                        delegate
                        {
                            ldr.content.addEventListener("onReady", onReady.ToFunction(), false, 0, false);
                        };
                }



                ldr.contentLoaderInfo.complete +=
                    delegate
                    {
                        if (Ready != null)
                            Ready();
                    };

                //ldr.mouseChildren = false;

                var ctx = new LoaderContext(true, ctx_app, ctx_sec);
                ldr.load(urlReq, ctx);

                var sprite2 = new Sprite
                {
                    z = 0.001
                }.AttachTo(this);


                sprite2.graphics.drawRect(0, 0, 100, 100);

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

                DoClean =
                    delegate
                    {

                        ldr.content.GetChildren().Where(k => k.GetType().Name == "InfoPanel").ToArray().WithEach(
                            k => k.Orphanize()
                        );

                    };

                ldr.AttachTo(sprite2);

                var Inspect = default(Action<DisplayObject, XElement>);

                Inspect = (Target, Journal) =>
                {

                    var SourceType = Target.GetType();

                    var n = new XElement(SourceType.Name);

                    n.Add(new XAttribute("Namespace", SourceType.Namespace));

                    SourceType.BaseType.With(
                        BaseType =>
                            n.Add(new XAttribute("BaseType", BaseType.FullName))
                    );


                    Journal.Add(n);

                    (Target as DisplayObjectContainer).With(
                        Container =>
                        {
                            for (int i = 0; i < Container.numChildren; i++)
                            {
                                Inspect(Container.getChildAt(i), n);
                            }
                        }
                    );

                };

                DoInspect =
                    delegate
                    {
                        var doc = new XElement("Inspection");

                        // SecurityError: Error #2121: Security sandbox violation: Loader.content: http://localhost:26925/assets/LoadExternalFlashComponent.Application/LoadExternalFlashComponent.Components.MySprite1.swf cannot access http://sketch.odopod.com/flash/OdoSketch.swf?sketchURL=/sketches/231498.xml&userURL=/users/21416&bgURL=/images/bigbg.jpg&mode=embed. This may be worked around by calling Security.allowDomain.
                        //	at flash.display::Loader/get content()

                        try
                        {
                            Inspect(ldr.content, doc);
                        }
                        catch (Exception exc)
                        {
                            var n = new XElement("error", exc.Message);

                            doc.Add(n);
                        }

                        if (Inspecting != null)
                            Inspecting(doc);
                    };

            };

            LoadTargetContent();
        }

        Action DoInspect;
        public void Inspect()
        {
            DoInspect();
        }

        Action DoClean;
        public void Clean()
        {
            DoClean();
        }

        Action DoplayVideo;
        public void playVideo()
        {
            DoplayVideo();
        }

        Action<string, double, string> DoloadVideoById;
        public void loadVideoById()
        {
            DoloadVideoById("eK9lNtxH8-E", 0, null);
        }
    }
}
