using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using jni;

namespace ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared
{
	[Script(Implements = typeof(global::ScriptCoreLib.Shared.PlatformInvocationServices))]
	internal class __PlatformInvocationServices
	{
		[Script]
		public class Func
		{
			public readonly string DllName;
			public readonly string EntryPoint;
			public Func(string DllName, string EntryPoint)
			{
				this.DllName = DllName;
				this.EntryPoint = EntryPoint;
			}

			CFunc _Method;
				 
			public CFunc Method
			{
				get
				{
					if (_Method == null)
						_Method = new CFunc(this.DllName, this.EntryPoint);

					return _Method;
				}
			}
		}

		[Script]
		public class Int32Func : Func
		{
			public Int32Func(string DllName, string EntryPoint) : base(DllName, EntryPoint)
			{
			}

			public int Invoke(object[] e)
			{
				return this.Method.callInt(e);
			}
		}

		// we should cache the func pointers somehow!
		public static int InvokeInt32(string DllName, string EntryPoint, object[] e)
		{
			var f = new Int32Func(DllName, EntryPoint);

			return f.Invoke(e);
		}
	}
}
