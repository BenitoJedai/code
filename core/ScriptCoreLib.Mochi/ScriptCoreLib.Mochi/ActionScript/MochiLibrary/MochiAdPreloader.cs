﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.MochiLibrary
{
	[Script]
	public abstract class MochiAdPreloader : MochiAdPreloaderBase
	{

		public abstract Type GetTargetType();

		public override DisplayObject CreateInstance()
		{
			return Activator.CreateInstance(GetTargetType()) as DisplayObject;
		}

		public override bool AutoCreateInstance()
		{
			return false;
		}

		public virtual void InitializeBackground()
		{

		}

		public MochiAdPreloader(string Key)
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

					_mochiads_game_id = Key;

					InitializeBackground();

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
