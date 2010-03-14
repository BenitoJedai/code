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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.CodeDom.Compiler;

namespace jsc.meta.Commands.Rewrite
{
	public partial class RewriteToAssembly
	{


		public void Invoke()
		{
			if (this.AttachDebugger)
				Debugger.Launch();

			#region ExternalContext defaults...


			this.ExternalContext.TypeCache.Resolve +=
				source =>
				{
					if (this.ExternalContext.TypeCache.BaseDictionary.ContainsKey(source))
						return;

					this.ExternalContext.TypeCache[source] = source;
				};

			this.ExternalContext.MethodCache.Resolve +=
				source =>
				{
					if (this.ExternalContext.MethodCache.BaseDictionary.ContainsKey(source))
						return;


					this.ExternalContext.MethodCache[source] = source;
				};

			this.ExternalContext.ConstructorCache.Resolve +=
				source =>
				{
					if (this.ExternalContext.ConstructorCache.BaseDictionary.ContainsKey(source))
						return;


					this.ExternalContext.ConstructorCache[source] = source;
				};

			#endregion


			var NameObfuscationRandom = new Random();

			#region NameObfuscation
			NameObfuscation.Resolve +=
				n =>
				{
					if (!this.obfuscate)
					{
						NameObfuscation[n] = n;

						return;
					}

					// see: http://www.cumps.be/obfuscation-making-reverse-engineering-harder/
					// see: http://www.codesqueeze.com/careless-obfuscation-can-lose-you-business/

					// should we add salt?
					var salt_length = NameObfuscationRandom.Next(7);

					var ObfuscatedName = new StringBuilder();

					ObfuscatedName.Append((char)(0xFEFC - NameObfuscation.BaseDictionary.Count));

					for (int i = 0; i < salt_length; i++)
					{
						ObfuscatedName.Append((char)(0xFEFC - NameObfuscationRandom.Next(0x1000)));
					}

					NameObfuscation[n] = ObfuscatedName.ToString();
				};
			#endregion



			if (this.assembly == null)
				this.staging = this.staging.CreateTemp();
			else
				this.staging = this.staging.Create(() => this.assembly.Directory.CreateSubdirectory("staging"));


			var assembly = this.assembly == null ? null : Assembly.LoadFile(this.assembly.FullName);
			//var assembly = this.assembly.LoadAssemblyAt(staging);
			_assembly = assembly;

			// load the rest of the references
			// maybe we shouldnt load those references which will be merged?
			if (assembly != null)
				assembly.LoadReferencesAt(staging, this.assembly.Directory);

			// AssemblyMerge will copy resources too... getting crowded!
			Action<AssemblyBuilder, ModuleBuilder> InvokeLater = delegate { };

			if (this.Output != null)
			{
				this.product = Path.GetFileNameWithoutExtension(this.Output.Name);
				this.productExtension = this.Output.Extension;
			}

			var Product_Name = (string.IsNullOrEmpty(this.product) ?
					this.assembly.Name + "Rewrite" :
					this.product);


			if (this.PrimaryTypes.Length == 0)
			{
				this.PrimaryTypes = this.AssemblyMerge.SelectMany(
					k =>
					{
						var shadow = Path.Combine(this.staging.FullName, Path.GetFileName(k.name));

						var loaded = File.Exists(shadow);

						if (!loaded)
							File.Copy(
								k.name,
								shadow
							);

						var shadow_assembly = Assembly.LoadFile(shadow);

						if (!loaded)
							shadow_assembly.LoadReferencesAt(staging, new DirectoryInfo(Path.GetDirectoryName(k.name)));

						InvokeLater +=
							(__a, __m) =>
							{
								// should we copy attributes? should they be opt-out?

								foreach (var item in shadow_assembly.GetCustomAttributes(false).Select(kk => kk.ToCustomAttributeBuilder()))
								{
									__a.SetCustomAttribute(item(this.RewriteArguments.context));
								}


								foreach (var item in shadow_assembly.GetManifestResourceNames())
								{
									var n = item;

									if (n.StartsWith(shadow_assembly.GetName().Name))
										n = Product_Name + n.Substring(shadow_assembly.GetName().Name.Length);

									__m.DefineManifestResource(
										n,
										shadow_assembly.GetManifestResourceStream(item), ResourceAttributes.Public
									);

								}
							};


						return shadow_assembly.GetTypes();
					}
				).ToArray();
			}

			if (this.PrimaryTypes.Length == 0)
				if (assembly != null)
					this.PrimaryTypes =
						(string.IsNullOrEmpty(this.type) ?
							(assembly.EntryPoint == null ? assembly.GetTypes() : new[] { assembly.EntryPoint.DeclaringType }) :
								new[] { assembly.GetType(this.type) }
						);



			var Product_Extension = this.assembly == null ? productExtension : this.assembly.Extension;

			var Product = new FileInfo(Path.Combine(staging.FullName, Product_Name + Product_Extension));

			// we might want to use temp path instead and later figure out if we are replacing input...
			var OutputUndefined = this.Output == null;

			if (OutputUndefined)
				this.Output = Product;

			var name = new AssemblyName(Path.GetFileNameWithoutExtension(Product.Name));

			if (OutputUndefined)
			{
				// we probably did not load the same file... and we can easly remove it!
				if (Product.Exists)
					Product.Delete();
			}


			var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave, staging.FullName);
			var m = a.DefineDynamicModule(Path.GetFileNameWithoutExtension(Product.Name),
				// Unable to add resource to transient module or transient assembly.
				OutputUndefined ? Product.Name : "~" + Product.Name

			);



			if (assembly != null)
				foreach (var ka in assembly.GetCustomAttributes<ObfuscationAttribute>())
				{
					a.DefineAttribute<ObfuscationAttribute>(ka);
				}

			var TypeDefinitionCache = new VirtualDictionary<Type, Type>();
			var TypeCache = new VirtualDictionary<Type, Type>();
			var TypeRenameCache = new VirtualDictionary<Type, string>();
			var FieldCache = new VirtualDictionary<FieldInfo, FieldInfo>();

			var ConstructorCache = new VirtualDictionary<ConstructorInfo, ConstructorInfo>();
			var MethodCache = new VirtualDictionary<MethodInfo, MethodInfo>();
			var PropertyCache = new VirtualDictionary<PropertyInfo, PropertyInfo>();


			this.RewriteArguments = new AssemblyRewriteArguments
			{
				Assembly = a,
				Module = m,

				context =
					new ILTranslationContext
					{

						ConstructorCache = ConstructorCache,
						MethodCache = MethodCache,
						TypeDefinitionCache = TypeDefinitionCache,
						TypeCache = TypeCache,
						FieldCache = FieldCache,
						TypeRenameCache = TypeRenameCache,
						PropertyCache = PropertyCache
					}
			};

			#region PropertyCache
			PropertyCache.Resolve +=
				SourceProperty =>
				{
					var Source = TypeCache[SourceProperty.DeclaringType];

					if (Source is TypeBuilder)
					{
						var k = SourceProperty;
						var t = Source as TypeBuilder;

						var PropertyName = NameObfuscation[k.Name];

						var _SetMethod = k.GetSetMethod(true);
						var _GetMethod = k.GetGetMethod(true);

						var kp = t.DefineProperty(PropertyName, k.Attributes, TypeCache[k.PropertyType], null);

						if (_SetMethod != null)
							kp.SetSetMethod((MethodBuilder)MethodCache[_SetMethod]);

						if (_GetMethod != null)
							kp.SetGetMethod((MethodBuilder)MethodCache[_GetMethod]);

						PropertyCache[SourceProperty] = kp;

						return;
					}

					PropertyCache[SourceProperty] = Source.GetProperty(SourceProperty.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
				};
			#endregion

			#region ConstructorCache
			ConstructorCache.Resolve +=
				SourceConstructor =>
				{
					// This unit was resolved for us...
					if (ExternalContext.ConstructorCache[SourceConstructor] != SourceConstructor)
					{
						ConstructorCache[SourceConstructor] = ExternalContext.ConstructorCache[SourceConstructor];
						return;
					}

					//    L_0086: newobj instance void [System.Core]System.Func`2<class [ScriptCoreLib.Archive.ZIP]ScriptCoreLib.Archive.ZIP.ZIPFile/Entry, bool>::.ctor(object, native int)

					var source = SourceConstructor.DeclaringType;
					var Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;


					if (source.IsGenericType)
						if (!source.IsGenericTypeDefinition)
						{
							// are we rewriting any part of this generic type? if we are not we need just to pass it!

							var source_GenericTypeDefinition = source.GetGenericTypeDefinition();

							if (!ShouldCopyType(source_GenericTypeDefinition))
							{
								// the type itself is not copied. what about arguments passed in?

								if (!source.GetGenericArguments().Any(ShouldCopyType))
								{
									ConstructorCache[SourceConstructor] = SourceConstructor;
									return;
								}
							}

							var Def = source.GetGenericTypeDefinition().GetConstructors(Flags).Single(k => k.MetadataToken == SourceConstructor.MetadataToken);

							// Define it in the TypeBuilder
							var Def1 = ConstructorCache[Def];

							var ResolvedType1 = TypeDefinitionCache[source.GetGenericTypeDefinition()];

							var ResolvedType2 = ResolvedType1.MakeGenericType(
								TypeDefinitionCache[source.GetGenericArguments()]
							);

							// http://connect.microsoft.com/VisualStudio/feedback/details/97424/confused-typebuilder-getmethod-constructor
							// http://msdn.microsoft.com/en-us/library/ms145835.aspx

							//var Def2 = ResolvedType.GetMethods(Flags).Single(k => k.MetadataToken == msource.MetadataToken);


							// The specified method must be declared on the generic type definition of the specified type.
							// Parameter name: type
							var Def2 = default(ConstructorInfo);

							//ResolvedType1 is TypeBuilder || TypeDefinitionCache[source.GetGenericArguments()].Any(k => k is TypeBuilder) ?

							try
							{
								Def2 = TypeBuilder.GetConstructor(ResolvedType2, Def1);
							}
							catch
							{
								Def2 = ResolvedType2.GetConstructors(Flags).Single(k => k.MetadataToken == SourceConstructor.MetadataToken);
							}



							ConstructorCache[SourceConstructor] = Def2;
							return;
						}




					if (ShouldCopyType(SourceConstructor.DeclaringType))
					{
						var DeclaringType = (TypeBuilder)TypeCache[SourceConstructor.DeclaringType];

						if (ConstructorCache.BaseDictionary.ContainsKey(SourceConstructor))
							return;

						CopyConstructor(
							SourceConstructor,
							DeclaringType,
							NameObfuscation,
							AtILOverride,
							this.RewriteArguments.context
						);
						return;
					}


					ConstructorCache[SourceConstructor] = SourceConstructor;

				};
			#endregion


			#region TypeRenameCache
			TypeRenameCache.Resolve +=
				SourceType =>
				{
					if (TypeRenameCache.BaseDictionary.ContainsKey(SourceType))
						return;

					TypeRenameCache[SourceType] = default(string);
				};
			#endregion


			#region MethodCache
			MethodCache.Resolve +=
				msource =>
				{
					//Console.WriteLine("MethodCache: " + msource.ToString());

					// This unit was resolved for us...
					if (ExternalContext.MethodCache[msource] != msource)
					{
						MethodCache[msource] = ExternalContext.MethodCache[msource];
						return;
					}

					var source = msource.DeclaringType;
					var Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;


					if (source.IsGenericType)
						if (!source.IsGenericTypeDefinition)
						{
							var Def = source.GetGenericTypeDefinition().GetMethods(Flags).Single(k => k.MetadataToken == msource.MetadataToken);

							// Define it in the TypeBuilder
							var Def1 = MethodCache[Def];

							var ResolvedType1 = TypeDefinitionCache[source.GetGenericTypeDefinition()];

							var ResolvedType2 = ResolvedType1.MakeGenericType(
								TypeDefinitionCache[source.GetGenericArguments()]
							);

							// http://connect.microsoft.com/VisualStudio/feedback/details/97424/confused-typebuilder-getmethod-constructor
							// http://msdn.microsoft.com/en-us/library/ms145835.aspx

							//var Def2 = ResolvedType.GetMethods(Flags).Single(k => k.MetadataToken == msource.MetadataToken);


							// The specified method must be declared on the generic type definition of the specified type.
							// Parameter name: type
							var Def2 = default(MethodInfo);

							// ResolvedType1 is TypeBuilder || TypeDefinitionCache[source.GetGenericArguments()].Any(k => k is TypeBuilder) ?
							try
							{
								Def2 = TypeBuilder.GetMethod(ResolvedType2, Def1);
							}
							catch
							{
								Def2 = ResolvedType2.GetMethods(Flags).Single(k => k.MetadataToken == msource.MetadataToken);
							}

							var Def3 = Def2;

							if (msource.IsGenericMethod)
								Def3 = Def2.MakeGenericMethod(
								 TypeDefinitionCache[msource.GetGenericArguments()]
								 );

							MethodCache[msource] = Def3;
							return;
						}

					if (msource.IsGenericMethod)
						if (!msource.IsGenericMethodDefinition)
						{
							MethodCache[msource] = MethodCache[msource.GetGenericMethodDefinition()].MakeGenericMethod(
								 TypeDefinitionCache[msource.GetGenericArguments()]
							);

							return;
						}


					#region ShouldCopyType - CopyMethod
					if (ShouldCopyType(msource.DeclaringType))
					{
						var tb_source = (TypeBuilder)TypeCache[msource.DeclaringType.IsGenericType ? msource.DeclaringType.GetGenericTypeDefinition() : msource.DeclaringType];

						CopyMethod(
							a,
							m,
							msource,
							tb_source,
							NameObfuscation,
							_assembly,
							this.codeinjecton,
							this.codeinjectonparams,
							this.AtILOverride,

							(SourceMethod, Method, GetILGenerator) =>
							{
								if (this.BeforeInstructions != null)
									this.BeforeInstructions(
										 new BeforeInstructionsArguments
										 {
											 SourceType = msource.DeclaringType,
											 Type = tb_source,
											 Assembly = a,
											 Module = m,

											 SourceMethod = SourceMethod,
											 Method = Method,
											 GetILGenerator = GetILGenerator,

											 context = this.RewriteArguments.context
										 }
									);
							},
							this.RewriteArguments.context
						);
						return;
					}
					#endregion


					if (!msource.IsGenericMethodDefinition)
					{
						// do we need to redirect the type also?
						if (source.GetGenericArguments().Any(k => k != TypeCache[k]))
						{
							//var msource_gp = msource.GetGenericMethodDefinition().GetParameterTypes();


							var GenericArguments = TypeDefinitionCache[source.GetGenericArguments()];


							var GenericTypeDefinition = source.GetGenericTypeDefinition();
							var GenericTypeDefinition_GetGenericArguments = GenericTypeDefinition.GetGenericArguments();

							var GenericType = GenericTypeDefinition.MakeGenericType(GenericArguments);
							var ParameterTypes =


							msource.IsGenericMethod ? msource.GetGenericMethodDefinition().GetParameterTypes().Select(
								k =>
								{
									if (k.IsGenericTypeDefinition)
										return k;

									return TypeCache[k];
								}
								).ToArray() : TypeDefinitionCache[msource.GetParameterTypes()];

							ParameterTypes = ParameterTypes.Select(k =>
							{

								#region resolve Type Generics
								for (int iii = 0; iii < GenericArguments.Length; iii++)
								{
									if (GenericArguments[iii] == k)
										return GenericTypeDefinition_GetGenericArguments[iii];
								}
								#endregion



								return k;
							}).ToArray();
							// Type must be a type provided by the runtime.
							// Parameter name: types
							var GenericTypeDefinitionMethod = GenericTypeDefinition.GetMethod(msource.Name, Flags, null, ParameterTypes, null);

							var GenericTypeMethod = TypeBuilder.GetMethod(GenericType, GenericTypeDefinitionMethod);

							var GenericTypeMethod__ = msource.IsGenericMethod ? GenericTypeMethod.MakeGenericMethod(
								 TypeDefinitionCache[msource.GetGenericArguments()]
								 ) : GenericTypeMethod;

							MethodCache[msource] = GenericTypeMethod__;
						}
						else
						{
							MethodCache[msource] =
								msource.IsGenericMethod ?

								MethodCache[msource.GetGenericMethodDefinition()]

								.MakeGenericMethod(
									TypeDefinitionCache[msource.GetGenericArguments()]
								) : msource;
						}

						return;
					}

					MethodCache[msource] = msource;
				};
			#endregion



			#region FieldCache
			FieldCache.Resolve +=
				SourceField =>
				{
					// if the datastruct is actually pointing to
					// a initialized data in .sdata
					// then we have to redefine it in our version
					// for some reason we cannot just copy this bit in current API

					var DeclaringType_ = TypeCache[SourceField.DeclaringType];

					// Things may have changed... abort?
					if (FieldCache.BaseDictionary.ContainsKey(SourceField))
						return;

					var source = SourceField.DeclaringType;

					if (source.IsGenericType)
						if (!source.IsGenericTypeDefinition)
						{
							var source_GenericTypeDefinition = source.GetGenericTypeDefinition();

							if (!ShouldCopyType(source_GenericTypeDefinition))
							{
								// the type itself is not copied. what about arguments passed in?

								if (!source.GetGenericArguments().Any(ShouldCopyType))
								{
									FieldCache[SourceField] = SourceField;
									return;
								}
							}

							var ResolvedType1 = TypeDefinitionCache[source.GetGenericTypeDefinition()];

							var ResolvedType2 = ResolvedType1.MakeGenericType(
								TypeDefinitionCache[source.GetGenericArguments()]
							);

							//var Def0 = ResolvedType1.GetField(
							//    SourceField.Name,
							//    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
							//);


							var Def1 = TypeBuilder.GetField(ResolvedType2, FieldCache[source_GenericTypeDefinition.GetField(
								SourceField.Name,
								BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
								)]
							);

							//try
							//{
							// Message	"The specified field must be declared on the generic type definition 
							// of the specified type.\r\nParameter name: type"	string

							// Message	"The specified Type must not be a generic type definition.\r\nParameter name: type"	string http://msdn.microsoft.com/en-us/library/ms145828(VS.95).aspx

							//Def1 = TypeBuilder.GetField(ResolvedType2, Def0);
							//}
							//catch
							//{
							//    Def1 = ResolvedType2.GetField(Def0.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
							//}

							FieldCache[SourceField] = Def1;

							return;
						}

					if (DeclaringType_ is TypeBuilder)
					{
						var DeclaringType = (TypeBuilder)DeclaringType_;
						var FieldName = NameObfuscation[SourceField.Name];

						if (SourceField.FieldType.IsInitializedDataFieldType())
						{
							var value = SourceField.GetValue(null).StructAsByteArray();

							var ff = DeclaringType.DefineInitializedData(FieldName, value, SourceField.Attributes);

							FieldCache[SourceField] = ff;
						}
						else
						{
							var ff = DeclaringType.DefineField(FieldName, TypeCache[SourceField.FieldType], SourceField.Attributes);

							if (SourceField.IsLiteral)
							{
								// should we enable constant value override? :)

								ff.SetConstant(SourceField.GetRawConstantValue());
							}


							FieldCache[SourceField] = ff;
						}
					}
					else
					{
						// Specified method is not supported.
						// http://msdn.microsoft.com/en-us/library/4ek9c21e.aspx

						FieldCache[SourceField] = DeclaringType_.GetField(
							SourceField.Name,
							BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
						);
					}
				};
			#endregion

			#region TypeDefinitionCache
			TypeDefinitionCache.Resolve +=
				(SourceType) =>
				{
					if (SourceType.IsGenericParameter)
					{
						TypeDefinitionCache[SourceType] = SourceType;
						return;
					}

					if (SourceType.IsByRef)
					{
						TypeDefinitionCache[SourceType] = TypeDefinitionCache[SourceType.GetElementType()].MakeByRefType();
						return;
					}

					if (SourceType.IsArray)
					{
						TypeDefinitionCache[SourceType] = TypeDefinitionCache[SourceType.GetElementType()].MakeArrayType();
						return;
					}

					if (SourceType.IsGenericType)
						if (!SourceType.IsGenericTypeDefinition)
						{
							TypeDefinitionCache[SourceType] =
								TypeDefinitionCache[SourceType.GetGenericTypeDefinition()].MakeGenericType(
									SourceType.GetGenericArguments().Select(
										k => TypeDefinitionCache[k]
									).ToArray()
								);
							return;
						}

					var ContextType = SourceType;
					if (ShouldCopyType(ContextType))
					{
						var ttt = new CopyTypeDefinition
						{
							TypeDefinitionCache = TypeDefinitionCache,
							TypeRenameCache = TypeRenameCache,
							SourceType = SourceType,
							m = m,

							OverrideDeclaringType = null,
							NameObfuscation = NameObfuscation,
							ShouldCopyType = ShouldCopyType,
							FullNameFixup = FullNameFixup,
							Diagnostics = null,

						};

						var t = ttt.Invoke();

						TypeDefinitionCache[SourceType] = t;
					}
					else
					{
						TypeDefinitionCache[SourceType] =

							SourceType.IsGenericType ? SourceType.GetGenericTypeDefinition().MakeGenericType(
								SourceType.GetGenericArguments().Select(
									k => TypeDefinitionCache[k]
								).ToArray()
							) : SourceType;
					}
				};
			#endregion


			#region TypeCache
			TypeCache.Resolve +=
				(source) =>
				{
					// This unit was resolved for us...
					if (ExternalContext.TypeCache[source] != source)
					{
						TypeCache[source] = ExternalContext.TypeCache[source];

						// was continuation honored?
						if (TypeCache[source] is TypeBuilder)
						{
							TypeCache.Flags[source] = new object();
							Console.WriteLine("CreateType:  " + source.FullName);

							if (TypeCreated != null)
								TypeCreated(
									new TypeRewriteArguments
									{
										SourceType = source,
										Type = (TypeBuilder)TypeCache[source],
										Assembly = a,
										Module = m,

										context = this.RewriteArguments.context
									}
								);
						}

						return;
					}

					if (TypeCache.BaseDictionary.ContainsKey(source))
					{
						// seems like we are not supposed to resolve this type and use
						// what has been inserted in the cache!
						return;
					}

					if (source.IsGenericParameter)
					{
						TypeCache[source] = source;
						return;
					}


					if (source.IsByRef)
					{
						TypeCache[source] = TypeCache[source.GetElementType()].MakeByRefType();
						return;
					}


					if (source.IsArray)
					{
						TypeCache[source] = TypeCache[source.GetElementType()].MakeArrayType();
						return;
					}

					if (source.IsGenericType)
						if (!source.IsGenericTypeDefinition)
						{
							TypeCache[source] =
								TypeCache[source.GetGenericTypeDefinition()].MakeGenericType(
									source.GetGenericArguments().Select(
										k => TypeCache[k]
									).ToArray()
								);
							return;
						}

					// should we actually copy the field type?
					// simple rule - same assembly equals must copy

					var ContextType = source;

					if (ShouldCopyType(ContextType))
					{
						CopyType(
							source, a, m,
							null,
							TypeRenameCache,
							NameObfuscation,
							ShouldCopyType,
							FullNameFixup,

							 t =>
							 {
								 #region PostTypeRewrite
								 if (PostTypeRewrite != null)
									 PostTypeRewrite(
										 new TypeRewriteArguments
										 {
											 SourceType = source,
											 Type = t,
											 Assembly = a,
											 Module = m,

											 context = this.RewriteArguments.context
										 }
									 );
								 #endregion
							 }
							,


							 t =>
							 {
								 #region PreTypeRewrite
								 if (PreTypeRewrite != null)
									 PreTypeRewrite(
										 new TypeRewriteArguments
										 {
											 SourceType = source,
											 Type = t,
											 Assembly = a,
											 Module = m,

											 context = this.RewriteArguments.context
										 }
									 );
								 #endregion

							 }
							 ,

							 t =>
							 {
								 #region TypeCreated
								 if (TypeCreated != null)
									 TypeCreated(
										 new TypeRewriteArguments
										 {
											 SourceType = source,
											 Type = t,
											 Assembly = a,
											 Module = m,

											 context = this.RewriteArguments.context
										 }
									 );
								 #endregion

							 },
							 this,
							 this.RewriteArguments.context
						);


					}
					else
					{
						TypeCache[source] =

							source.IsGenericType ? source.GetGenericTypeDefinition().MakeGenericType(
								source.GetGenericArguments().Select(
									k => TypeCache[k]
								).ToArray()
							) : source;
					}

				};
			#endregion

			if (PreAssemblyRewrite != null)
				PreAssemblyRewrite(
					RewriteArguments
				);

			// we cannot be rewriting initialized data types...
			PrimaryTypes = PrimaryTypes.Where(k => !k.IsInitializedDataFieldType()).ToArray();

			Console.WriteLine("");
			Console.WriteLine("rewriting... primary types: " + PrimaryTypes.Length);
			Console.WriteLine("");

			// ask for our primary types to be copied
			var kt = PrimaryTypes.Select(k => TypeCache[k]).ToArray();

			// did we define any type declarations which we did not actually create yet?
			// fixme: maybe we shold just close the unclosed TypeBuilders?

			#region close unclosed definitions
			foreach (var item in TypeDefinitionCache.BaseDictionary.Keys.Except(TypeCache.BaseDictionary.Keys))
			{

				var tb = TypeDefinitionCache[item] as TypeBuilder;

				if (tb != null)
				{
					tb.CreateType();

					TypeCache[item] = tb;
					TypeCache.Flags[item] = new object();
					Console.WriteLine("CreateType:  " + item.FullName);

					if (TypeCreated != null)
						TypeCreated(
							new TypeRewriteArguments
							{
								SourceType = item,
								Type = (TypeBuilder)TypeCache[item],
								Assembly = a,
								Module = m,

								context = this.RewriteArguments.context
							}
						);
				}

			}
			#endregion




			#region maybe the rewriter wants to add some types at this point?
			if (PostAssemblyRewrite != null)
				PostAssemblyRewrite(
					RewriteArguments
				);
			#endregion

			InvokeLater(a, m);


			Console.WriteLine("");
			Console.WriteLine("rewriting... done");
			Console.WriteLine("");

			// http://blogs.msdn.com/fxcop/archive/2007/04/27/correct-usage-of-the-compilergeneratedattribute-and-the-generatedcodeattribute.aspx

			a.SetCustomAttribute(
				typeof(GeneratedCodeAttribute).GetConstructors().Single(),
				typeof(RewriteToAssembly).FullName + " at " + DateTime.Now,
				""
				//typeof(RewriteToAssembly).Assembly.GetCustomAttributes<AssemblyVersionAttribute>().Single().Version
				//new GeneratedCodeAttribute(
			);


			if (OutputUndefined)
			{
				a.Save(
					Product.Name
				);
			}
			else
			{
				// we probably loaded that assembly and now are trying to write to it...
				a.Save(
					"~" + Product.Name
				);

				new FileInfo(
					Path.Combine(Product.Directory.FullName, "~" + Product.Name)
				).CopyTo(this.Output.FullName, true);
			}

			Product.Refresh();
		}




		private static bool IsMarkedForMerge(Type t)
		{
			return t.Assembly.GetCustomAttributes<ObfuscationAttribute>().Any(k => k.Feature == "merge");
		}



		public AssemblyRewriteArguments RewriteArguments { get; private set; }

		public class AtShouldCopyTypeTuple
		{
			public Type ContextType;

			public bool DisableCopyType;
		}






		public string FullNameFixup(string n)
		{
			if (this.obfuscate)
				return NameObfuscation[n];

			return InternalFullNameFixup(n);
		}

		public string InternalFullNameFixup(string n)
		{


			if (this.rename != null)
				foreach (var k in this.rename)
				{
					if (n.StartsWith(k.From))
						return k.To + n.Substring(k.From.Length);
				}

			return n;
		}
	}
}
