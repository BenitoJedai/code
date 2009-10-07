using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
	[Script(IsNative = true, Header = "string.h", IsSystemHeader = true)]
	public static class string_h
	{
		public static int strlen(string e)
		{
			return default(int);
		}
	}
}
