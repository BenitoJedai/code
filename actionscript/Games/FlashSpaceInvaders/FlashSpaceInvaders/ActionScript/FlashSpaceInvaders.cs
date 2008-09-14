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
using ScriptCoreLib.ActionScript.MochiLibrary;

namespace FlashSpaceInvaders.ActionScript
{
	/// <summary>
	/// testing...
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = DefaultWidth, Height = Game.DefaultHeight)]
	[SWF(backgroundColor = Colors.Black, width = DefaultWidth, height = Game.DefaultHeight)]
	[GoogleGadget(
	   author_email = "dadeval@gmail.com",
	   author_link = "http://zproxy.wordpress.com",
	   author = "Arvo Sulakatko",
	   category = "lifestyle",
	   category2 = "funandgames",
	   screenshot = "http://picon.ngfiles.com/453000/portal_453076.gif",
	   thumbnail = "http://picon.ngfiles.com/453000/portal_453076.gif",
	   description = FlashSpaceInvaders.Description,
	   width = DefaultWidth,
	   height = Game.DefaultHeight,
	   title = "FlashSpaceInvaders",
	   title_url = "http://nonoba.com/zproxy/flashspaceinvaders"

   )]
	public class FlashSpaceInvaders : MochiAdPreloaderBase
	{
		public const int DefaultWidth = Game.DefaultWidth + NonobaClient.NonobaChatWidth;

		// todo: add http://gimme.badsectoracula.com/flashmodplayer/modplayer.html

		// http://zproxy.wordpress.com/2007/03/03/jsc-space-invaders/

		// http://cdexos.sourceforge.net/?q=download

		// http://en.wikipedia.org/wiki/Space_Invaders


		public const string MochiAdKey = "5ea4cb6ec61420b1";

		const string Description = "A remake of the classic Space Invaders.";

		const string Instructions = "Use arrow keys or mouse to move around. Space to shoot.";

		public FlashSpaceInvaders()
		{
			//PlayMultiPlayerWithMochi();
			PlaySplitScreen();
			
		}

		public override DisplayObject CreateInstance()
		{
			throw new NotImplementedException();
		}

		private void PlayMultiPlayerWithMochi()
		{
			this.InvokeWhenStageIsReady(
				delegate
				{
					_mochiads_game_id = MochiAdKey;

					showPreGameAd(
						delegate
						{
							PlayMultiPlayer();
						}
					);
				}
			);
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

			//g.Element.x = (DefaultWidth - MultiPlayer.NonobaClient.DefaultWidth) / 2;

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
