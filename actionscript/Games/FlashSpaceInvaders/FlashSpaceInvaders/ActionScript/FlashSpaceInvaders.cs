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
		
			var g = new Game
				{
					x = DefaultWidth  / 4
				};


			g.AttachTo(this);
		}

	}

}
