using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Alchemy.Headers;

namespace FlashPlasma.Alchemy
{
	using AS3_Val = AS3_h._AS3_Val;

	[Script]
	public static  partial class Program
	{

		[global::System.Runtime.CompilerServices.CompilerGenerated]
		static AS3_Val generatePlasma(object self, AS3_Val args)
		{
			int width;
			int height;
			AS3_h.AS3_ArrayValue(args, "IntType, IntType", __arglist(out width, out height));
			var value = generatePlasma(width, height);
			width = default(int);
			height = default(int);
			return AS3_h.AS3_Ptr(value);
		}

		[global::System.Runtime.CompilerServices.CompilerGenerated]
		static AS3_Val shiftPlasma(object self, AS3_Val args)
		{
			int shift;
			AS3_h.AS3_ArrayValue(args, "IntType", __arglist(out shift));
			var value = shiftPlasma(shift);
			shift = default(int);
			return AS3_h.AS3_Ptr(value);
		}

		[global::System.Runtime.CompilerServices.CompilerGenerated]
		[Script(NoDecoration = true)]
		static int main()
		{




			//define the methods exposed to ActionScript
			//typed as an ActionScript Function instance
			var echoMethod = AS3_h.AS3_Function(null, echo);
			var generatePlasmaMethod = AS3_h.AS3_Function(null, generatePlasma);
			var shiftPlasmaMethod = AS3_h.AS3_Function(null, shiftPlasma);

			// construct an object that holds references to the functions
			var result = AS3_h.AS3_Object("echo: AS3ValType,generatePlasma: AS3ValType,shiftPlasma: AS3ValType",
				__arglist(echoMethod, generatePlasmaMethod, shiftPlasmaMethod)
			);

			// Release
			AS3_h.AS3_Release(echoMethod);
			AS3_h.AS3_Release(generatePlasmaMethod);
			AS3_h.AS3_Release(shiftPlasmaMethod);

			// notify that we initialized -- THIS DOES NOT RETURN!
			AS3_h.AS3_LibInit(result);

			// should never get here!
			return 0;
		}
	}
}
