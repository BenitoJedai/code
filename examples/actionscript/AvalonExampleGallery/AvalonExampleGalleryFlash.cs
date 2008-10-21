using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;



namespace AvalonExampleGallery.ActionScript
{
	using TargetCanvas = global::AvalonExampleGallery.Shared.AvalonExampleGalleryCanvas;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight)]
	public class AvalonExampleGalleryFlash : Sprite
	{
		public AvalonExampleGalleryFlash()
		{
			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new TargetCanvas(), this);
		}

		static AvalonExampleGalleryFlash()
		{
			// add resources to be found by ImageSource
		
			// local private assets
			KnownEmbeddedAssets.RegisterTo(
				KnownEmbeddedResources.Default.Handlers
			);

			// register assets from referenced assembly
			global::NavigationButtons.Assets.ActionScript.KnownAndReferencedEmbeddedAssets.RegisterTo(
				KnownEmbeddedResources.Default.Handlers
			);

			// assets are in the same library and do not have their own namespace
			global::TextSuggestions.ActionScript.KnownEmbeddedAssets.RegisterTo(
				KnownEmbeddedResources.Default.Handlers
			);
		}


	}



	[Script]
	public class KnownEmbeddedAssets
	{


		[EmbedByFileName]
		public static Class ByFileName(string e)
		{
			throw new NotImplementedException();
		}

		public static void RegisterTo(List<Converter<string, Class>> Handlers)
		{
			// assets from current assembly
			Handlers.Add(e => ByFileName(e));

			//// assets from referenced assemblies
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.Cursors.EmbeddedAssets.Default[e]);
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.TiledImageButton.Assets.Default[e]);

		}
	}
	
}