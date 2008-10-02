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
	public class AvalonPipeManiaCanvas : Canvas
	{
		public const int DefaultWidth = 400;
		public const int DefaultHeight = 300;

		public AvalonPipeManiaCanvas()
		{
			this.Width = DefaultWidth;
			this.Height = DefaultHeight;

			var t = new TextBox
			{
				AcceptsReturn = true,
				Text = "hello world",
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

			foreach (var n in KnownAssets.Default.FileNames)
			{
				t.AppendTextLine(n);
			}
		}
	}
}
