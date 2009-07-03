using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using java.lang.reflect;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
	[Script(Implements = typeof(MethodBase))]
	internal abstract class __MethodBase : __MemberInfo
	{
		public java.lang.reflect.Method InternalMethod;

		public bool IsStatic { get { return Modifier.isStatic(InternalMethod.getModifiers()); } }
		public bool IsPublic { get { return Modifier.isPublic(InternalMethod.getModifiers()); } }

		public object Invoke(object obj, object[] parameters)
		{
			var n = default(object);

			try
			{
				n = this.InternalMethod.invoke(obj, parameters);
			}
			catch (csharp.ThrowableException e)
			{
				throw new csharp.RuntimeException(e.Message);
			}

			return n;
		}

		public abstract ParameterInfo[] GetParameters();
		
	}
}
