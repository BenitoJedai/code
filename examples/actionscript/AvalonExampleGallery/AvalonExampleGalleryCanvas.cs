using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Carousel;
using ScriptCoreLib.Shared.Avalon.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.TextButton;
using ScriptCoreLib.Shared.Avalon.TiledImageButton;
using ScriptCoreLib.Shared.Lambda;

namespace AvalonExampleGallery.Shared
{

	public class AvalonExampleGalleryCanvas : Canvas
	{
		public const int DefaultWidth = 800;
		public const int DefaultHeight = 640;

		public class OptionPosition
		{
			public int X;
			public int Y;

			public Action Clear;
		}

		public AvalonExampleGalleryCanvas()
			: this(true, null)
		{
		}

		public AvalonExampleGalleryCanvas(bool EnableBackground, Func<string, OptionPosition> GetOptionPosition)
		{
			Width = DefaultWidth;
			Height = DefaultHeight;




			var navbar = new AeroNavigationBar();

			var Container = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
				Name = "AvalonExampleGalleryCanvas_Container"
			}.AttachTo(this);

			Container.ClipTo(0, 0, DefaultWidth, DefaultHeight);

			var Pages = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
				Name = "AvalonExampleGalleryCanvas_Pages"
			}.AttachTo(this);


			var CarouselPages = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
				Name = "AvalonExampleGalleryCanvas_CarouselPages"
			}.AttachTo(this);

			var Overlay = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
				Name = "AvalonExampleGalleryCanvas_Overlay"
			}.AttachTo(this);


			if (EnableBackground)
			{
				#region background
				var bg = new TiledBackgroundImage(
					"assets/AvalonExampleGallery/bg.png".ToSource(),
					96,
					96,
					9,
					8
				).AttachContainerTo(Container);

	
				#endregion

			}

	
			var Toolbar = new Canvas
			{
				Width = DefaultWidth,
				Height = navbar.Height,
				Opacity = 0,
				Name = "Toolbar"
			}.AttachTo(this);

			1000.AtDelay(
				Toolbar.FadeIn
			);

			#region shadow
			Colors.Black.ToTransparentGradient(40).Select(
				(c, i) =>
				{
					return new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 1,
						Opacity = c.A / 255.0
					}.MoveTo(0, i).AttachTo(Toolbar);
				}
			).ToArray();
			#endregion

			var cc = new SimpleCarouselControl(DefaultWidth, DefaultHeight);
			const string cc_Caption = "Click on a thumbnail!";

			cc.Caption.Text = cc_Caption;
			cc.Timer.Stop();

			var btnCarouselCanvas = new Canvas
			{
				Width = 128,
				Height = 32,
			}.AttachTo(this).MoveTo(DefaultWidth - 128, 4);


			#region Options
			var AllPages = KnownPages.Value;

			AllPages.ForEach(
				(k, i) =>
				{
					var o = new OptionWithShadowAndType(k.Key, k.Value);


					var ce = new SimpleCarouselControl.EntryInfo
					{
						Source = (k.Key + "/Preview.png").ToSource(),
						Position = i * Math.PI * 2 / AllPages.Count,
						MouseEnter =
							delegate
							{
								cc.Caption.Text = o.Caption.Text;
							},
						MouseLeave =
							delegate
							{
								cc.Caption.Text = cc_Caption;
							},
						Click =
							delegate
							{
								o.InitializeHint();


								navbar.History.Add(
									delegate
									{
										cc.Timer.Start();
										o.Target.Orphanize();
										CarouselPages.Show();
										Overlay.Show();
										btnCarouselCanvas.Show();
									},
									delegate
									{
										btnCarouselCanvas.Hide();
										cc.Timer.Stop();
										CarouselPages.Hide();
										Overlay.Hide();
										o.Target.AttachTo(Container);
									}
								);
							}
					};

					cc.AddEntry(ce);


					OptionPosition p = null;

					if (GetOptionPosition != null)
						p = GetOptionPosition(o.Caption.Text);

					if (p == null)
					{
						o.MoveTo(
							48 + (180) * (i % 4),
							48 + Convert.ToInt32(i / 4) * 140
						);
					}
					else
					{
						p.Clear();

						o.MoveTo(
							p.X,
							p.Y
						);
					}

					o.AttachContainerTo(Pages);
					o.Overlay.AttachTo(Overlay);

					o.TargetInitialized +=
						delegate
						{
							o.Target.MoveTo(
								(DefaultWidth - o.Target.Width) / 2,
								(DefaultHeight - o.Target.Height) / 2
							);

							//o.Target.ClipTo(0, 0, Convert.ToInt32( o.Target.Width), Convert.ToInt32(o.Target.Height));
						};

					o.Click +=
						delegate
						{

							navbar.History.Add(
								delegate
								{
									o.Target.Orphanize();
									Pages.Show();
									Overlay.Show();
									btnCarouselCanvas.Show();
								},
								delegate
								{
									btnCarouselCanvas.Hide();
									Pages.Hide();
									Overlay.Hide();
									o.Target.AttachTo(Container);
								}
							);
						};
				}
			);
			#endregion

			#region btnCarousel

		
			var btnCarousel = new TextButtonControl
			{
				Width = btnCarouselCanvas.Width,
				Height = btnCarouselCanvas.Height,
				Text = "View as carousel...",
				Foreground = Brushes.White
			}.AttachContainerTo(btnCarouselCanvas);

			btnCarousel.MouseEnter +=
				delegate
				{
					btnCarousel.Foreground = Brushes.Blue;
				};

			btnCarousel.MouseLeave +=
				delegate
				{
					btnCarousel.Foreground = Brushes.White;
				};

			btnCarousel.Click +=
				delegate
				{
					navbar.History.Add(
						delegate
						{
							Pages.Show();
							btnCarousel.Container.Show();
							CarouselPages.Hide();
							
							cc.Hide();
							cc.Timer.Stop();
						},
						delegate
						{
							Pages.Hide();
							CarouselPages.Show();
							btnCarousel.Container.Hide();
							cc.Show();
							cc.Timer.Start();
						}
					);
				};
			#endregion


			cc.Hide();

			CarouselPages.Hide();
			cc.AttachContainerTo(CarouselPages);

			cc.Overlay.Name = "cc_Overlay";
			cc.Overlay.AttachTo(Overlay);



			#region logo
			var logo = new Image
			{
				Source = "assets/AvalonExampleGallery/jsc.png".ToSource(),
				Width = 96,
				Height = 96
			}.MoveTo(DefaultWidth - 96, DefaultHeight - 96).AttachTo(Container);

			var logo_Overlay = new Rectangle
			{
				Width = 96,
				Height = 96,
				Fill = Brushes.Blue,
				Opacity = 0,
				Cursor = Cursors.Hand
			}.MoveTo(DefaultWidth - 96, DefaultHeight - 96).AttachTo(Overlay);

			logo_Overlay.MouseEnter +=
				delegate
				{
					Pages.Opacity = 0.5;
					CarouselPages.Opacity = 0.5;
				};

			logo_Overlay.MouseLeave +=
				delegate
				{
					Pages.Opacity = 1;
					CarouselPages.Opacity = 1;
				};

			logo_Overlay.MouseLeftButtonUp +=
				delegate
				{
					new Uri("http://jsc.sourceforge.net").NavigateTo();
				};
			#endregion


			navbar.MoveContainerTo(4, 4).AttachContainerTo(Toolbar);


		}
	}


	[Script]
	public class OptionWithShadowAndType : OptionWithShadow
	{
		public event Action TargetInitialized;

		public OptionWithShadowAndType(string src, Type t) : base((src + "/Preview.png").ToSource())
		{
			this.Initialize +=
				delegate
				{
					this.Target = (Canvas)Activator.CreateInstance(t);

					if (TargetInitialized != null)
						TargetInitialized();
				};

			var Suffix = "Canvas";

			var Name = t.Name;

			if (Name.EndsWith(Suffix))
				Name = Name.Substring(0, Name.Length - Suffix.Length);

			this.Caption.Text = Name;
		}
	}

	[Script]
	public class OptionWithShadow : ISupportsContainer
	{
		public Canvas Container { get; set; }

		public Rectangle Overlay;

		public OptionWithShadow MoveTo(int x, int y)
		{
			this.Container.MoveTo(x, y);
			this.Overlay.MoveTo(x + 9, y + 9);

			return this;
		}

		public TextBox Caption { get; set; }

		public OptionWithShadow(ImageSource src)
		{
			this.Overlay = new Rectangle
			{
				Fill = Brushes.Black,
				Width = 120,
				Height = 90 + 18 + 4 + 20,
				Opacity = 0,
				Cursor = Cursors.Hand
			};

			this.Container = new Canvas
			{
				//Background = Brushes.Red,
				Width = 166 + 9,
				Height = 90 + 18 + 4 + 20
			}.MoveTo(48, 48);

			//new Rectangle
			//{
			//    Fill = Brushes.Yellow,
			//    Width = 196,
			//    Height = 90,
			//}.AttachTo(this.Container).MoveTo(0, 0); 

			new Image
			{
				// no stretch by default
				//Stretch = Stretch.Fill,
				Source =
					"assets/AvalonExampleGallery/PreviewShadow.png".ToSource(),
				Width = 166,
				Height = 90
			}.AttachTo(this.Container).MoveTo(9, 9);

			var PreviewSelection = new Image
			{
				Source =
					"assets/AvalonExampleGallery/PreviewSelection.png".ToSource(),
				Width = 138,
				Height = 108,
				Visibility = Visibility.Hidden
			}.AttachTo(this.Container);

			this.Caption = new TextBox
			{
				Background = Brushes.Transparent,
				Foreground = Brushes.White,
				BorderThickness = new Thickness(0),
				Text = "title",
				Width = 120 + 9*2,
				Height = 20,
				IsReadOnly = true,
				TextAlignment = TextAlignment.Center
			}.AttachTo(this.Container).MoveTo(0, 104);

			this.Overlay.MouseEnter +=
				delegate
				{
					Caption.Foreground = Brushes.Blue;
					PreviewSelection.Show();
				};

			this.Overlay.MouseLeave +=
				delegate
				{
					Caption.Foreground = Brushes.White;
					PreviewSelection.Hide();
				};

			this.Overlay.MouseLeftButtonUp +=
				delegate
				{
					InitializeHint();

					if (this.Click != null)
						this.Click();

				};

			new Image
			{
				Source = src,
				Width = 120,
				Height = 90
			}.MoveTo(9, 9).AttachTo(this.Container);
		}

		public Canvas Target;

		public void InitializeHint()
		{
			if (this.Target == null)
				if (this.Initialize != null)
					this.Initialize();
		}

		public event Action Initialize;

		public event Action Click;
	}

}
