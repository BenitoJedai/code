using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Alchemy;
using ScriptCoreLib.Alchemy.Headers;


namespace FlashPlasma.Alchemy
{
	using AS3_Val = AS3_h._AS3_Val;

	[Script]
	public static partial class Program
	{

		static int width;
		static int height;

		static uint[] palette;
		static uint[] plasma;
		static uint[] newPlasma;

		[Alchemy]
		static string echo()
		{
			return "alchemy via c# via jsc";
		}

		[Alchemy]
		static uint[] generatePlasma(int width, int height)
		{
			Program.width = width;
			Program.height = height;



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
		static uint[] shiftPlasma(int shift)
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
