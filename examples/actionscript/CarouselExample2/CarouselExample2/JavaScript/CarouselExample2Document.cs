using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace CarouselExample2.JavaScript
{
	using TargetCanvas = global::CarouselExample2.Shared.CarouselExample2Canvas;

	[Script, ScriptApplicationEntryPoint]
	public class CarouselExample2Document
	{
		public CarouselExample2Document(IHTMLElement e)
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

		static CarouselExample2Document()
		{
			typeof(CarouselExample2Document).SpawnTo(i => new CarouselExample2Document(i));
		}

	}
}
