using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Alchemy;

namespace FlashPlasma.SharedAlchemy
{
	[Script]
	public static class Plasma
	{
		static int width;
		static int height;

		static uint[] palette;
		static uint[] plasma;
		public static uint[] newPlasma;

		[Alchemy]
		public static string echo()
		{
			return "alchemy via c# via jsc";
		}

		[Alchemy]
		public static uint[] generatePlasma(int width, int height)
		{
			Plasma.width = width;
			Plasma.height = height;



			palette = new uint[256];
			plasma = new uint[width * height];
			newPlasma = new uint[width * height];

			for (var x = 0; x < 256; x++)
			{
				var b = (int)(128.0 + 128 * Math.Sin(Math.PI * x / 16.0));
				var g = (int)(128.0 + 128 * Math.Sin(Math.PI * x / 128.0));
				var r = 0;

				uint color = (uint)(r << 16 | g << 8 | b);

				color |= 0xff000000u;

				palette[x] = color;
			}

			int index = 0;

			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					uint color = (uint)((
						128.0 + (128.0 * Math.Sin(x / 16.0)) +
						128.0 + (128.0 * Math.Sin(y / 8.0)) +
						128.0 + (128.0 * Math.Sin((x + y) / 16.0)) +
						128.0 + (128.0 * Math.Sin(Math.Sqrt(x * x + y * y) / 8.0))
					) / 4);


					color |= 0xff000000u;

					plasma[index++] = color;
				}
			}

			return plasma;
		}



		[Alchemy]
		public static uint[] shiftPlasma(int shift)
		{
			var index = 0;


			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					var paletteIndex = (int)((uint)(plasma[index] + shift) % 256);
					newPlasma[index] = palette[paletteIndex];
					index++;
				}
			}

			return newPlasma;
		}
	}
}
