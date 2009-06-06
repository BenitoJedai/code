using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Int32)
		// native type cast conflict: ,ExternalTarget="java.lang.Integer"
		, ImplementationType = typeof(java.lang.Integer)
		)]
	internal class __Int32
	{
		[Script(ExternalTarget = "parseInt")]
		public static int Parse(string e)
		{
			return default(int);
		}
	}
}
