using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;

namespace FlashResources.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF]
	public class FlashResources : Sprite
	{


		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashResources()
		{
			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new MyCanvas(), this);

		}

		static FlashResources()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedResources.Default.Handlers.Add(
				e =>  global::FlashResources.ActionScript.Assets.Default[e]
			);

		}
	}
}