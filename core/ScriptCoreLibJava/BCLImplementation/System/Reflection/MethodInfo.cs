using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
	[Script(Implements = typeof(MethodInfo))]
	internal class __MethodInfo : __MethodBase
	{

		public override string Name
		{
			get { return InternalMethod.getName(); }
		}

		public override Type DeclaringType
		{
			get
			{
				var h = (__RuntimeTypeHandle)InternalMethod.getDeclaringClass();

				return __Type.GetTypeFromHandle((RuntimeTypeHandle)(object)h);
			}
		}

		public override ParameterInfo[] GetParameters()
		{
			var a = this.InternalMethod.getParameterTypes();
			var n = new ParameterInfo[a.Length];

			for (int i = 0; i < a.Length; i++)
			{
				n[i] = new __ParameterInfo
				{
					ParameterType = (__Type)a[i],
					Position = i
				};
			}

			return n;
		}
	}
}
