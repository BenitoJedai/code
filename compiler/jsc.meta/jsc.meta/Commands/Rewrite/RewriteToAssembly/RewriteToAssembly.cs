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
								CopyAttributes(shadow_assembly, __a);

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
						TypeRenameCache = TypeRenameCache
					}
			};


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


					if (ShouldCopyType(SourceConstructor.DeclaringType))
					{
						var DeclaringType = (TypeBuilder)TypeCache[SourceConstructor.DeclaringType];

						if (ConstructorCache.BaseDictionary.ContainsKey(SourceConstructor))
							return;

						CopyConstructor(
							SourceConstructor,
							DeclaringType,
							TypeCache,
							FieldCache,
							ConstructorCache,
							ConstructorCache,
							MethodCache,
							NameObfuscation,
							AtILOverride);
						return;
					}

					//    L_0086: newobj instance void [System.Core]System.Func`2<class [ScriptCoreLib.Archive.ZIP]ScriptCoreLib.Archive.ZIP.ZIPFile/Entry, bool>::.ctor(object, native int)

					var source = SourceConstructor.DeclaringType;

					if (source.IsGenericType)
					{
						if (source.GetGenericArguments().Any(k => k != TypeCache[k]))
						{
							// http://connect.microsoft.com/VisualStudio/feedback/details/94516/typebuilder-getconstructor-throws-argumentexception-when-supplied-created-generic-type
							var GenericArguments = TypeCache[source.GetGenericArguments()];


							var GenericTypeDefinition = source.GetGenericTypeDefinition();
							var GenericType = GenericTypeDefinition.MakeGenericType(GenericArguments);
							var Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
							var ParameterTypes = TypeCache[SourceConstructor.GetParameterTypes()];

							var GenericTypeDefinitionConstructor =
								GenericTypeDefinition.GetConstructor(Flags, null, ParameterTypes, null);

							var GenericTypeConstructor =
								//GenericType.GetConstructor(Flags, null, ParameterTypes, null);

							TypeBuilder.GetConstructor(GenericType, GenericTypeDefinitionConstructor);

							ConstructorCache[SourceConstructor] = GenericTypeConstructor;
						}
						else
							ConstructorCache[SourceConstructor] = SourceConstructor;

					}
					else
					{
						ConstructorCache[SourceConstructor] = SourceConstructor;
					}

				};
			#endregion


			TypeRenameCache.Resolve +=
				SourceType =>
				{
					if (TypeRenameCache.BaseDictionary.ContainsKey(SourceType))
						return;

					TypeRenameCache[SourceType] = default(string);
				};

			#region MethodCache
			MethodCache.Resolve +=
				msource =>
				{


					// This unit was resolved for us...
					if (ExternalContext.MethodCache[msource] != msource)
					{
						MethodCache[msource] = ExternalContext.MethodCache[msource];
						return;
					}

					if (ShouldCopyType(msource.DeclaringType))
					{
						var source = (TypeBuilder)TypeCache[msource.DeclaringType];

						CopyMethod(
							a,
							m,
							msource,
							source,
							TypeCache,
							FieldCache,
							ConstructorCache,
							MethodCache, NameObfuscation,
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
											 Type = source,
											 Assembly = a,
											 Module = m,

											 SourceMethod = SourceMethod,
											 Method = Method,
											 GetILGenerator = GetILGenerator,

											 context = this.RewriteArguments.context
										 }
									);
							}
						);
						return;
					}
					else
					{
						var source = msource.DeclaringType;

						// do we need to redirect the type also?
						if (source.GetGenericArguments().Any(k => k != TypeCache[k]))
						{
							//var msource_gp = msource.GetGenericMethodDefinition().GetParameterTypes();


							var GenericArguments = TypeCache[source.GetGenericArguments()];


							var GenericTypeDefinition = source.GetGenericTypeDefinition();
							var GenericTypeDefinition_GetGenericArguments = GenericTypeDefinition.GetGenericArguments();

							var GenericType = GenericTypeDefinition.MakeGenericType(GenericArguments);
							var Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
							var ParameterTypes =


							msource.IsGenericMethod ? msource.GetGenericMethodDefinition().GetParameterTypes().Select(
								k =>
								{
									if (k.IsGenericTypeDefinition)
										return k;

									return TypeCache[k];
								}
								).ToArray() : TypeCache[msource.GetParameterTypes()];

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
								 TypeCache[msource.GetGenericArguments()]
								 ) : GenericTypeMethod;

							MethodCache[msource] = GenericTypeMethod__;
						}
						else
						{
							MethodCache[msource] =
								msource.IsGenericMethod ? msource.GetGenericMethodDefinition().MakeGenericMethod(
									TypeCache[msource.GetGenericArguments()]
								) : msource;
						}
					}

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
						FieldCache[SourceField] = DeclaringType_.GetField(SourceField.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					}
				};
			#endregion

			#region TypeDefinitionCache
			TypeDefinitionCache.Resolve +=
				(source) =>
				{

					if (source.IsArray)
					{
						TypeDefinitionCache[source] = TypeDefinitionCache[source.GetElementType()].MakeArrayType();
						return;
					}
					var ContextType = source;
					if (ShouldCopyType(ContextType))
					{
						var ttt = new CopyTypeDefinition
						{
							TypeDefinitionCache = TypeDefinitionCache,
							TypeRenameCache = TypeRenameCache,
							SourceType = source,
							m = m,

							OverrideDeclaringType = null,
							NameObfuscation = NameObfuscation,
							ShouldCopyType = ShouldCopyType,
							FullNameFixup = FullNameFixup,
							Diagnostics = null,

						};

						var t = ttt.Invoke();

						TypeDefinitionCache[source] = t;
					}
					else
					{
						TypeDefinitionCache[source] =

							source.IsGenericType ? source.GetGenericTypeDefinition().MakeGenericType(
								source.GetGenericArguments().Select(
									k => TypeCache[k]
								).ToArray()
							) : source;
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

					if (source.IsArray)
					{
						TypeCache[source] = TypeCache[source.GetElementType()].MakeArrayType();
						return;
					}

					// should we actually copy the field type?
					// simple rule - same assembly equals must copy

					var ContextType = source;

					if (ShouldCopyType(ContextType))
					{
						CopyType(
							source, a, m,
							TypeDefinitionCache,
							TypeCache,
							FieldCache,

							ConstructorCache,
							MethodCache,
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
							 this
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

		private static void CopyAttributes(Assembly shadow_assembly, AssemblyBuilder __a)
		{
			var TypeAttributes = shadow_assembly.GetCustomAttributes(false);

			foreach (var item in TypeAttributes)
			{
				// for now we cannot copy ctor attributes / nonoba branch knows how...
				if (item.GetType().GetConstructor() == null)
				{
					var ctors = item.GetType().GetConstructors().OrderByDescending(k => k.GetParameters().Length);

					foreach (var _ctor in ctors)
					{

						if (CopyAttributes(item, __a, _ctor))
							break;
					}
				}
				else
				{
					// call a callback?
					__a.DefineAttribute(item, item.GetType());
				}
			}
		}

		private static bool CopyAttributes(object item, AssemblyBuilder __a, ConstructorInfo ctor)
		{
			var xb = new ILBlock(ctor);

			var a = Enumerable.ToDictionary(
				from i in xb.Instructrions
				let p = i.TargetParameter
				where p != null
				let stfld = i.NextInstruction
				let f = stfld.TargetField
				where f != null
				group f by p
			, k => k.Key, k => k.First());

			if (a.Count < ctor.GetParameters().Length)
				return false;

			__a.SetCustomAttribute(
				ctor, Enumerable.ToArray(
					from p in ctor.GetParameters()
					let f = a[p]
					select f.GetValue(item)
				)
			);

			return true;
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
