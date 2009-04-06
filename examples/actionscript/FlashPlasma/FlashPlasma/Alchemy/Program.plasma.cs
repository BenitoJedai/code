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

		static int[] palette;
		static int[] plasma;
		static int[] newPlasma;


		[Script(NoDecoration = true)]
		static AS3_Val generatePlasma(object self, AS3_Val args)
		{
			int x, y, r, g, b, index = 0;

			AS3_h.AS3_ArrayValue(args, "IntType, IntType", __arglist(ref width, ref height));

			palette = new int[256];
			plasma = new int[width * height];
			newPlasma = new int[width * height];

			for (x = 0; x < 256; x++)
			{
				r = (int)(128.0 + 128 * math_h.sin(Math.PI * x / 16.0));
				g = (int)(128.0 + 128 * math_h.sin(Math.PI * x / 128.0));
				b = 0;
				palette[x] = 0xff << 24 | r << 16 | g << 8 | b;
			}

			for (x = 0; x < width; x++)
			{
				for (y = 0; y < height; y++)
				{
					int color = (int)((
						128.0 + (128.0 * math_h.sin(x / 16.0)) +
						128.0 + (128.0 * math_h.sin(y / 8.0)) +
						128.0 + (128.0 * math_h.sin((x + y) / 16.0)) +
						128.0 + (128.0 * math_h.sin(math_h.sqrt(x * x + y * y) / 8.0))
					) / 4);
					plasma[index++] = 0xff << 24 | color;
				}
			}


			return AS3_h.AS3_Ptr(plasma);
		}

		[Script(NoDecoration = true)]
		static AS3_Val shiftPlasma(object self, AS3_Val args)
		{
			int shift = 0, x, y, index = 0, paletteIndex;
			AS3_h.AS3_ArrayValue(args, "IntType", __arglist( &shift));

			for (x = 0; x < width; x++)
			{
				for (y = 0; y < height; y++)
				{
					paletteIndex = (int)( (uint)(plasma[index] + shift) % 256);
					newPlasma[index] = palette[paletteIndex];
					index++;
				}
			}

			return AS3_h.AS3_Ptr(newPlasma);
		}
	}
}
