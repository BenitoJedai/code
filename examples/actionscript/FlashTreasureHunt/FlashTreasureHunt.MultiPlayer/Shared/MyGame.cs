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

		[Script]
		public class AvailibleAchievement
		{
			public readonly string Key;

			bool GivenOnce;

			public void Give()
			{
				if (GivenOnce)
					return;

				GivenOnce = true;

				_Submit(Key);
			}

			Func<string, uint> _Submit;

			public AvailibleAchievement(Func<string, uint> Submit, string Key)
			{
				this._Submit = Submit;
				this.Key = Key;
			}
		}

		public override void UserJoined(MyPlayer player)
		{
			Console.WriteLine("- UserJoined " + player.Username);


			var exitfound = new AvailibleAchievement(player.AwardAchievement, "exitfound");
			var firstblood = new AvailibleAchievement(player.AwardAchievement, "firstblood");
			var portalfound = new AvailibleAchievement(player.AwardAchievement, "portalfound");
			var getrich = new AvailibleAchievement(player.AwardAchievement, "getrich");
			var massacre = new AvailibleAchievement(player.AwardAchievement, "massacre");
			var levelup = new AvailibleAchievement(player.AwardAchievement, "levelup");


			var x = AnyOtherUser(player);

			//player.FromPlayer.LockGame += e => this.GameState = MyGame.GameStateEnum.ClosedGameInProgress;
			//player.FromPlayer.UnlockGame += e => this.GameState = MyGame.GameStateEnum.OpenGameInProgress;

			var total_score = 0;
			var total_kills = 0;
			var total_level = 0;

			//// registered nonoba rankings
			player.FromPlayer.ReportScore +=
				e =>
				{
					if (e.level > 0)
						exitfound.Give();

					if (e.kills > 0)
						firstblood.Give();

					if (e.teleports > 0)
						portalfound.Give();

					total_score += e.score;
					total_kills += e.kills;
					total_level += e.level;

					if (total_score > 2000)
						getrich.Give();

					if (total_kills > 50)
						massacre.Give();

					if (total_level > 15)
						levelup.Give();

					player.AddScore("score", e.score);
					player.AddScore("kills", e.kills);
					player.AddScore("level", e.level);
					player.AddScore("teleports", e.teleports);
					player.SetScore("fps", e.fps);
				};


			////player.FromPlayer.AddScore += e => player.AddScore("worms", e.worms);

			//player.FromPlayer.AwardAchievementFirst += e => player.AwardAchievement("first");
			//player.FromPlayer.AwardAchievementFiver += e => player.AwardAchievement("fiver");
			//player.FromPlayer.AwardAchievementUFOKill += e => player.AwardAchievement("ufokill");
			//player.FromPlayer.AwardAchievementMaxGun += e => player.AwardAchievement("maxgun");

			var user_with_map = -1;

			if (x != null)
			{
				user_with_map = x.UserId;
			}

			// let new player know how it is named, also send magic bytes to verify
			player.ToPlayer.ServerPlayerHello(
				player.UserId, player.Username, user_with_map, new Handshake().ToArray()
			);

			// let other players know that there is a new player in the map
			player.ToOthers.ServerPlayerJoined(
			   player.UserId, player.Username
			);

			if (x != null)
			{
				// the new player wont wait forever for the new map.
				// if certain time passes and no map is recieved, it will use
				// its own map, this desyncing from others
				Console.WriteLine("Will ask for map transfer from " + x.Username);
				x.ToPlayer.ServerSendMap();
			}

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
