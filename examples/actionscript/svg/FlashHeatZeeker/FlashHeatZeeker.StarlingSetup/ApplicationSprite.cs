using FlashHeatZeeker.StarlingSetup.Library;
using net.hires.debug;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;



// wtf. why are we getting missing references?
// X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.StarlingSetup\ApplicationSprite.cs
// this project has dependency on 
// "X:\jsc.svn\market\synergy\starling\starling\bin\staging.AssetsLibrary\starling.AssetsLibrary.dll"
// which has the swc embedded yet no flash natives.
// where are they?
// 213KB
// "X:\jsc.svn\market\synergy\starling\starling\bin\staging.AssetsLibrary\output.swc\starling.AssetsLibrary.swc.dll"
// so has a final merge failed?
// a clean rebuild restores it
// so at what point will we loose it?
// doing a clean? no

//Additional information: Could not load file or assembly 'net.hires.debug, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.

//updated { id = FlashHeatZeeker.Core }
//{ FixupHintPath = X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\packages\net.hires.debug.1.0.0.0 }
//will need to find package  { id = net.hires.debug }
//will find package  { id = net.hires.debug }
//updating { id = net.hires.debug }
//{ FixupHintPath = X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\packages\starling.1.0.0.0 }
//will need to find package  { id = starling }
//will find package  { id = starling }
//updating { id = starling }

// http://my.jsc-solutions.net/?s=starling
// what if the package was not found?

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
