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

    [Frame(typeof(XApplicationSpritePreloader))]
    [SWF(backgroundColor = 0, width = 800, height = 600, frameRate = 60)]
    public sealed class ApplicationSprite : ApplicationSpriteWithConnection, IAlternator
    {
        public string Alternate { get; set; }

        public ApplicationSprite()
        {
            #region AtInitializeConsoleFormWriter

            var w = new __OutWriter();
            var o = Console.Out;
            var __reentry = false;

            var __buffer = new StringBuilder();

            w.AtWrite =
                x =>
                {
                    __buffer.Append(x);
                };

            w.AtWriteLine =
                x =>
                {
                    __buffer.AppendLine(x);
                };

            Console.SetOut(w);

            this.AtInitializeConsoleFormWriter = (
                Action<string> Console_Write,
                Action<string> Console_WriteLine
            ) =>
            {

                try
                {


                    w.AtWrite =
                        x =>
                        {
                            o.Write(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_Write(x);
                                __reentry = false;
                            }
                        };

                    w.AtWriteLine =
                        x =>
                        {
                            o.WriteLine(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_WriteLine(x);
                                __reentry = false;
                            }
                        };

                    Console.WriteLine("flash Console.WriteLine should now appear in JavaScript form!");
                    Console.WriteLine(__buffer.ToString());
                }
                catch
                {

                }
            };
            #endregion

            this.InvokeWhenStageIsReady(
                delegate
                {
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

                        lobby.ytp.pauseVideo();
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

                    };


                }
            );
        }




        Action<Action<string>, Action<string>> AtInitializeConsoleFormWriter;


        #region InitializeConsoleFormWriter
        class __OutWriter : TextWriter
        {
            public Action<string> AtWrite;
            public Action<string> AtWriteLine;

            public override void Write(string value)
            {
                AtWrite(value);
            }

            public override void WriteLine(string value)
            {
                AtWriteLine(value);
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        public void InitializeConsoleFormWriter(
            Action<string> Console_Write,
            Action<string> Console_WriteLine
        )
        {
            AtInitializeConsoleFormWriter(Console_Write, Console_WriteLine);
        }
        #endregion
    }
}
