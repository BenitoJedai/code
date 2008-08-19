﻿using System;
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
		public const int DefaultWidth = 640;
		public const int DefaultHeight = 640;

		const int z = 48;


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

			var container = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

			var mousehollow = new Image
			{
				Source = "assets/FlashMouseMaze/mouse_hollow.png".ToSource()
			}.MoveTo(0, 0).AttachTo(this);

			var canvasdrawing = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(container);

			InitializeCanvasDrawing(canvasdrawing);


			var canvas = new Rectangle
			{
				Fill = Brushes.Red,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Opacity = 0
			}.MoveTo(0, 0).AttachTo(this);

			canvas.MouseLeftButtonUp +=
				delegate
				{
					canvasdrawing.Visibility = Visibility.Hidden;

					canvasdrawing = new Canvas
					{
						Width = DefaultWidth,
						Height = DefaultHeight
					}.AttachTo(container);

					maze = new MazeGenerator(maze.Width, maze.Height, null);

					InitializeCanvasDrawing(canvasdrawing);
				};

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
					mousehollow.MoveTo(p.X - 8, p.Y - 8);
				};




		}

		private void InitializeCanvasDrawing(Canvas c)
		{

			Action<int, int> fillRect =
				(_x, _y) =>
				{
					new Rectangle
					{
						Fill = Brushes.GreenYellow,
						Width = z / 2,
						Height = z / 2,
					}.MoveTo(_x, _y).AttachTo(c);

				};

			int x, y;
			var offset = 0;


			for (x = 1; x < maze.Width - 1; x++)
				for (y = 1; y < maze.Height - 1; y++)
				{
					var v = maze[x, y];

					var IsTop = (v & 1) != 0;
					var IsLeft = (v & 4) != 0;
					var IsBottom = (v & 2) != 0;
					var IsRight = (v & 8) != 0;

					fillRect(offset + x * z + z / 4, y * z + z / 4);

					if (!IsTop)
						fillRect(offset + x * z + z / 4, y * z - z / 4);

					if (!IsBottom)
						fillRect(offset + x * z + z / 4, y * z + z * 3 / 4);

					if (!IsLeft)
						fillRect(offset + x * z - z /4, y * z + z / 4);

					if (!IsRight)
						fillRect(offset + x * z + z * 3 / 4, y * z + z / 4);



				}
		}


		MazeGenerator maze = new MazeGenerator(12, 12, null);
	}
}
