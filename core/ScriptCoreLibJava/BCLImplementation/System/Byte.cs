using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Byte)
		, ImplementationType = typeof(java.lang.Byte)
		)]
	internal class __Byte
	{
		[Script(ExternalTarget = "parseByte")]
		public static byte Parse(string e)
		{
			return default(byte);
		}

		[Script(DefineAsStatic = true)]
		public string ToString(string format)
		{
			if (format != "x2")
				throw new NotImplementedException();

			return ToHexString((byte)(object)this);
		}

		public static string ToHexString(byte e)
		{
			const string u = "0123456789abcdef";

			return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
		}
	}
}
