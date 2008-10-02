using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace AvalonPipeMania.Labs.ActionScript
{
	using TargetCanvas = global::AvalonPipeMania.Code.AvalonPipeManiaCanvas;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight)]
	public class LabsFlash : Sprite
	{
		public LabsFlash()
		{
			// spawn the wpf control
			new TargetCanvas().AttachToContainer(this);
		}

		static LabsFlash()
		{
			// add resources to be found by ImageSource
			//KnownEmbeddedResources.Default.Handlers.Add(
			//    e => Assets.Default[e]
			//);

		}
	}
}