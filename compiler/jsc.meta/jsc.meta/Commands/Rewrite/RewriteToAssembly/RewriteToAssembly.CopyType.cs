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

				TypeBuilder OverrideDeclaringType,
				VirtualDictionary<Type, string> TypeRenameCache,
				VirtualDictionary<string, string> NameObfuscation,
				Func<Type, bool> ShouldCopyType,
				Func<string, string> FullNameFixup,
				Action<TypeBuilder> PostTypeRewrite,
				Action<TypeBuilder> PreTypeRewrite,

				Action<TypeBuilder> TypeCreated,

				RewriteToAssembly r,

				ILTranslationContext context
			)
		{


			Action<string> Diagnostics =
				e =>
				{
					Debug.WriteLine(e);

					Console.WriteLine(e);
				};


			// sanity check
			if (context.TypeCache.BaseDictionary.ContainsKey(SourceType))
				return;

			#region invalidmerge
			if (SourceType.GetCustomAttributes<ObfuscationAttribute>().Any(k => k.Feature == "invalidmerge"))
				throw new InvalidOperationException(SourceType.FullName);
			#endregion


			var t = (TypeBuilder)context.TypeDefinitionCache[SourceType]; ;
			context.TypeCache[SourceType] = t;

			foreach (var item in SourceType.GetCustomAttributes(false).Select(kk => kk.ToCustomAttributeBuilder()))
			{
				t.SetCustomAttribute(item(context));
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

				// can we get away with defs only?
				var km = context.TypeDefinitionCache[k];
			}


			#region define fields now! as they are actually what the type is all about!
			foreach (var f in SourceType.GetFields(
						BindingFlags.DeclaredOnly |
						BindingFlags.Public | BindingFlags.NonPublic |
						BindingFlags.Instance | BindingFlags.Static))
			{
				//Diagnostics("Field: " + SourceType.Name + "." + f.Name);

				var ff = context.FieldCache[f];
			}
			#endregion



			if (PreTypeRewrite != null)
				PreTypeRewrite(t);

			// including other nested types?
			// if we dont need these types we will waste them
			// if we need them later we are doomed! :)

			CopyTypeMembers(SourceType,  NameObfuscation, t, context);



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

		

			Action AtTypeCreated =
				delegate
				{
					// seems like base types better be completed...
					var BaseType = SourceType.BaseType == null ? null : context.TypeCache[SourceType.BaseType];

					var _Interfaces = Enumerable.ToArray(
						from k in SourceType.GetInterfaces()
						where ShouldCopyType(k) || k.IsPublic
						select context.TypeCache[k]
					).ToArray();

					// explicit interfaces?

					var __explicit =
						from i in SourceType.GetInterfaces()
						let map = SourceType.GetInterfaceMap(i)
						from j in Enumerable.Range(0, map.InterfaceMethods.Length)
						let TargetMethod = map.TargetMethods[j]
						let InterfaceMethod = map.InterfaceMethods[j]
						where TargetMethod.DeclaringType == SourceType
						where !TargetMethod.IsPublic || TargetMethod.Name != InterfaceMethod.Name
						select new { TargetMethod, InterfaceMethod };


					foreach (var item in __explicit)
					{
						t.DefineMethodOverride(context.MethodCache[item.TargetMethod], context.MethodCache[item.InterfaceMethod]);
					}

					// Method 'MoveNext' in type '<LoadReferencedAssemblies>d__0' from 
					// assembly '20100313_jsc.installer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
					// does not have an implementation.

					t.CreateType();
					context.TypeCache.Flags[SourceType] = new object();

					if (TypeCreated != null)
						TypeCreated(t);
				};

			if (SourceType.IsNested && SourceType.IsClass && !(context.TypeCache.Flags.ContainsKey(SourceType.DeclaringType)))
			{
				//Diagnostics("Delayed:  " + SourceType.FullName);


				r.TypeCreated +=
					tt =>
					{
						if (tt.SourceType != SourceType.DeclaringType)
							return;

						//Diagnostics("Delayed CreateType:  " + SourceType.FullName);

						AtTypeCreated();
					};


				return;
			}


			//Diagnostics("CreateType:  " + SourceType.FullName);

			AtTypeCreated();

		}

		public class CopyTypeDefinition
		{
			public VirtualDictionary<Type, Type> TypeDefinitionCache;
			public VirtualDictionary<Type, string> TypeRenameCache;
			public Type SourceType;
			public ModuleBuilder m;
			//public VirtualDictionary<Type, Type> TypeCache;
			public TypeBuilder OverrideDeclaringType;
			public VirtualDictionary<string, string> NameObfuscation;
			public Func<Type, bool> ShouldCopyType;
			public Func<string, string> FullNameFixup;
			public Action<string> Diagnostics;




			public TypeBuilder Invoke()
			{


				if (Diagnostics == null)
					Diagnostics =
					e =>
					{
						Debug.WriteLine(e);

						Console.WriteLine(e);
					};

				var TypeName =
					SourceType.IsNested ?
					TypeRenameCache[SourceType] ?? SourceType.Name :

					// http://msdn.microsoft.com/en-us/library/system.type.fullname.aspx
					// a null reference (Nothing in Visual Basic) if the current instance represents a 
					// generic type parameter, an array type, pointer type, or byref type based on a 
					// type parameter, or a generic type that is not a generic type definition
					// but contains unresolved type parameters.
					TypeRenameCache[SourceType] ?? SourceType.FullName;


				Diagnostics("CopyTypeDefinition: " +
					SourceType.FullName
				);

				// we should not reenter here!
				TypeDefinitionCache[SourceType] = null;

				// interfaces dont have base types!
				var BaseType = SourceType.BaseType == null ? null : TypeDefinitionCache[SourceType.BaseType];

				//var DeclaringTypeContinuation = default(Action);

				//if (SourceType.IsNested)
				//{
				//    Diagnostics("Should create " + SourceType.DeclaringType.Name + " before " + SourceType.Name);
				//}

				// We beed a separate TypeDeclarationCache for this to work:
				// Type { NestedType, Delegate1(NestedType) }

				var _DeclaringType = (OverrideDeclaringType ?? (
					SourceType.DeclaringType == null ? null :
						(TypeBuilder)TypeDefinitionCache[SourceType.DeclaringType]

					)
				);

				var t = default(TypeBuilder);


				var _Interfaces = Enumerable.ToArray(

					from k in SourceType.GetInterfaces()

					where ShouldCopyType(k) || k.IsPublic

					select TypeDefinitionCache[k]
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

				if (SourceType.IsGenericTypeDefinition)
				{
					var ga = SourceType.GetGenericArguments();
					var gp = t.DefineGenericParameters(ga.Select(k => k.Name).ToArray());

				}

				TypeDefinitionCache[SourceType] = t;

				//Diagnostics("TypeDefinitionCache: " + TypeName);

				return t;

			}

		}

		internal static void CopyTypeMembers(
			Type SourceType,
	
			VirtualDictionary<string, string> NameObfuscation,
			TypeBuilder t,
			ILTranslationContext context
			)
		{





			foreach (var k in SourceType.GetConstructors(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{

				var km = context.ConstructorCache[k];
			}


			foreach (var k in SourceType.GetMethods(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var km = context.MethodCache[k];
			}

			foreach (var k in SourceType.GetProperties(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var km = context.PropertyCache[k];
				
			}


			foreach (var k in SourceType.GetEvents(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var EventName = NameObfuscation[k.Name];
				var kp = t.DefineEvent(EventName, k.Attributes, context.TypeCache[k.EventHandlerType]);

				var _AddMethod = k.GetAddMethod(true);
				if (_AddMethod != null)
					kp.SetAddOnMethod((MethodBuilder)context.MethodCache[_AddMethod]);

				var _GetRemoveMethod = k.GetRemoveMethod(true);
				if (_GetRemoveMethod != null)
					kp.SetRemoveOnMethod((MethodBuilder)context.MethodCache[_GetRemoveMethod]);


			}


		}




	}
}
