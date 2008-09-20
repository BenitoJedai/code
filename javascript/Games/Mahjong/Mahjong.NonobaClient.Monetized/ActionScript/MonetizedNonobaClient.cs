using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace Mahjong.NonobaClient.Monetized.ActionScript
{
	using TargetCanvas = global::Mahjong.NonobaClient.ActionScript.NonobaClient;
	using ScriptCoreLib.Shared;
	using ScriptCoreLib.ActionScript.MochiLibrary;
	using Mahjong.PromotionalAssets;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight)]
	[Frame(typeof(MochiPreloader))]
	public class MonetizedNonobaClient : TargetCanvas
	{
		public MonetizedNonobaClient()
		{
	
		}
	}

	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight, backgroundColor = 0)]
	public class MochiPreloader : MochiAdPreloaderBase
	{
		[TypeOfByNameOverride]
		public override DisplayObject CreateInstance()
		{
			return Activator.CreateInstance(typeof(TargetCanvas)) as DisplayObject;
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

					_mochiads_game_id = MahjongInfo.MochiAd;

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