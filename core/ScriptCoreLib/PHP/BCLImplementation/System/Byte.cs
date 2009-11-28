using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Byte))]
    internal class __Byte
    {
		public static __Byte Parse(string e)
        {
			return (__Byte)((object)Native.API.intval(e));
        }

		[Script(DefineAsStatic = true)]
		public string ToString(string format)
		{
			if (format != "x2")
				throw new NotImplementedException("format");

			return ToHexString((byte)(object)this);
		}

		public static string ToHexString(byte e)
		{
			const string u = "0123456789abcdef";

			return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
		}
    }
}
