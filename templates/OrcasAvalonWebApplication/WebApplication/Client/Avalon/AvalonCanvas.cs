using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using WebApplication.Shared;
using System.Windows.Input;

namespace WebApplication.Client.Avalon
{
	[Script]
	public class AvalonCanvas : Canvas
	{
		public const int DefaultWidth = 640;
		public const int DefaultHeight = 400;


		public AvalonCanvas()
		{
			this.Width = DefaultWidth;
			this.Height = DefaultHeight;

			this.ClipToBounds = true;

			Colors.Black.ToGradient(Colors.Red, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();

			var t = new TextBox
			{
				FontSize = 20,
				Text = "C# to JavaScript and ActionScript".WithBranding(),
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = DefaultWidth,
				Height = 40,
				TextAlignment = TextAlignment.Center
			}.MoveTo(0, 32).AttachTo(this);

			#region link 1
			{
				var vs = new TextBox
				{
					FontSize = 20,
					Text = "View Source",
					BorderThickness = new Thickness(0),
					Foreground = Brushes.Blue,
					Background = Brushes.Transparent,
					IsReadOnly = true,
					Width = DefaultWidth,
					Height = 40,
					TextAlignment = TextAlignment.Center
				}.MoveTo(0, 64).AttachTo(this);


				var r = new Rectangle
				{
					Width = DefaultWidth,
					Height = 40,

					Fill = Brushes.Red,
					Opacity = 0,

					Cursor = Cursors.Hand
				}.MoveTo(0, 64).AttachTo(this);


				r.MouseLeftButtonUp +=
					delegate
					{
						new Uri(SharedExtensions.TemplateSourceCode).NavigateTo();
					};
			}
			#endregion

			#region link 2
			{
				var vs = new TextBox
				{
					FontSize = 20,
					Text = SharedExtensions.HomePageText,
					BorderThickness = new Thickness(0),
					Foreground = Brushes.Blue,
					Background = Brushes.Transparent,
					IsReadOnly = true,
					Width = DefaultWidth,
					Height = 40,
					TextAlignment = TextAlignment.Center
				}.MoveTo(0, 100).AttachTo(this);


				var r = new Rectangle
				{
					Width = DefaultWidth,
					Height = 40,

					Fill = Brushes.Red,
					Opacity = 0,

					Cursor = Cursors.Hand
				}.MoveTo(0, 100).AttachTo(this);


				r.MouseLeftButtonUp +=
					delegate
					{
						new Uri(SharedExtensions.HomePage).NavigateTo();
					};
			}
			#endregion

			var img = new Image
			{
				Source = (KnownAssets.Path.Assets + "/jsc.png").ToSource(),
				Width = 96,
				Height = 96
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);


		}
	}
}
