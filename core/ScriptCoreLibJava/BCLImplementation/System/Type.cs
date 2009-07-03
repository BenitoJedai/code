using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Reflection;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Type))]
	internal class __Type : __MemberInfo
	{
		public static implicit operator __Type(Type e)
		{
			return (__Type)(object)e;
		}

		RuntimeTypeHandle _TypeHandle;

		public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
		{
			var e = new __Type
			{
				_TypeHandle = TypeHandle
			};

			return (Type)(object)e;
		}

		public string FullName
		{
			get
			{
				// fixme: should return .net styled names
				return InternalFullName;
			}
		}

		public java.lang.Class TypeDescription
		{
			get
			{
				var a = this._TypeHandle.Value;
				var b = (object)a;
				var c = (__IntPtr)b;

				return c.ClassToken;
			}
		}

		public string InternalFullName
		{
			get
			{
				var t = this.TypeDescription;
				var p = t.getPackage();

				if (p != null)
					return p.getName() + "." + t.getName();

				return this.TypeDescription.getName();
			}
		}

		public override string Name
		{
			get
			{
				var t = this.TypeDescription;
				var p = t.getPackage();
				var n = t.getName();
				
				if (p != null)
				{
					var x = p.getName();

					if (n.StartsWith(x))
						return n.Substring(x.Length + 1);

					return x;
				}

				return n;
			}
		}
	}
}
