using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
	[Script(Implements = typeof(FieldInfo))]
	internal class __FieldInfo : __MemberInfo
	{
		public java.lang.reflect.Field InternalField;

		public override string Name
		{
			get { return InternalField.getName(); }
		}

		public override Type DeclaringType
		{
			get
			{
				return (__Type)InternalField.getDeclaringClass();
			}
		}

		public Type FieldType
		{
			get
			{
				return (__Type)InternalField.getType();
			}
		}

		public object GetValue(object obj)
		{
			var n = default(object);
			try
			{
				n = InternalField.get(obj);
			}
			catch
			{
				throw new csharp.RuntimeException();
			}
			return n;
		}

		public void SetValue(object obj, object value)
		{
			try
			{
				InternalField.set(obj, value);
			}
			catch
			{
				throw new csharp.RuntimeException();
			}
		}
	}
}
