using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript.Extensions;
using BrowserAvalonExample.Code;
using ScriptCoreLib.ActionScript;
using BrowserAvalonExample.ActionScript;

namespace FlashAvalonExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = BrowserAvalonExampleCanvas.DefaultWidth, Height = BrowserAvalonExampleCanvas.DefaultHeight)]
	[SWF(width = BrowserAvalonExampleCanvas.DefaultWidth, height = BrowserAvalonExampleCanvas.DefaultHeight)]
	public class FlashAvalonExample : Sprite
	{



		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashAvalonExample()
		{


			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new BrowserAvalonExampleCanvas(), this);
		}

		static FlashAvalonExample()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedAssets.RegisterTo(
				KnownEmbeddedResources.Default.Handlers
			);
			
		}
	}
}