using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashAlchemyEcho.Alchemy.System;

namespace FlashAlchemyEcho.Alchemy
{
	using AS3_Val = AS3_h._AS3_Val;

	[Script]
	public static class Program
	{
		[Script(NoDecoration = true)]
		static AS3_Val echo(object self, AS3_Val args)
		{
			var nullString2 = "alchemy via c# via jsc";

			return AS3_h.AS3_String(nullString2);
		}

		[Script(NoDecoration = true)]
		static int main()
		{
			//define the methods exposed to ActionScript
			//typed as an ActionScript Function instance
			AS3_Val echoMethod = AS3_h.AS3_Function(null, echo);

			// construct an object that holds references to the functions
			AS3_Val result = AS3_h.AS3_Object("echo: AS3ValType", __arglist(echoMethod));

			// Release
			AS3_h.AS3_Release(echoMethod);

			// notify that we initialized -- THIS DOES NOT RETURN!
			AS3_h.AS3_LibInit(result);

			// should never get here!
			return 0;
		}
	}
}
