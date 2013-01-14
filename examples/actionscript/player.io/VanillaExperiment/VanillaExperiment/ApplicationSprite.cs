using playerio;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;

namespace VanillaExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public event Action<string> AtWriteLine;


        public void WhenReady()
        {
            Action<string> WriteLine = e =>
                {
                    if (AtWriteLine != null)
                        AtWriteLine(e);
                };

            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);


                    Action<PlayerIOError> handleError = e =>
                     {
                         // error: { errorID = 10, message = Unknown game id: [Enter your game id here] }

                         // error: { errorID = 16, message = Could not find a game class with the correct room type: MyCode. You have to add this attribute: [RoomType("MyCode")] to your main game class. You can read more about this on our blog: http://playerio.com/blog/ }
                         WriteLine("error: " + new { e.errorID, e.message });

                     };

                    Action<Client> handleConnect = client =>
                    {
                        WriteLine("Sucessfully connected to player.io");

                        var multiplayer = client.multiplayer;

                        //Set developmentsever (Comment out to connect to your server online)
                        multiplayer.developmentServer = "localhost:8184";

                        Action<Connection> handleJoin = c =>
                        {

                        };

                        //                        V:\web\VanillaExperiment\ApplicationSprite___c__DisplayClass6.as(26): col: 20 Error: Call to a possibly undefined method get_multiplayer through a reference with static type playerio:Client.

                        //            client.get_multiplayer().set_developmentServer("localhost:8184");
                        //                   ^

                        //V:\web\VanillaExperiment\ApplicationSprite___c__DisplayClass6.as(34): col: 20 Error: Call to a possibly undefined method get_multiplayer through a reference with static type playerio:Client.

                        //            client.get_multiplayer().createJoinRoom("test", "MyCode", true, {}, {}, CommonExtensions.ToFunction_100665553(action_10), CommonExtensions.ToFunction_100665553(this.handleError));

                        //Create pr join the room test
                        multiplayer.createJoinRoom(
                            "test",								//Room id. If set to null a random roomid is used
                            "MyCode",							//The game type started on the server
                            true,								//Should the room be visible in the lobby?
                            new object(),									//Room data. This data is returned to lobby list. Variabels can be modifed on the server
                            new object(),									//User join data
                            callback: handleJoin.ToFunction(),							//Function executed on successful joining of the room
                            errorHandler: handleError.ToFunction()							//Function executed if we got a join error
                        );
                    };



                    WriteLine("before connect");

                    PlayerIO.connect(
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
}
