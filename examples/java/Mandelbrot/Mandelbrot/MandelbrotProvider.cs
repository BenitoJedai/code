using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mandelbrot
{
	public static class MandelbrotProvider
	{
		public const int DefaultWidth = 320;
		public const int DefaultHeight = 200;

		static int _max = 30;
		static int _escape = 20;

		static double rmin = -.75;
		static double rmax = -.46;
		static double imin = -.65;
		static double imax = -.50;

		// javascript wont support uint at this time

		static int[] bitmap = new int[DefaultWidth * DefaultHeight];

		public static int[] DrawMandelbrotSet(int shift)
		{
			var rmin = MandelbrotProvider.rmin - .002 * (double)shift;
			var rmax = MandelbrotProvider.rmax + .002 * (double)shift;
			var imin = MandelbrotProvider.imin - .002 * (double)shift;
			var imax = MandelbrotProvider.imax + .002 * (double)shift;


			DrawMandelbrotSet(rmin, rmax, imin, imax, DefaultWidth, DefaultHeight);

			return bitmap;
		}

		static void DrawMandelbrotSet(double rmin, double rmax,
							double imin, double imax, int width, int height)
		{
			// http://www.eggheadcafe.com/tutorials/aspnet/05748429-75a4-449a-9aab-82758cfb13df/animating-mandelbrot-frac.aspx

			// uncomment next line and 3 lines at bottom to see rendering time in output window
			//  DateTime start = DateTime.Now;
			// Silverlight 2 - you can use Joe Stegner's WriteableBitmap class separately

			double dr = (rmax - rmin) / (width - 1);
			double di = (imax - imin) / (height - 1);

			for (int x = 0; x < width; x++)
			{
				double cr = rmin + (x * dr);
				for (int y = 0; y < height; y++)
				{
					double ci = imin + (y * di);
					double zr = cr;
					double zi = ci;
					int count = 0;

					while (count < _max)
					{
						double zr2 = zr * zr;
						double zi2 = zi * zi;

						if (zr2 + zi2 > _escape)
						{
							bitmap[(y * width) + x] = (int)Math.Pow(count + 1, 5) % int.MaxValue;
							break;
						}
						zi = ci + (2.0 * zr * zi);
						zr = cr + zr2 - zi2;
						count++;
					}

					if (count == _max)
						bitmap[(y * width) + x] = 0; // Black
				}
			}


			//   DateTime end = DateTime.Now;
			//  TimeSpan elapsed = end - start;
			// System.Diagnostics.Debug.WriteLine( elapsed.TotalMilliseconds.ToString() );
		}
	}
}
