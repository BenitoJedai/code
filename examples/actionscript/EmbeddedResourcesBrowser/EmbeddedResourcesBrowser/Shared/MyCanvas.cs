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

namespace EmbeddedResourcesBrowser.Shared
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public MyCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			#region Gradient
			for (int i = 0; i < DefaultHeight; i += 4)
			{
				new Rectangle
				{
					Fill = ((uint)(0xff00007F + Convert.ToInt32(128 * i / DefaultHeight))).ToSolidColorBrush(),
					Width = DefaultWidth,
					Height = 4,
				}.MoveTo(0, i).AttachTo(this);
			}
			#endregion

			
			var t = new TextBox
			{
				AcceptsReturn = true,
				Text = "",
				BorderThickness = new Thickness(0),
				Foreground = Brushes.Black,
				Background = Brushes.White,
				IsReadOnly = true,
				Width = 400,
				Height = 300
			}.MoveTo(32, 10).AttachTo(this);


			foreach (var v in Assets.Default.FileNames)
			{
				t.AppendTextLine(v);
			}


		}
	}
}
