using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Diagnostics
{
	[Script(Implements = typeof(global::System.Diagnostics.Trace))]
	internal class __Trace
	{
		public static void Assert(bool condition)
		{
			if (condition)
				return;

			throw new Exception("Assert failed");
		}
	}
}
