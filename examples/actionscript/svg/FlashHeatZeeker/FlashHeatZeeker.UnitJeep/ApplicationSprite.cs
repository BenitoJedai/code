using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitJeep.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using starling.core;
using System;

namespace FlashHeatZeeker.UnitJeep
{
    [SWF(backgroundColor = 0xA26D41)]
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
              delegate
              {
                  // http://gamua.com/starling/first-steps/
                  // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
                  Starling.handleLostContext = true;

                  var s = new Starling(
                      typeof(StarlingGameSpriteWithJeep).ToClassToken(),
                      this.stage
                      //,
                      //  Use this parameter to force "software" rendering.
                      // The Context3DProfile that should be requested.
                      // http://forum.starling-framework.org/topic/air-34

                      // Chrome OpenGL 60 FPS
                      // Nexus 7 Tab OpenGL 27 FPS
                      // Galaxy S OpenGL 16 FPS
                      //profile: "baseline"
                      // constrained mode gives same perfrmance!
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

                          yield(this.stage.stageWidth, this.stage.stageHeight);
                      };




                  this.stage.enterFrame +=
                      delegate
                      {




                          StarlingGameSpriteBase.onframe(this.stage, s);
                      };

                  s.start();

              }
          );
        }

    }
}
