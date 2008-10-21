using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace TextSuggestions.ActionScript
{
	using TargetCanvas = global::TextSuggestions.Shared.TextSuggestionsCanvas;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = TargetCanvas.DefaultWidth, Height = TargetCanvas.DefaultHeight)]
	[SWF(width = TargetCanvas.DefaultWidth, height = TargetCanvas.DefaultHeight)]
	public class TextSuggestionsFlash : Sprite
	{
		public TextSuggestionsFlash()
		{
			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new TargetCanvas(), this);
		}

		static TextSuggestionsFlash()
		{
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

			//// assets from referenced assemblies
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.Cursors.EmbeddedAssets.Default[e]);
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.TiledImageButton.Assets.Default[e]);

		}
	}
}