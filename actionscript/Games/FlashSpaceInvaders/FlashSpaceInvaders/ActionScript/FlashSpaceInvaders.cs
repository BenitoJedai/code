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
using FlashSpaceInvaders.ActionScript.MultiPlayer;

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

			PlayMultiPlayer();
		}

		private void PlaySplitScreen()
		{
			var s = new SplitScreen();

			s.Righty.Element.x = Game.DefaultWidth;

			s.Lefty.Element.AttachTo(this);
			s.Lefty.Map.PlayerInput.MovementArrows.Enabled = false;

			s.Righty.Element.AttachTo(this);
			s.Righty.Map.PlayerInput.MovementWASD.Enabled = false;

		
		}

		private void PlayMultiPlayer()
		{
			var g = new MultiPlayer.NonobaClient();

			g.Element.x = (DefaultWidth - MultiPlayer.NonobaClient.DefaultWidth) / 2;

			g.Element.AttachTo(this);
		}

		void PlaySinglePlayer()
		{
			var g = new Game
			{
				x = DefaultWidth / 4
			};

			g.AttachTo(this);
		}
	}

}
