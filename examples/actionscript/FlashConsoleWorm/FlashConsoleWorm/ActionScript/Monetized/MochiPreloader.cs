using System;
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
		height = PreloaderContent.OuterControlHeight,
		backgroundColor = 0
		)]
	[GoogleGadget(
		   author_email = "dadeval@gmail.com",
		   author_link = "http://zproxy.wordpress.com",
		   author = "Arvo Sulakatko",
		   category = "lifestyle",
		   category2 = "funandgames",
		   screenshot = "http://files.nonoba.com/GameScreenshot/2282/screenshot_rev1.png",
		   thumbnail = "http://files.nonoba.com/GameIcon/2282/gameicon_rev1.png",
		   description = MochiPreloader.Description,
		   width = PreloaderContent.OuterControlWidth,
		   height = PreloaderContent.OuterControlHeight,
		   title = MochiPreloader.Title,
		   title_url = MochiPreloader.NonobaLink

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

		public const string NonobaLink = "http://nonoba.com/zproxy/flashconsoleworm";
		// http://www.mochiads.com/games/flashconsoleworm/

		public MochiPreloader()
		{
			this.InvokeWhenStageIsReady(
				delegate
				{
					_mochiads_game_id = MochiAdKey;

					showPreGameAd(
						delegate
						{
							new PreloaderContent().AttachTo(stage);
						}
					);
				}
			);
		}
	}
}
