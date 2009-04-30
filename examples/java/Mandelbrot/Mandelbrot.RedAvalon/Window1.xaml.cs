using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace Mandelbrot.RedAvalon
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();

			SnapsToDevicePixels = true;
			SizeToContent = SizeToContent.WidthAndHeight;

			var i = new Image
			{
				Width = MandelbrotProvider.DefaultWidth,
				Height = MandelbrotProvider.DefaultHeight
			};

	

			var s = new WriteableBitmap(MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight, 
				// dpi does not seem to matter at this point
				96, 96, PixelFormats.Pbgra32, null);

			
			var shift = 0;

			Action Refresh =
				delegate
				{

					var buffer = MandelbrotProvider.DrawMandelbrotSet(shift);

					s.Lock();


					for (int j = 0; j < buffer.Length; j++)
					{
						Marshal.WriteInt32(s.BackBuffer, j * 4, unchecked((int)((uint)buffer[j] | 0xff000000)));
					}

					s.AddDirtyRect(new Int32Rect(0, 0, MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight));
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
