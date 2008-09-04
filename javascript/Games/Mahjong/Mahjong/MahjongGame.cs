//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
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

	[Script, ScriptApplicationEntryPoint]
	public class MahjongGame
	{
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

			Assets.Default.FileNames.ForEach(
				(string src, int index, Action SignalNext) =>
				{
					IHTMLImage img = src;

					loading.innerText = "loading #" + index + " " + src;

					img.InvokeOnComplete(_img => SignalNext());
				}
			)(() => AvalonExtensions.AttachToContainer(new MyCanvas(), clip));

		}

		static MahjongGame()
		{
			typeof(MahjongGame).SpawnTo(i => new MahjongGame(i));
		}


	}

}
