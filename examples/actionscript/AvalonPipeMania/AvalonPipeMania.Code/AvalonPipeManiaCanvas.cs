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

			new[]
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

			var field = new Field(8, 5);
			var field_x = 64;
			var field_y = 200;

			field.Container.MoveTo(field_x, field_y).AttachTo(this);
			field.Overlay.MoveTo(field_x, field_y).AttachTo(OverlayCanvas);


			var WaterFlow = new Queue<Action>();

			{

				var pipe = new Pipe.PumpToLeft();


				pipe.Container.MoveTo(field_x + Tile.ShadowBorder + Tile.Size * 5, field_y + Tile.ShadowBorder + 52 * 0 - 12).AttachTo(this);

				pipe.Water.ForEach(
					i =>
						WaterFlow.Enqueue(() => i.Visibility = Visibility.Visible)
				);

			}


			Enumerable.Range(0, 2).ForEach(
				ix_ =>
				{
					var ix = 4 - ix_;

					var pipe = new Pipe.LeftToRight();


					pipe.Container.MoveTo(field_x + Tile.ShadowBorder + Tile.Size * ix, field_y + Tile.ShadowBorder + 52 * 0 - 12).AttachTo(this);

					pipe.Water.ForEachReversed(
						i => WaterFlow.Enqueue(() => i.Visibility = Visibility.Visible)
					);

				}

			);

			{

				var pipe = new Pipe.RightToBottom();


				pipe.Container.MoveTo(field_x + Tile.ShadowBorder + Tile.Size * 2, field_y + Tile.ShadowBorder + 52 * 0 - 12).AttachTo(this);


				WaterFlow.Enqueue(() => pipe.Pipe_0_16.Visibility = Visibility.Visible);
				WaterFlow.Enqueue(() => pipe.Pipe_16_32.Visibility = Visibility.Visible);
				WaterFlow.Enqueue(() => pipe.Pipe_32_48.Visibility = Visibility.Visible);
				WaterFlow.Enqueue(() => pipe.Pipe_48_64.Visibility = Visibility.Visible);
			}

			Enumerable.Range(1, 2).ForEach(
				iy =>
				{

					var pipe = new Pipe.TopToBottom();


					pipe.Container.MoveTo(field_x + Tile.ShadowBorder + Tile.Size * 2, field_y + Tile.ShadowBorder + 52 * iy - 12).AttachTo(this);


					pipe.Water.ForEach(
						i => WaterFlow.Enqueue(() => i.Visibility = Visibility.Visible)
					);


				}
			);

			{

				var pipe = new Pipe.TopToRight();


				pipe.Container.MoveTo(field_x + Tile.ShadowBorder + Tile.Size * 2, field_y + Tile.ShadowBorder + 52 * 3 - 12).AttachTo(this);


				WaterFlow.Enqueue(() => pipe.Pipe_0_16.Visibility = Visibility.Visible);
				WaterFlow.Enqueue(() => pipe.Pipe_16_32.Visibility = Visibility.Visible);
				WaterFlow.Enqueue(() => pipe.Pipe_32_48.Visibility = Visibility.Visible);
				WaterFlow.Enqueue(() => pipe.Pipe_48_64.Visibility = Visibility.Visible);
			}

			Enumerable.Range(3, 3).ForEach(
				ix =>
				{

					var pipe = new Pipe.LeftToRight();


					pipe.Container.MoveTo(field_x + Tile.ShadowBorder + Tile.Size * ix, field_y + Tile.ShadowBorder + 52 * 3 - 12).AttachTo(this);


					pipe.Water.ForEach(
						i => WaterFlow.Enqueue(() => i.Visibility = Visibility.Visible)
					);


				}
			);

			field[3, 2].Drain.Visibility = Visibility.Visible;

			{

				var pipe = new Pipe.LeftToDrain();

				field[6, 3].Drain.Visibility = Visibility.Visible;

				pipe.Container.MoveTo(field_x + Tile.ShadowBorder + Tile.Size * 6, field_y + Tile.ShadowBorder + 52 * 3 - 12).AttachTo(this);


				pipe.Water.ForEach(
					i => WaterFlow.Enqueue(() => i.Visibility = Visibility.Visible)
				);

			}


			{

				var pipe = new Pipe.PumpToRight();


				pipe.Container.MoveTo(field_x + Tile.ShadowBorder + Tile.Size * 0, field_y + Tile.ShadowBorder + 52 * 1 - 12).AttachTo(this);

				pipe.Water.ForEach(
					i =>
						WaterFlow.Enqueue(() => i.Visibility = Visibility.Visible)
				);

			}

			{

				var pipe = new Pipe.LeftToRight();


				pipe.Container.MoveTo(field_x + Tile.ShadowBorder + Tile.Size * 1, field_y + Tile.ShadowBorder + 52 * 1 - 12).AttachTo(this);


				pipe.Water.ForEach(
					i => WaterFlow.Enqueue(() => i.Visibility = Visibility.Visible)
				);


			}


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
