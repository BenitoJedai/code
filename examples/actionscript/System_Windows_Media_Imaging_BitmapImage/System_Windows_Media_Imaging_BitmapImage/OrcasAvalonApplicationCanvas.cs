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

namespace System_Windows_Media_Imaging_BitmapImage.Shared
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

			this.ClipToBounds = true;

			Colors.Blue.ToGradient(Colors.Red, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();

			var img = new Image
			{
				Source = (KnownAssets.Path.Assets + "/jsc.png").ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);



			var ii = new Image
			{

			}.AttachTo(this).MoveTo(0, 0);

			var s = new BitmapImage();

			s.DownloadCompleted +=
				delegate
				{
					img.Opacity = 0.5;
				};

			s.BeginInit();
			s.UriSource = new Uri("http://zproxy.appspot.com/assets/Bulldog/jsc.png");
			s.EndInit();

			ii.Source = s;

		

		}
	}
}
