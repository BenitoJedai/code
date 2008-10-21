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
using ScriptCoreLib.Shared.Avalon.TiledImageButton;

namespace AvalonExampleGallery.Shared
{

	[Script]
	public class AvalonExampleGalleryCanvas : Canvas
	{
		public const int DefaultWidth = 640;
		public const int DefaultHeight = 640;

		public AvalonExampleGalleryCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;




			var navbar = new AeroNavigationBar();

			var Container = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);


			var Pages = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

			var Overlay = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

			#region background
			var bg = new TiledBackgroundImage(
				"assets/AvalonExampleGallery/bg.png".ToSource(),
				96,
				96,
				8,
				8
			).AttachContainerTo(Container);
			#endregion

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
					}.MoveTo(0, i).AttachTo(this);
				}
			).ToArray();
			#endregion

			KnownPages.Value.ForEach(
				(k, i) =>
				{
					var o = new OptionWithShadow((k.Key + "/Preview.png").ToSource());

					o.MoveTo(48 + (120 + 32) * i, 48).AttachContainerTo(Pages);
					o.Overlay.AttachTo(Overlay);

					o.Initialize +=
						delegate
						{
							o.Target = (Canvas)Activator.CreateInstance(k.Value);

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
								},
								delegate
								{
									Pages.Hide();
									Overlay.Hide();
									o.Target.AttachTo(Container);
								}
							);
						};
				}
			);






			navbar.MoveContainerTo(4, 4).AttachContainerTo(this);


		}
	}

	[Script]
	public class TiledBackgroundImage : ISupportsContainer
	{
		public Canvas Container { get; set; }


		public TiledBackgroundImage(ImageSource src, int width, int height, int x, int y)
		{


			this.Container = new Canvas
			{
				Width = width * x,
				Height = height * y
			};

			for (int i = 0; i < x; i++)
				for (int j = 0; j < y; j++)
				{
					new Image
					{
						Source = src,
						Width = width,
						Height = height
					}.MoveTo(i * width, j * height).AttachTo(this.Container);
				}
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

		public OptionWithShadow(ImageSource src)
		{
			this.Overlay = new Rectangle
			{
				Fill = Brushes.Black,
				Width = 120,
				Height = 90,
				Opacity = 0
			};

			this.Container = new Canvas
			{
				Width = 196,
				Height = 90 + 9
			}.MoveTo(48, 48);

			new Image
			{
				Source =
					"assets/AvalonExampleGallery/PreviewShadow.png".ToSource(),
				Width = 196,
				Height = 90
			}.MoveTo(0, 9).AttachTo(this.Container);

			var PreviewSelection = new Image
			{
				Source =
					"assets/AvalonExampleGallery/PreviewSelection.png".ToSource(),
				Width = 138,
				Height = 108,
				Visibility = Visibility.Hidden
			}.AttachTo(this.Container);

			this.Overlay.MouseEnter +=
				delegate
				{
					PreviewSelection.Show();
				};

			this.Overlay.MouseLeave +=
				delegate
				{
					PreviewSelection.Hide();
				};

			this.Overlay.MouseLeftButtonUp +=
				delegate
				{
					if (this.Target == null)
						if (this.Initialize != null)
							this.Initialize();
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

		public event Action Initialize;

		public event Action Click;
	}

}
