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
		public VirtualDictionary<Type, TypeBuilder> OverrideDeclaringType;

		public VirtualDictionary<MethodInfo, MethodAttributes> MethodAttributesCache;
		public VirtualDictionary<MemberInfo, string> MemberRenameCache;
		public VirtualDictionary<Type, string> TypeRenameCache;
		public VirtualDictionary<Type, Type> TypeDefinitionCache;
		public VirtualDictionary<Type, Type> TypeCache;
		public VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache;
		public VirtualDictionary<MethodInfo, MethodInfo> MethodCache;
		public VirtualDictionary<FieldInfo, FieldInfo> FieldCache;
		public VirtualDictionary<PropertyInfo, PropertyInfo> PropertyCache;


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
