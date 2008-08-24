using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Nonoba.Generic;

namespace FlashTreasureHunt.Shared
{

    [Script]
    public class MyGame : ServerGameBase<SharedClass1.IEvents, SharedClass1.IMessages, MyPlayer>
    {


        public override void UserJoined(MyPlayer player)
        {
            Console.WriteLine("UserJoined " + player.Username);


            var x = AnyOtherUser(player);

			//player.FromPlayer.LockGame += e => this.GameState = MyGame.GameStateEnum.ClosedGameInProgress;
			//player.FromPlayer.UnlockGame += e => this.GameState = MyGame.GameStateEnum.OpenGameInProgress;

			//// registered nonoba rankings
			//player.FromPlayer.AddScore += e => player.AddScore("score", e.score);

			////player.FromPlayer.AddScore += e => player.AddScore("worms", e.worms);

			//player.FromPlayer.AwardAchievementFirst += e => player.AwardAchievement("first");
			//player.FromPlayer.AwardAchievementFiver += e => player.AwardAchievement("fiver");
			//player.FromPlayer.AwardAchievementUFOKill += e => player.AwardAchievement("ufokill");
			//player.FromPlayer.AwardAchievementMaxGun += e => player.AwardAchievement("maxgun");

			//player.ToPlayer.ServerPlayerHandshake(
			//    new  [] { 0x12, 0x34 }
			//);

            player.ToPlayer.ServerPlayerHello(
				player.UserId, player.Username
			);

            player.ToOthers.ServerPlayerJoined(
               player.UserId, player.Username
            );

			//if (x != null)
			//{
			//    x.ToPlayer.ServerSendMap();
			//}

        }

        public override void UserLeft(MyPlayer player)
        {
            player.ToOthers.ServerPlayerLeft(player.UserId, player.Username);
        }

        public override void GameStarted()
        {
        }

    }
}
