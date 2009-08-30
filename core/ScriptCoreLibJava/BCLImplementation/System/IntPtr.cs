using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.lang.reflect;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.IntPtr))]
	internal class __IntPtr
	{
		//public string StringToken;
		//public Function FunctionToken;
		public java.lang.Class ClassToken;



		//public static explicit operator __IntPtr(string _Token)
		//{
		//    return new __IntPtr { StringToken = _Token };
		//}

		//public static explicit operator __IntPtr(Function _Token)
		//{
		//    return new __IntPtr { FunctionToken = _Token };
		//}

		//public static explicit operator string(__IntPtr _ptr)
		//{
		//    return _ptr.StringToken;
		//}

		//public static explicit operator Function(__IntPtr _ptr)
		//{
		//    return _ptr.FunctionToken;
		//}

	
		public java.lang.reflect.Method MethodToken;

		public static __IntPtr Of(java.lang.Class Target, string MethodName, java.lang.Class[] Parameters)
		{
			var MethodToken = default(Method);
			var Methods = Target.getDeclaredMethods();

			foreach (var m in Methods)
			{
				if (MethodToken != null)
					break;

				if (m.getName() == MethodName)
				{

					var p = m.getParameterTypes();

					if (p.Length == Parameters.Length)
					{
						MethodToken = m;

						for (int i = 0; i < Parameters.Length; i++)
						{
							// name by name comparision... might not be that great!
							if (Parameters[i].getName() != p[i].getName())
							{
								MethodToken = null;
								break;
							}
						}
					}
				}
			}

			// should we worry about return type overloads too?
			// this wont work in applet viewer!
			// MethodToken.setAccessible(true);

			return new __IntPtr { MethodToken = MethodToken };
		}

	}
}
