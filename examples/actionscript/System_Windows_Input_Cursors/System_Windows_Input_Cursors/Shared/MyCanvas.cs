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
using System.Windows.Input;

namespace System_Windows_Input_Cursors.Shared
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

			new TextBox
			{
				Foreground = Brushes.Blue,
				Text = "Next Layout »",
				TextAlignment = TextAlignment.Center,
				BorderThickness = new Thickness(0),
				IsReadOnly = true,
				Width = 120,
				Height = 22,
			}.MoveTo(8, DefaultHeight - 32).AttachTo(this);

			var overlay = new Rectangle
			{
				Fill = Brushes.Red,
				Width = 122,
				Height = 24,
				Opacity = 0.5,
				Cursor = Cursors.Hand
			}.MoveTo(7, DefaultHeight - 33).AttachTo(this);


		}
	}
}
