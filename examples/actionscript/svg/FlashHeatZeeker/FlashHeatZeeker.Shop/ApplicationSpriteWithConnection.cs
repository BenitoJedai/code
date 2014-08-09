using FlashHeatZeeker.Shop.Library;
using FlashHeatZeeker.StarlingSetup.Library;
//using playerio; // rebuild that with non partial jsc
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using starling.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FlashHeatZeeker.Shop
{
    //static class X
    //{
    //    public static void addMessageHandler(this Connection c, string m, Action<Message, uint> y)
    //    {
    //        c.addMessageHandler(m, y.ToFunction());

    //    }
    //}


    public class ApplicationSpriteWithConnection : Sprite
    {



        public void Initialize()
        {

            this.InvokeWhenStageIsReady(
                delegate
                {
                    // http://gamua.com/starling/first-steps/
                    // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
                    Starling.handleLostContext = true;

                    var s = new Starling(
                        typeof(StarlingGameSpriteWithShop).ToClassToken(),
                        this.stage
                    );

                    //[SWF(backgroundColor = 0xB27D51)]

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

            WhenReady();
        }


        void WhenReady()
        {
            Action<string> WriteLine = Console.WriteLine;

            this.InvokeWhenStageIsReady(
                () =>
                {
                    //content.AttachToContainer(this);
                    //content.AutoSizeTo(this.stage);

                    //content.Opacity = 0;

#if FPLAYERIO
                    Action<global::playerio.PlayerIOError> handleError = e =>
                    {
                        // error: { errorID = 10, message = Unknown game id: [Enter your game id here] }

                        // error: { errorID = 16, message = Could not find a game class with the correct room type: MyCode. You have to add this attribute: [RoomType("MyCode")] to your main game class. You can read more about this on our blog: http://playerio.com/blog/ }
                        WriteLine("error: " + new { e.errorID, e.message });

                    };

                    Action<global::playerio.Client> handleConnect = client =>
                    {

                        WriteLine("Sucessfully connected to player.io");

                        CurrentClient = client;
                        global::playerio.PlayerIO.showLogo(stage, "BR");


                        var multiplayer = client.multiplayer;

                        //Set developmentsever (Comment out to connect to your server online)

#if DEBUG
                        multiplayer.developmentServer = "localhost:8184";
#endif

                        //                        multiplayer.developmentServer =
                        //                            // http://192.168.1.103
                        //                            "192.168.1.103:8184";

                        //"localhost:8184";

                        Action<global::playerio.Connection> handleJoin = connection =>
                        {
                            WriteLine("handleJoin");

                            Action handleDisconnect = delegate
                            {
                                WriteLine("Disconnected from server");
                            };

                            //Add disconnect listener
                            connection.addDisconnectHandler(handleDisconnect.ToFunction());


                            //Add listener for messages of the type "hello"
                            //connection.addMessageHandler("hello",
                            //    (m, userid) =>
                            //    {
                            //        if (m.length > 0)
                            //        {
                            //            WriteLine("hello: " + m.getString(m.length - 1));
                            //        }
                            //        else
                            //        {
                            //            WriteLine("Recived a message with the type hello from the server");
                            //        }
                            //    }
                            //);

                            connection.addMessageHandler("xhello",
                                  (m, userid) =>
                                  {
                                      if (m.length > 0)
                                      {
                                          WriteLine("hello: " + m.getString(m.length - 1));
                                      }
                                      else
                                      {
                                          WriteLine("xhello: Recived a message with the type hello from the server");
                                      }
                                  }
                              );

                            //this.content.MouseLeftButtonUp +=
                            //   (sender, e) =>
                            //   {
                            //       var p = e.GetPosition(content);

                            //       var m = connection.createMessage("hello", "howdy! " + new { p.X, p.Y });

                            //       connection.sendMessage(m);
                            //   };


                            //Add message listener for users joining the room
                            connection.addMessageHandler("UserJoined",
                                (m, userid) =>
                                {
                                    try
                                    {
                                        if (m.length > 0)
                                        {
                                            WriteLine("UserJoined: " + m.getString(1));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLine("error: " + ex.Message);
                                    }

                                    WriteLine("xPlayer with the userid " + userid + " just joined the room" + new { m.length });
                                }
                            );


                            //this.__game_postMessage = (XElement e) =>
                            //{
                            //    this.__game_onmessage(e);
                            //};

                            //  connection.addMessageHandler("__game_postMessage",
                            //    (m, userid) =>
                            //    {
                            //        if (m.length > 0)
                            //        {
                            //            this.__transport_in(m.getString(m.length - 1));
                            //        }

                            //    }
                            //);

                            //  this.__transport_out +=
                            //      e =>
                            //      {
                            //          connection.send("__context_postMessage", e.ToString());
                            //      };

                            //Add message listener for users leaving the room

                            connection.addMessageHandler("UserLeft",
                                (m, userid) =>
                                {
                                    WriteLine("Player with the userid " + userid + " just left the room");
                                }
                            );
                        };


                        //Create pr join the room test
                        multiplayer.createJoinRoom(
                            "test",								//Room id. If set to null a random roomid is used
                            "x",							//The game type started on the server
                            true,								//Should the room be visible in the lobby?
                            new object(),									//Room data. This data is returned to lobby list. Variabels can be modifed on the server
                            new object(),									//User join data
                            callback: handleJoin.ToFunction(),							//Function executed on successful joining of the room
                            errorHandler: handleError.ToFunction()							//Function executed if we got a join error
                        );
                    };



                    WriteLine("before connect");

                    global::playerio.PlayerIO.connect(
                        stage,								//Referance to stage
                        __gameid,			//Game id (Get your own at playerio.com)
                        "public",							//Connection id, default is public
                        "GuestUser",						//Username
                        "",									//User auth. Can be left blank if authentication is disabled on connection
                        null,								//Current PartnerPay partner.
                        callback: handleConnect.ToFunction(),						//Function executed on successful connect
                        errorhandler: handleError.ToFunction()							//Function executed if we recive an error
                    );

                    WriteLine("after connect");
#endif


                }
            );
        }

        // wtf. why is the ref missing?
        // it is missing any swc.
        // X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.Shop\ApplicationSpriteWithConnection.cs
        // why?
        // 23 KB
        // "X:\jsc.svn\market\synergy\PlayerIO.GameLibrary\PlayerIO.GameLibrary\bin\staging.AssetsLibrary\PlayerIO.GameLibrary.AssetsLibrary.dll"
        // was jsc a partial build and then messed up the cache?
        //public static global::playerio.Client CurrentClient;
        public static string __gameid = "test-4jazuo9jw0qx0cye9ihrqg";
    }
}
