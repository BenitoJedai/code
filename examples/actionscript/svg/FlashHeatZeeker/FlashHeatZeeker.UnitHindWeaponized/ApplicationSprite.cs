using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitHindWeaponized.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using starling.core;
using System;

namespace FlashHeatZeeker.UnitHindWeaponized
{
    [SWF(backgroundColor = 0x007F7F)]
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

                  var s = new Starling(
                      typeof(StarlingGameSpriteWithHindWeaponized).ToClassToken(),
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

                          yield(this.stage.stageWidth, this.stage.stageHeight);
                      };




                  this.stage.enterFrame +=
                      delegate
                      {




                          StarlingGameSpriteBase.onframe(this.stage, s);
                      };

                  s.start();

              
                  #region FULL_SCREEN_INTERACTIVE
                  this.stage.keyUp +=
                       e =>
                       {
                           //dynamic dstage = this.stage;

                           if (e.keyCode == (uint)System.Windows.Forms.Keys.F11)
                           {
                               this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;

                               // ReferenceError: Error #1056: Cannot create property mouseLock on flash.display.Stage.
                               //dstage.mouseLock = true;
                           }

                           if (e.keyCode == (uint)System.Windows.Forms.Keys.F)
                           {
                               this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                               //dstage.mouseLock = true;

                               // http://www.levelxgames.com/2012/11/how-to-overlay-abode-air-sdk-over-flex-sdk/
                           }
                       };
                  #endregion
              }
          );
        }

    }
}
