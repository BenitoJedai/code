using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

namespace MovieBlog.Client.JavaScript
{
	using TargetCanvas = global::MovieBlog.Client.Avalon.AvalonCanvas;


	[Script, ScriptApplicationEntryPoint]
	public class AvalonDocument
	{
		public AvalonDocument(IHTMLElement e)
		{
			var c = new IHTMLDiv().AttachTo(e.parentNode);

			c.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
			c.style.SetSize(TargetCanvas.DefaultWidth, TargetCanvas.DefaultHeight);
			c.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

			AvalonExtensions.AttachToContainer(new TargetCanvas(), c);
		}

		static AvalonDocument()
		{
			typeof(AvalonDocument).SpawnTo(i => new AvalonDocument(i));
		}
	}
}
