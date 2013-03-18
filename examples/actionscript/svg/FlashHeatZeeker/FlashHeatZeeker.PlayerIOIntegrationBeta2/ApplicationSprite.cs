using FlashHeatZeeker.CoreAudio.Library;
using FlashHeatZeeker.PromotionPreloader;
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
using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using FlashHeatZeeker.Shop;
using System.Windows.Media;
using playerio;
using FlashHeatZeeker.PlayerIOIntegrationBeta2.Library;
using ScriptCoreLib.ActionScript.flash.system;

namespace FlashHeatZeeker.PlayerIOIntegrationBeta2
{
    public class XApplicationSpritePreloader : ApplicationSpritePreloader
    {
        [TypeOfByNameOverride]
        public override Type GetTargetType()
        {
            return typeof(ApplicationSprite);
        }
    }

    // https://www.fgl.com/view_game.php?from=dev&game_id=27918
    //#if false
    //[Frame(typeof(XApplicationSpritePreloader))]
    //#endif
    [SWF(backgroundColor = 0, width = 800, height = 600, frameRate = 60)]
    public sealed class ApplicationSprite : ApplicationSpriteWithConnection, IAlternator
    {
        public string Alternate { get; set; }

        public ApplicationSprite()
        {

            var disable_F9 = false;
            var sb = new Soundboard();

            this.InvokeWhenStageIsReady(
                delegate
                {
                    Security.allowDomain("*");
                    Security.allowInsecureDomain("*");

                    var lasterror = 0;

                    this.root.loaderInfo.uncaughtErrorEvents.uncaughtError +=
                       e =>
                       {
                           if (lasterror == e.errorID)
                               return;

                           Console.WriteLine("error: " + new { e.errorID, e.error, e } + "\n run in flash debugger for more details!");

                           lasterror = e.errorID;
                       };


                    this.stage.keyUp +=
                       e =>
                       {
                           if (e.keyCode == (uint)System.Windows.Forms.Keys.F9)
                           {
                               if (disable_F9)
                                   return;

                               disable_F9 = true;
                               sb.snd_click.play();

                               Abstractatech.ActionScript.ConsoleFormPackage.ConsoleFormPackageExperience.Initialize();
                           }

                           if (e.keyCode == (uint)System.Windows.Forms.Keys.F11)
                           {
                               sb.snd_click.play();

                               this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                           }

                           if (e.keyCode == (uint)System.Windows.Forms.Keys.F)
                           {
                               sb.snd_click.play();

                               this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                           }
                       };
                }
            );


            var lobby = new FlashHeatZeeker.Lobby.ApplicationSprite();
            lobby.AttachTo(this);


            this.InvokeWhenPromotionIsReady(
                delegate
                {
                    lobby.StartClicked += delegate
                    {
                        if (lobby == null)
                            return;

                        sb.snd_click.play();

                        try
                        {
                            lobby.ytp.Loader.unloadAndStop(true);
                            //lobby.ytp.pauseVideo();
                        }
                        catch
                        {
                        }
                        lobby.Orphanize();
                        lobby = null;

                        //new ApplicationSpriteContent().AttachTo(this);

                        Initialize();

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




                        var shop = new ShopExperience(this);

                        StarlingGameSpriteBeta2.ShopEnter += ped =>
                            {
                                // no go
                                if (ApplicationSpriteWithConnection.CurrentClient == null)
                                    return;

                                shop.ShopEnter(ped);
                            };

                        StarlingGameSpriteBeta2.ShopExit += shop.ShopExit;
                    };


                }
            );
        }




    }
}
