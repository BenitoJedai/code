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
			}.ToGradient(DefaultHeight / 4
			//Colors.White.ToGradient(Colors.Blue, DefaultHeight / 4 / 2).Concat(
				//Colors.Blue.ToGradient(Colors.Red, DefaultHeight / 4 / 2)
			).Select(
				(c, i) =>
				new Rectangle
				{
					Fill = new SolidColorBrush(c),
					Width = DefaultWidth,
					Height = 4,
				}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();

		
			var t = new TextBox
			{
				AcceptsReturn = true,
				Background = Brushes.White,
				BorderThickness = new Thickness(0),
				Text = "hello world" + Environment.NewLine,
				Width = DefaultWidth / 2,
				Height = DefaultHeight / 3
			}.AttachTo(this).MoveTo(DefaultWidth / 4, 4);


			var rnd = new Random();

			var OverlayCanvas = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			};

			

			Enumerable.Range(0, 6).ForEach(
				x =>
				{
					Enumerable.Range(0, 4).ForEach(
						y =>
						{
							new Image
							{
								Source = (KnownAssets.Path.Data + "/tile0_black_unfocus8.png").ToSource(),
							}.MoveTo(64 + 64 * x - 8, DefaultHeight / 2 + 52 * y - 8).AttachTo(this);

							new Image
							{
								Source = (KnownAssets.Path.Data + "/tile0.png").ToSource(),
							}.MoveTo(64 + 64 * x, DefaultHeight / 2 + 52 * y).AttachTo(this);



							var BlackFilter = new Image
							{
								Source = (KnownAssets.Path.Data + "/tile0_black.png").ToSource(),
								Opacity = rnd.NextDouble() * 0.5
							}.MoveTo(64 + 64 * x, DefaultHeight / 2 + 52 * y).AttachTo(this);

							var YellowFilter = new Image
							{
								Source = (KnownAssets.Path.Data + "/tile0_yellow.png").ToSource(),
								Opacity = 0.4,
								Visibility = System.Windows.Visibility.Hidden,
							}.MoveTo(64 + 64 * x, DefaultHeight / 2 + 52 * y).AttachTo(this);


							var Overlay = new Rectangle
							{
								Cursor = Cursors.Hand,
								Fill = Brushes.Black,
								Width = 64,
								Height = 52,
								Opacity = 0
							}.MoveTo(64 + 64 * x, DefaultHeight / 2 + 52 * y).AttachTo(OverlayCanvas);

							if (x == 2)
							{
								var Pipe = new Image
								{
									Source = (KnownAssets.Path.Data + "/pipe_brown_tb.png").ToSource(),
								}.MoveTo(64 + 64 * x, DefaultHeight / 2 + 52 * y - 12).AttachTo(this);

								var PipeGlow = new Image
								{
									Source = (KnownAssets.Path.Data + "/pipe_glow_tb.png").ToSource(),
								}.MoveTo(64 + 64 * x, DefaultHeight / 2 + 52 * y - 12).AttachTo(this);
							}

							Overlay.MouseEnter +=
								delegate
								{
									YellowFilter.Visibility = System.Windows.Visibility.Visible;
								};

							Overlay.MouseLeave +=
								delegate
								{
									YellowFilter.Visibility = System.Windows.Visibility.Hidden;
								};
						}
					);
				}
			);

			OverlayCanvas.AttachTo(this).MoveTo(0, 0);


			var Navigationbar = new AeroNavigationBar();

			Navigationbar.Container.MoveTo(4, 4).AttachTo(this);

			//var i2 = new Image
			//{
			//    Source = (KnownAssets.Path.Data + "/draft.png").ToSource(),
			//}.MoveTo(64 * 3, DefaultHeight / 2).AttachTo(this);

			//i2.MouseLeftButtonUp +=
			//    delegate
			//    {
			//        PlaySound("place_tile");
			//    };

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
		}
	}
}
