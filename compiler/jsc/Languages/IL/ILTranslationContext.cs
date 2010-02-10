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

		/// <summary>
		/// For some reason we need to track ourself what fields have we we 
		/// created.
		/// </summary>
		public VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache;

		public FieldInfo TypeFieldCacheFunc(FieldInfo TargetField)
		{
			return ToTypeFieldCacheFunc(TypeCache)(TargetField);
		}

		public Func<FieldInfo, FieldInfo> ToTypeFieldCacheFunc(Func<Type, Type> TypeCacheFunc)
		{
			return TargetField => this.TypeFieldCache[TypeCacheFunc(TargetField.DeclaringType)].SingleOrDefault(k => k.Name == TargetField.Name) ?? TargetField;
		}

		public VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache;
		public VirtualDictionary<MethodInfo, MethodInfo> MethodCache;
	}
}
