using FlashHeatZeeker.PromotionPreloader;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.TestDriversWithAudio.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;
using starling.core;
using System;

namespace FlashHeatZeeker.TestDriversWithAudio
{
    public class ApplicationSpritePreloader : FlashHeatZeeker.PromotionPreloader.ApplicationSpritePreloader
    {
        [TypeOfByNameOverride]
        public override Type GetTargetType()
        {
            return typeof(ApplicationSprite);
        }
    }

    [Frame(typeof(ApplicationSpritePreloader))]
    [SWF(backgroundColor = 0xB27D51)]
    public sealed class ApplicationSprite : Sprite, IAlternator
    {
        public string Alternate { get; set; }

        public ApplicationSprite()
        {
            this.InvokeWhenPromotionIsReady(
              delegate
              {
                  new ApplicationSpriteContent().AttachTo(this);
              }
            );
        }
    }

    public sealed class ApplicationSpriteContent : Sprite
    {
        public ApplicationSpriteContent()
        {
            this.InvokeWhenStageIsReady(
              delegate
              {
                  this.stage.frameRate = 30;
                  this.stage.frameRate = 60;

                  // http://gamua.com/starling/first-steps/
                  // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
                  Starling.handleLostContext = true;

                  var s = new Starling(
                      typeof(StarlingGameSpriteWithTestDriversWithAudio).ToClassToken(),
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
                           if (e.keyCode == (uint)System.Windows.Forms.Keys.F11)
                           {
                               this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                           }

                           if (e.keyCode == (uint)System.Windows.Forms.Keys.F)
                           {
                               this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                           }
                       };
                  #endregion

              }
          );
        }

    }
}
