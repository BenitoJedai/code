using System;
using System.Collections.Generic;
using Mahjong.Specialize.ActionScript;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;

namespace Mahjong.SplitScreen.ActionScript
{
	using Mahjong.ActionScript;
	using TargetCanvas = global::Mahjong.SplitScreen.Shared.SplitScreenCanvas;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight, backgroundColor = 0)]
	public class SplitScreen : Sprite
	{
		public SplitScreen()
		{
			

			var c = new TargetCanvas();

			c.PlaySoundFuture.BindToPlaySound();

			c.Client.Lefty.Map.BindToFullScreen();
			c.Client.Righty.Map.BindToFullScreen();

			// spawn the wpf control
			AvalonExtensions.AttachToContainer(c, this);
		}

		static SplitScreen()
		{
			Specialize.ActionScript.Specialize.AddKnownEmbeddedResources();
		}
	}
}