using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using System.Windows.Media;
using PromotionWebApplication.AvalonLogo.Avalon.Images;
using System.Windows.Shapes;
using System.Windows.Media.Effects;
using System.Diagnostics;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Input;
using System.Windows.Threading;

namespace PromotionWebApplication.AvalonLogo
{
	public class AvalonLogoCanvas : ISupportsContainer
	{
		public const int DefaultWidth = 400;
		public const int DefaultHeight = 400;

		public class XImage
		{
			public double Opacity { get; set; }
			public double Radius { get; set; }

			public Image Image;

		}

		public AvalonLogoCanvas()
		{
			this.Container = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			};

			//var r = new Rectangle
			//{
			//    Fill = Brushes.Red,
			//    Opacity = 0.05
			//};

			//r.SizeTo(DefaultWidth, DefaultHeight);
			//r.AttachTo(this);

			//this.Container.Background = Brushes.Transparent;
			//this.Container.Background = Brushes.Red;

			var y = 0;

			var s = new Stopwatch();

			s.Start();

			var images = new List<XImage>();

			Func<Image, XImage> Add =
				i =>
				{
					y += 32;

					var n = new XImage
					{
						Image = i,
						Opacity = 0,
						Radius = 72
					};



					images.Add(n);
					i.Opacity = 0;
					i.AttachTo(this);

					return n;
				};

			Add(new Apple_Safari());
			Add(new Google_Chrome());
			Add(new Internet_Explorer_7_Logo());
			Add(new Firefox_3());
			Add(new Opera());
			Add(new Flash());
			Add(new Java());
			Add(new PHP());
			Add(new DotNet());

			var size = 64;
			var step = 0.0002;

			Action<DispatcherTimer> AtAnimation = delegate { };

			(1000 / 100).AtIntervalWithTimer(
				t =>
				{
					var ms = s.ElapsedMilliseconds;


					var i = 0;
					foreach (var item_ in images)
					{
						var item = item_.Image;
						var phase = Math.PI * 2 * i / images.Count;


						var cos = Math.Cos(step * ms + phase);
						var sin = Math.Sin(step * ms + phase);

						var z1margin = 0.3;
						var z1 = (cos + (1 + z1margin)) / (2 + z1margin);

						var z2margin = 1.0;
						var z2 = (cos + (1 + z2margin)) / (2 + z2margin);


						item.Opacity = z1 * item_.Opacity;
						item.SizeTo(size * z2, size * z2);
						item.MoveTo(
							-sin * item_.Radius

							+ (DefaultWidth + jsc.ImageDefaultWidth * 0.3) / 2
							//+ jsc.ImageDefaultWidth 

							+ cos * item_.Radius
							- size * z2 * 0.5

							,
							sin * item_.Radius

							+ (DefaultHeight + jsc.ImageDefaultHeight * 0.3) / 2
							//+ jsc.ImageDefaultHeight

							- size * z2 * 0.5

						);

						Canvas.SetZIndex(item, Convert.ToInt32(z1 * 1000));
						i++;
					}

					AtAnimation(t);
				}
			);

			var logo = new jsc();


			#region WaitAndAppear
			Action<int, double, Action<double>> WaitAndAppear =
				(delay, step__, set_Opacity) =>
				{
					var a = 0.0;

					set_Opacity(a);

					delay.AtDelay(
						delegate
						{


							(1000 / 30).AtIntervalWithTimer(
								t =>
								{
									a += step__;

									if (a > 1)
									{
										set_Opacity(1);

										t.Stop();
										return;
									}
									set_Opacity(a);
								}
							);
						}
					);
				};
			#endregion

			WaitAndAppear(200, 0.07, n => logo.Opacity = n);

			var ii = 0;
			foreach (var item__ in images
				//.Randomize()
				)
			{
				var item = item__;

				WaitAndAppear(500 + ((ii + images.Count / 2) % images.Count) * 200, 0.07, n => item.Opacity = n);
				ii++;
			}

			Canvas.SetZIndex(logo, Convert.ToInt32(500));

			logo.AttachTo(this).MoveTo(
				(DefaultWidth - jsc.ImageDefaultWidth) / 2,
				(DefaultHeight - jsc.ImageDefaultHeight) / 2
			);

			var logo_hit = new Rectangle
			{
				Fill = Brushes.Red,
				Opacity = 0
			};

			Canvas.SetZIndex(logo_hit, Convert.ToInt32(501));

			logo_hit.Cursor = Cursors.Hand;
			logo_hit.AttachTo(this).MoveTo(
				(DefaultWidth - jsc.ImageDefaultWidth) / 2,
				(DefaultHeight - jsc.ImageDefaultHeight) / 2
			);
			logo_hit.SizeTo(jsc.ImageDefaultWidth, jsc.ImageDefaultHeight);

			logo_hit.MouseLeftButtonUp +=
				delegate
				{
					if (AtLogoClick != null)
						AtLogoClick();
					//new Uri("http://www.jsc-solutions.net").NavigateTo(this.Container);
					Close();
					logo_hit.Orphanize();
				};

			this.Close =
				delegate
				{
					WaitAndAppear(1, 0.12, n => logo.Opacity = 1 - n);

					foreach (var item__ in images)
					{
						var item = item__;

						WaitAndAppear(1 + 1000.Random(), 0.12, n => item.Opacity = 1 - n);
						ii++;
					}

					AtAnimation =
						t =>
						{
							if (images.Any(k => k.Opacity > 0))
								return;

							t.Stop();

							if (AtClose != null)
								AtClose();
						};
				};
		}

		public event Action AtLogoClick;

		public event Action AtClose;

		public readonly Action Close;



		#region ISupportsContainer Members

		public System.Windows.Controls.Canvas Container
		{
			get;
			set;
		}

		#endregion
	}
}
