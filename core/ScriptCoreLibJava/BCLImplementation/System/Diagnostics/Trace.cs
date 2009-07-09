using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Diagnostics
{
	[Script(Implements = typeof(global::System.Diagnostics.Trace))]
	internal class __Trace
	{
		public static void Assert(bool condition)
		{
			// at this time we provide a convinient method to throw an exception
			// exceptions must be reworked at later releases of jsc

			if (condition)
				return;

			throw new csharp.RuntimeException("Assert failed");
		}
	}
}
