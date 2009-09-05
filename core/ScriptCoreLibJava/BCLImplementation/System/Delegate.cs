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
		// jsc will currently look for a field with specific type...
		public object Target { get; set; }
		public MethodInfo Method { get; set; }

		public __Delegate(object e, global::System.IntPtr p)
		{
			this.Target = e;
			this.Method = new __MethodInfo { InternalMethod = ((__IntPtr)(object)p).MethodToken };
		}


		public static __Delegate Combine(__Delegate a, __Delegate b)
		{
			if (a == null)
			{
				return b;
			}
			if (b == null)
			{
				return a;
			}

			return a.CombineImpl(b);
		}

		protected virtual __Delegate CombineImpl(__Delegate d)
		{
			return default(__Delegate);
		}

		public static __Delegate Remove(__Delegate source, __Delegate value)
		{
			if (source == null)
			{
				return null;
			}
			if (value == null)
			{
				return source;
			}
			return source.RemoveImpl(value);
		}

		protected virtual __Delegate RemoveImpl(__Delegate d)
		{
			return default(__Delegate);
		}

		public virtual __Delegate[] GetInvocationList()
		{
			return default(__Delegate[]);
		}
	}
}
