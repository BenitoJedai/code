using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;

namespace ScriptCoreLib.Avalon
{
	[Description("Displays a centered image with sattelite spinning images. For example jsc spinning logo.")]
	public class ImageCarouselCanvas : ISupportsContainer
	{
		public const int DefaultWidth = 400;
		public const int DefaultHeight = 400;

		public class XImage
		{
			public double Opacity { get; set; }
			public double Radius { get; set; }

			public Image Image;

		}

		public class Arguments
		{
			public Action<Func<Image, XImage>> AddImages;

			public int ImageDefaultWidth;
			public int ImageDefaultHeight;

			public Func<Image> CreateCenterImage;

            
		}

        
		public ImageCarouselCanvas(Arguments args)
		{
			this.CloseOnClick = true;

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

			args.AddImages(Add);


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

							+ (DefaultWidth + args.ImageDefaultWidth * 0.3) / 2
							//+ jsc.ImageDefaultWidth 

							+ cos * item_.Radius
							- size * z2 * 0.5

							,
							sin * item_.Radius

							+ (DefaultHeight + args.ImageDefaultHeight * 0.3) / 2
							//+ jsc.ImageDefaultHeight

							- size * z2 * 0.5

						);

						Canvas.SetZIndex(item, Convert.ToInt32(z1 * 1000));
						i++;
					}

				}
			);

			var logo = args.CreateCenterImage();


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
				(DefaultWidth - args.ImageDefaultWidth) / 2,
				(DefaultHeight - args.ImageDefaultHeight) / 2
			);

			var logo_hit = new Rectangle
			{
				Fill = Brushes.Red,
				Opacity = 0
			};

			Canvas.SetZIndex(logo_hit, Convert.ToInt32(501));

			logo_hit.Cursor = Cursors.Hand;
			logo_hit.AttachTo(this).MoveTo(
				(DefaultWidth - args.ImageDefaultWidth) / 2,
				(DefaultHeight - args.ImageDefaultHeight) / 2
			);
			logo_hit.SizeTo(args.ImageDefaultWidth, args.ImageDefaultHeight);

			logo_hit.MouseLeftButtonUp +=
				delegate
				{
					if (AtLogoClick != null)
						AtLogoClick();
					//new Uri("http://www.jsc-solutions.net").NavigateTo(this.Container);

					if (CloseOnClick)
					{
						Close();
						logo_hit.Orphanize();
					}
				};

			this.HideSattelites =
				delegate
                {
                    Action TriggerClose = () => AtAnimation =
						t =>
						{
							if (images.Any(k => k.Opacity > 0))
								return;

							t.Stop();

							if (AtClose != null)
								AtClose();
						};


                    AtAnimation =
                        t =>
                        {
                            if (images.Any(k => k.Opacity < 1))
                                return;

                            foreach (var item__ in images)
                            {
                                var item = item__;

                                WaitAndAppear(1 + 1000.Random(), 0.12, n => item.Opacity = 1 - n);
                            }

                            TriggerClose();
                        };

                    
				};

			this.Close =
				delegate
				{
					WaitAndAppear(1, 0.12, n => logo.Opacity = 1 - n);

					HideSattelites();
				};
		}

		public bool CloseOnClick { get; set; }

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

		public readonly Action HideSattelites;
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
