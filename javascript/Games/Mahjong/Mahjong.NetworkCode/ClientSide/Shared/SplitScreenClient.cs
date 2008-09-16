using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	[Script]
	public class SplitScreenClient
	{
		public readonly Client Lefty;
		public readonly Client Righty;

		public SplitScreenClient()
		{
			const int Lag = 200;

			var a_to_server = new Communication.Bridge { VirtualLatency = e => Lag.AtDelay(e) };
			var server_to_a = new Communication.Bridge { VirtualLatency = e => Lag.AtDelay(e) };

			var b_to_server = new Communication.Bridge { VirtualLatency = e => Lag.AtDelay(e) };
			var server_to_b = new Communication.Bridge { VirtualLatency = e => Lag.AtDelay(e) };

			var a = new Client
			{
				Events = server_to_a,
				Messages = a_to_server
			};

			this.Lefty = a;

			var b = new Client
			{
				Events = server_to_b,
				Messages = b_to_server
			};

			this.Righty = b;

			// game instance that would run on the server
			var s = new VirtualGame
			{

			};





			var player_a = new VirtualPlayer
			{
				FromPlayer = a_to_server,
				ToPlayer = server_to_a,
				ToOthers = server_to_b,
				UserId = 0,
				Username = "Lefty",
				AddScore = delegate { },

			};

			new Communication.RemoteEvents.WithUserArgumentsRouter
			{
				user = player_a.UserId,
				Target = player_a.ToOthers
			}.CombineDelegates(a_to_server);

			var player_b = new VirtualPlayer
			{
				FromPlayer = b_to_server,
				ToPlayer = server_to_b,
				ToOthers = server_to_a,
				UserId = 1,
				Username = "Righty",
				AddScore = delegate { },

			};

			new Communication.RemoteEvents.WithUserArgumentsRouter
			{
				user = player_b.UserId,
				Target = player_b.ToOthers
			}.CombineDelegates(b_to_server);




			a.InitializeEvents();
			b.InitializeEvents();

			s.GameStarted();

			a.InitializeMap();
			b.InitializeMap();

			s.Users.Add(player_a);
			s.UserJoined(player_a);



			s.Users.Add(player_b);
			s.UserJoined(player_b);
		}
	}
}
