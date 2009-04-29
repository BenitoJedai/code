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
using FlashPlasmaEngine;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace FlashPlasmaBlueAvalon
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			this.Width = 600;
			this.Height = 600;

			var i = new Image();

			//var s = new WriteableBitmap(600, 600, 96, 96, PixelFormats.Pbgra32, null);
			var s = new WriteableBitmap(600, 600, PixelFormats.Pbgra32);

			Plasma.generatePlasma(600, 600);
			var shift = 0;

			Action Refresh =
				delegate
				{

					var buffer = Plasma.shiftPlasma(shift);

					s.Lock();


					for (int j = 0; j < buffer.Length; j++)
					{
						//Marshal.WriteInt32(s.BackBuffer, j * 4, unchecked((int)(buffer[j] | 0xff000000)));
						s[j] = unchecked((int)(buffer[j] | 0xff000000));
					}

					//s.AddDirtyRect(new Int32Rect(0, 0, 600, 600));
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

			t.Interval = TimeSpan.FromMilliseconds(50);
			t.Start();

			Refresh();

			i.Source = s;

			
			this.Content = i;
		}
	}
}
