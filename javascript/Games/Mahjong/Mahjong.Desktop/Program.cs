using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.Desktop
{
	class ServerWindow : Canvas
	{
		public ServerWindow(Action SpawnClient)
		{
			Width = 300;
			Height = 200;

			var b = new Button { Content = "Spawn client", Margin = new Thickness(16), Padding = new Thickness(4) }.AttachTo(this);
			var b3 = new Button { Content = "Spawn 3 clients", Margin = new Thickness(16), Padding = new Thickness(4) }.AttachTo(this);

			Canvas.SetTop(b3, 100);

			b.Click += delegate { SpawnClient(); };
			b3.Click += delegate { SpawnClient(); SpawnClient(); SpawnClient(); };
		}
	}

	class Program
	{
		public const int Lag = 50;

		[STAThread]
		static public void Main(string[] args)
		{
			// we will start an inmemory server and launch 3 clients
			Func<Communication.Bridge> Bridge = 
				() => 
					new Communication.Bridge {
						VirtualLatency = e => Lag.AtDelay(e) 
						// VirtualLatency = e => e()
					};

			var Server = new VirtualGame
			{
				
			};

			Server.GameStarted();

			var UserId = 0;

			new ServerWindow(
				delegate
				{
					Console.WriteLine("new client");

					var server_to_client = Bridge();
					var client_to_server = Bridge();

					var u = new VirtualPlayer
					{
						UserId = UserId++,
						FromPlayer = client_to_server,
						ToPlayer = server_to_client,
						Username = "guest"
					};

					u.ToOthers =
						new Communication.RemoteMessages
						{
							VirtualTargets =
								() => Server.Users.Where(k => k != u).Select(k => k.ToPlayer)
						};

					new Communication.RemoteEvents.WithUserArgumentsRouter_Broadcast
					{
						user = u.UserId,
						Target = u.ToOthers
					}.CombineDelegates(client_to_server);

					new Communication.RemoteEvents.WithUserArgumentsRouter_Singlecast
					{
						user = u.UserId,
						Target =
							user => Server.Users.Where(k => k.UserId == user).Select(k => k.ToPlayer).Single()

					}.CombineDelegates(client_to_server);

				

					var c = new global::Mahjong.NetworkCode.ClientSide.Shared.Client();

					c.Messages = client_to_server;
					c.Events = server_to_client;

					c.InitializeMap();
					c.InitializeEvents();

					var w = c.Element.ToWindow();
						
						
					w.Show();

					Server.Users.Add(u);
					Server.UserJoined(u);

					w.Closed +=
						delegate
						{
							Server.Users.Remove(u);
							Server.UserLeft(u);

							server_to_client.VirtualLatency =
								delegate
								{

								};

							client_to_server.VirtualLatency =
								delegate
								{

								};
						};
				}
			).ToWindow().ShowDialog();
		}
	}
}
