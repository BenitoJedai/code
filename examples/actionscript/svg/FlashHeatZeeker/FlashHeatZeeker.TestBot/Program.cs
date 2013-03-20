using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;
using System;
using System.Diagnostics;

namespace FlashHeatZeeker.TestBot
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

            try
            {
                var connection = client.Multiplayer.CreateJoinRoom(
                    "test", "x", true, null, null
                    );

                Console.WriteLine("Joined Multiplayer Room");

                // on message => print to console
                connection.OnMessage += delegate(object sender, PlayerIOClient.Message m)
                {
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
            catch (Exception ex)
            {
                //+		ex	{"Object reference not set to an instance of an object."}	System.Exception {System.NullReferenceException}
                //-		st	{   at PlayerIOClient.Connection..ctor(ServerEndpoint endpoint, String joinKey, Dictionary`2 joinData)
                //   at PlayerIOClient.Internal.identifier512.CreateJoinRoom(String roomId, String roomType, Boolean visible, Dictionary`2 roomData, Dictionary`2 joinData)
                //   at FlashHeatZeeker.TestBot.Program.BotTest() in x:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.TestBot\Program.cs:line 50
                //}	System.Diagnostics.StackTrace
                //        FrameCount	0x00000003	int
                //-		frames	{System.Diagnostics.StackFrame[0x00000003]}	System.Diagnostics.StackFrame[]
                //-		[0x00000000]	{.ctor at offset 325 in file:line:column <filename unknown>:0:0
                //}	System.Diagnostics.StackFrame
                //        fIsLastFrameFromForeignExceptionStackTrace	false	bool
                //        iColumnNumber	0x00000000	int
                //        iLineNumber	0x00000000	int
                //        ILOffset	0x00000070	int
                //+		method	{Void .ctor(PlayerIOClient.ServerEndpoint, System.String, System.Collections.Generic.Dictionary`2[System.String,System.String])}	System.Reflection.MethodBase {System.Reflection.RuntimeConstructorInfo}
                //        offset	0x00000145	int
                //        strFileName	null	string
                //+		Static members		
                //-		[0x00000001]	{CreateJoinRoom at offset 226 in file:line:column <filename unknown>:0:0
                //}	System.Diagnostics.StackFrame
                //        fIsLastFrameFromForeignExceptionStackTrace	false	bool
                //        iColumnNumber	0x00000000	int
                //        iLineNumber	0x00000000	int
                //        ILOffset	0x00000031	int
                //+		method	{PlayerIOClient.Connection CreateJoinRoom(System.String, System.String, Boolean, System.Collections.Generic.Dictionary`2[System.String,System.String], System.Collections.Generic.Dictionary`2[System.String,System.String])}	System.Reflection.MethodBase {System.Reflection.RuntimeMethodInfo}
                //        offset	0x000000e2	int
                //        strFileName	null	string
                //+		Static members		
                //+		[0x00000002]	{BotTest at offset 225 in file:line:column x:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.TestBot\Program.cs:50:17
                //}	System.Diagnostics.StackFrame
                //        m_iMethodsToSkip	0x00000000	int
                //        m_iNumOfFrames	0x00000003	int


                var st = new StackTrace(ex, true);
                Debugger.Break();


            }
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
