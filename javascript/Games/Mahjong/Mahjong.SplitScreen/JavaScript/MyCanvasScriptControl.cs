using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.SplitScreen.JavaScript
{
	using TargetCanvas = global::Mahjong.SplitScreen.Shared.MyCanvas;
	using Mahjong.SplitScreen.Shared;
	using Mahjong.Shared;

	[Script, ScriptApplicationEntryPoint]
	public class MyCanvasScriptControl
	{
		public MyCanvasScriptControl(IHTMLElement e)
		{
			// wpf here
			var clip = new IHTMLDiv();

			clip.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
			clip.style.SetSize(MyCanvas.DefaultWidth, MyCanvas.DefaultHeight);
			clip.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

			if (e == null)
				clip.AttachToDocument();
			else
				e.insertPreviousSibling(clip);

			var loading = new IHTMLElement(IHTMLElement.HTMLElementEnum.h3);

			loading.AttachTo(clip);

			var Images = Assets.Default.FileNames.Where(k => k.EndsWith(".png") || k.EndsWith(".jpg")).ToArray();

			Images.ForEach(
				(string src, int index, Action SignalNext) =>
				{
					IHTMLImage img = src;

					var c = Images.Length;

					var p = Convert.ToInt32((index + 1) * 100 / c);

					loading.innerText = "loading... " + p + "% " + src;

					img.InvokeOnComplete(_img => SignalNext(), 5);
				}
			)(
				delegate
				{
					loading.Dispose();

					AvalonExtensions.AttachToContainer(new MyCanvas(), clip);
				}
			);

		}

		static MyCanvasScriptControl()
		{
			typeof(MyCanvasScriptControl).SpawnTo(i => new MyCanvasScriptControl(i));
		}

	}
}
