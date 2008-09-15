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
	using TargetCanvas = global::Mahjong.SplitScreen.Shared.SplitScreenCanvas;
	using Mahjong.SplitScreen.Shared;
	using Mahjong.Shared;
	using ScriptCoreLib.JavaScript;

	[Script, ScriptApplicationEntryPoint]
	public class SplitScreenDocument
	{
		public SplitScreenDocument(IHTMLElement e)
		{
			
			// wpf here
			var clip = new IHTMLDiv();

			clip.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
			clip.style.SetSize(SplitScreenCanvas.DefaultWidth, SplitScreenCanvas.DefaultHeight);
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

					Native.Document.body.style.backgroundColor = "black";

					AvalonExtensions.AttachToContainer(new SplitScreenCanvas(), clip);
				}
			);

		}

		static SplitScreenDocument()
		{
			typeof(SplitScreenDocument).SpawnTo(i => new SplitScreenDocument(i));
		}

	}
}
