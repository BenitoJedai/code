using System;
using System.Collections.Generic;
using AvalonPipeMania.Assets.ActionScript;
using AvalonPipeMania.Assets.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;

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
			new TargetCanvas
			{
				PlaySound =
					e =>
					{
						var f = KnownAssets.Path.Sounds + "/" + e + ".mp3";

						var c = KnownEmbeddedAssets.ByFileName(f);

						if (c == null)
							throw new Exception("Resource not found - " + f);

						c.ToSoundAsset().play();
					}
			}.AttachToContainer(this);
		}

		static LabsFlash()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedAssets.RegisterTo(KnownEmbeddedResources.Default.Handlers);


		}
	}
}