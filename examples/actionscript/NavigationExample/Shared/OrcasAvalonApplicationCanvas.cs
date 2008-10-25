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

namespace NavigationExample.Shared
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
				FontSize = 16,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = 300,
				Height = 32
			}.MoveTo(32, 32).AttachTo(this);

			var Overlay = new Rectangle
			{
				Fill = Brushes.Black,
				Opacity = 0,
				Width = 300,
				Height = 32,
				Cursor = Cursors.Hand
			}.MoveTo(32, 32).AttachTo(this);

			help_idle.Opacity = 0;
			help.Opacity = 1;
			img.Opacity = 0.5;

			Overlay.MouseEnter +=
				delegate
				{
					help_idle.Opacity = 1;
					help.Opacity = 0;

					img.Opacity = 1;
					t.Foreground = 0xffffff00.ToSolidColorBrush();
				};

			Overlay.MouseLeave +=
				delegate
				{
					help_idle.Opacity = 0;
					help.Opacity = 1;

					img.Opacity = 0.5;
					t.Foreground = 0xffffffff.ToSolidColorBrush();
				};

			Overlay.MouseLeftButtonUp +=
				delegate
				{
					new Uri("http://jsc.sf.net").NavigateTo();

				};

			3000.AtDelay(
				delegate
				{
					// load new text from embedded resource

					(KnownAssets.Path.Assets + "/about.txt").ToStringAsset(
						e =>
						{
							//t.FontSize = 16;
							t.Text = e;
						}
					);
				}
			);

		}
	}
}
