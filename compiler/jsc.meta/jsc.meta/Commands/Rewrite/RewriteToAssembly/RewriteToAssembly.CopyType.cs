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
using System.Diagnostics;

namespace jsc.meta.Commands.Rewrite
{
	public partial class RewriteToAssembly
	{
		internal static void CopyType(
				Type SourceType,
				AssemblyBuilder a,
				ModuleBuilder m,
				VirtualDictionary<Type, Type> TypeDefinitionCache,
				VirtualDictionary<Type, Type> TypeCache,
				VirtualDictionary<FieldInfo, FieldInfo> FieldCache,
				VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
				VirtualDictionary<MethodInfo, MethodInfo> MethodCache,
				TypeBuilder OverrideDeclaringType,
				VirtualDictionary<Type, string> TypeRenameCache,
				VirtualDictionary<string, string> NameObfuscation,
				Func<Type, bool> ShouldCopyType,
				Func<string, string> FullNameFixup,
				Action<TypeBuilder> PostTypeRewrite,
				Action<TypeBuilder> PreTypeRewrite,

				Action<TypeBuilder> TypeCreated,

				RewriteToAssembly r
			)
		{
			Action<string> Diagnostics =
				e =>
				{
					Debug.WriteLine(e);

					Console.WriteLine(e);
				};


			// sanity check
			if (TypeCache.BaseDictionary.ContainsKey(SourceType))
				return;

			if (SourceType.GetCustomAttributes<ObfuscationAttribute>().Any(k => k.Feature == "invalidmerge"))
				throw new InvalidOperationException(SourceType.FullName);

			var TypeName = SourceType.IsNested ? TypeRenameCache[SourceType] ?? SourceType.Name :
				TypeRenameCache[SourceType] ?? SourceType.FullName;



			Diagnostics("CopyType: " + TypeName);

			var t = CopyTypeDefinition(SourceType, m, TypeCache, OverrideDeclaringType, NameObfuscation, ShouldCopyType, FullNameFixup, Diagnostics, TypeName);

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

			// at this point we should signal back? that a nested declaration can continue?
			// does everything still work after this change? :D


			foreach (var k in SourceType.GetNestedTypes(
				BindingFlags.Public | BindingFlags.NonPublic
				))
			{
				// just like try/catch initialized data fields are special....

				if (k.IsInitializedDataFieldType())
					continue;

				var km = TypeCache[k];
			}


			#region define fields now! as they are actually what the type is all about!
			foreach (var f in SourceType.GetFields(
						BindingFlags.DeclaredOnly |
						BindingFlags.Public | BindingFlags.NonPublic |
						BindingFlags.Instance | BindingFlags.Static))
			{
				Diagnostics("Field: " + SourceType.Name + "." + f.Name);

				var ff = FieldCache[f];
			}
			#endregion


			//ContextContinuation(
			//    delegate
			//    {
			//Console.WriteLine("ContextContinuation:  " + SourceType.FullName);

			if (PreTypeRewrite != null)
				PreTypeRewrite(t);

			// including other nested types?
			// if we dont need these types we will waste them
			// if we need them later we are doomed! :)

			CopyTypeMembers(SourceType, TypeCache, FieldCache, ConstructorCache, MethodCache, NameObfuscation, t);



			if (PostTypeRewrite != null)
				PostTypeRewrite(t);

			// http://msdn.microsoft.com/en-us/library/system.reflection.emit.typebuilder.createtype.aspx


			// maybe we should call create type once we are sure there
			// are no more nested types?
			// actually if the members refer to the nested type
			// they have been declared by now...
			// more testing is needed to clarify this!

			// fixme:D
			// if we rewrite nested interfaces we cannot 
			// implement them?

			//foreach (var i in SourceType.GetInterfaces().Select(ii => SourceType.GetInterfaceMap(ii)))
			//{

			//}

			//if (DeclaringTypeContinuation != null)
			//{
			//    Console.WriteLine("DeclaringTypeContinuation:  " + SourceType.FullName);

			//    DeclaringTypeContinuation();
			//}


			if (SourceType.IsNested && SourceType.IsClass && !(TypeCache.Flags.ContainsKey(SourceType.DeclaringType)))
			{
				Diagnostics("Delayed:  " + SourceType.FullName);


				r.TypeCreated +=
					tt =>
					{
						if (tt.SourceType != SourceType.DeclaringType)
							return;

						Diagnostics("Delayed CreateType:  " + SourceType.FullName);

						// How is it possible?
						// "Duplicate definition for runtime implemented delegate method"
						t.CreateType();

						if (TypeCreated != null)
							TypeCreated(t);
					};


				return;
			}




			t.CreateType();
			TypeCache.Flags[SourceType] = new object();
			Diagnostics("CreateType:  " + SourceType.FullName);

			if (TypeCreated != null)
				TypeCreated(t);


			//    }
			//);
		}

		private static TypeBuilder CopyTypeDefinition(
			Type SourceType, 
			ModuleBuilder m, 
			VirtualDictionary<Type, Type> TypeCache, 
			TypeBuilder OverrideDeclaringType, 
			VirtualDictionary<string, string> NameObfuscation, 
			Func<Type, bool> ShouldCopyType, 
			Func<string, string> FullNameFixup, 
			Action<string> Diagnostics, 
			string TypeName)
		{
			// we should not reenter here!
			TypeCache[SourceType] = null;

			// interfaces dont have base types!
			var BaseType = SourceType.BaseType == null ? null : TypeCache[SourceType.BaseType];

			//var DeclaringTypeContinuation = default(Action);

			if (SourceType.IsNested)
			{
				Diagnostics("Should create " + SourceType.DeclaringType.Name + " before " + SourceType.Name);
			}

			// We beed a separate TypeDeclarationCache for this to work:
			// Type { NestedType, Delegate1(NestedType) }

			var _DeclaringType = (OverrideDeclaringType ?? (
				SourceType.DeclaringType == null ? null :
					(TypeBuilder)TypeCache[SourceType.DeclaringType /*, n => DeclaringTypeContinuation = n*/]

				)
			);

			var t = default(TypeBuilder);

			var _Interfaces = Enumerable.ToArray(

				from k in SourceType.GetInterfaces()

				where ShouldCopyType(k) || k.IsPublic

				select TypeCache[k]
			).ToArray();



			// we might define as a nested type instead!
			#region DefineType
			if (SourceType.IsNested)
			{

				var _NestedTypeName = NameObfuscation[TypeName];



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
					FullNameFixup(TypeName),
					SourceType.Attributes,
					BaseType,
					_Interfaces
				);

			}
			#endregion


			TypeCache[SourceType] = t;
			return t;
		}

		internal static void CopyTypeMembers(
			Type SourceType,
			VirtualDictionary<Type, Type> TypeCache,
			VirtualDictionary<FieldInfo, FieldInfo> FieldCache,
			VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
			VirtualDictionary<MethodInfo, MethodInfo> MethodCache,
			VirtualDictionary<string, string> NameObfuscation,
			TypeBuilder t
			)
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
