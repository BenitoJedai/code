using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Mandelbrot.Forms
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);


			var f = new DoubleBufferedForm
			{
				MaximizeBox = false,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				ClientSize = new Size(
					MandelbrotProvider.DefaultWidth,
					MandelbrotProvider.DefaultHeight
				)

			};

			var bitmap = new Bitmap(
				MandelbrotProvider.DefaultWidth,
				MandelbrotProvider.DefaultHeight
			);

			f.Paint +=
				(object sender, PaintEventArgs e) =>
				{
					e.Graphics.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
				};

			var shift = 0;

			Action Refresh =
				delegate
				{
					var buffer = Mandelbrot.MandelbrotProvider.DrawMandelbrotSet(shift);

					var data = bitmap.LockBits(
						new Rectangle(0, 0, bitmap.Width, bitmap.Height),
						System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb
					);


					for (int i = 0; i < buffer.Length; i++)
					{
						Marshal.WriteInt32(data.Scan0, i * 4, unchecked((int)((uint)buffer[i] | 0xff000000)));
					}



					bitmap.UnlockBits(data);
					f.Invalidate();
				};

			Refresh();

			var t = new Timer();

			t.Interval = 1;
			t.Tick +=
				delegate
				{
					shift += 1;
					Refresh();
				};

			t.Start();

			Application.Run(f);
		}

		class DoubleBufferedForm : Form
		{
			public DoubleBufferedForm()
			{
				this.DoubleBuffered = true;
			}
		}
	}
}
