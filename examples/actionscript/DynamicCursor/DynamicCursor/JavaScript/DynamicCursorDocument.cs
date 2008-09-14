using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace DynamicCursor.JavaScript
{
	using TargetCanvas = global::DynamicCursor.Shared.DynamicCursorCanvas;

	[Script, ScriptApplicationEntryPoint]
	public class DynamicCursorDocument
	{
		public DynamicCursorDocument(IHTMLElement e)
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

		static DynamicCursorDocument()
		{
			typeof(DynamicCursorDocument).SpawnTo(i => new DynamicCursorDocument(i));
		}

	}
}
