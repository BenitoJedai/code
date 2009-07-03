using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Reflection;
using System.Reflection;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Delegate))]
	internal abstract class __Delegate
	{
		public object Target;
		public MethodInfo Method;

		public __Delegate(object e, global::System.IntPtr p)
		{
			this.Target = e;
			this.Method = new __MethodInfo { InternalMethod = ((__IntPtr)(object)p).MethodToken };
		}

	
	}
}
