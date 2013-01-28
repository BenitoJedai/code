using playerio;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions;
using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace FlashHeatZeeker.PlayerIOIntegration
{


    static class X
    {
        public static void addMessageHandler(this Connection c, string m, Action<Message, uint> y)
        {
            c.addMessageHandler(m, y.ToFunction());

        }
    }

    public  class ApplicationSpriteContent : global::FlashHeatZeekerWithStarlingT22.ApplicationSpriteContent
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSpriteContent()
        {
            WhenReady();
        }

        public void WhenReady()
        {
            Action<string> WriteLine = Console.WriteLine;

            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);

                    content.Opacity = 0;

                    Action<global::playerio.PlayerIOError> handleError = e =>
                    {
                        // error: { errorID = 10, message = Unknown game id: [Enter your game id here] }

                        // error: { errorID = 16, message = Could not find a game class with the correct room type: MyCode. You have to add this attribute: [RoomType("MyCode")] to your main game class. You can read more about this on our blog: http://playerio.com/blog/ }
                        WriteLine("error: " + new { e.errorID, e.message });

                    };

                    Action<global::playerio.Client> handleConnect = client =>
                    {
                        WriteLine("Sucessfully connected to player.io");

                        var multiplayer = client.multiplayer;

                        //Set developmentsever (Comment out to connect to your server online)
                        multiplayer.developmentServer = "localhost:8184";

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

                            this.content.MouseLeftButtonUp +=
                               (sender, e) =>
                               {
                                   var p = e.GetPosition(content);

                                   var m = connection.createMessage("hello", "howdy! " + new { p.X, p.Y });

                                   connection.sendMessage(m);
                               };


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


                            this.__game_postMessage = (XElement e) =>
                            {
                                this.__game_onmessage(e);
                            };

                            connection.addMessageHandler("__game_postMessage",
                              (m, userid) =>
                              {
                                  if (m.length > 0)
                                  {
                                      this.__game_postMessage(XElement.Parse(m.getString(m.length - 1)));
                                  }

                              }
                          );

                            this.__context_postMessage +=
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
                        "test-4jazuo9jw0qx0cye9ihrqg",			//Game id (Get your own at playerio.com)
                        "public",							//Connection id, default is public
                        "GuestUser",						//Username
                        "",									//User auth. Can be left blank if authentication is disabled on connection
                        null,								//Current PartnerPay partner.
                        callback: handleConnect.ToFunction(),						//Function executed on successful connect
                        errorhandler: handleError.ToFunction()							//Function executed if we recive an error
                    );

                    WriteLine("after connect");

                }
            );
        }


    }

    public sealed class ApplicationSprite : ApplicationSpriteContent
    {


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
