using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Alchemy.Headers;

namespace FlashPlasma.Alchemy
{
	using AS3_Val = AS3_h._AS3_Val;

	partial class Program
	{
		[Script(NoDecoration = true)]
		static AS3_Val echo(object self, AS3_Val args)
		{
			var nullString2 = "alchemy via c# via jsc";

			return AS3_h.AS3_String(nullString2);
		}
	}
}
