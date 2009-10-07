using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
	[Script(IsNative = true, Header = "assert.h", IsSystemHeader = true)]
	public static class assert_h
	{
		public static void assert(int expression) { }
	}
}
