using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mandelbrot.Core;

namespace Mandelbrot
{
	public static class MandelbrotProvider
	{
		// // alcemy does not seem to work with type definitions yet?

		//public const int DefaultWidth = 320;
		//public const int DefaultHeight = 200;

		public const int DefaultWidth = 128;
		public const int DefaultHeight = 128;
		// javascript wont support uint at this time
		// javascript needs power of 2 ?

		static int[] bitmap = new int[DefaultWidth * DefaultHeight];

		public static int[] DrawMandelbrotSet(int shift)
		{
			var rmin = MandelbrotCore.rmin - .002 * (double)shift;
			var rmax = MandelbrotCore.rmax + .002 * (double)shift;
			var imin = MandelbrotCore.imin - .002 * (double)shift;
			var imax = MandelbrotCore.imax + .002 * (double)shift;

			MandelbrotCore.DrawMandelbrotSet(bitmap, rmin, rmax, imin, imax, DefaultWidth, DefaultHeight);

			return bitmap;
		}

		static MandelbrotProvider()
		{

		}

	}
}
