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
using System.Windows.Media.Imaging;
using System.Windows.Input;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Tween;

namespace InteractiveSketchupWarehouse.Shared
{
	[Script]
	public class InteractiveSketchupWarehouseCanvas : Canvas
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 400;

		public InteractiveSketchupWarehouseCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;
			Background = Brushes.White;

			this.ClipToBounds = true;

			//Colors.Cyan.ToGradient(Colors.White, DefaultHeight / 4).Select(
			//    (c, i) =>
			//        new Rectangle
			//        {
			//            Fill = new SolidColorBrush(c),
			//            Width = DefaultWidth,
			//            Height = 4,
			//        }.MoveTo(0, i * 4).AttachTo(this)
			//).ToArray();




			var img = new Image
			{
				Width = 400,
				Height = 300
			};

			img.AttachTo(this);

			new TextBox
			{
				Foreground = Brushes.Red,
				Width = DefaultWidth,
				BorderThickness = new Thickness(0),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Height = 32,
				Text = "Flash version will only work in web context!"
			}.AttachTo(this);

			var imagenum = 34;

			var src_uri = ApplyFrame(imagenum);



			img.Cursor = Cursors.Hand;

			var xoverlay = new Rectangle
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
				Fill = Brushes.Black,
				Opacity = 0,
				Cursor = Cursors.Hand,
			}.AttachTo(this);


			var bg = new Rectangle
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
				Fill = Brushes.Black,
			}.AttachTo(this);

			var bga = bg.ToAnimatedOpacity();

			bg.Opacity = 0.5;

			var c = new ScriptCoreLib.Shared.Avalon.Carousel.SimpleCarouselControl(DefaultWidth, DefaultHeight);

			var options = new[]
			{
				"58eca9d1551fde5a3b70edf326d07e86",
				"6e006174683f7372de48c10a5a3895c4",
				"5ff285283e8ec610434fdb742d7e0cea",
				"8d4d82e8807c3823bd4169514fca71f2",
				"bad472a435d3fab41992a70eb6b3a2a6",
				"3a4a2c47e8d8fdbb7302a5e36ce363de",
				"982a7381b7c5f4d36dce312d2cfb61e8",
				"84bafa682ff9c15b665aa76cf50df2c3",
				"9533d2d07d433ac072f17dbd3c237580",
				"d7222464c02cca18e92340be97470aea",
				"67a2216e4253e3f75eb112e8bc6bbc53",
				"5f310d576a8180e2ae504fdbdb5a7e96",
				"d106aaeaa51217cc465e08d496c0420f",
				"d2f505aca92ee65fbdb8512730b99253"
			};

			var position = 0.0;


			var a = c.Container.ToAnimatedOpacity();
			
			a.Opacity = 1;
		
			foreach (var mid_ in options.Randomize())
			{
				var mid = mid_;
				position += 0.6;
				c.AddEntry(
					new ScriptCoreLib.Shared.Avalon.Carousel.SimpleCarouselControl.EntryInfo
					{
						Position = position,
						Source = new Sketchup { mid = mid },
						Click =
							delegate
							{
								if (this.Current.mid == mid)
								{
									Uri u = new Sketchup { mid = Current.mid, IsPreview = true };

									u.NavigateTo();
									return;
								}

								this.Current.mid = mid;

								c.Overlay.Hide();
								a.Opacity = 0;

							
										Update(img, imagenum);
								

								1500.AtDelay(
									delegate
									{
										bga.Opacity = 0;
									}
								);

							}
					}
				);


			}

			xoverlay.MouseLeftButtonUp +=
				delegate
				{
					if (a.Opacity == 0)
					{
						c.Overlay.Show();
						a.Opacity = 1;
						bga.Opacity = 0.7;
					}
					else
					{
						a.Opacity = 0;
						bga.Opacity = 0;
						c.Overlay.Hide();
					}
				};

			this.MouseMove +=
				(sender, args) =>
				{

					var p = args.GetPosition(this);

					var z = (p.Y / DefaultHeight) * 0.7;

					img.MoveTo((DefaultWidth - DefaultWidth * z) / 2, (DefaultHeight - DefaultHeight * z) / 2);
					img.Width = DefaultWidth * z;
					img.Height = DefaultHeight * z;

					if (a.Opacity < 0.5)
					{
						imagenum = Convert.ToInt32((p.X / DefaultWidth + 0.5) * 35) % 36;

						img.Source = ApplyFrame(imagenum);
					}
				};


			c.AttachContainerTo(this);
			c.Overlay.AttachTo(this);
			c.Show();
		}

		private void Update(Image img, int imagenum)
		{
			ApplyFrameCache.Clear();

			Enumerable.Range(0, 36).ForEach(
				(i, next) =>
				{
					ApplyFrame(i);

					1.AtDelay(next);

					if (i == 0)
						img.Source = ApplyFrame(0);
				}
			);

		}



		public Sketchup Current = new Sketchup();

		public Dictionary<int, BitmapImage> ApplyFrameCache = new Dictionary<int, BitmapImage>();

		private BitmapImage ApplyFrame(int imagenum)
		{
			if (ApplyFrameCache.ContainsKey(imagenum))
				return ApplyFrameCache[imagenum];

			var nw = DefaultWidth / 36;

			var n = new Rectangle
			{
				Width = nw,
				Height = nw / 2,
				Fill = Brushes.Blue
			}.MoveTo(nw * imagenum, DefaultHeight - nw / 2).AttachTo(this);

			var src_uri = new BitmapImage();
			ApplyFrameCache[imagenum] = src_uri;
			src_uri.DownloadCompleted +=
				delegate
				{
					//bg.Fill = Color_Active();

					500.AtDelay(
						delegate
						{
							n.Orphanize();
						}
					);
				};

			src_uri.DownloadFailed +=
				delegate
				{
					n.Fill = Brushes.Red;

					//bg.Fill = Color_Error();
				};

			src_uri.BeginInit();
			src_uri.UriSource = new Sketchup { mid = Current.mid, imagenum = imagenum };
			src_uri.EndInit();

			return src_uri;
		}

		[Script]
		public class Sketchup
		{
			public string mid;
			public int imagenum = 0;

			public bool IsPreview;
			public static implicit operator Uri(Sketchup e)
			{
				if (e.IsPreview)
					return new Uri("http://sketchup.google.com/3dwarehouse/details?mid=" + e.mid + "&ct=hppm");

				return new Uri("http://sketchup.google.com/3dwarehouse/download?mid=" + e.mid + "&rtyp=swivel&setnum=0&imagenum=" + e.imagenum);

			}

			public static implicit operator ImageSource(Sketchup e)
			{
				var src_uri = new BitmapImage();

				src_uri.BeginInit();
				src_uri.UriSource = e;
				src_uri.EndInit();

				return src_uri;
			}
		}
	}
}
