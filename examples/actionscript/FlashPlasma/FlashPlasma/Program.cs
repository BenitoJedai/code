using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using FlashPlasmaEngine;

namespace FlashPlasma
{
	public static unsafe class Program
	{
		class DoubleBufferedForm : Form
		{
			public DoubleBufferedForm()
			{
				this.DoubleBuffered = true;
			}
		}

		public static void Main(string[] args)
		{
			var f = new DoubleBufferedForm
			{
				FormBorderStyle = FormBorderStyle.FixedDialog,
				ClientSize = new Size(
					global::FlashPlasma.ActionScript.FlashPlasma.DefaultWidth,
					global::FlashPlasma.ActionScript.FlashPlasma.DefaultHeight
				)

			};

			Plasma.generatePlasma(
				global::FlashPlasma.ActionScript.FlashPlasma.DefaultWidth,
				global::FlashPlasma.ActionScript.FlashPlasma.DefaultHeight
			);

			var shift = 0;

			var bitmap = new Bitmap(
				global::FlashPlasma.ActionScript.FlashPlasma.DefaultWidth,
				global::FlashPlasma.ActionScript.FlashPlasma.DefaultHeight
			);


			f.Paint +=
				(object sender, PaintEventArgs e) =>
				{


					e.Graphics.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
				};

			var t = new Timer();

			t.Interval = 1;
			t.Tick +=
				delegate
				{
					shift++;
					Plasma.shiftPlasma(shift);

					var buffer = Plasma.newPlasma;

					var data = bitmap.LockBits(
						new Rectangle(0, 0, bitmap.Width, bitmap.Height),
						System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb
					);


					for (int i = 0; i < buffer.Length; i++)
					{
						Marshal.WriteInt32(data.Scan0, i * 4, unchecked((int)buffer[i]));
					}



					bitmap.UnlockBits(data);

					f.Invalidate();
				};

			t.Start();

			Application.Run(f);
		}
	}
}
