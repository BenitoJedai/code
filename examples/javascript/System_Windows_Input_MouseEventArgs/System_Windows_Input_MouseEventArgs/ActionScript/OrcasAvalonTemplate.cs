﻿using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace System_Windows_Input_MouseEventArgs.ActionScript
{
	using TargetCanvas = global::System_Windows_Input_MouseEventArgs.Shared.MyCanvas;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight)]
	public class System_Windows_Input_MouseEventArgs : Sprite
	{
		public System_Windows_Input_MouseEventArgs()
		{
			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new TargetCanvas(), this);
		}

		static System_Windows_Input_MouseEventArgs()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedResources.Default.Handlers.Add(
				e => Assets.Default[e]
			);

		}
	}
}