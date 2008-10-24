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

namespace ScriptApplication.source.js.Controls
{
	[Script]
	public class AvalonExampleGalleryContainer
	{
		AvalonExampleGallery.JavaScript.AvalonExampleGalleryDocument Reference;


		public AvalonExampleGalleryContainer(IHTMLElement e)
		{
			e.childNodes.Where(k => k.nodeName.ToLower() == "div").Select(k => (IHTMLDiv)k).ForEach(
				k =>
				{
					Console.WriteLine(k.className);

					k.style.backgroundColor = Color.Yellow;
				}
			);
		}

		static AvalonExampleGalleryContainer()
		{
			typeof(AvalonExampleGalleryContainer).SpawnTo(i => new AvalonExampleGalleryContainer(i));
		}

	}


}
