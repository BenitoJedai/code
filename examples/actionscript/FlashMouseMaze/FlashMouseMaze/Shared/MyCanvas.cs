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

namespace FlashMouseMaze.Shared
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

			var help_idle = new Image
			{
				Source = "assets/FlashMouseMaze/help_idle.png".ToSource()
			}.AttachTo(this);

			var help = new Image
			{
				Source = "assets/FlashMouseMaze/help.png".ToSource()
			}.AttachTo(this);

			help.Opacity = 0;

			var img = new Image
			{
				Source = "assets/FlashMouseMaze/jsc.png".ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);

			
			var mouse = new Image
			{
				Source = "assets/FlashMouseMaze/mouse.png".ToSource()
			}.MoveTo(0, 0).AttachTo(this);


			var canvas = new Rectangle
			{
				Fill = Brushes.Red,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Opacity = 0
			}.MoveTo(0, 0).AttachTo(this);


			this.Cursor = Cursors.None;

			canvas.MouseEnter +=
				delegate
				{
					mouse.Opacity = 1;
				};

			canvas.MouseLeave +=
				delegate
				{
					mouse.Opacity = 0;
				};

			canvas.MouseMove +=
				(s, ev) =>
				{
					var p = ev.GetPosition(canvas);

					mouse.MoveTo(p.X - 8, p.Y - 8);
				};

			var t = new TextBox
			{
				FontSize = 32,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true
			}.MoveTo(32, 32).AttachTo(this);

			help_idle.Opacity = 0;
			help.Opacity = 1;
			img.Opacity = 0.5;

			t.MouseEnter +=
				delegate
				{
					help_idle.Opacity = 1;
					help.Opacity = 0;

					img.Opacity = 1;
					t.Foreground = 0xffffff00.ToSolidColorBrush();
				};

			t.MouseLeave +=
				delegate
				{
					help_idle.Opacity = 0;
					help.Opacity = 1;

					img.Opacity = 0.5;
					t.Foreground = 0xffffffff.ToSolidColorBrush();
				};



		}
	}
}
