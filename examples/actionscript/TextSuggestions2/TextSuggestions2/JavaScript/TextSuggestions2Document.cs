using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace TextSuggestions2.JavaScript
{
	using TargetCanvas = global::TextSuggestions2.Shared.TextSuggestions2Canvas;

	[Script, ScriptApplicationEntryPoint]
	public class TextSuggestions2Document
	{
		public TextSuggestions2Document(IHTMLElement e)
		{
			// wpf here
			var clip = new IHTMLDiv();

			clip.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
			clip.style.SetSize(TargetCanvas.DefaultWidth, TargetCanvas.DefaultHeight);
			clip.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

			if (e == null)
				clip.AttachToDocument();
			else
				e.insertPreviousSibling(clip);

			AvalonExtensions.AttachToContainer(new TargetCanvas(), clip);

		}

		static TextSuggestions2Document()
		{
			typeof(TextSuggestions2Document).SpawnTo(i => new TextSuggestions2Document(i));
		}

	}
}
