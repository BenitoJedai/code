﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.MochiLibrary;

namespace FlashConsoleWorm.ActionScript.Monetized
{
	using PreloaderContent = Nonoba.Client;

	[Script,
	   ScriptApplicationEntryPoint(
		   Width = PreloaderContent.OuterControlWidth,
		   Height = PreloaderContent.OuterControlHeight
		   )]
	[SWF(
		width = PreloaderContent.OuterControlWidth,
		height = PreloaderContent.OuterControlHeight
		)]
	[GoogleGadget(
		   author_email = "dadeval@gmail.com",
		   author_link = "http://zproxy.wordpress.com",
		   author = "Arvo Sulakatko",
		   category = "lifestyle",
		   category2 = "funandgames",
		   screenshot = "http://jsc.sourceforge.net/?",
		   thumbnail = "http://jsc.sourceforge.net/?",
		   description = MochiPreloader.Description,
		   width = PreloaderContent.OuterControlWidth,
		   height = PreloaderContent.OuterControlHeight,
		   title = MochiPreloader.Title,
		   title_url = "http://nonoba.com/zproxy/?"

	   )]
	public class MochiPreloader : MochiAdPreloaderBase
	{
		// http://www.google.ee/search?hl=et&q=FlashConsoleWorm&btnG=Google+otsing&lr=
		public const string Title = "FlashConsoleWorm";

		// Internet Explorer should not be in "Offline Mode"
		public const string MochiAdKey = "046096cedbe4eec9";

		public const string Description = "Eat apples and grow. Smaller worms can also be eaten.";

		// how to play
		public const string Instructions = "Use arrowkeys or your mouse to change direction.";

		public const string Keywords = "worm, multiplayer";

		// http://nonoba.com/zproxy/flashconsoleworm
		// http://www.mochiads.com/games/flashconsoleworm/

		public MochiPreloader()
		{

			_mochiads_game_id = MochiAdKey;

			showPreGameAd(
				delegate
				{
					new PreloaderContent().AttachTo(stage);
				}
			);
		}
	}
}
