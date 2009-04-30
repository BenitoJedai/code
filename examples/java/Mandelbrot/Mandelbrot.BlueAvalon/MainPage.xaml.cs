using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace Mandelbrot.BlueAvalon
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			//SnapsToDevicePixels = true;
			//SizeToContent = SizeToContent.WidthAndHeight;

			var i = new Image
			{
				Width = MandelbrotProvider.DefaultWidth,
				Height = MandelbrotProvider.DefaultHeight
			};



			var s = new WriteableBitmap(MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight,
				// dpi does not seem to matter at this point
				PixelFormats.Pbgra32);
				//96, 96, PixelFormats.Pbgra32, null);


			var shift = 0;

			Action Refresh =
				delegate
				{

					var buffer = MandelbrotProvider.DrawMandelbrotSet(shift);

					s.Lock();


					for (int j = 0; j < buffer.Length; j++)
					{
						s[j] = unchecked((int)((uint)buffer[j] | 0xff000000));
						//Marshal.WriteInt32(s.BackBuffer, j * 4, unchecked((int)((uint)buffer[j] | 0xff000000)));
					}

					//s.AddDirtyRect(new Int32Rect(0, 0, MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight));
					s.Invalidate();
					s.Unlock();
				};

			var t = new DispatcherTimer();

			t.Tick +=
				delegate
				{
					shift++;
					Refresh();

				};

			t.Interval = TimeSpan.FromMilliseconds(10);
			t.Start();

			Refresh();

			i.Source = s;

			this.Content = i;
		}
	}
}
