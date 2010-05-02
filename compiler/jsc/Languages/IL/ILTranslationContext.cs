using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jsc.Library;
using System.Reflection;
using System.Reflection.Emit;
using ScriptCoreLib.Ultra.Library;

namespace jsc.Languages.IL
{
	public class ILTranslationContext
	{
		public VirtualDictionary<Type, TypeBuilder> OverrideDeclaringType = new VirtualDictionary<Type, TypeBuilder>();

		public VirtualDictionary<MethodInfo, MethodAttributes> MethodAttributesCache = new VirtualDictionary<MethodInfo, MethodAttributes>();
		public VirtualDictionary<MemberInfo, string> MemberRenameCache = new VirtualDictionary<MemberInfo, string>();

		public VirtualDictionary<Type, string> TypeRenameCache = new VirtualDictionary<Type, string>();
		public VirtualDictionary<Type, Type> TypeDefinitionCache = new VirtualDictionary<Type, Type>();
		public VirtualDictionary<Type, Type> TypeCache = new VirtualDictionary<Type, Type>();
		public VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache = new VirtualDictionary<ConstructorInfo, ConstructorInfo>();
		public VirtualDictionary<MethodInfo, MethodInfo> MethodCache = new VirtualDictionary<MethodInfo, MethodInfo>();
		public VirtualDictionary<FieldInfo, FieldInfo> FieldCache = new VirtualDictionary<FieldInfo, FieldInfo>();
		public VirtualDictionary<PropertyInfo, PropertyInfo> PropertyCache = new VirtualDictionary<PropertyInfo, PropertyInfo>();



		/// <summary>
		/// Changes to the context shall be disposed after using it.
		/// 
		/// Be careful using this method! Any referenced types which are not meant to be transient shall be prefetched.
		/// </summary>
		/// <returns></returns>
		public IDisposable ToTransientTransaction()
		{
			return (Disposable)
				new VirtualDictionaryBase[]{
					OverrideDeclaringType,
					MethodAttributesCache,
					MemberRenameCache,
					TypeRenameCache,
					TypeDefinitionCache,
					TypeCache,
					ConstructorCache,
					MethodCache,
					FieldCache,
					PropertyCache
				}.Select(k => k.ToTransientTransaction()).ToArray()
			;
		}
	}
}
