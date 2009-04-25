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
using FlashPlasmaEngine;
using System.Windows.Threading;

namespace FlashPlasmaPurpleAvalon
{
	/// <summary>
	/// Interaction logic for Page1.xaml
	/// </summary>
	public partial class Page1 : Page
	{
		public Page1()
		{
			// Request for the permission of type 'System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' failed.

			InitializeComponent();

			var i = new Image();

			var s = new WriteableBitmap(600, 600, 96, 96, PixelFormats.Pbgra32, null);

			Plasma.generatePlasma(600, 600);
			var shift = 0;

			var t = new DispatcherTimer();

			t.Tick +=
				delegate
				{
					shift++;

					var buffer = Plasma.shiftPlasma(shift);

					s.Lock();


					for (int j = 0; j < buffer.Length; j++)
					{
						Marshal.WriteInt32(s.BackBuffer, j * 4, unchecked((int)buffer[j]));
					}

					s.AddDirtyRect(new Int32Rect(0, 0, 600, 600));
					s.Unlock();
				};

			t.Interval = TimeSpan.FromMilliseconds(1);
			t.Start();

			i.Source = s;

			this.Content = i;
		}
	}
}
