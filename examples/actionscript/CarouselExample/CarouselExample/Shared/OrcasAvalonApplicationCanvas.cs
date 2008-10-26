using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace CarouselExample.Shared
{
	[Script]
	public class Boxed<T>
	{
		public T Value;
	}

	[Script]
	public class OrcasAvalonApplicationCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public Canvas Container { get; set; }
		public Canvas Overlay { get; set; }

		public OrcasAvalonApplicationCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			Colors.Yellow.ToGradient(Colors.White, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();


			this.Container = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

			this.Overlay = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

			var a = new
			{
				o = default(Boxed<double>),
				cy = default(Boxed<double>),
				pc = default(Canvas),
				ps = default(Image),
				Overlay = default(Rectangle),
				Tick = default(Action),
			}.ToEmptyList();

			(1000 / 30).AtInterval(
				delegate
				{
					a.ForEach(k => k.Tick());

					a.OrderBy(k => k.cy.Value).ForEach(
						k =>
						{


							k.Overlay.Orphanize();
							k.Overlay.AttachTo(this.Overlay);

							k.pc.Orphanize();
							k.pc.AttachTo(this.Container);

						}
					);
				}
			);

			var s = 0.01;

			Action<ImageSource, double, Uri> AddEntry =
				(Source, u, Target) =>
				{
					var pc_Width = 166 + 9;
					var pc_Height = 90 + 9 * 2;

					var pc = new Canvas
					{
						//Background = Brushes.Green,
						Width = pc_Width,
						Height = pc_Height
					}.AttachTo(this.Container);

					var p = new Image
					{
						Width = 166,
						Height = 90,
						Stretch = Stretch.Fill,
						Source = (KnownAssets.Path.Assets + "/PreviewShadow.png").ToSource()
					}.AttachTo(pc);


					var ps = new Image
					{
						Width = 138,
						Height = 108,
						Stretch = Stretch.Fill,
						Source = (KnownAssets.Path.Assets + "/PreviewSelection.png").ToSource()
					}.AttachTo(pc);

					var pi = new Image
					{
						Width = 120,
						Height = 90,
						Stretch = Stretch.Fill,
						Source = Source,
						Cursor = Cursors.Hand
					}.AttachTo(pc);

					var Overlay_Width = 120;
					var Overlay_Height = 90;

					var Overlay = new Rectangle
					{
						Fill = Brushes.Black,
						Width = Overlay_Width,
						Height = Overlay_Height,
						Cursor = Cursors.Hand,
						Opacity = 0
					}.AttachTo(this.Overlay);

					ps.Visibility = Visibility.Hidden;

					Overlay.MouseLeftButtonUp +=
						delegate
						{
							Target.NavigateTo();

						};

					var IsHot = false;

					Overlay.MouseEnter +=
						delegate
						{
							IsHot = true;
							s = 0.004;

							p.Opacity = 1;
							pi.Opacity = 1;

							a.ForEach(
								k =>
								{
									if (k.ps == ps)
									{
										ps.Show();
									}
									else
									{
										k.ps.Hide();
									}



								}
							);

						};

					var o = new Boxed<double>();

					Overlay.MouseLeave +=
						delegate
						{
							IsHot = false;
							p.Opacity = o.Value;
							pi.Opacity = o.Value;


							s = 0.01;

							ps.Hide();
						};

					var _cy = new Boxed<double>();




					#region Tick
					Action Tick =
						delegate
						{

							var x = Math.Cos(u);
							var y = Math.Sin(u);

							var z = (y + 3) / 4;

							o.Value = (y + 1) / 2;

							if (!IsHot)
							{
								pi.Opacity = o.Value;
								p.Opacity = o.Value;
							}

							u += s * z;

							pc.Width = pc_Width * z;
							pc.Height = pc_Height * z;

							Overlay.Width = Overlay_Width * z;
							Overlay.Height = Overlay_Height * z;

							p.Width = 166 * z;
							p.Height = 90 * z;


							pi.Width = 120 * z;
							pi.Height = 90 * z;

							ps.Width = 138 * z;
							ps.Height = 108 * z;

							var cx = x * DefaultWidth / 4 + DefaultWidth / 2;
							var cy = y * DefaultHeight / 6 + DefaultHeight / 2;

							_cy.Value = cy;

							pc.MoveTo(
								cx - pc.Width / 2,
								cy - pc.Height / 2
							);

							p.MoveTo(
								z * 9,
								z * 9
							);

							pi.MoveTo(
								z * 9,
								z * 9
							);


							ps.MoveTo(
								0,
								0
							);

							Overlay.MoveTo(
								cx - pc.Width / 2 + 9 * z,
								cy - pc.Height / 2 + 9 * z
							);
						};
					#endregion


					a.Add(
						new
						{
							o = (Boxed<double>)o,
							cy = (Boxed<double>)_cy,
							pc = (Canvas)pc,
							ps = (Image)ps,
							Overlay = (Rectangle)Overlay,
							Tick = Tick,
						}
					);

					Tick();
				};

		


			10.Times(
				i =>
				{
					AddEntry(
						(KnownAssets.Path.Assets + "/AvalonGalleryExample.png").ToSource(),
						Math.PI / 5 * i,
						new Uri("http://jsc.sourceforge.net/examples/avalon/MyWebPage.php")
					);
				}
			);






		}
	}
}
