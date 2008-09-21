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

namespace NavigationButtons.Shared
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public MyCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			Colors.Blue.ToGradient(Colors.Red, DefaultHeight / 4).Select(
				(color, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(color),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();

			// http://social.msdn.microsoft.com/forums/en-US/wpf/thread/21504c22-0d79-404e-ba0e-1cee91a02c2a/

			

			var n1 = new AeroNavigationBar();

			n1.Container.MoveTo(50, 50).AttachTo(this);

			n1.GoBack +=
				delegate
				{
					n1.ButtonGoBack.Enabled = false;
					n1.ButtonGoForward.Enabled = true;

				};

			n1.GoForward +=
				delegate
				{
					n1.ButtonGoForward.Enabled = false;
					n1.ButtonGoBack.Enabled = true;

				};


			var n2 = new AeroNavigationBar();

			n2.Container.MoveTo(120, 50).AttachTo(this);

			n2.GoBack +=
				delegate
				{
					n2.ButtonGoBack.Enabled = false;
					n2.ButtonGoForward.Enabled = true;

				};

			n2.GoForward +=
				delegate
				{
					n2.ButtonGoForward.Enabled = false;
					n2.ButtonGoBack.Enabled = true;

				};


			var fs = new FullscreenButtonControl();

			fs.Container.AttachTo(this).MoveTo(8, DefaultHeight - 8 - 24);


			var fs2 = new FullscreenButtonControl();

			fs2.ButtonGoFullscreen.Enabled = false;
			fs2.Container.AttachTo(this).MoveTo(8 + 24, DefaultHeight - 8 - 24);
		}

	}


}
