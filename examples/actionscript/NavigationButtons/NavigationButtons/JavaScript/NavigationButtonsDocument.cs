using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace NavigationButtons.JavaScript
{
	using TargetCanvas = global::NavigationButtons.Code.MyCanvas;

	[Script, ScriptApplicationEntryPoint(IsClickOnce=true, ScriptedLoading=true)]
	public class NavigationButtonsDocument
	{
		public NavigationButtonsDocument() : this(null)
		{
			// scripted loading will need a default ctor
		}

		public NavigationButtonsDocument(IHTMLElement e)
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

		static NavigationButtonsDocument()
		{
			typeof(NavigationButtonsDocument).SpawnTo(i => new NavigationButtonsDocument(i));
		}

	}
}
