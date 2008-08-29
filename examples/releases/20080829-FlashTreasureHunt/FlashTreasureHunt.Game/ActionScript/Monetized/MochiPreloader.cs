using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.MochiLibrary;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.Shared;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashTreasureHunt.ActionScript.Monetized
{
	[Script, ScriptApplicationEntryPoint(Width = Game.ControlWidth, Height = Game.ControlHeight)]
	[SWF(width = Game.ControlWidth, height = Game.ControlHeight, backgroundColor = 0)]
	public class MochiPreloader : MochiAdPreloaderBase
	{
		[TypeOfByNameOverride]
		public override DisplayObject CreateInstance()
		{
			return typeof(Game).CreateInstance() as DisplayObject;

		}

		public override bool AutoCreateInstance()
		{
			return false;
		}


		public MochiPreloader()
		{
			var Ready = default(Action);

			Ready = delegate
			{
				// 1 more
				Ready = delegate
				{
					// done
					Ready = delegate
					{
						// nothing
					};

					CreateInstance().AttachTo(this);
				};
			};

			this.InvokeWhenStageIsReady(
				delegate
				{
					stage.scaleMode = StageScaleMode.NO_SCALE;
					stage.align = StageAlign.TOP_LEFT;

					_mochiads_game_id = FlashTreasureHunt.PublishedInfo.MochiAd;

					showPreGameAd(
						() => Ready()
						, stage.stageWidth, stage.stageHeight
					);
				}
			);

			this.LoadingComplete += () => Ready();
		}


	}


}
