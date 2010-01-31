using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.Library;
using jsc;

namespace jsc.meta.Commands.Rewrite
{
	public partial class RewriteToAssembly
	{
		internal static void CopyType(
		   Type source,
		   AssemblyBuilder a,
		   ModuleBuilder m,
		   VirtualDictionary<Type, Type> TypeCache,
		   VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache,
		   VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
		   VirtualDictionary<MethodInfo, MethodInfo> MethodCache,
		   TypeBuilder OverrideDeclaringType,
		   VirtualDictionary<string, string> NameObfuscation,
		   Func<Type, bool> ShouldCopyType,
			Func<string, string> FullNameFixup,
			Action<TypeBuilder> PostTypeRewrite)
		{
			var BaseType = TypeCache[source.BaseType];
			var _DeclaringType = (OverrideDeclaringType ?? (

				source.DeclaringType == null ? null :
					(TypeBuilder)TypeCache[source.DeclaringType]

				)
			);

			// sanity check
			if (TypeCache.BaseDictionary.ContainsKey(source))
				return;

			var t = default(TypeBuilder);

			var _Interfaces = source.GetInterfaces().Where(k => k.IsPublic || ShouldCopyType(k)).Select(k => TypeCache[k]).ToArray();

			// we might define as a nested type instead!
			if (source.IsNested)
			{

				var _NestedTypeName = NameObfuscation[source.Name];



				if (source.StructLayoutAttribute.Size > 0)
				{
					t = _DeclaringType.DefineNestedType(
						_NestedTypeName,
						source.Attributes,
						 BaseType,
						source.StructLayoutAttribute.Size
					);
				}
				else
				{
					t = _DeclaringType.DefineNestedType(

						_NestedTypeName,
						source.Attributes,
						BaseType,
						_Interfaces
					);
				}
			}
			else
			{
				t = m.DefineType(
					FullNameFixup(source.FullName),
					source.Attributes,
					BaseType,
					_Interfaces
				);

			}

			// should we copy attributes? should they be opt-out?
			var TypeAttributes = source.GetCustomAttributes(false);

			foreach (var item in TypeAttributes)
			{
				// call a callback?
				t.DefineAttribute(item, item.GetType());
			}

			TypeCache[source] = t;

			CopyTypeMembers(source, TypeCache, TypeFieldCache, ConstructorCache, MethodCache, NameObfuscation, t);

			if (PostTypeRewrite != null)
				PostTypeRewrite(t);

			t.CreateType();
		}

		internal static void CopyTypeMembers(Type source, VirtualDictionary<Type, Type> TypeCache, VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache, VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache, VirtualDictionary<MethodInfo, MethodInfo> MethodCache, VirtualDictionary<string, string> NameObfuscation, TypeBuilder t)
		{
			foreach (var f in source.GetFields(
						BindingFlags.DeclaredOnly |
						BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				// if the datastruct is actually pointing to
				// a initialized data in .sdata
				// then we have to redefine it in our version
				// for some reason we cannot just copy this bit in current API

				var FieldName = NameObfuscation[f.Name];

				if (f.FieldType.StructLayoutAttribute != null && f.FieldType.StructLayoutAttribute.Size > 0)
				{
					var ff = t.DefineInitializedData(FieldName, f.GetValue(null).StructAsByteArray(), f.Attributes);

					TypeFieldCache[source].Add(ff);
				}
				else
				{
					var ff = t.DefineField(FieldName, TypeCache[f.FieldType], f.Attributes);



					//ff.setd
					//var ff3 = t.DefineInitializedData(f.Name + "___", 100, f.Attributes);

					TypeFieldCache[source].Add(ff);
				}
			}




			foreach (var k in source.GetConstructors(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var km = ConstructorCache[k];
			}


			foreach (var k in source.GetMethods(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var km = MethodCache[k];
			}

			foreach (var k in source.GetProperties(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var PropertyName = NameObfuscation[k.Name];

				var _SetMethod = k.GetSetMethod(true);
				var _GetMethod = k.GetGetMethod(true);

				var kp = t.DefineProperty(PropertyName, k.Attributes, TypeCache[k.PropertyType], null);

				if (_SetMethod != null)
					kp.SetSetMethod((MethodBuilder)MethodCache[_SetMethod]);

				if (_GetMethod != null)
					kp.SetGetMethod((MethodBuilder)MethodCache[_GetMethod]);

			}


			foreach (var k in source.GetEvents(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var EventName = NameObfuscation[k.Name];
				var kp = t.DefineEvent(EventName, k.Attributes, TypeCache[k.EventHandlerType]);

				var _AddMethod = k.GetAddMethod(true);
				if (_AddMethod != null)
					kp.SetAddOnMethod((MethodBuilder)MethodCache[_AddMethod]);

				var _GetRemoveMethod = k.GetRemoveMethod(true);
				if (_GetRemoveMethod != null)
					kp.SetRemoveOnMethod((MethodBuilder)MethodCache[_GetRemoveMethod]);


			}



		}




	}
}
