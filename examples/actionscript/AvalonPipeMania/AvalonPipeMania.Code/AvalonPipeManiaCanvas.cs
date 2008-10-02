using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;

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

			new TextBox
			{
				Text = "hello world"
			}.AttachTo(this);
		}
	}
}
