using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Nonoba.Generic;
using Nonoba.GameLibrary;

namespace Mahjong.NetworkCode.Shared
{
	[Script]
	public partial class VirtualGame : ServerGameBase<Communication.IEvents, Communication.IMessages, VirtualPlayer>
	{




		public override void UserJoined(VirtualPlayer player)
		{
			Console.WriteLine("- UserJoined " + player.Username);


			var a100 = new AvailibleAchievement(player.AwardAchievement, "a100");
			var a1000 = new AvailibleAchievement(player.AwardAchievement, "a1000");
			var a50000 = new AvailibleAchievement(player.AwardAchievement, "a50000");
			var aLC = new AvailibleAchievement(player.AwardAchievement, "alc");

			player.FromPlayer.AddScore += e =>
				{
					player.AddScore("score", e.score);

					if (e.score > 100)
						a100.Give();

					if (e.score > 1000)
						a1000.Give();

					if (e.score > 50000)
						a50000.Give();
				};

			player.FromPlayer.AwardAchievementLayoutCompleted +=
				e =>
				{
					aLC.GiveMultiple();
				};

			//var firstblood = new AvailibleAchievement(player.AwardAchievement, "firstblood");
			//var portalfound = new AvailibleAchievement(player.AwardAchievement, "portalfound");
			//var getrich = new AvailibleAchievement(player.AwardAchievement, "getrich");
			//var massacre = new AvailibleAchievement(player.AwardAchievement, "massacre");
			//var levelup = new AvailibleAchievement(player.AwardAchievement, "levelup");


			//var x = AnyOtherUser(player);

			//player.FromPlayer.LockGame += e => this.GameState = MyGame.GameStateEnum.ClosedGameInProgress;
			//player.FromPlayer.UnlockGame += e => this.GameState = MyGame.GameStateEnum.OpenGameInProgress;

			//var total_score = 0;
			//var total_kills = 0;
			//var total_level = 0;

			//// registered nonoba rankings
			//player.FromPlayer.ReportScore +=
			//    e =>
			//    {
			//        if (e.level > 0)
			//            exitfound.Give();

			//        if (e.kills > 0)
			//            firstblood.Give();

			//        if (e.teleports > 0)
			//            portalfound.Give();

			//        total_score += e.score;
			//        total_kills += e.kills;
			//        total_level += e.level;

			//        if (total_score > 2000)
			//            getrich.Give();

			//        if (total_kills > 50)
			//            massacre.Give();

			//        if (total_level > 15)
			//            levelup.Give();

			//        player.AddScore("score", e.score);
			//        player.AddScore("kills", e.kills);
			//        player.AddScore("level", e.level);
			//        player.AddScore("teleports", e.teleports);
			//        player.SetScore("fps", e.fps);
			//    };



			//player.FromPlayer.AwardAchievementFirst += e => player.AwardAchievement("first");
			//player.FromPlayer.AwardAchievementFiver += e => player.AwardAchievement("fiver");
			//player.FromPlayer.AwardAchievementUFOKill += e => player.AwardAchievement("ufokill");
			//player.FromPlayer.AwardAchievementMaxGun += e => player.AwardAchievement("maxgun");

			//var user_with_map = -1;

			//if (x != null)
			//{
			//    user_with_map = x.UserId;
			//}

			var navbar = 1;
			var layoutinput = 1;
			var vote = 1;
			var hints = 0;

			if (this.Settings.GetBoolean(SettingsInfo.navbar, false))
				navbar = 0;

			if (this.Settings.GetBoolean(SettingsInfo.layoutinput, false))
				layoutinput = 0;

			if (this.Settings.GetBoolean(SettingsInfo.vote, false))
				vote = 0;

			if (this.Settings.GetBoolean(SettingsInfo.hints, false))
				hints = 1;

			// let new player know how it is named, also send magic bytes to verify
			player.ToPlayer.ServerPlayerHello(
				player.UserId, player.Username, this.Users.Count - 1,
				navbar,
				vote,
				layoutinput,
				hints,
				new Handshake().Bytes
			);

			// let other players know that there is a new player in the map
			player.ToOthers.ServerPlayerJoined(
			   player.UserId, player.Username
			);

			var PreventStatic = 0;

			player.FromPlayer.ServerPlayerHello +=
				e =>
				{
					var StaticPrevented = PreventStatic;

					new Handshake().Verify(e.handshake);
				};

			player.FromPlayer.UserMapResponse +=
				e =>
				{
					var StaticPrevented = PreventStatic;

					Console.WriteLine("map: " + e.bytes.Length);

				};
				
		}

	

		public override void UserLeft(VirtualPlayer player)
		{
			player.ToOthers.ServerPlayerLeft(player.UserId, player.Username);
		}

		public override void GameStarted()
		{
		}

	}
}
