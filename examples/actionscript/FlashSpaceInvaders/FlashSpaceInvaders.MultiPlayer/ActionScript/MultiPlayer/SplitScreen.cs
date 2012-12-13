using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.Shared;
using FlashSpaceInvaders.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript.MultiPlayer
{
	[Script]
	public class SplitScreen
	{
		public readonly Client Lefty;
		public readonly Client Righty;

		public readonly Action JoinRightyToGame;

		public SplitScreen()
		{
			var a_to_server = new SharedClass1.Bridge();
			var server_to_a = new SharedClass1.Bridge();

			var b_to_server = new SharedClass1.Bridge();
			var server_to_b = new SharedClass1.Bridge();

			var a = new MultiPlayer.Client
			{
				Events = server_to_a,
				Messages = a_to_server
			};

			this.Lefty = a;

			var b = new MultiPlayer.Client
			{
				Events = server_to_b,
				Messages = b_to_server
			};

			this.Righty = b;

			var s = new MyGame
			{

			};





			var player_a = new MyPlayer
			{
				FromPlayer = a_to_server,
				ToPlayer = server_to_a,
				ToOthers = server_to_b,
				UserId = 0,
				Username = "Lefty",
				AddScore = delegate { },

			};

			new SharedClass1.RemoteEvents.WithUserArgumentsRouter
			{
				user = player_a.UserId,
				Target = player_a.ToOthers
			}.CombineDelegates(a_to_server);

			var player_b = new MyPlayer
			{
				FromPlayer = b_to_server,
				ToPlayer = server_to_b,
				ToOthers = server_to_a,
				UserId = 1,
				Username = "Righty",
				AddScore = delegate { },

			};

			new SharedClass1.RemoteEvents.WithUserArgumentsRouter
			{
				user = player_b.UserId,
				Target = player_b.ToOthers
			}.CombineDelegates(b_to_server);




			a.InitializeEvents();
			b.InitializeEvents();

			s.GameStarted();

			a.InitializeMapOnce();
			b.InitializeMapOnce();

			s.Users.Add(player_a);
			s.UserJoined(player_a);



			s.Users.Add(player_b);
			s.UserJoined(player_b);

		}
	}
}
