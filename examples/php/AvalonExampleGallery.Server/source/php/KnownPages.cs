extern alias pages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptApplication.source.php
{
	[Script]
	public static class KnownPages
	{
		public static void StreamTo(this Func<string, string, IHTMLElement> c, Action<IHTMLElement> h)
		{
			StreamTo(
				(Path, Text) =>
				{
					var v = c(Path, Text);

					h(v);

					return v;
				}
			);

		}

		public static void StreamTo(this Func<string, string, IHTMLElement> c)
		{
			// the titles should be of the expected value

			c(pages::NavigationButtons.Assets.Shared.KnownAssets.Path.Assets,
				"My");

			c(pages::TextSuggestions.Shared.KnownAssets.Path.Assets,
				"TextSuggestions");

			c(pages::TextSuggestions2.Shared.KnownAssets.Path.Assets,
				"TextSuggestions2");

			c(pages::FlashMouseMaze.Shared.KnownAssets.Path.Assets,
				"MouseMaze");

			c(pages::FlashAvalonQueryExample.Shared.KnownAssets.Path.Assets,
				"AvalonQueryExample");

			c(pages::DynamicCursor.Shared.KnownAssets.Path.Assets,
				"DynamicCursor");

			c(pages::DraggableClipRectangle.Shared.KnownAssets.Path.Assets,
				"DraggableClipRectangle");

			c(pages::BrowserAvalonExample.Shared.KnownAssets.Path.Assets,
				"BrowserAvalonExample");

			c(pages::System_Windows_Input_MouseEventArgs.Shared.KnownAssets.Path.Assets,
				"MouseEventArgs");

			c(pages::NumericTransmitter.Shared.KnownAssets.Path.Assets,
				"NumericTransmitter");

			c(pages::System_IO_StringReader.Shared.KnownAssets.Path.Assets,
				"System_IO_StringReader");
		}
	}
}
