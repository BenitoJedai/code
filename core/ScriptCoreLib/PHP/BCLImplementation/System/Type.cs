using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib.PHP.BCLImplementation.System.Reflection;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Type))]
	internal class __Type : __MemberInfo
	{
		public static class API
		{
			#region string gettype ( mixed var )

			/// <summary>  
			/// Returns the type of the PHP variable var.   
			/// </summary>  
			/// <param name="_var">mixed var</param>  
			[Script(IsNative = true)]
			public static string gettype(object _var) { return default(string); }

			#endregion

			[Script(IsNative = true)]
			public static ScriptCoreLib.PHP.Runtime.IArray get_class_vars(string n) { return default(ScriptCoreLib.PHP.Runtime.IArray); }

			[Script(IsNative = true)]
			public static string get_parent_class(object e) { return default(string); }

			[Script(IsNative = true)]
			public static string get_class(object e) { return default(string); }


		}

		public static implicit operator __Type(Type e)
		{
			return (__Type)(object)e;
		}

		RuntimeTypeHandle _TypeHandle;

		public static Type GetTypeFromValue(object x)
		{
			return GetTypeFromHandle((RuntimeTypeHandle)(object)(__RuntimeTypeHandle)API.get_class(x));
		}

		public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
		{
			var e = new __Type
			{
				_TypeHandle = TypeHandle
			};

			return (Type)(object)e;
		}

		public FieldInfo[] GetFields()
		{
			var a = new List<FieldInfo>();

			var ClassTokenName = ((__IntPtr)(object)(this._TypeHandle.Value)).ClassTokenName;

			var f = (string[])ScriptCoreLib.PHP.Runtime.IArray.API.array_keys(API.get_class_vars(ClassTokenName));

			foreach (var k in f)
			{
				var n = new __FieldInfo
				{
					InternalDeclaringType = (Type)(object)this,
					InternalName = k
				};

				a.Add((FieldInfo)(object)n);
			}

			return a.ToArray();
		}

		public override Type DeclaringType
		{
			get { throw new NotImplementedException("DeclaringType"); }
		}

		public override string Name
		{
			get { throw new NotImplementedException("Name"); }
		}

		public Type BaseType
		{
			get
			{
				var ClassTokenName = ((__IntPtr)(object)(this._TypeHandle.Value)).ClassTokenName;

				return GetTypeFromHandle((RuntimeTypeHandle)(object)(__RuntimeTypeHandle)API.get_parent_class(ClassTokenName));
			}
		}
	}
}
