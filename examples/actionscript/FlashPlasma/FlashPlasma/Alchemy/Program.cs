using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashPlasma.Alchemy.System;

namespace FlashPlasma.Alchemy
{
	using AS3_Val = AS3_h._AS3_Val;

	[Script]
	public static  partial class Program
	{
	

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
