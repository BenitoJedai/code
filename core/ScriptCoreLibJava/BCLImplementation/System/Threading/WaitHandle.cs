using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
	[Script(Implements = typeof(global::System.Threading.WaitHandle))]
	internal class __WaitHandle
	{
		public virtual bool WaitOne()
		{
			return false;
		}
	}
}
