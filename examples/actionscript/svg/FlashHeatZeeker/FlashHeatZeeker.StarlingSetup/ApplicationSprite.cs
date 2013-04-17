using FlashHeatZeeker.StarlingSetup.Library;
using net.hires.debug;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using starling.core;
using starling.text;
using starling.utils;
using System;

namespace FlashHeatZeeker.StarlingSetup
{
    [SWF(backgroundColor = 0xA26D41, width = 800, height = 600)]
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                delegate
                {
                    this.stage.color = 0xA26D41;
                    this.stage.frameRate = 30;
                    this.stage.frameRate = 60;

                    // http://gamua.com/starling/first-steps/
                    // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
                    Starling.handleLostContext = true;

                    // 30 FPS
                    //StarlingGameSpriteDemo.DefaultLogoCount = 64;

                    // 60 FPS
                    StarlingGameSpriteDemo.DefaultLogoCount = 32;


                    var s = new Starling(
                        typeof(StarlingGameSpriteDemo).ToClassToken(),
                        this.stage,

                        // http://forum.starling-framework.org/topic/air-34
                        profile: "baseline"
                    );


                    //Starling.current.showStats

                    s.showStats = true;

                    #region atresize
                    Action atresize = delegate
                    {
                        // http://forum.starling-framework.org/topic/starling-stage-resizing

                        s.viewPort = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                            0, 0, this.stage.stageWidth, this.stage.stageHeight
                        );

                        s.stage.stageWidth = this.stage.stageWidth;
                        s.stage.stageHeight = this.stage.stageHeight;


                        //b2stage_centerize();
                    };

                    atresize();
                    #endregion


                    StarlingGameSpriteBase.onresize =
                        yield =>
                        {
                            this.stage.resize += delegate
                            {
                                atresize();

                                yield(this.stage.stageWidth, this.stage.stageHeight);
                            };
                        };


                    var c = 0;

                    this.stage.enterFrame +=
                        delegate
                        {
                          
                            StarlingGameSpriteBase.onframe(this.stage, s);

                            c++;

                        };

                    s.start();

                    // http://www.flare3d.com/support/index.php?topic=1101.0
                    this.addChild(new Stats());
                }
            );
        }

    }
}
