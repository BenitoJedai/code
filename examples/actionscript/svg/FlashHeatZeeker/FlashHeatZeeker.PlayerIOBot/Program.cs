using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;
using System;
using System.Xml.Linq;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace FlashHeatZeeker.PlayerIOBot
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static string __gameid = "test-4jazuo9jw0qx0cye9ihrqg";

        public static void BotTest()
        {
            //---- Connecting to Player.IO  --
            //--------------------------------
            Console.WriteLine("Connect to Player.IO...");

            // Additional information: Connection to Player.IO WebService Unexpectly Terminated
            var client = PlayerIOClient.PlayerIO.Connect(
                __gameid,	// Game id (Get your own at playerio.com. 1: Create user, 2:Goto admin pannel, 3:Create game, 4: Copy game id inside the "")
                "public",						// The id of the connection, as given in the settings section of the admin panel. By default, a connection with id='public' is created on all games.
                "user-id",						// The id of the user connecting. This can be any string you like. For instance, it might be "fb10239" if you´re building a Facebook app and the user connecting has id 10239
                null,							// If the connection identified by the connection id only accepts authenticated requests, the auth value generated based on UserId is added here
                null							// The partnerid to tag the user with, if using PartnerPay
            );
            Console.WriteLine("Connected to Player.IO");

            //---- BigDB Example       -------
            //--------------------------------

            // load my player object from BigDB
            var myPlayerObject = client.BigDB.LoadMyPlayerObject();
            myPlayerObject.Set("awesome", true); // set properties
            myPlayerObject.Save(); // save changes


            //---- Multiplayer Example -------
            //--------------------------------

            // join a multiplayer room

            //            at PlayerIOClient.Connection..ctor(ServerEndpoint endpoint, String joinKey, Dictionary`2 joinData)
            //at PlayerIOClient.Internal.identifier512.CreateJoinRoom(String roomId, String roomType, Boolean visible, Dictionary`2 roomData, Dictionary`2 joinData)
            //at FlashHeatZeeker.TestBot.Program.BotTest() in x:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.TestBot\Program.cs:line 41
            //at FlashHeatZeeker.TestBot.Program.Main(String[] args) in x:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.TestBot\Program.cs:line 66


            var connection = client.Multiplayer.CreateJoinRoom(
                "test", "x", true, null, null
                );

            Console.WriteLine("Joined Multiplayer Room");

            var egoid = 1337;

            #region __transport_out
            Action<string> __transport_out =
                xml =>
                {
                    connection.Send(
                        "__context_postMessage",
                        xml.ToString()
                    );
                };
            #endregion


            (1000 / 15).AtIntervalWithCounter(
                syncfameid =>
                {
                    // { xml = <SetVelocityFromInput egoid="437787647" i="437787647:ego" k="0" x="139.64736611187865" y="-4.662922528074573" angle="9.574294529666886" /> }

                    {
                        var __identity = egoid + ":ego";
                        var __KeySample = "0";
                        var __fixup_x = "139";
                        var __fixup_y = "-4";
                        var __fixup_angle = "9";

                        var xml = new XElement("SetVelocityFromInput",

                          new XAttribute("egoid", egoid),


                          new XAttribute("i", __identity),
                          new XAttribute("k", __KeySample),
                          new XAttribute("x", __fixup_x),
                          new XAttribute("y", __fixup_y),
                          new XAttribute("angle", __fixup_angle)

                      );

                        __transport_out(xml.ToString());
                    }

                    //msg[0] = <sync egoid="437787647"/>  (6)

                    {
                        var xml = new XElement("sync", new XAttribute("egoid", egoid));

                        __transport_out(xml.ToString());
                    }
                }
            );

            // on message => print to console
            connection.OnMessage += delegate(object sender, PlayerIOClient.Message m)
            {
                if (m.Type == "__game_postMessage")
                {
                    var xml = XElement.Parse(
                        m.GetString(0)
                    );

                    // { xml = <sync egoid="437787647" /> }
                    if (xml.Name.LocalName == "sync")
                    {
                        // goot to know.. discard
                        return;
                    }

                    // { xml = <SetVelocityFromInput egoid="437787647" i="437787647:ego" k="0" x="139.64736611187865" y="-4.662922528074573" angle="9.574294529666886" /> }

                    Console.WriteLine(new { xml });

                    //                msg.Type= __game_postMessage, 1 entries
                    //msg[0] = <sync egoid="437787647"/>  (6)
                    return;
                }


                Console.WriteLine(m.ToString());
            };

            // when disconnected => print reason
            connection.OnDisconnect += delegate(object sender, string reason)
            {
                Console.WriteLine("Disconnected, reason = " + reason);
            };

            Console.WriteLine(" - press enter to quit - ");
            Console.ReadLine();
        }


        public static void Main(string[] args)
        {
            BotTest();
#if DEBUG
            DesktopAvalonExtensions.Launch(
                () => new ApplicationCanvas()
            );
#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }
}
