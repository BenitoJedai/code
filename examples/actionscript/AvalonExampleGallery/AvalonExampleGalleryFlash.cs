extern alias pages;
using System;
using System.Collections.Generic;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;


namespace AvalonExampleGallery.ActionScript
{
	using TargetCanvas = global::AvalonExampleGallery.Shared.AvalonExampleGalleryCanvas;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight, WithResources = true)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight)]
	public class AvalonExampleGalleryFlash : Sprite
	{
		public AvalonExampleGalleryFlash()
		{
			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new TargetCanvas(), this);
		}
	}




}