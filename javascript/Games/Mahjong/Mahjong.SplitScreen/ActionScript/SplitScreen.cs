using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace Mahjong.SplitScreen.ActionScript
{
	using TargetCanvas = global::Mahjong.SplitScreen.Shared.MyCanvas;
	using Mahjong.ActionScript;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight)]
	public class SplitScreen : Sprite
	{
		public SplitScreen()
		{
			var c = new TargetCanvas();

			c.PlaySoundFuture.Value = global::Mahjong.ActionScript.__Assets.Default.PlaySound;

			// spawn the wpf control
			AvalonExtensions.AttachToContainer(c, this);
		}

		static SplitScreen()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedResources.Default.Handlers.AddRange(__Assets.ReferencedKnownEmbeddedResources());

		}
	}
}