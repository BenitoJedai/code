using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Reflection;
using System.Reflection;

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

		public static Type GetTypeFromValue(object x)
		{
			object i = (__RuntimeTypeHandle)java.lang.Object.getClass(x);

			return GetTypeFromHandle((RuntimeTypeHandle)i);
		}

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


		public MethodInfo[] GetMethods()
		{
			var a = this.TypeDescription.getMethods();
			var n = new MethodInfo[a.Length];

			for (int i = 0; i < a.Length; i++)
			{
				n[i] = (MethodInfo)(object)new __MethodInfo { InternalMethod = a[i] };

			}

			return n;
		}

		public MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			var a = GetMethods();

			var that = (Type)(object)this;

			for (int i = 0; i < a.Length; i++)
			{
				var IsStatic = (bindingAttr & BindingFlags.Static) == BindingFlags.Static;
				var IsInstance = (bindingAttr & BindingFlags.Instance) == BindingFlags.Instance;
				var DeclaredOnly = (bindingAttr & BindingFlags.DeclaredOnly) == BindingFlags.DeclaredOnly;
				if (!IsInstance)
					if (IsStatic)
					{
						if (!a[i].IsStatic)
						{
							a[i] = null;
						}
					}

				if (!IsStatic)
					if (IsInstance)
					{
						if (a[i].IsStatic)
						{
							a[i] = null;
						}
					}

				if (DeclaredOnly)
				{
					if (a[i] != null)
						if (!a[i].DeclaringType.Equals(that))
						{
							a[i] = null;
						}
				}
			}

			var c = 0;

			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != null)
					c++;
			}

			var m = new MethodInfo[c];

			c = 0;

			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != null)
				{
					m[c] = a[i];
					c++;
				}
			}


			return m;
		}

		public override Type DeclaringType
		{
			get { return null; }
		}

		public static implicit operator Type(__Type e)
		{
			return (Type)(object)e;
		}


		public static implicit operator __Type(java.lang.Class e)
		{
			object i = (__RuntimeTypeHandle)e;

			return GetTypeFromHandle((RuntimeTypeHandle)i);
		}

		public bool Equals(__Type e)
		{
			if (this.TypeDescription.isAssignableFrom(e.TypeDescription))
				return this.FullName == e.FullName;

			return false;
		}
	}
}
