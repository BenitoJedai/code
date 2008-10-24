using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

using System.Linq;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Collections.Generic;

namespace ScriptApplication.source.js.Controls
{
	[Script]
	public class AvalonExampleGalleryContainer
	{
		AvalonExampleGallery.JavaScript.AvalonExampleGalleryDocument Reference;


		public AvalonExampleGalleryContainer(IHTMLElement e)
		{
			var lookup = new Dictionary<string, AvalonExampleGallery.Shared.AvalonExampleGalleryCanvas.OptionPosition>();

			e.childNodes.Where(k => k.nodeName.ToLower() == "div").Select(k => (IHTMLDiv)k).ForEach(
				k =>
				{
					var p = new AvalonExampleGallery.Shared.AvalonExampleGalleryCanvas.OptionPosition
					{
						X = k.Bounds.Left,
						Y = k.Bounds.Top
					};

					p.Clear = () => k.Dispose();

					lookup[k.className] = p;

				}
			);

			new AvalonExampleGallery.Shared.AvalonExampleGalleryCanvas(false,
				Text =>
				{
					if (lookup.ContainsKey(Text))
						return lookup[Text];

					return null;
				}
			).AttachToContainer(e);
		}

		static AvalonExampleGalleryContainer()
		{
			typeof(AvalonExampleGalleryContainer).SpawnTo(i => new AvalonExampleGalleryContainer(i));
		}

	}


}
