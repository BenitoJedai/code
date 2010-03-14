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
using System.Threading;

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


					RenderOptions.SetBitmapScalingMode(i, BitmapScalingMode.Fant);
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
			//Add(new Flash());
			//Add(new Java());
			//Add(new PHP());
			//Add(new DotNet());

			var size = 64;


			var step = 0.0002;

			Action<DispatcherTimer> AtAnimation = delegate { };

			// .net is fast, but js will be slow :)

			var randomphase = Math.PI * 2 * new Random().NextDouble();

			(1000 / 50).AtIntervalWithTimer(
				t =>
				{
					var ms = s.ElapsedMilliseconds;


					var i = 0;
					
					AtAnimation(t);

					// using for each must be the last thing in a method 
					// because .leave operator currently cannot be understood by jsc

					foreach (var item_ in images)
					{
						var item = item_.Image;
						var phase = Math.PI * 2 * i / images.Count + randomphase;


						var cos = Math.Cos(step * ms + phase);
						var sin = Math.Sin(step * ms + phase);

						var z1margin = 0.7;
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

			ShowSattelites(images, WaitAndAppear);

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

				

					AtAnimation =
						t =>
						{
							if (images.Any(k => k.Opacity > 0))
								return;

							t.Stop();

							if (AtClose != null)
								AtClose();
						};

					foreach (var item__ in images)
					{
						var item = item__;

						WaitAndAppear(1 + 1000.Random(), 0.12, n => item.Opacity = 1 - n);
					}
				};
		}

		private static void ShowSattelites(List<XImage> images, Action<int, double, Action<double>> WaitAndAppear)
		{
			var ii = 0;
			foreach (var item__ in images
				//.Randomize()
				)
			{
				var item = item__;

				WaitAndAppear(500 + ((ii + images.Count / 2) % images.Count) * 200, 0.07, n => item.Opacity = n);
				ii++;
			}
			// we would be breaking jsc atm...
			//return ii;
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

	namespace Desktop
	{
		using ScriptCoreLib.CSharp.Avalon.Extensions;

		public static class AvalonLogoForDesktop
		{
			// note: this class can only run under .net

			public static Thread ShowDialogSplash()
			{
				return ShowDialog(c => 4500.AtDelay(c));
			}

			public static void ShowDialogSplash(Action h)
			{
				var t = ShowDialogSplash();

				h();

				t.Join();
			}

			public static Thread ShowDialog()
			{
				return ShowDialog(null);
			}

			internal static void Main(string[] args)
			{
				AvalonLogoForDesktop.ShowDialogSplash(
					// primary task executes longer than splash
					() => Thread.Sleep(7000)
				);
			}

			public static Thread ShowDialog(Action<Action> AnnounceCloseAction)
			{
				var t = new Thread(
					delegate()
					{
						InternalShowDialog(AnnounceCloseAction);
					}
				)
				{
					ApartmentState = ApartmentState.STA,
					IsBackground = true
				};

				t.Start();

				return t;
			}

			private static void InternalShowDialog(Action<Action> AnnounceCloseAction)
			{
				var c = new PromotionWebApplication.AvalonLogo.AvalonLogoCanvas();

				if (AnnounceCloseAction != null)
					AnnounceCloseAction(c.Close);

				//c.Container.Effect = new DropShadowEffect();
				//c.Container.BitmapEffect = new DropShadowBitmapEffect();

				var w = c.ToWindow();

				w.ToTransparentWindow();

				c.AtClose += w.Close;

				// http://blog.joachim.at/?p=39
				// http://blogs.msdn.com/changov/archive/2009/01/19/webbrowser-control-on-transparent-wpf-window.aspx
				// http://blogs.interknowlogy.com/johnbowen/archive/2007/06/20/20458.aspx
				w.AllowsTransparency = true;
				w.WindowStyle = System.Windows.WindowStyle.None;
				w.Background = Brushes.Transparent;
				w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
				w.Topmost = true;
				w.ShowInTaskbar = false;

				w.ShowDialog();
			}
		}
	}
}
