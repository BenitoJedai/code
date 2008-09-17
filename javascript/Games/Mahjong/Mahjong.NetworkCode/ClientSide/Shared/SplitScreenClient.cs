using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;

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

			
			// game instance that would run on the server
			var s = new VirtualGame
			{

			};

			var a_connection = new Future();
			var b_connection = new Future();

			var a_to_server = new Communication.Bridge { 
				VirtualLatency = e => a_connection.Continue(() => Lag.AtDelay(e)) 
			};
			var server_to_a = new Communication.Bridge { VirtualLatency = delegate {} };
			a_connection.Continue(() => server_to_a.VirtualLatency = a_to_server.VirtualLatency);

			var b_to_server = new Communication.Bridge { 
				
				VirtualLatency = e => b_connection.Continue(() => Lag.AtDelay(e)) 
			};
			var server_to_b = new Communication.Bridge { VirtualLatency = delegate {} };
			b_connection.Continue(() => server_to_b.VirtualLatency = b_to_server.VirtualLatency);

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





			var player_a_user = 7;
			var player_b_user = 8;

			var player_a = new VirtualPlayer
			{
				FromPlayer = a_to_server,
				ToPlayer = server_to_a,
				ToOthers = server_to_b,
				
				UserId = player_a_user,
				Username = "Lefty",
				AddScore = delegate { },

			};


			var player_b = new VirtualPlayer
			{
				FromPlayer = b_to_server,
				ToPlayer = server_to_b,
				ToOthers = server_to_a,
				UserId = player_b_user,
				Username = "Righty",
				AddScore = delegate { },
			};

			#region attach automatic routing
			new Communication.RemoteEvents.WithUserArgumentsRouter_Broadcast
			{
				user = player_a.UserId,
				Target = player_a.ToOthers
			}.CombineDelegates(a_to_server);

			new Communication.RemoteEvents.WithUserArgumentsRouter_Singlecast
			{
				user = player_a.UserId,
				Target =
					user =>
					{
						if (user == player_b.UserId)
							return player_b.ToPlayer;

						return null;
					}
			}.CombineDelegates(a_to_server);

			new Communication.RemoteEvents.WithUserArgumentsRouter_Broadcast
			{
				user = player_b.UserId,
				Target = player_b.ToOthers
			}.CombineDelegates(b_to_server);

			new Communication.RemoteEvents.WithUserArgumentsRouter_Singlecast
			{
				user = player_b.UserId,
				Target = 
					user =>
					{
						if (user == player_a.UserId)
							return player_a.ToPlayer;

						return null;
					}
			}.CombineDelegates(b_to_server);
			#endregion



			a.InitializeEvents();
			b.InitializeEvents();

			s.GameStarted();

			a.InitializeMap();
			b.InitializeMap();


			// enable virtualized network connection for lefty
			a_connection.Signal();

			s.Users.Add(player_a);
			s.UserJoined(player_a);



			// enable virtualized network connection for righty
			b_connection.Signal();

			s.Users.Add(player_b);
			s.UserJoined(player_b);

		
		}
	}
}
