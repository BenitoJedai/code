using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.UInt32)
		, ImplementationType = typeof(java.lang.Integer)
		)]
	internal class __UInt32
	{
		[Script(ExternalTarget = "parseInt")]
		public static uint Parse(string e)
		{
			return default(uint);
		}
	}
}
