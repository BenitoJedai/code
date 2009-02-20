using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Threading
{
	[Script(Implements = typeof(global::System.Threading.Thread))]
	internal class __Thread
	{
		public static void Sleep(int millisecondsTimeout)
		{
			Native.API.usleep(millisecondsTimeout * 1000);
		}
	}
}
