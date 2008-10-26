extern alias pages;

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

			global::ScriptCoreLib.ActionScript.Avalon.Carousel.KnownEmbeddedAssets.RegisterTo(Handlers);

			// register assets from referenced assembly
			pages::NavigationButtons.Assets.ActionScript.KnownAndReferencedEmbeddedAssets.RegisterTo(
				Handlers
			);

			// assets are in the same library and do not have their own namespace
			pages::TextSuggestions.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);

			pages::TextSuggestions2.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);

			pages::FlashMouseMaze.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);

			pages::FlashAvalonQueryExample.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);

			pages::DynamicCursor.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);

			pages::DraggableClipRectangle.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);

			pages::BrowserAvalonExample.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);


			pages::System_Windows_Input_MouseEventArgs.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);


			pages::NumericTransmitter.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);

			pages::System_IO_StringReader.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);

			pages::CarouselExample2.ActionScript.KnownEmbeddedAssets.RegisterTo(
				Handlers
			);
		}
	}
	
}