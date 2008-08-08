using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FlashSpaceInvaders.ActionScript.Extensions;
using FlashSpaceInvaders.ActionScript.FragileEntities;
using FlashSpaceInvaders.ActionScript.StarShips;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.mx.core;
using FlashSpaceInvaders.Shared;

namespace FlashSpaceInvaders.ActionScript
{
	/// <summary>
	/// testing...
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF(backgroundColor = Colors.Black, width = DefaultWidth, height = Game.DefaultHeight)]
	public class FlashSpaceInvaders : Sprite
	{
		public const int DefaultWidth = Game.DefaultWidth * 2;

		// todo: add http://gimme.badsectoracula.com/flashmodplayer/modplayer.html

		// http://zproxy.wordpress.com/2007/03/03/jsc-space-invaders/

		// http://cdexos.sourceforge.net/?q=download


		public FlashSpaceInvaders()
		{
			// why the virtual latency doesnt work?
			//Action<Action> VirtualLatency = e => 200.AtDelayDo(e);
			Action<Action> VirtualLatency = e => e();

			var a_to_server = new SharedClass1.Bridge { VirtualLatency = VirtualLatency };
			var server_to_a = new SharedClass1.Bridge { VirtualLatency = VirtualLatency };

			var b_to_server = new SharedClass1.Bridge { VirtualLatency = VirtualLatency };
			var server_to_b = new SharedClass1.Bridge { VirtualLatency = VirtualLatency };

			var a = new MultiPlayer.Client
			{
				Events = server_to_a,
				Messages = a_to_server
			};

			var b = new MultiPlayer.Client
			{
				Events = server_to_b,
				Messages = b_to_server
			};

			var s = new MyGame
			{
				
			};

			b.Element.x = Game.DefaultWidth;

			a.Element.AttachTo(this);
			b.Element.AttachTo(this);


		
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

		private void PlayMultiPlayer()
		{
			var g = new MultiPlayer.NonobaClient();

			g.Element.x = (DefaultWidth - MultiPlayer.NonobaClient.DefaultWidth) / 2;

			g.Element.AttachTo(this);
		}

		void SinglePlayer()
		{
			var g = new Game
			{
				x = DefaultWidth / 4
			};

			g.AttachTo(this);
		}
	}

}
