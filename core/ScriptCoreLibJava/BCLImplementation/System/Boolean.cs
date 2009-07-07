using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Boolean),
		ImplementationType = typeof(java.lang.Boolean))]
	internal class __Boolean
	{
		[Script(ExternalTarget = "parseBoolean")]
		public static bool Parse(string e)
		{
			return java.lang.Boolean.parseBoolean(e);
		}
	}

}
