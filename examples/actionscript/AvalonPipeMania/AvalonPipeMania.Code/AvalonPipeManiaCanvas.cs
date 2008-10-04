using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using AvalonPipeMania.Assets.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Cursors;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.TiledImageButton;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace AvalonPipeMania.Code
{
	[Script]
	public partial class AvalonPipeManiaCanvas : Canvas
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 600;

		public Action<string> PlaySound = delegate { };

		public AvalonPipeManiaCanvas()
		{
			this.Width = DefaultWidth;
			this.Height = DefaultHeight;

			new []
			{
				Colors.White,
				Colors.Blue,
				Colors.Red,
				Colors.Yellow
			}.ToGradient(DefaultHeight / 4).ForEach(
				(c, i) =>
				new Rectangle
				{
					Fill = new SolidColorBrush(c),
					Width = DefaultWidth,
					Height = 5,
				}.MoveTo(0, i * 4).AttachTo(this)
			);

		
			var t = new TextBox
			{
				AcceptsReturn = true,
				Background = Brushes.White,
				BorderThickness = new Thickness(0),
				Text = "hello world" + Environment.NewLine,
				Width = DefaultWidth / 2,
				Height = DefaultHeight / 3
			}.AttachTo(this).MoveTo(DefaultWidth / 4, 4);


			
			var OverlayCanvas = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			};

			var WaterFlow = new Queue<Action>();

			Enumerable.Range(0, 6).ForEach(
				ix =>
				{
					Enumerable.Range(0, 4).ForEach(
						iy =>
						{
							var tile = new Tile();

							tile.Container.MoveTo(64 + 64 * ix - 8, DefaultHeight / 2 + 52 * iy - 8).AttachTo(this);
							tile.Overlay.MoveTo(64 + 64 * ix , DefaultHeight / 2 + 52 * iy ).AttachTo(OverlayCanvas);

							if (ix == 2)
							{
								var pipe = new Pipe.TopToBottom();


								pipe.Container.MoveTo(64 + 64 * ix, DefaultHeight / 2 + 52 * iy - 12).AttachTo(this);


								WaterFlow.Enqueue(() => pipe.Pipe_0_16.Visibility = Visibility.Visible);
								WaterFlow.Enqueue(() => pipe.Pipe_16_32.Visibility = Visibility.Visible);
								WaterFlow.Enqueue(() => pipe.Pipe_32_48.Visibility = Visibility.Visible);
								WaterFlow.Enqueue(() => pipe.Pipe_48_64.Visibility = Visibility.Visible);
							}

							tile.Overlay.MouseEnter +=
								delegate
								{
									PlaySound("place_tile");

									tile.YellowFilter.Visibility = System.Windows.Visibility.Visible;
								};

							tile.Overlay.MouseLeave +=
								delegate
								{
									tile.YellowFilter.Visibility = System.Windows.Visibility.Hidden;
								};
						}
					);
				}
			);

			OverlayCanvas.AttachTo(this).MoveTo(0, 0);


			var Navigationbar = new AeroNavigationBar();

			Navigationbar.Container.MoveTo(4, 4).AttachTo(this);

	

			Enumerable.Range(1, 4).ForEach(
				i =>
				{
					var c1 = new ArrowCursorControl
					{

					};

					c1.Container.MoveTo(32 * i, 32).AttachTo(this);
				}
			);



			foreach (var n in KnownAssets.Default.FileNames)
			{
				t.AppendTextLine(n);
			}


			3000.AtDelay(
				delegate
				{
					(1000 / 10).AtIntervalWithTimer(
						ttt =>
						{
							if (WaterFlow.Count == 0)
							{
								ttt.Stop();
								return;
							}

							WaterFlow.Dequeue()();
						}
					);
				}
			);
		}
	}
}
