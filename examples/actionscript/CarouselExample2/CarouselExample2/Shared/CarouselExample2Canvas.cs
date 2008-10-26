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
using ScriptCoreLib.Shared.Avalon.Carousel;
using ScriptCoreLib.Shared.Lambda;

namespace CarouselExample2.Shared
{
	[Script]
	public class CarouselExample2Canvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public CarouselExample2Canvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			Colors.Black.ToGradient(Colors.Red, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();


			var cc = new SimpleCarouselControl(DefaultWidth, DefaultHeight);

			var t = new TextBox
			{
				Width = DefaultWidth,
				Height = 32,
				TextAlignment = TextAlignment.Center,
				Foreground = Brushes.White,
				BorderThickness = new Thickness(0),
				Background = Brushes.Transparent,
				IsReadOnly = true
			}.MoveTo(0, (DefaultHeight - 32) / 2);

			new[]
			{
				"/item1.png",
				"/item2.png",
				"/item3.png",
				"/Preview.png"
			}.ForEach(
				(string k, int index) =>
				{
					cc.AddEntry(
						new SimpleCarouselControl.EntryInfo
							{
								Click = cc.Timer.Toggle,
								Source = (KnownAssets.Path.Assets + k).ToSource(),
								Position = Math.PI / 2 * index,
								MouseEnter =
									delegate
									{
										t.Text = k;
									},
								MouseLeave =
									delegate
									{
										t.Text = "";
									}
							}
					);
				}
			);



			cc.AttachContainerTo(this);

			t.AttachTo(this);

			cc.Overlay.AttachTo(this);

		}
	}
}
