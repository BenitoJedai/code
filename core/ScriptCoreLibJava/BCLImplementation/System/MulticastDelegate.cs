using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.MulticastDelegate))]
	internal class __MulticastDelegate : __Delegate
	{
		public __MulticastDelegate(object e, global::System.IntPtr p)
			: base(e, p)
		{
		}
	}
}
