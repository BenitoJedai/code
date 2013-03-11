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
    [Frame(typeof(XApplicationSpritePreloader))]
    //#endif
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

            var sb = new Soundboard();

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


                        var shopcontent = new FlashHeatZeeker.Shop.ApplicationCanvas();

                        #region BuyAmmo
                        shopcontent.BuyAmmo += delegate
                        {
                            Console.WriteLine("BuyAmmo");

                            sb.snd_click.play();

                            this.facebookOAuthConnectPopupItems(
                                (Client xclient, string access_token, string facebookuserid, object[] items) =>
                                {
                                    Console.WriteLine("BuyAmmo  " + new { facebookuserid, xclient.payVault.items.Length });

                                    #region itemKey_exists
                                    var itemKey = "Shotgun3";
                                    var itemKey_exists = false;

                                    foreach (var item in items)
                                    {
                                        var dyn = new DynamicContainer { Subject = item };

                                        var dyn_itemKey = (string)dyn["itemKey"];

                                        Console.WriteLine(new { item });

                                        if (dyn_itemKey == itemKey)
                                            itemKey_exists = true;
                                    }

                                    if (itemKey_exists)
                                    {
                                        shopcontent.bg_ammo.Fill = Brushes.Green;
                                        shopcontent.bg_shotgun.Fill = Brushes.Green;
                                        sb.snd_SelectWeapon.play();
                                        return;
                                    }
                                    #endregion


                                    //                              facebookOAuthConnectPopup callback { connectUserId = fb1527339800, access_token = AAADLSABgZCZC0BAOroZC3jsNhsgRVFhBK4VcAT19uePwd2iZBKX8ZCVwtdu8ZBhtmU9bH6nMJHO0qJ6I5dzvhw9Ty1kt542zpH2BZCMKKPI0wZDZD, facebookuserid = 1527339800 }
                                    //BuyAmmo { facebookuserid = 1527339800, Length = 1 }
                                    //{ item = [playerio.VaultItem][itemKey="Shotgun3", id="403939388", purchaseDate=Mon Mar 11 21:32:08 GMT+0200 2013] = {
                                    //FreelyGivable:true
                                    //PriceUSD:99
                                    //} }

                                }
                            );
                        };
                        #endregion

                        #region BuyShotgun
                        shopcontent.BuyShotgun += delegate
                        {
                            Console.WriteLine("BuyShotgun");
                            sb.snd_click.play();

                            this.facebookOAuthConnectPopupItems(
                              (Client xclient, string access_token, string facebookuserid, object[] items) =>
                              {
                                  Console.WriteLine("BuyShotgun  " + new { facebookuserid, xclient.payVault.items.Length });

                                  #region itemKey_exists
                                  var itemKey = "Shotgun3";
                                  var itemKey_exists = false;

                                  foreach (var item in items)
                                  {
                                      var dyn = new DynamicContainer { Subject = item };

                                      var dyn_itemKey = (string)dyn["itemKey"];

                                      Console.WriteLine(new { item });

                                      if (dyn_itemKey == itemKey)
                                          itemKey_exists = true;
                                  }

                                  if (itemKey_exists)
                                  {
                                      shopcontent.bg_ammo.Fill = Brushes.Green;
                                      shopcontent.bg_shotgun.Fill = Brushes.Green;
                                      sb.snd_SelectWeapon.play();
                                      return;
                                  }
                                  #endregion


                                  // Gets information about how to make a direct item purchase with the specified PayVault provider.
                                  this.getBuyDirectInfo(
                                      xclient,
                                      facebookuserid,

                                      item_name: "Operation Heat Zeeker - Shotgun",
                                      itemKey: itemKey,

                                      yield_paypalurl: uri =>
                                      {
                                          shopcontent.bg_shotgun.Fill = Brushes.Red;

                                          ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.NavigateTo(uri);
                                          //uri.NavigateTo();
                                      }
                                  );


                              }
                          );


                        };
                        #endregion


                        StarlingGameSpriteBeta2.ShopEnter +=
                            delegate
                            {
                                // no go
                                if (ApplicationSpriteWithConnection.CurrentClient == null)
                                    return;

                                shopcontent.AttachToContainer(this);
                                shopcontent.AutoSizeTo(this.stage);
                            };


                        StarlingGameSpriteBeta2.ShopExit +=
                           delegate
                           {
                               ScriptCoreLib.ActionScript.Extensions.CommonExtensions.Orphanize(
                                   ScriptCoreLib.ActionScript.Extensions.AvalonExtensions.ToSprite(shopcontent)
                                   );
                           };
                    };


                }
            );
        }






        #region InitializeConsoleFormWriter
        Action<Action<string>, Action<string>> AtInitializeConsoleFormWriter;
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
