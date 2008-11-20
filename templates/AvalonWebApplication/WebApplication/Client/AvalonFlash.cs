using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;

namespace WebApplication.Client.ActionScript
{
	using TargetCanvas = global::WebApplication.Client.Avalon.AvalonCanvas;


	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight, backgroundColor = 0)]
	public class AvalonFlash : Sprite
	{
		public AvalonFlash()
		{
			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new TargetCanvas(), this);
		}

		static AvalonFlash()
		{
			// add resources to be found by ImageSource
			//KnownEmbeddedAssets.RegisterTo(
			//    KnownEmbeddedResources.Default.Handlers
			//);

		}
	}
}
