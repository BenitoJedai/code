using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using AvalonPipeMania.Assets.Shared;

namespace AvalonPipeMania.Code
{
	[Script]
	public partial class AvalonPipeManiaCanvas : Canvas
	{
		public const int DefaultWidth = 400;
		public const int DefaultHeight = 300;

		public Action<string> PlaySound = delegate { };

		public AvalonPipeManiaCanvas()
		{
			this.Width = DefaultWidth;
			this.Height = DefaultHeight;

			var t = new TextBox
			{
				AcceptsReturn = true,
				Text = "hello world",
				Width = DefaultWidth,
				Height = DefaultHeight / 2
			}.AttachTo(this);

			new Image
			{
				Source = (KnownAssets.Path.Data + "/draft.png").ToSource(),
			}.MoveTo(0, DefaultHeight / 2).AttachTo(this);

			var i2 = new Image
			{
				Source = (KnownAssets.Path.Data + "/draft.png").ToSource(),
			}.MoveTo(64, DefaultHeight / 2).AttachTo(this);

			i2.MouseLeftButtonUp +=
				delegate
				{
					PlaySound("place_tile");
				};
			foreach (var n in KnownAssets.Default.FileNames)
			{
				t.AppendTextLine(n);
			}
		}
	}
}
