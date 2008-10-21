using System;
using System.Collections.Generic;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;

using NavigationButtons.Assets.ActionScript;

namespace NavigationButtons.ActionScript
{
	using TargetCanvas = global::NavigationButtons.Code.MyCanvas;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight)]
	public class NavigationButtons : Sprite
	{
		public NavigationButtons()
		{
			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new TargetCanvas(), this);
		}

		static NavigationButtons()
		{
			KnownAndReferencedEmbeddedAssets.RegisterTo(KnownEmbeddedResources.Default.Handlers);

			//// add resources to be found by ImageSource
			//KnownEmbeddedResources.Default.Handlers.Add(
			//    e => ScriptCoreLib.ActionScript.Avalon.TiledImageButton.Assets.Default[e]
			//);

		}
	}
}