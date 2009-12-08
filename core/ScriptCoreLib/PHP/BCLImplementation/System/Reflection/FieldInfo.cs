using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Reflection
{
	[Script(Implements = typeof(global::System.Reflection.FieldInfo))]
	internal class __FieldInfo : __MemberInfo
	{
		public Type InternalDeclaringType;

		public override Type DeclaringType
		{
			get { return InternalDeclaringType; }
		}

		public string InternalName;

		public override string Name
		{
			get { return InternalName; }
		}

		[Script(OptimizedCode = @"return $o->{$i};")]
		internal static object GetArrayMember(object o, object i)
		{
			return default(object);
		}

		[Script(OptimizedCode = @"$o->{$i} = $v;")]
		internal static void SetArrayMember([ScriptParameterByRef] object o, object i, object v)
		{
		}


		public object GetValue(object obj)
		{
			return GetArrayMember(obj, this.InternalName);
		}

		public void SetValue(object obj, object value)
		{
			SetArrayMember(obj, this.InternalName, value);
		}
	}
}
