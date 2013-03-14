using FlashHeatZeeker.CoreAudio.Library;
using FlashHeatZeeker.Shop.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using playerio;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using starling.core;
using System;
using System.IO;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.ActionScript;
using System.Windows.Media;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.UnitPedControl.Library;


namespace FlashHeatZeeker.Shop
{
    // Error	2	Do not use 'System.Runtime.CompilerServices.DynamicAttribute'. Use the 'dynamic' keyword instead.	X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.Shop\ApplicationSprite.cs	18	6	FlashHeatZeeker.Shop


    [DynamicType]
    sealed class item_object
    {

        public string itemKey;
    }

    public delegate void facebookOAuthConnectPopupCallback(Client xclient, string access_token, string facebookuserid);
    public delegate void facebookOAuthConnectPopupItemsCallback(Client xclient, string access_token, string facebookuserid, object[] items);

    public static class ApplicationSpriteShopExtensions
    {

        static Action<facebookOAuthConnectPopupItemsCallback> facebookOAuthConnectPopupItems_cache;

        public static void facebookOAuthConnectPopupItems(this Sprite that, facebookOAuthConnectPopupItemsCallback yield)
        {
            if (facebookOAuthConnectPopupItems_cache != null)
            {


                facebookOAuthConnectPopupItems_cache(yield);
                return;
            }

            facebookOAuthConnectPopupCallback c =
                 (Client xclient, string access_token, string facebookuserid) =>
                 {

                     Action callback = delegate
                     {
                         Console.WriteLine("at callback");

                         yield(xclient, access_token, facebookuserid, xclient.payVault.items);


                         // oh and the next time you dod that, lets cache it
                         // for how long?
                         Console.WriteLine("set facebookOAuthConnectPopupItems_cache");

                         facebookOAuthConnectPopupItems_cache =
                             future_yield =>
                             {
                                 Console.WriteLine("at facebookOAuthConnectPopupItems_cache");

                                 Action future_callback = delegate
                                 {
                                     Console.WriteLine("at future_callback");

                                     yield(xclient, access_token, facebookuserid, xclient.payVault.items);
                                 };

                                 xclient.payVault.refresh(callback: future_callback.ToFunction());

                             };
                     };

                     xclient.payVault.refresh(callback: callback.ToFunction());
                 };

            that.facebookOAuthConnectPopup(c);
        }
        static Soundboard sb = new Soundboard();


        public static string facebookOAuthConnectPopup_window = "_blank";

        public static void facebookOAuthConnectPopup(this Sprite that, facebookOAuthConnectPopupCallback yield)
        {

            //ApplicationSpriteWithConnection.CurrentClient.With(
            //   client =>
            //   {
            //       Console.WriteLine("client " + new { client.connectUserId });


            Action<PlayerIOError> errorHandler = e =>
            {
                Console.WriteLine("facebookOAuthConnectPopup errorHandler " + e);


            };

            // http://playerio.com/documentation/reference/actionscript3/playerio.quickconnect#facebookConnectPopup
            // http://playerio.com/documentation/reference/actionscript3/playerio.quickconnect#facebookOAuthConnectPopup

            facebookOAuthConnectPopupCallback callback =
                (Client xclient, string access_token, string facebookuserid) =>
                {
                    Console.WriteLine("facebookOAuthConnectPopup callback " + new { xclient.connectUserId, access_token, facebookuserid });

                    // facebookOAuthConnectPopup callback { xclient = [Player.IO Client], access_token = AAADLSABgZCZC0BAOroZC3jsNhsgRVFhBK4VcAT19uePwd2iZBKX8ZCVwtdu8ZBhtmU9bH6nMJHO0qJ6I5dzvhw9Ty1kt542zpH2BZCMKKPI0wZDZD, facebookuserid = 1527339800 }

                    yield(xclient, access_token, facebookuserid);
                };

            // http://test-4jazuo9jw0qx0cye9ihrqg.fb.playerio.com/fb/_fb_quickconnect_oauth
            // QuickConnect for Facebook is not enabled for this game
            // facebookConnectPopup errorHandler Error: FacebookConnectPopup is no longer supported by Facebook. Please use FacebookConnectOAuthPopup

            Console.WriteLine("facebookOAuthConnectPopup: " + new { facebookOAuthConnectPopup_window });

            playerio.PlayerIO.quickConnect.facebookOAuthConnectPopup(
                stage: that.stage,
                gameId: ApplicationSpriteWithConnection.__gameid,

                window: facebookOAuthConnectPopup_window,

                permissions: new object[0],
                  callback: callback.ToFunction(),

              errorHandler: errorHandler.ToFunction()

             );

            Console.WriteLine("after facebookOAuthConnectPopup");
            //    }
            //);
        }

        public static void getBuyDirectInfo(this Sprite that,
            Client client, string facebookuserid,

            string item_name = "Operation Heat Zeeker - Shotgun",
            string itemKey = "Shotgun3",

            Action<Uri> yield_paypalurl = null
            )
        {
            Console.WriteLine("before getBuyDirectInfo " + new { client.connectUserId, facebookuserid });

            // error: { errorID = 0, error = NotImplementedException: __CallSite.Create, e = [UncaughtErrorEvent type="uncaughtError" bubbles=true cancelable=true eventPhase=2] }

            var purchaseArguments = new DynamicContainer { Subject = new object() };

            purchaseArguments["currency"] = "USD";
            purchaseArguments["item_name"] = item_name + " [for http://www.facebook.com/profile.php?id=" + facebookuserid + "]";

            var item = new item_object();

            // .99 USD
            item.itemKey = itemKey;

            var items = new object[] { item };

            Action<dynamic> callback = info =>
            {
                string paypalurl = info.paypalurl;

                Console.WriteLine("getBuyDirectInfo callback " + new { paypalurl });

                var uri = new Uri(paypalurl);

                if (yield_paypalurl != null)
                    yield_paypalurl(uri);
            };

            Action<PlayerIOError> errorHandler = e =>
            {
                Console.WriteLine("getBuyDirectInfo errorHandler " + e);


                //that.content.bg_shotgun.Fill = System.Windows.Media.Brushes.Red;
            };


            Console.WriteLine("just before getBuyDirectInfo");

            // error: { errorID = 0, message = Unable to connect to the API due to securityError. Please verify that your internet connection is working! }
            // getBuyDirectInfo errorHandler Error: Items with keys [shotgun1] do not exist in PayVaultItems
            // etBuyDirectInfo errorHandler Error: Missing 'currency' in purchaseArguments. It should be a currency code supported by PayPal, such as 'USD'. The price is found in the PayVault items property 'Price[currency]', such as 'PriceUSD'

            client.payVault.getBuyDirectInfo(
                provider: "paypal",
                purchaseArguments: purchaseArguments.Subject,
                items: items,
                callback: callback.ToFunction(),
                errorHandler: errorHandler.ToFunction()
            );

            Console.WriteLine("after getBuyDirectInfo");
        }
    }

    public class ShopExperience
    {
        public readonly Action<IPhysicalUnit> ShopEnter;
        public readonly Action ShopExit;

        public ShopExperience(Sprite that)
        {
            var currentped = default(IPhysicalUnit);

            var shopcontent = new ApplicationCanvas();
            var sb = new Soundboard();

            #region GiveMeWhatIWant
            Action GiveMeWhatIWant = delegate
            {
                shopcontent.bg_ammo.Fill = Brushes.Green;
                shopcontent.bg_shotgun.Fill = Brushes.Green;
                sb.snd_SelectWeapon.play();
                shopcontent.t2o.Opacity = 0.1;
                (currentped as PhysicalPed).With(
                    ped =>
                    {
                        ped.visual.StandWithVisibleGun = true;
                    }
                );
            };

            #endregion

            #region BuyAmmo
            shopcontent.BuyAmmo += delegate
            {
                Console.WriteLine("BuyAmmo");

                //                BuyAmmo
                //facebookOAuthConnectPopup: { facebookOAuthConnectPopup_window = _blank }
                //after facebookOAuthConnectPopup
                //still in window: { Right = 739, Left = 675 }
                //{ zombie_forceA = 4.867357484065154 }
                //facebookOAuthConnectPopup callback { connectUserId = fb1527339800, access_token = AAADLSABgZCZC0BAOroZC3jsNhsgRVFhBK4VcAT19uePwd2iZBKX8ZCVwtdu8ZBhtmU9bH6nMJHO0qJ6I5dzvhw9Ty1kt542zpH2BZCMKKPI0wZDZD, facebookuserid = 1527339800 }
                //at callback
                //BuyAmmo { facebookuserid = 1527339800, Length = 1 }
                //{ item = [playerio.VaultItem][itemKey="Shotgun3", id="403939388", purchaseDate=Mon Mar 11 21:32:08 GMT+0200 2013] = {
                //FreelyGivable:true
                //PriceUSD:99
                //} }
                //set facebookOAuthConnectPopupItems_cache


                sb.snd_click.play(
                      sndTransform: new ScriptCoreLib.ActionScript.flash.media.SoundTransform(0.5)
                  );

                that.facebookOAuthConnectPopupItems(
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
                            GiveMeWhatIWant();
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

                sb.snd_click.play(
                  sndTransform: new ScriptCoreLib.ActionScript.flash.media.SoundTransform(0.5)
              );

                that.facebookOAuthConnectPopupItems(
                  (Client xclient, string access_token, string facebookuserid, object[] items) =>
                  {
                      Console.WriteLine("BuyShotgun  " + new { facebookuserid, xclient.payVault.items.Length });

                      var itemKey = "Shotgun3";


                      var BuyAnyway = true;
                      if (BuyAnyway)
                      {

                      }
                      else
                      {

                          #region itemKey_exists
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
                              GiveMeWhatIWant();

                              return;
                          }
                          #endregion

                      }

                      Console.WriteLine("before getBuyDirectInfo");

                      // Gets information about how to make a direct item purchase with the specified PayVault provider.
                      that.getBuyDirectInfo(
                          xclient,
                          facebookuserid,

                          item_name: "Operation Heat Zeeker - Shotgun",
                          itemKey: itemKey,

                          yield_paypalurl: uri =>
                          {
                              Console.WriteLine("at getBuyDirectInfo");

                              shopcontent.bg_shotgun.Fill = Brushes.Red;


                              uri.NavigateTo();
                          }
                      );


                  }
              );


            };
            #endregion


            ShopEnter =
                ped =>
                {
                    currentped = ped;


                    shopcontent.AttachToContainer(that);
                    shopcontent.AutoSizeTo(that.stage);
                };


            shopcontent.Close += delegate
            {
                if (currentped == null)
                    return;

                currentped = null;

                ScriptCoreLib.ActionScript.Extensions.CommonExtensions.Orphanize(
                    ScriptCoreLib.ActionScript.Extensions.AvalonExtensions.ToSprite(shopcontent)
                    );
            };

            ShopExit =
               delegate
               {
                   if (currentped == null)
                       return;

                   currentped = null;

                   ScriptCoreLib.ActionScript.Extensions.CommonExtensions.Orphanize(
                       ScriptCoreLib.ActionScript.Extensions.AvalonExtensions.ToSprite(shopcontent)
                       );
               };
        }
    }
    public sealed class ApplicationSprite : ApplicationSpriteWithConnection
    {
        /*
         * 1. add starling
         */




        public ApplicationSprite()
        {
            this.loaderInfo.uncaughtErrorEvents.uncaughtError +=
                e =>
                {
                    Console.WriteLine("error: " + new { e.errorID, e.error, e } + "\n run in flash debugger for more details!");

                };

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
                  Initialize();


                  var shop = new ShopExperience(this);

                  StarlingGameSpriteWithShop.ShopEnter += ped =>
                  {
                      // no go
                      if (ApplicationSpriteWithConnection.CurrentClient == null)
                          return;

                      shop.ShopEnter(ped);
                  };
                  StarlingGameSpriteWithShop.ShopExit += shop.ShopExit;

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


        public void SetIFrameName(string value)
        {
            ApplicationSpriteShopExtensions.facebookOAuthConnectPopup_window = value;
        }

    }
}
