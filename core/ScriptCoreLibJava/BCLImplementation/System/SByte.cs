using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.SByte)
		, ImplementationType = typeof(java.lang.Byte)
		)]
	internal class __SByte
	{
		[Script(ExternalTarget = "parseByte")]
		public static sbyte Parse(string e)
		{
			return default(sbyte);
		}
	}
}
