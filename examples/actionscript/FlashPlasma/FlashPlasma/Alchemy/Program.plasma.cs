using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashPlasma.Alchemy.System;

namespace FlashPlasma.Alchemy
{
	using AS3_Val = AS3_h._AS3_Val;

	unsafe partial class Program
	{
		static int width;
		static int height;

		static uint[] palette;
		static uint[] plasma;
		static uint[] newPlasma;


		static AS3_Val generatePlasma(object self, AS3_Val args)
		{
			AS3_h.AS3_ArrayValue(args, "IntType, IntType", __arglist(ref width, ref height));

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
						128.0 + (128.0 * Math.Sin(math_h.sqrt(x * x + y * y) / 8.0))
					) / 4);


					color |= 0xff000000u;

					plasma[index++] = color;
				}
			}


			return AS3_h.AS3_Ptr(plasma);
		}

		static AS3_Val shiftPlasma(object self, AS3_Val args)
		{
			var shift = 0;
			var index = 0;

			AS3_h.AS3_ArrayValue(args, "IntType", __arglist( ref shift));

			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					var paletteIndex = (int)( (uint)(plasma[index] + shift) % 256);
					newPlasma[index] = palette[paletteIndex];
					index++;
				}
			}

			return AS3_h.AS3_Ptr(newPlasma);
		}
	}
}
