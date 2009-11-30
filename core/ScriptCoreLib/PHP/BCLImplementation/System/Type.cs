using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Type))]
	internal class __Type
	{
		public static implicit operator __Type(Type e)
		{
			return (__Type)(object)e;
		}

		RuntimeTypeHandle _TypeHandle;

		public static Type GetTypeFromValue(object x)
		{
			//object i = (__RuntimeTypeHandle)java.lang.Object.getClass(x);

			//return GetTypeFromHandle((RuntimeTypeHandle)i);

			return null;
		}

		public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
		{
			var e = new __Type
			{
				_TypeHandle = TypeHandle
			};

			return (Type)(object)e;
		}
	}
}
