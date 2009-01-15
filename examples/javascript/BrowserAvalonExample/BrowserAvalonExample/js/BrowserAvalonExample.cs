using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using BrowserAvalonExample.Code;


namespace BrowserAvalonExample.js
{
	using TargetCanvas = BrowserAvalonExampleCanvas;

	[Script, ScriptApplicationEntryPoint]
	public class BrowserAvalonExample
	{
		public BrowserAvalonExample(IHTMLElement e)
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

		static BrowserAvalonExample()
		{
			typeof(BrowserAvalonExample).SpawnTo(i => new BrowserAvalonExample(i));
		}

	}

	[Script(), ScriptApplicationEntryPoint(IsClickOnce = true, ScriptedLoading = true, Format = SerializedDataFormat.none)]
	public class BrowserAvalonExamplePreloader : BrowserAvalonExample
	{
		public BrowserAvalonExamplePreloader() : base(null)
		{
		}


		static BrowserAvalonExamplePreloader()
		{
			typeof(BrowserAvalonExamplePreloader).Spawn();
		}
	}

}
