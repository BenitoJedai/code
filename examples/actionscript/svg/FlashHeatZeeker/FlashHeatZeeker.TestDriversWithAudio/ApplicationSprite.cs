using FlashHeatZeeker.CoreAudio.Library;
using FlashHeatZeeker.PromotionPreloader;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.TestDriversWithAudio.Library;
using FlashHeatZeeker.UnitBunkerControl.Library;
using FlashHeatZeeker.UnitCannonControl.Library;
using FlashHeatZeeker.UnitHindControl.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitPedControl.Library;
using FlashHeatZeeker.UnitTankControl.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;
using starling.core;
using System;
using System.Xml.Linq;

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
            var lobby = new FlashHeatZeeker.Lobby.ApplicationSprite();
            lobby.AttachTo(this);

            this.InvokeWhenPromotionIsReady(
              delegate
              {

                  lobby.StartClicked += delegate
                  {
                      if (lobby == null)
                          return;

                      lobby.ytp.pauseVideo();
                      lobby.Orphanize();
                      lobby = null;

                      new ApplicationSpriteContent().AttachTo(this);
                  };

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


                  #region F8
                  var CanConnectAndroid = true;
                  this.stage.keyUp +=
                       e =>
                       {
                           if (e.keyCode == (uint)System.Windows.Forms.Keys.F8)
                           {
                               if (!CanConnectAndroid)
                                   return;

                               CanConnectAndroid = false;

                               var sb = new Soundboard();
                               sb.snd_lookingforlongrangecomms.play(
                                   loops: 2,
                                   sndTransform: new ScriptCoreLib.ActionScript.flash.media.SoundTransform(0.4)
                               );


                               var text = new ScriptCoreLib.ActionScript.flash.text.TextField().AttachTo(this);
                               text.width = 800;
                               text.y = 72;
                               text.textColor = 0xffffff;
                               text.multiline = true;

                               FlashHeatZeeker.TestGamePad.Library.MulticastService.InitializeConnection(
                                    StarlingGameSpriteWithTestDriversWithAudio.__keyDown,
                                    WriteMode: false,
                                    ReadMode: true,
                                    text: text,

                                    yield_PostMessage:
                                        PostMessage =>
                                        {
                                            StarlingGameSpriteWithTestDriversWithAudio.current_changed +=
                                                g =>
                                                {
                                                    Action<string> __switchto =
                                                        type =>
                                                        {
                                                            PostMessage(
                                                                new XElement(
                                                                    "switchto",
                                                                    new XAttribute("type", type),
                                                                    new XAttribute("syncframeid", "" + g.syncframeid)

                                                                    ).ToString());
                                                        };
                                                    if (g.current is PhysicalPed)
                                                        __switchto("ped");
                                                    if (g.current is PhysicalTank)
                                                        __switchto("tank");
                                                    if (g.current is PhysicalJeep)
                                                        __switchto("jeep");
                                                    if (g.current is PhysicalHind)
                                                        __switchto("hind");
                                                    if (g.current is PhysicalBunker)
                                                        __switchto("bunker");
                                                    if (g.current is PhysicalCannon)
                                                        __switchto("cannon");
                                                    if (g.current is PhysicalSilo)
                                                        __switchto("silo");
                                                    if (g.current is PhysicalWatertower)
                                                        __switchto("watertower");
                                                };
                                        }
                               );


                           }
                       };
                  #endregion


              }
          );
        }

    }
}
