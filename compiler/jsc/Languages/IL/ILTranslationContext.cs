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
		public VirtualDictionary<Type, string> TypeRenameCache;
		public VirtualDictionary<Type, Type> TypeDefinitionCache;
		public VirtualDictionary<Type, Type> TypeCache;
		public VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache;
		public VirtualDictionary<MethodInfo, MethodInfo> MethodCache;
		public VirtualDictionary<FieldInfo, FieldInfo> FieldCache;


		/// <summary>
		/// Changes to the context shall be disposed after using it.
		/// </summary>
		/// <returns></returns>
		public IDisposable ToTransientTransaction()
		{
			return (Disposable)
				new VirtualDictionaryBase[]{
					TypeRenameCache,
					TypeDefinitionCache,
					TypeCache,
					ConstructorCache,
					MethodCache,
					FieldCache
				}.Select(k => k.ToTransientTransaction()).ToArray()
			;
		}
	}
}
