﻿using System;
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
		   Type SourceType,
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
			Action<TypeBuilder> PostTypeRewrite,
			Action<TypeBuilder> PreTypeRewrite,
			Action<Action> ContextContinuation
			)
		{
			if (ContextContinuation == null)
				ContextContinuation = e => e();


			// sanity check
			if (TypeCache.BaseDictionary.ContainsKey(SourceType))
				return;

	

			Console.WriteLine("CopyType: " + SourceType.FullName);

			// we should not reenter here!
			TypeCache[SourceType] = null;

			// interfaces dont have base types!
			var BaseType = SourceType.BaseType == null ? null : TypeCache[SourceType.BaseType];

			var DeclaringTypeContinuation = default(Action);

			var _DeclaringType = (OverrideDeclaringType ?? (

				SourceType.DeclaringType == null ? null :
					(TypeBuilder)TypeCache[SourceType.DeclaringType, n => DeclaringTypeContinuation = n]

				)
			);



			var t = default(TypeBuilder);

			var _Interfaces = SourceType.GetInterfaces() /*.Where(k => ShouldCopyType(k)) */ .Select(
				k => TypeCache[k]
				//k => k
			).ToArray();



			// we might define as a nested type instead!
			#region DefineType
			if (SourceType.IsNested)
			{

				var _NestedTypeName = NameObfuscation[SourceType.Name];



				if (SourceType.StructLayoutAttribute != null && SourceType.StructLayoutAttribute.Size > 0)
				{
					t = _DeclaringType.DefineNestedType(
						_NestedTypeName,
						SourceType.Attributes,
						 BaseType,
						SourceType.StructLayoutAttribute.Size
					);
				}
				else
				{
					t = _DeclaringType.DefineNestedType(

						_NestedTypeName,
						SourceType.Attributes,
						BaseType,
						_Interfaces
					);
				}
			}
			else
			{
				t = m.DefineType(
					FullNameFixup(SourceType.FullName),
					SourceType.Attributes,
					BaseType,
					_Interfaces
				);

			}
			#endregion


			// should we copy attributes? should they be opt-out?
			var TypeAttributes = SourceType.GetCustomAttributes(false);

			foreach (var item in TypeAttributes)
			{
				// for now we cannot copy ctor attributes / nonoba branch knows how...
				if (item.GetType().GetConstructor() == null)
					continue;

				// call a callback?
				t.DefineAttribute(item, item.GetType());
			}

			TypeCache[SourceType] = t;
			
			// at this point we should signal back? that a nested declaration can continue?
			// does everything still work after this change? :D

			#region define fields now! as they are actually what the type is all about!
			foreach (var f in SourceType.GetFields(
						BindingFlags.DeclaredOnly |
						BindingFlags.Public | BindingFlags.NonPublic |
						BindingFlags.Instance | BindingFlags.Static))
			{
				// if the datastruct is actually pointing to
				// a initialized data in .sdata
				// then we have to redefine it in our version
				// for some reason we cannot just copy this bit in current API

				var FieldName = NameObfuscation[f.Name];

				if (f.FieldType.StructLayoutAttribute != null && f.FieldType.StructLayoutAttribute.Size > 0)
				{
					var ff = t.DefineInitializedData(FieldName, f.GetValue(null).StructAsByteArray(), f.Attributes);

					TypeFieldCache[SourceType].Add(ff);
				}
				else
				{
					var ff = t.DefineField(FieldName, TypeCache[f.FieldType], f.Attributes);



					//ff.setd
					//var ff3 = t.DefineInitializedData(f.Name + "___", 100, f.Attributes);

					TypeFieldCache[SourceType].Add(ff);
				}
			}
			#endregion


			ContextContinuation(
				delegate
				{
					Console.WriteLine("ContextContinuation:  " + SourceType.FullName);

					if (PreTypeRewrite != null)
						PreTypeRewrite(t);

					// including other nested types?
					// if we dont need these types we will waste them
					// if we need them later we are doomed! :)

					CopyTypeMembers(SourceType, TypeCache, TypeFieldCache, ConstructorCache, MethodCache, NameObfuscation, t);



					if (PostTypeRewrite != null)
						PostTypeRewrite(t);

					// http://msdn.microsoft.com/en-us/library/system.reflection.emit.typebuilder.createtype.aspx

					Console.WriteLine("CreateType:  " + SourceType.FullName);

					// maybe we should call create type once we are sure there
					// are no more nested types?
					// actually if the members refer to the nested type
					// they have been declared by now...
					// more testing is needed to clarify this!
					t.CreateType();

					if (DeclaringTypeContinuation != null)
					{
						Console.WriteLine("DeclaringTypeContinuation:  " + SourceType.FullName);

						DeclaringTypeContinuation();
					}
				}
			);
		}

		internal static void CopyTypeMembers(
			Type SourceType,
			VirtualDictionary<Type, Type> TypeCache,
			VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache,
			VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
			VirtualDictionary<MethodInfo, MethodInfo> MethodCache,
			VirtualDictionary<string, string> NameObfuscation,
			TypeBuilder t)
		{
			




			foreach (var k in SourceType.GetConstructors(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var km = ConstructorCache[k];
			}


			foreach (var k in SourceType.GetMethods(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var km = MethodCache[k];
			}

			foreach (var k in SourceType.GetProperties(
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


			foreach (var k in SourceType.GetEvents(
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
