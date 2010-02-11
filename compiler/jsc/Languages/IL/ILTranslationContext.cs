using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jsc.Library;
using System.Reflection;
using System.Reflection.Emit;

namespace jsc.Languages.IL
{
	public class ILTranslationContext
	{
		public VirtualDictionary<Type, Type> TypeCache;
		public VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache;
		public VirtualDictionary<MethodInfo, MethodInfo> MethodCache;
		public VirtualDictionary<FieldInfo, FieldInfo> FieldCache;
	}
}
