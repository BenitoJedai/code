using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.SplitScreen.Shared
{
	using SinglePlayerCanvas = Mahjong.Code.MyCanvas;

	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = SinglePlayerCanvas.DefaultScaledWidth * 2;
		public const int DefaultHeight = SinglePlayerCanvas.DefaultScaledHeight;

		public readonly FutureAction<string> PlaySoundFuture = new FutureAction<string>();

		public MyCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			var lefty = new SinglePlayerCanvas().AttachTo(this);
			var righty = new SinglePlayerCanvas().MoveTo(SinglePlayerCanvas.DefaultScaledWidth, 0).AttachTo(this);


			PlaySoundFuture.Continue(lefty.PlaySoundFuture);
			PlaySoundFuture.Continue(righty.PlaySoundFuture);

		}
	}
}
