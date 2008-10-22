extern alias pages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace AvalonExampleGallery.Shared
{
	[Script]
	public static class KnownPages
	{
		// http://msdn.microsoft.com/en-us/library/ms173212.aspx
		// Each extern alias declaration introduces an additional root-level namespace that parallels (but does not lie within) the global namespace. Thus types from each assembly can be referred to without ambiguity by using their fully qualified name, rooted in the appropriate namespace-alias.

		public static Dictionary<string, Type> Value
		{
			get
			{
				return new Dictionary<string, Type>
				{
					{ 
						pages::NavigationButtons.Assets.Shared.KnownAssets.Path.Assets, 
						typeof(pages::NavigationButtons.Code.MyCanvas) },
					{ 
						pages::TextSuggestions.Shared.KnownAssets.Path.Assets, 
						typeof(pages::TextSuggestions.Shared.TextSuggestionsCanvas) },
					{ 
						pages::TextSuggestions2.Shared.KnownAssets.Path.Assets, 
						typeof(pages::TextSuggestions2.Shared.TextSuggestions2Canvas) },
					{ 
						pages::FlashMouseMaze.Shared.KnownAssets.Path.Assets, 
						typeof(pages::FlashMouseMaze.Shared.MouseMazeCanvas) },
					{ 
						pages::FlashAvalonQueryExample.Shared.KnownAssets.Path.Assets, 
						typeof(pages::FlashAvalonQueryExample.Shared.AvalonQueryExampleCanvas) },
					{ 
						pages::DynamicCursor.Shared.KnownAssets.Path.Assets, 
						typeof(pages::DynamicCursor.Shared.DynamicCursorCanvas) },
					{ 
						pages::DraggableClipRectangle.Shared.KnownAssets.Path.Assets, 
						typeof(pages::DraggableClipRectangle.Shared.DraggableClipRectangleCanvas) },
					{ 
						pages::BrowserAvalonExample.Shared.KnownAssets.Path.Assets, 
						typeof(pages::BrowserAvalonExample.Code.BrowserAvalonExampleCanvas) },
					{ 
						pages::System_Windows_Input_MouseEventArgs.Shared.KnownAssets.Path.Assets, 
						typeof(pages::System_Windows_Input_MouseEventArgs.Shared.MouseEventArgsCanvas) },
					{ 
						pages::NumericTransmitter.Shared.KnownAssets.Path.Assets, 
						typeof(pages::NumericTransmitter.Shared.NumericTransmitterCanvas) },
					{ 
						pages::System_IO_StringReader.Shared.KnownAssets.Path.Assets, 
						typeof(pages::System_IO_StringReader.Shared.System_IO_StringReaderCanvas) },

				};
			}
		}
	}
}
