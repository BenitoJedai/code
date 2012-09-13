﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MandelbrotFormsControl.Library
{
	public static class MandelbrotCore
	{
		// alcemy does not seem to work with type definitions yet?

		static int _max = 30;
		static int _escape = 20;

		public static double rmin = -.75;
		public static double rmax = -.46;
		public static double imin = -.65;
		public static double imax = -.50;

		public static int InitializeMandelbrotCore()
		{
			_max = 30;
			_escape = 20;


			rmin = -.75;
			rmax = -.46;
			imin = -.65;
			imax = -.50;




			return 0;
		}

		public static void DrawMandelbrotSet(int[] bitmap, double rmin, double rmax,
							double imin, double imax, int width, int height)
		{
			// http://www.eggheadcafe.com/tutorials/aspnet/05748429-75a4-449a-9aab-82758cfb13df/animating-mandelbrot-frac.aspx


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
							var index = (y * width) + x;
							var value = ((int)Math.Pow(count + 1, 5) % 0xFFFFFF);
							//bitmap[index] = (int)(value | 0xFF000000u);
							bitmap[index] = value;
							break;
						}
						zi = ci + (2.0 * zr * zi);
						zr = cr + zr2 - zi2;
						count++;
					}

					if (count == _max)
					{
						var index = (y * width) + x;
						bitmap[index] = 0; // Black
					}
				}
			}


		}
	}
}
