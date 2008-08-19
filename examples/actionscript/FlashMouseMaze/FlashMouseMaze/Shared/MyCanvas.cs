using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Maze;
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



			var img = new Image
			{
				Source = "assets/FlashMouseMaze/jsc.png".ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);


			var mouse = new Image
			{
				Source = "assets/FlashMouseMaze/mouse.png".ToSource()
			}.MoveTo(0, 0).AttachTo(this);

			var canvasdrawing = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);


			InitializeCanvasDrawing(canvasdrawing);

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
					mouse.Visibility = Visibility.Visible;
				};

			canvas.MouseLeave +=
				delegate
				{
					mouse.Visibility = Visibility.Hidden;
				};

			canvas.MouseMove +=
				(s, ev) =>
				{
					var p = ev.GetPosition(canvas);

					mouse.MoveTo(p.X - 8, p.Y - 8);
				};




		}

		private void InitializeCanvasDrawing(Canvas c)
		{
			Action<int, int, int, int> fillRect =
				(_x, _y, _w, _h) =>
				{
					new Rectangle
					{
						Fill = Brushes.GreenYellow,
						Width = _w,
						Height = _h,
					}.MoveTo(_x, _y).AttachTo(c);

				};

			int x, y;
			int z = 24;
			var offset = z;

			for (x = 1; x < maze.Width - 1; x++)
				for (y = 1; y < maze.Height - 1; y++)
				{
					var v = maze[x, y];

					var IsTop = (v & 1) != 0;
					var IsLeft = (v & 4) != 0;
					var IsBottom = (v & 2) != 0;
					var IsRight = (v & 8) != 0;

			
					if (IsTop) /* This cell has a top wall */
						fillRect(offset + x * z, y * z, z, z / 6);

					if (IsLeft) /* This cell has a left wall */
						fillRect(offset + x * z, y * z, z / 6, z);

					if (IsRight)
					{
						fillRect(offset + x * z + z - z / 6, y * z, z / 6, z);
					}

					if (IsBottom)
					{
						fillRect(offset + x * z, y * z + z - z / 6, z, z / 6);
					}


				}
		}


		MazeGenerator maze = new MazeGenerator(12, 12, null);
	}
}
