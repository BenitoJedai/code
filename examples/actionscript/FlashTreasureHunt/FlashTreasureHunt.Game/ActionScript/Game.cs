using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.MochiLibrary.Ad;
using ScriptCoreLib.Shared;
using FlashTreasureHunt.ActionScript.Monetized;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = ControlWidth, Height = ControlHeight)]
	[SWF(width = ControlWidth, height = ControlHeight, backgroundColor = 0)]
	[Frame(typeof(MochiPreloader))]
	public class Game : Sprite
	{
		public const int ControlWidth = FlashTreasureHunt.DefaultControlWidth + NonobaClient.NonobaChatWidth;
		public const int ControlHeight = FlashTreasureHunt.DefaultControlHeight;


		/// <summary>
		/// Default constructor
		/// </summary>
		public Game()
		{
			// nonoba + mochiad
			
			PlayMultiPlayer();

			// PlaySinglePlayer();

		}

		private void PlayMultiPlayer()
		{
			new NonobaClient("arvo-pc").Element.AttachTo(this);
		}


		private void PlaySinglePlayer()
		{
			new FlashTreasureHunt().AttachTo(this);
		}
	}


}