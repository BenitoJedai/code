using FlashHeatZeeker.PlayerIOIntegrationBeta2.Library;
using FlashHeatZeeker.StarlingSetup.Library;
//using playerio;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using starling.core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace FlashHeatZeeker.PlayerIOIntegrationBeta2
{
#if FPLAYERIO
    static class X
    {
        public static void addMessageHandler(this Connection c, string m, Action<Message, uint> y)
        {
            c.addMessageHandler(m, y.ToFunction());

        }
    }
#endif

    public class ApplicationSpriteWithConnection : Sprite
    {


        #region __transport_in_fakelag
        public event Action<string> __transport_out;

        public void __transport_in_fakelag(string xmlstring)
        {
            PendingInput.Add(xmlstring);
        }

        public void __transport_in(string xmlstring)
        {
            var xml = XElement.Parse(xmlstring);

            if (xml.Name.LocalName == "enterorexit")
            {
                StarlingGameSpriteBeta2.__at_enterorexit(
                    egoid: xml.Attribute("egoid").Value,
                    to: xml.Attribute("to").Value,
                    from: xml.Attribute("from").Value
                );
            }

            if (xml.Name.LocalName == "sync")
            {
                StarlingGameSpriteBeta2.__at_sync(
                    xml.Attribute("egoid").Value
                );
            }


            if (xml.Name.LocalName == "SetVerticalVelocity")
            {
                StarlingGameSpriteBeta2.__at_SetVerticalVelocity(
                    sessionid: xml.Attribute("__sessionid").Value,
                    identity: xml.Attribute("identity").Value,
                    value: xml.Attribute("value").Value
                );
            }

            if (xml.Name.LocalName == "SetVelocityFromInput")
            {
                StarlingGameSpriteBeta2.__at_SetVelocityFromInput(
                    __egoid: xml.Attribute("egoid").Value,
                    __identity: xml.Attribute("i").Value,
                    __KeySample: xml.Attribute("k").Value,
                    __fixup_x: xml.Attribute("x").Value,
                    __fixup_y: xml.Attribute("y").Value,
                    __fixup_angle: xml.Attribute("angle").Value
                );
            }
        }

        public void __raise_transport_out(string xml)
        {
            if (__transport_out != null)
                __transport_out(xml);
        }

        Queue<List<string>> lag = new Queue<List<string>>();
        List<string> PendingInput = new List<string>();
        #endregion

        public void Initialize()
        {
            #region __transport
            for (int i = 0; i < 7; i++)
            {
                lag.Enqueue(new List<string>());
            }

            var lagtimer = new ScriptCoreLib.ActionScript.flash.utils.Timer(1000 / 15);

            lagtimer.timer +=
                delegate
                {
                    lag.Enqueue(PendingInput);

                    PendingInput = lag.Dequeue();

                    foreach (var xml in PendingInput)
                    {
                        this.__transport_in(xml);
                    }

                    PendingInput.Clear();
                };

            lagtimer.start();

            StarlingGameSpriteBeta2.__raise_enterorexit +=
                (string egoid, string from, string to) =>
                {
                    var xml = new XElement("enterorexit",

                        new XAttribute("egoid", egoid),


                        new XAttribute("from", from),
                        new XAttribute("to", to)

                    );

                    if (__transport_out != null)
                        __transport_out(xml.ToString());
                };

            StarlingGameSpriteBeta2.__raise_sync +=
               egoid =>
               {
                   // Error	8	Argument 1: cannot convert from 'System.Xml.Linq.XAttribute' to 'System.Xml.Linq.XStreamingElement'	X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.UnitPedSync\ApplicationSprite.cs	40	33	FlashHeatZeeker.UnitPedSync

                   var xml = new XElement("sync", new XAttribute("egoid", egoid));

                   if (__transport_out != null)
                       __transport_out(xml.ToString());
               };

            StarlingGameSpriteBeta2.__raise_SetVerticalVelocity +=
                (string __sessionid, string identity, string value) =>
                {
                    var xml = new XElement("SetVerticalVelocity",

                        new XAttribute("__sessionid", __sessionid),


                        new XAttribute("identity", identity),
                        new XAttribute("value", value)

                    );

                    if (__transport_out != null)
                        __transport_out(xml.ToString());
                };

            StarlingGameSpriteBeta2.__raise_SetVelocityFromInput +=
                (
                    string __egoid,
                    string __identity,
                    string __KeySample,
                    string __fixup_x,
                    string __fixup_y,
                    string __fixup_angle

                    ) =>
                {
                    var xml = new XElement("SetVelocityFromInput",

                        new XAttribute("egoid", __egoid),


                        new XAttribute("i", __identity),
                        new XAttribute("k", __KeySample),
                        new XAttribute("x", __fixup_x),
                        new XAttribute("y", __fixup_y),
                        new XAttribute("angle", __fixup_angle)

                    );

                    if (__transport_out != null)
                        __transport_out(xml.ToString());
                };
            #endregion

            this.InvokeWhenStageIsReady(
                delegate
                {
                    // http://gamua.com/starling/first-steps/
                    // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
                    Starling.handleLostContext = true;

                    var s = new Starling(
                        typeof(StarlingGameSpriteBeta2).ToClassToken(),
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

#if PLAYERIO
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

                        // logo will be shown automatically for free tier_
                        //global::playerio.PlayerIO.showLogo(stage, "BR");

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

                            connection.addMessageHandler("__game_postMessage",
                              (m, userid) =>
                              {
                                  if (m.length > 0)
                                  {
                                      this.__transport_in(m.getString(m.length - 1));
                                  }

                              }
                          );

                            this.__transport_out +=
                                e =>
                                {
                                    connection.send("__context_postMessage", e.ToString());
                                };

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

        //public static global::playerio.Client CurrentClient;
        public static string __gameid = "test-4jazuo9jw0qx0cye9ihrqg";

    }
}
