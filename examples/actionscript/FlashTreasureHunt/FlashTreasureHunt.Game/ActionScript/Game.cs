using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = ControlWidth, Height = FlashTreasureHunt.DefaultControlHeight)]
	[SWF(width = ControlWidth, height = FlashTreasureHunt.DefaultControlHeight, backgroundColor = 0)]
	public class Game : Sprite
	{
		public const int ControlWidth = FlashTreasureHunt.DefaultControlWidth + NonobaClient.NonobaChatWidth;


		/// <summary>
		/// Default constructor
		/// </summary>
		public Game()
		{
			// nonoba + mochiad
			PlayMultiPlayer();


		}

		private void PlayMultiPlayer()
		{
			var g = new NonobaClient("arvo-pc");

			//g.Element.x = (DefaultWidth - MultiPlayer.NonobaClient.DefaultWidth) / 2;

			g.Element.AttachTo(this);
		}


		private void PlaySinglePlayer()
		{
			new global::FlashTreasureHunt.ActionScript.FlashTreasureHunt().AttachTo(this);
		}
	}
}