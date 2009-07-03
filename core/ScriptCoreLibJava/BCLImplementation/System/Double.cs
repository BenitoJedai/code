using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Double),
		ImplementationType = typeof(java.lang.Double))]
	internal class __Double
	{
		[Script(ExternalTarget = "parseDouble")]
		public static double Parse(string e)
		{
			return default(double);
		}
	}
}
