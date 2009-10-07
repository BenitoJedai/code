using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;

namespace ScriptCoreLibNative.BCLImplementation.System.Threading
{
	[Script(Implements = typeof(global::System.Threading.Thread))]
	internal class __Thread
	{
		public static void Sleep(int p)
		{
			windows_h.Sleep(p);
		}
	}
}
