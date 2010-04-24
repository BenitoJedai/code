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

			foreach (var item in
				from kk in SourceType.GetCustomAttributes(false)
				let aa = kk.ToCustomAttributeBuilder()(context)
				where aa != null
				select aa
				)
			{
				t.SetCustomAttribute(item);
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

			CopyTypeMembers(SourceType, NameObfuscation, t, context);



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

					if (SourceType.IsInterface)
					{
						// System.ArgumentException: 'this' type cannot be an interface itself.
					}
					else
					{
						var __explicit =
							from i in SourceType.GetInterfaces()
							let map = SourceType.GetInterfaceMap(i)
							from j in Enumerable.Range(0, map.InterfaceMethods.Length)
							let TargetMethod = map.TargetMethods[j]
							
							// abstract class with interfaces?
							where TargetMethod != null

							let InterfaceMethod = map.InterfaceMethods[j]
							where TargetMethod.DeclaringType == SourceType
							where !TargetMethod.IsPublic || TargetMethod.Name != InterfaceMethod.Name
							select new { TargetMethod, InterfaceMethod };


						foreach (var item in __explicit)
						{
							t.DefineMethodOverride(context.MethodCache[item.TargetMethod], context.MethodCache[item.InterfaceMethod]);
						}
					}

					// Method 'MoveNext' in type '<LoadReferencedAssemblies>d__0' from 
					// assembly '20100313_jsc.installer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
					// does not have an implementation.

					t.CreateType();
					context.TypeCache.Flags[SourceType] = new object();

					if (TypeCreated != null)
						TypeCreated(t);
				};


			var AtTypeCreatedFilter = new List<Type>();

			if (SourceType.IsNested && SourceType.IsClass)
			{
				//Diagnostics("Delayed:  " + SourceType.FullName);

				AtTypeCreatedFilter.Add(SourceType.DeclaringType);
			}

			if (SourceType.IsGenericTypeDefinition)
				AtTypeCreatedFilter.AddRange(
					SourceType.GetGenericArguments().SelectMany(k => k.GetGenericParameterConstraints()).Where(ShouldCopyType)
				);



			//Diagnostics("CreateType:  " + SourceType.FullName);

			if (AtTypeCreatedFilter.Any(k => !(context.TypeCache.Flags.ContainsKey(k))))
			{
				r.TypeCreated +=
					tt =>
					{
						if (AtTypeCreatedFilter == null)
							return;

						if (AtTypeCreatedFilter.Any(k => !(context.TypeCache.Flags.ContainsKey(k))))
							return;

						//Diagnostics("Delayed CreateType:  " + SourceType.FullName);

						AtTypeCreatedFilter = null;
						AtTypeCreated();
					};
			}
			else
			{
				AtTypeCreated();
			}

		}

		public class CopyTypeDefinition
		{
			public ILTranslationContext context;

			public Type SourceType;
			public ModuleBuilder m;
			//public VirtualDictionary<Type, Type> TypeCache;
			//public TypeBuilder OverrideDeclaringType;
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
						if (Debugger.IsAttached)
						{
							Debug.WriteLine(e);
						}
						else
						{
							Console.WriteLine(e);
						}
					};

				//Diagnostics("CopyTypeDefinition: " +
				//    SourceType.FullName
				//);

				// we should not reenter here!
				context.TypeDefinitionCache[SourceType] = null;

				Diagnostics("CopyTypeDefinition: " +
					SourceType.FullName + " @ " + context.TypeDefinitionCache.BaseDictionary.Keys.Count.ToString("x8")
				);


				var _DeclaringType = (context.OverrideDeclaringType[SourceType] ?? (
					SourceType.DeclaringType == null ? null :
						(TypeBuilder)context.TypeDefinitionCache[SourceType.DeclaringType]

					)
				);

				var TypeName =
					_DeclaringType != null ?
					context.TypeRenameCache[SourceType] ?? SourceType.Name :

					// http://msdn.microsoft.com/en-us/library/system.type.fullname.aspx
					// a null reference (Nothing in Visual Basic) if the current instance represents a 
					// generic type parameter, an array type, pointer type, or byref type based on a 
					// type parameter, or a generic type that is not a generic type definition
					// but contains unresolved type parameters.
					context.TypeRenameCache[SourceType] ?? SourceType.FullName;



				// interfaces dont have base types!
				var BaseType = SourceType.BaseType == null ? null : context.TypeDefinitionCache[SourceType.BaseType];

				//var DeclaringTypeContinuation = default(Action);

				//if (SourceType.IsNested)
				//{
				//    Diagnostics("Should create " + SourceType.DeclaringType.Name + " before " + SourceType.Name);
				//}

				// We beed a separate TypeDeclarationCache for this to work:
				// Type { NestedType, Delegate1(NestedType) }




				var t = default(TypeBuilder);


				var _Interfaces = Enumerable.ToArray(

					from k in SourceType.GetInterfaces()

					where ShouldCopyType(k) || k.IsPublic

					select context.TypeDefinitionCache[k]
				).ToArray();


				var TypeAttributes = SourceType.Attributes;

				TypeAttributes &= ~TypeAttributes.HasSecurity;

				// http://msdn.microsoft.com/en-us/library/system.reflection.typeattributes.aspx
				//                Bad type attributes. Reserved bits set on the type.
				//Public | BeforeFieldInit | HasSecurity



				// we might define as a nested type instead!
				try
				{
					DefineType(_DeclaringType, TypeName, BaseType, ref t, _Interfaces, ref TypeAttributes);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(TypeName, ex);
				}

				context.TypeDefinitionCache[SourceType] = t;

				if (SourceType.IsGenericTypeDefinition)
				{
					var ga = SourceType.GetGenericArguments();

					var gp = t.DefineGenericParameters(ga.Select(k => k.Name).ToArray());

					for (int i = 0; i < gp.Length; i++)
					{
						// http://msdn.microsoft.com/en-us/library/system.reflection.emit.generictypeparameterbuilder(v=VS.95).aspx

						foreach (var item in ga[i].GetGenericParameterConstraints())
						{
							if (item.IsInterface)
								gp[i].SetInterfaceConstraints(context.TypeDefinitionCache[item]);
							else
								gp[i].SetBaseTypeConstraint(context.TypeDefinitionCache[item]);
						}
					}
				}


				//Diagnostics("TypeDefinitionCache: " + TypeName);

				return t;

			}

			private void DefineType(TypeBuilder _DeclaringType, string TypeName, Type BaseType, ref TypeBuilder t, Type[] _Interfaces, ref TypeAttributes TypeAttributes)
			{
				#region DefineType
				if (_DeclaringType != null)
				{

					var _NestedTypeName = NameObfuscation[TypeName];


					//TypeAttributes = ReplaceTypeAttributes(TypeAttributes, TypeAttributes.NotPublic, TypeAttributes.NestedFamORAssem);
					TypeAttributes = ReplaceTypeAttributes(TypeAttributes, TypeAttributes.Public, TypeAttributes.NestedPublic);


					if (SourceType.StructLayoutAttribute != null && SourceType.StructLayoutAttribute.Size > 0)
					{
						t = _DeclaringType.DefineNestedType(
							_NestedTypeName,
							TypeAttributes,
							 BaseType,
							SourceType.StructLayoutAttribute.Size
						);
					}
					else
					{

						t = _DeclaringType.DefineNestedType(

							_NestedTypeName,
							TypeAttributes,
							BaseType,
							_Interfaces
						);
					}
				}
				else
				{
					var DefineTypeName = FullNameFixup(TypeName);

					Func<IEnumerable<Type>> GetDuplicates =
						() => context.TypeDefinitionCache.BaseDictionary.Values.Where(k => k != null).Where(k => (k.Namespace + "." + k.Name) == DefineTypeName);


					while (GetDuplicates().Any())
					{

						if (SourceType.IsAnonymousType())
						{
							DefineTypeName += "´";
						}
						else
						{
							// have been merging types twice ?

							// C = A + B
							// D = C + B

							var Conflicts = GetDuplicates().Concat(new[] { SourceType }).Select(k => k.Assembly.ToString()).ToArray();


							throw new InvalidOperationException(
								"Duplicate type name within an assembly.  "
								+ "Multiple projects shall reference one version of a component.  "
								+ SourceType.ToString()
								+ " at "
								+ string.Join(", ", Conflicts)
							);
						}
					}

					// "Object reference not set to an instance of an object."
					//    at System.Reflection.Emit.SignatureHelper.AddOneArgTypeHelperWorker(Type clsArgument, Boolean lastWasGenericInst)

					// Duplicate type name within an assembly.
					t = m.DefineType(
						DefineTypeName,
						TypeAttributes,
						BaseType,
						_Interfaces
					);



				}
				#endregion
			}

			private static TypeAttributes ReplaceTypeAttributes(TypeAttributes TypeAttributes, TypeAttributes _From, TypeAttributes _To)
			{
				if ((TypeAttributes & _From) == _From)
					TypeAttributes = (TypeAttributes & ~_From) | _To;
				return TypeAttributes;
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
