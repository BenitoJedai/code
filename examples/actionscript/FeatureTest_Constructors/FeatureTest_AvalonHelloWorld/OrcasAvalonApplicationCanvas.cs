using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FeatureTest_AvalonHelloWorld.Shared
{
	[Script]
	public class OrcasAvalonApplicationCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public OrcasAvalonApplicationCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			var t = new TextBox
			{
				Text = "hello world",
				IsReadOnly = true
			}.AttachTo(this);

			t.GotFocus +=
				delegate
				{
					t.Foreground = Brushes.Blue;
				};
		}
	}
}
