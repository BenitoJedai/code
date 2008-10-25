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

namespace CarouselExample.Shared
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

			Colors.Blue.ToGradient(Colors.Red, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();


			var help_idle = new Image
			{
				Source = (KnownAssets.Path.Assets + "/help_idle.png").ToSource()
			}.AttachTo(this);

			var help = new Image
			{
				Source = (KnownAssets.Path.Assets + "/help.png").ToSource()
			}.AttachTo(this);

			help.Opacity = 0;

			var img = new Image
			{
				Source = (KnownAssets.Path.Assets + "/jsc.png").ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);

			var t = new TextBox
			{
				FontSize = 32,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = 300
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


			Action<ImageSource, double, Uri> AddEntry =
				(Source, u, Target) =>
				{
					var p = new Image
					{
						Width = 166,
						Height = 90,
						Stretch = Stretch.Fill,
						Source = (KnownAssets.Path.Assets + "/PreviewShadow.png").ToSource()
					}.AttachTo(this);


					var ps = new Image
					{
						Width = 138,
						Height = 108,
						Stretch = Stretch.Fill,
						Source = (KnownAssets.Path.Assets + "/PreviewSelection.png").ToSource()
					}.AttachTo(this);

					var pi = new Image
					{
						Width = 120,
						Height = 90,
						Stretch = Stretch.Fill,
						Source = Source,
						Cursor = Cursors.Hand
					}.AttachTo(this);

					ps.Visibility = Visibility.Hidden;

					pi.MouseLeftButtonUp +=
						delegate
						{
							Target.NavigateTo();

						};

					pi.MouseEnter +=
						delegate
						{
							ps.Visibility = Visibility.Visible;
						};

					pi.MouseLeave +=
						delegate
						{
							ps.Visibility = Visibility.Hidden;
						};
					var s = 0.01;

					(1000 / 30).AtIntervalWithCounter(
						c =>
						{

							var x = Math.Cos(u);
							var y = Math.Sin(u);

							var z = (y + 3) / 4;

							u += s * z;

							p.Width = 166 * z;
							p.Height = 90 * z;


							pi.Width = 120 * z;
							pi.Height = 90 * z;

							ps.Width = 138 * z;
							ps.Height = 108 * z;

							var cx = x * DefaultWidth / 4 + DefaultWidth / 2;
							var cy = y * DefaultHeight / 6 + DefaultHeight / 2;

							p.MoveTo(
								cx - p.Width / 2,
								cy - p.Height / 2
							);

							pi.MoveTo(
								cx - p.Width / 2,
								cy - p.Height / 2
							);


							ps.MoveTo(
								cx - p.Width / 2 - 9 * z,
								cy - p.Height / 2 - 9 * z
							);
						}
					);

				};

			AddEntry(
				(KnownAssets.Path.Assets + "/AvalonGalleryExample.png").ToSource(),
				0,
				new Uri("http://jsc.sourceforge.net/examples/avalon/MyWebPage.php")
			);


			AddEntry(
				(KnownAssets.Path.Assets + "/AvalonGalleryExample.png").ToSource(),
				Math.PI,
				new Uri("http://jsc.sourceforge.net/examples/avalon/MyWebPage.php")
			);

			3000.AtDelay(
				delegate
				{
					// load new text from embedded resource

					(KnownAssets.Path.Assets + "/about.txt").ToStringAsset(
						e =>
						{
							t.FontSize = 16;
							t.Text = e;
						}
					);
				}
			);

		}
	}
}
