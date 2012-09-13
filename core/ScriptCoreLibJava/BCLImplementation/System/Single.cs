using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Single),
		ImplementationType = typeof(java.lang.Float))]
    internal class __Single
	{
		[Script(ExternalTarget = "parseFloat")]
		public static float Parse(string e)
		{
            return default(float);
		}
	}
}
