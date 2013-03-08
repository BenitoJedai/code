using InteractivePromotionB.Components;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions.Avalon;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Extensions;
using System;
using ScriptCoreLib.ActionScript.flash.text;

namespace FlashHeatZeeker.Lobby
{
    [SWF(backgroundColor = 0xB38248, width = 800, height = 600, frameRate = 60)]
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();


        public YouTubePlayer ytp = null;

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {
                    this.stage.align = StageAlign.TOP_LEFT;
                    this.stage.scaleMode = StageScaleMode.NO_SCALE;

                    //this.stage.color = 0xB38248;


                    // read more: http://www.senocular.com/flash/tutorials/contentdomains/
                    Security.allowDomain("*");
                    Security.allowInsecureDomain("*");

                    var yNext = default(Action);

                    #region yinit
                    Action yinit = delegate
                    {
                        ytp.Loader.content.x = (this.stage.stageWidth - 1280) / 2;
                        ytp.Loader.content.y = (this.stage.stageHeight - 720) / 2;

                        this.stage.resize += delegate
                        {
                            if (this.stage == null)
                                return;

                            ytp.Loader.content.x = (this.stage.stageWidth - 1280) / 2;
                            ytp.Loader.content.y = (this.stage.stageHeight - 720) / 2;

                        };

                        content.AttachToContainer(this);
                        content.AutoSizeTo(this.stage);

                        yNext();
                    };
                    #endregion

                    // fails on android?
                    ytp = new YouTubePlayer(
                        DefaultVideo: "f9xV-LJCmV4",
                        //DefaultVideo: "qZni5895I-M",

                        // https://groups.google.com/forum/?fromgroups=#!topic/youtube-api-gdata/Bx5z7CrL9Cs
                        suggestedQuality: "medium",

                        yield_init: yinit
                    );


                    var yall = ytp["all", 0, 100000];

                    #region yNext
                    yNext = delegate
                    {
                        ytp.PlayScene(
                            yall,
                            delegate
                            {
                                if (ytp.CurrentVideoId == "qZni5895I-M")
                                    ytp.loadVideoById("f9xV-LJCmV4");
                                else
                                    ytp.loadVideoById("qZni5895I-M");


                                yNext();
                            }
                        );
                    };
                    #endregion

                    var te = new TextField { autoSize = TextFieldAutoSize.LEFT };

                    //.AttachTo(this);

                    ytp.StatusToClients += xx => te.text = xx;

                    var ys = new Sprite { mouseEnabled = false, mouseChildren = false }.AttachTo(this);
                    ytp.Loader.AttachTo(ys);




                    #region ToAnimatedOpacity
                    var o = content.i.ToAnimatedOpacity();
                    o.Opacity = 1.0;

                    var VideoPlayingOpacity = 1.0;

                    ytp.Playing += delegate
                    {
                        VideoPlayingOpacity = 0;
                        o.Opacity = VideoPlayingOpacity;
                    };

                    ytp.NotPlaying += delegate
                    {
                        VideoPlayingOpacity = 1;
                        o.Opacity = VideoPlayingOpacity;
                    };


                    var entero = content.enter.ToAnimatedOpacity();

                    entero.Opacity = 0.8;
                    content.enter.MouseEnter +=
                        delegate
                        {
                            entero.Opacity = 1;

                            o.Opacity = 1;
                        };

                    content.enter.MouseLeftButtonUp +=
                        delegate
                        {

                            if (StartClicked != null)
                                StartClicked();

                            entero.Opacity = 0.0;
                        };

                    content.enter.MouseLeave +=
                     delegate
                     {
                         entero.Opacity = 0.8;

                         // got video?
                         o.Opacity = VideoPlayingOpacity;
                     };
                    #endregion

                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);
                }
            );


        }

        public event Action StartClicked;

    }
}
