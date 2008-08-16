using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using BrowserAvalonExample.Code;


namespace BrowserAvalonExample.js
{
	[Script, ScriptApplicationEntryPoint]
	public class BrowserAvalonExample
	{
		public BrowserAvalonExample(IHTMLElement e)
		{
			// wpf here
			var clip = new IHTMLDiv();

			clip.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
			clip.style.SetSize(MyCanvas.DefaultWidth, MyCanvas.DefaultHeight);
			clip.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

			e.insertPreviousSibling(clip);

			AvalonExtensions.AttachToContainer(new MyCanvas(), clip);

		}

		static BrowserAvalonExample()
		{
			typeof(BrowserAvalonExample).SpawnTo(i => new BrowserAvalonExample(i));
		}

	}

}
