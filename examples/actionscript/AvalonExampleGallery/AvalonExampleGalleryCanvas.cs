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
using ScriptCoreLib.Shared.Avalon.TiledImageButton;

namespace AvalonExampleGallery.Shared
{
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
	public class AvalonExampleGalleryCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public AvalonExampleGalleryCanvas()
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


			var navbar = new AeroNavigationBar();

			var Container = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

			var bg = new TiledBackgroundImage(
				"assets/AvalonExampleGallery/bg.png".ToSource(),
				96,
				96,
				5,
				4
			).AttachContainerTo(Container);

			Colors.Black.ToTransparentGradient(32).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 1,
						Opacity = (double)(c.A) / 255.0
					}.MoveTo(0, i).AttachTo(this)
			).ToArray();


			new Image
			{
				Source =
					"assets/AvalonExampleGallery/PreviewShadow.png".ToSource(),
				Width = 196,
				Height = 90
			}.MoveTo(0, 9).AttachTo(Container);

			new Image
			{
				Source =
					"assets/AvalonExampleGallery/PreviewSelection.png".ToSource(),
				Width = 138,
				Height = 108
			}.AttachTo(Container);

			new Image
			{
				Source =
					//(global::NavigationButtons.Assets.Shared.KnownAssets.Path.Assets + "/Preview.png").ToSource(),
					(global::TextSuggestions.Shared.KnownAssets.Path.Assets + "/Preview.png").ToSource(),
				Width = 120,
				Height = 90
			}.MoveTo(9, 9).AttachTo(Container);

			//bg.Container.Opacity = 0.5;

			Canvas page1 =
				//new NavigationButtons.Code.MyCanvas();
				new global::TextSuggestions.Shared.TextSuggestionsCanvas();

			navbar.History.Add(
				delegate
				{
					page1.Orphanize();
				},
				delegate
				{
					page1.AttachTo(Container);
				}
			);


			navbar.Container.AttachTo(this);


		}
	}
}
