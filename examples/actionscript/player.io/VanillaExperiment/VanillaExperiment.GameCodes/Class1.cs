
using PlayerIO.GameLibrary;
using System;
using System.IO;
namespace MyGame
{

    public class Player : BasePlayer
    {
        public string Name;
    }

    //---------------------------
    //Not a game dll
    //---------------------------
    //Sorry, that is not a valid game dll. Valid game dlls must have a class that inherits from PlayerIO.GameLibrary.Game<Player> having a [RoomType(...)] attribute and must follow the rules for valid gamecode.
    //---------------------------
    //OK   
    //---------------------------

    // error: { errorID = 16, message = Could not find a game class with the correct room type: x. You have to add this attribute: [RoomType("x")] to your main game class. You can read more about this on our blog: http://playerio.com/blog/ }


    //Changing dll to user picked dll
    // - error: Could not find any Player.IO room types [RoomType(...)] in the dll.

    //Changing dll to user picked dll
    // - error: Reference found to Player.IO Development Server. You can only reference PlayerIO.GameLibrary.dll
    // - error: MyGame.Program.Main(...) uses the non-allowed method: System.Void PlayerIO.DevelopmentServer.Server::StartWithDebugging()

    //    Trying last loaded dll
    // - error: MyGame.XConsole.Out is static -- Static fields are disallowed

    //Trying last loaded dll
    // - error: Could not find any Player.IO room types [RoomType(...)] in the dll.

    //Checking if VanillaExperiment.GameCodes.dll is a game dll (from application directory)
    // - error: Could not find any Player.IO room types [RoomType(...)] in the dll.

    //Checking if VanillaExperiment.GameCodes.dll is a game dll (from application directory)
    // - error: MyGame.XConsole.Out is static -- Static fields are disallowed

    //public static class XConsole
    //{
    //    public static TextWriter Out;
    //}

    [RoomType("x")]
    public class GameCode : Game<Player>
    {
        // This method is called when an instance of your the game is created
        public override void GameStarted()
        {
            // anything you write to the Console will show up in the 
            // output window of the development server
            Console.WriteLine("xGame is started: " + RoomId);

            // This is how you setup a timer
            AddTimer(delegate
            {
                // code here will code every 100th millisecond (ten times a second)
            }, 100);

            // Debug Example:
            // Sometimes, it can be very usefull to have a graphical representation
            // of the state of your game.
            // An easy way to accomplish this is to setup a timer to update the
            // debug view every 250th second (4 times a second).
            AddTimer(delegate
            {
                // This will cause the GenerateDebugImage() method to be called
                // so you can draw a grapical version of the game state.
                RefreshDebugView();
            }, 250);
        }

        // This method is called when the last player leaves the room, and it's closed down.
        public override void GameClosed()
        {
            Console.WriteLine("RoomId: " + RoomId);
        }

        // This method is called whenever a player joins the game
        public override void UserJoined(Player player)
        {
            // this is how you send a player a message
            player.Send("hello");

            // this is how you broadcast a message to all players connected to the game
            Console.WriteLine("xUserJoined: " + player.Id);
            Broadcast("UserJoined", player.Id, "fooo");
        }

        // This method is called when a player leaves the game
        public override void UserLeft(Player player)
        {
            Broadcast("UserLeft", player.Id);
        }

        // This method is called when a player sends a message into the server code
        public override void GotMessage(Player player, Message message)
        {
            switch (message.Type)
            {
                // This is how you would set a players name when they send in their name in a 
                // "MyNameIs" message
                case "MyNameIs":
                    player.Name = message.GetString(0);
                    break;


                case "hello":
                    Console.WriteLine(new { player.Id, rest = message.GetString(0) });


                    this.Broadcast("hello", "server rebroadcast, " + new { player.Id, rest = message.GetString(0) });
                    player.Send("xhello", "server rebroadcast, " + new { player.Id, rest = message.GetString(0) });
                    break;
            }
        }


        // During development, it's very usefull to be able to cause certain events
        // to occur in your serverside code. If you create a public method with no
        // arguments and add a [DebugAction] attribute like we've down below, a button
        // will be added to the development server. 
        // Whenever you click the button, your code will run.
        [DebugAction("Play", DebugAction.Icon.Play)]
        public void PlayNow()
        {
            Console.WriteLine("The xplay button was clicked!");
        }


    }
}
