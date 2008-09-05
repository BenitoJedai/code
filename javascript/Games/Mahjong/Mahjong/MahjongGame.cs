//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;
using System.Linq;
using System;
using Mahjong.Shared;
using Mahjong.Code;



namespace Mahjong.js
{

	[Script, ScriptApplicationEntryPoint(IsClickOnce = true, ScriptedLoading = true)]
	public class MahjongGame
	{
		public MahjongGame() : this(null)
		{

		}

		public MahjongGame(IHTMLElement e)
		{



			// wpf here
			var clip = new IHTMLDiv();

			clip.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
			clip.style.SetSize(MyCanvas.DefaultScaledWidth, MyCanvas.DefaultScaledHeight);
			clip.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

			if (e == null)
				clip.AttachToDocument();
			else
				e.insertPreviousSibling(clip);

			var loading = new IHTMLSpan();

			loading.AttachTo(clip);

			var Images = Assets.Default.FileNames.Where(k => k.EndsWith(".png") || k.EndsWith(".png")).ToArray();

			Images.ForEach(
				(string src, int index, Action SignalNext) =>
				{
					IHTMLImage img = src;

					var c = Images.Length;

					var p = Convert.ToInt32((index + 1) * 100 / c);

					loading.innerText = "loading #" + p + " " + src;

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

		static MahjongGame()
		{
			typeof(MahjongGame).SpawnTo(i => new MahjongGame(i));
		}


	}

}
