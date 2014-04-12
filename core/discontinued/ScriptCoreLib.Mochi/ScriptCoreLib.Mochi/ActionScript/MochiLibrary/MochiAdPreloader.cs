﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.MochiLibrary.Ad;

namespace ScriptCoreLib.ActionScript.MochiLibrary
{
	[Script, DynamicType]
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

		public virtual bool AdsEnabled()
		{
			return true;
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

					MochiAdPreloaderInitialize(Key);
				};
			};

			this.InvokeWhenStageIsReady(
				delegate
				{
					stage.scaleMode = StageScaleMode.NO_SCALE;
					stage.align = StageAlign.TOP_LEFT;

					_mochiads_game_id = Key;

					InitializeBackground();

					if (!AdsEnabled())
					{
						this.LoadingComplete +=
							delegate
							{
								MochiAdPreloaderInitialize(Key);

							};

						return;
					}


					showPreGameAd(
						() => Ready()
						, stage.stageWidth, stage.stageHeight
					);
				}
			);

			this.LoadingComplete += () => Ready();
		}

		private void MochiAdPreloaderInitialize(string Key)
		{
			MochiServices.connect(Key, this);
			CreateInstance().AttachTo(this);
		}
	}
}
