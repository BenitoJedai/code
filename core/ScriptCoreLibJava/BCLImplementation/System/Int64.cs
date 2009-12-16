using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Int64),
		ImplementationType = typeof(java.lang.Long))]
	internal class __Int64
	{
		[Script(ExternalTarget = "parseLong")]
		public static long Parse(string e)
		{
			return default(long);
		}
	}
}
