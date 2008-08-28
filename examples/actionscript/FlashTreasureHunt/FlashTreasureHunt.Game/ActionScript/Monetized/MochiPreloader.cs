using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.MochiLibrary;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

namespace FlashTreasureHunt.ActionScript.Monetized
{
	[Script, ScriptApplicationEntryPoint(Width = Game.ControlWidth, Height = FlashTreasureHunt.DefaultControlHeight)]
	[SWF(width = Game.ControlWidth, height = FlashTreasureHunt.DefaultControlHeight, backgroundColor = 0)]
	public class MochiPreloader : MochiAdPreloaderBase
	{
		public MochiPreloader()
		{
			this.InvokeWhenStageIsReady(
				delegate
				{
					_mochiads_game_id = FlashTreasureHunt.PublishedInfo.MochiAd;

					showPreGameAd(
						delegate
						{
							new Game().AttachTo(this);
						}
					);
				}
			);
		}
	}
}
