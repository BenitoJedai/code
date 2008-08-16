﻿using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript.Extensions;
using BrowserAvalonExample.Code;

namespace FlashAvalonExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	public class FlashAvalonExample : Sprite
	{
		


		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashAvalonExample()
		{


			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new MyCanvas(), this);
		}

		static FlashAvalonExample()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedResources.Default.Handlers.Add(
				e =>  global::BrowserAvalonExample.ActionScript.Assets.Default[e]
			);

		}
	}
}