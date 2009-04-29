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
					600,
					600
				)

			};

			var bitmap = new Bitmap(
				600,
				600
			);

			f.Paint +=
				(object sender, PaintEventArgs e) =>
				{
					e.Graphics.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
				};

			Action Refresh =
				delegate
				{
					Mandelbrot.MandelbrotProvider.DrawMandelbrotSet();

					var data = bitmap.LockBits(
						new Rectangle(0, 0, bitmap.Width, bitmap.Height),
						System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb
					);

					var buffer = Mandelbrot.MandelbrotProvider.bitmap;

					for (int i = 0; i < buffer.Length; i++)
					{
						Marshal.WriteInt32(data.Scan0, i * 4, unchecked((int)((uint)buffer[i] | 0xff000000)));
					}



					bitmap.UnlockBits(data);
				};

			Refresh();

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
