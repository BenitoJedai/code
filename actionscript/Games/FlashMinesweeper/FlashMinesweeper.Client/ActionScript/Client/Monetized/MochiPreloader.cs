using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.MochiLibrary;
using ScriptCoreLib.Shared;

namespace FlashMinesweeper.ActionScript.Client.Monetized
{
	[Script,
	ScriptApplicationEntryPoint(
		Width = TeamPlay.DefaultControlWidth + TeamPlay.NonobaChatWidth, Height = TeamPlay.DefaultControlHeight)]
	[SWF(width = TeamPlay.DefaultControlWidth + TeamPlay.NonobaChatWidth, height = TeamPlay.DefaultControlHeight, backgroundColor = 0)]
	[GoogleGadget(
		   author_email = "zproxy@hot.ee",
		   author_link = "http://zproxy.wordpress.com",
		   author = "Arvo Sulakatko",
		   category = "lifestyle",
		   category2 = "funandgames",
		   screenshot = "http://jsc.sourceforge.net/examples/web/MineSweeper/assets/MineSweeper/Preview.png",
		   thumbnail = "http://jsc.sourceforge.net/examples/web/MineSweeper/assets/MineSweeper/Preview.png",
		   description = "Classic minesweeper game, compiled from c# source to actionscript",
		   width = FlashMinesweeper.DefaultControlWidth + TeamPlay.NonobaChatWidth,
		   height = FlashMinesweeper.DefaultControlHeight,
		   title = "FlashMinesweeper",
		   title_url = "http://nonoba.com/zproxy/flashminesweepermp"

	   )]
	[Frame(typeof(MochiAdPreloaderImplementation))]
	public class MochiPreloader : Sprite
	{


		public MochiPreloader()
		{
			this.InvokeWhenStageIsReady(
				delegate
				{
					new TeamPlay().AttachTo(stage);
				}
			);
		}
	}

	[Script]
	public class MochiAdPreloaderImplementation : MochiAdPreloader
	{
		[TypeOfByNameOverride]
		public override Type GetTargetType()
		{
			return typeof(MochiPreloader);
		}


		public override bool IsBackgroundVisible()
		{
			return false;
		}

		public override bool AdsEnabled()
		{
			//#if PUBLISH
			return true;
			//#else
			//            return false;
			//#endif
		}

		//[Embed(source = "/assets/AvalonTycoonMansion/preview.jpg", mimeType = "image/jpeg")]
		//internal static Class CustomBackground;

		public MochiAdPreloaderImplementation()
			: base("5a5be1df755e6cdc")
		{
		}

		public override void InitializeBackground()
		{
			//CustomBackground.ToBitmapAsset().AttachTo(this);
		}
	}
}
