using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace DynamicCursor.ActionScript
{
	using TargetCanvas = global::DynamicCursor.Shared.DynamicCursorCanvas;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight)]
	public class DynamicCursor : Sprite
	{
		public DynamicCursor()
		{
			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new TargetCanvas(), this);
		}

		static DynamicCursor()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedResources.Default.Handlers.Add(
				e => ScriptCoreLib.ActionScript.Avalon.Cursors.EmbeddedAssets.Default[e]
			);

		}
	}
}