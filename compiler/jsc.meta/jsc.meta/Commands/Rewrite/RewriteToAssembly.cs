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

			this.staging = this.staging.Create(() => this.assembly.Directory.CreateSubdirectory("staging"));


			var assembly = this.assembly == null ? null : Assembly.LoadFile(this.assembly.FullName);
			//var assembly = this.assembly.LoadAssemblyAt(staging);
			_assembly = assembly;

			// load the rest of the references
			// maybe we shouldnt load those references which will be merged?
			if (assembly != null)
				assembly.LoadReferencesAt(staging, this.assembly.Directory);


			if (this.PrimaryTypes.Length == 0)
				if (assembly != null)
					this.PrimaryTypes =
						(string.IsNullOrEmpty(this.type) ?
							(assembly.EntryPoint == null ? assembly.GetTypes() : new[] { assembly.EntryPoint.DeclaringType }) :
								new[] { assembly.GetType(this.type) }
						);


			var Product_Name = (string.IsNullOrEmpty(this.product) ?
					this.assembly.Name + "Rewrite" :
					this.product);

			var Product_Extension = this.assembly == null ? ".dll" : this.assembly.Extension;

			var Product = new FileInfo(Path.Combine(staging.FullName, Product_Name + Product_Extension));

			this.Output = Product;

			var name = new AssemblyName(Path.GetFileNameWithoutExtension(Product.Name));


			if (Product.Exists)
				Product.Delete();


			var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave, staging.FullName);
			var m = a.DefineDynamicModule(Path.GetFileNameWithoutExtension(Product.Name), Product.Name);



			if (assembly != null)
				foreach (var ka in assembly.GetCustomAttributes<ObfuscationAttribute>())
				{
					a.DefineAttribute<ObfuscationAttribute>(ka);
				}

			var TypeCache = new VirtualDictionary<Type, Type>();
			var FieldCache = new VirtualDictionary<FieldInfo, FieldInfo>();

			var ConstructorCache = new VirtualDictionary<ConstructorInfo, ConstructorInfo>();
			var MethodCache = new VirtualDictionary<MethodInfo, MethodInfo>();

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

			this.RewriteArguments = new AssemblyRewriteArguments
					{
						Assembly = a,
						Module = m,

						context =
							new ILTranslationContext
							{

								ConstructorCache = ConstructorCache,
								MethodCache = MethodCache,
								TypeCache = TypeCache,
								FieldCache = FieldCache,
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
							ILOverride);
						return;
					}

					ConstructorCache[SourceConstructor] = SourceConstructor;
				};
			#endregion


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
							MethodCache,
							ConstructorCache, MethodCache, NameObfuscation,
							_assembly,
							this.codeinjecton,
							this.codeinjectonparams,
							this.ILOverride,

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

							//ff.setd
							//var ff3 = t.DefineInitializedData(f.Name + "___", 100, f.Attributes);

							FieldCache[SourceField] = ff;
						}
					}
					else
					{
						FieldCache[SourceField] = DeclaringType_.GetField(SourceField.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
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
							TypeCache,
							FieldCache,

							ConstructorCache,
							MethodCache,
							null,
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
						TypeCache[source] = source;
					}

				};
			#endregion

			if (PreRewrite != null)
				PreRewrite(
					RewriteArguments
				);

			// we cannot be rewriting initialized data types...
			PrimaryTypes = PrimaryTypes.Where(k => !k.IsInitializedDataFieldType()).ToArray();

			Console.WriteLine("");
			Console.WriteLine("rewriting... primary types: " + PrimaryTypes.Length);
			Console.WriteLine("");

			// ask for our primary types to be copied
			var kt = PrimaryTypes.Select(k => TypeCache[k]).ToArray();

			Console.WriteLine("");
			Console.WriteLine("rewriting... done");
			Console.WriteLine("");

			#region maybe the rewriter wants to add some types at this point?
			if (PostRewrite != null)
				PostRewrite(
					RewriteArguments
				);
			#endregion

			a.Save(
				Product.Name
			);


			Product.Refresh();
		}


		private static bool IsMarkedForMerge(Type t)
		{
			return t.Assembly.GetCustomAttributes<ObfuscationAttribute>().Any(k => k.Feature == "merge");
		}



		public AssemblyRewriteArguments RewriteArguments { get; private set; }

		private bool ShouldCopyType(Type ContextType)
		{
			return PrimaryTypes.Any(k => k.Assembly == ContextType.Assembly)
				||
				this.merge.Any(k => k.name == ContextType.Assembly.GetName().Name)
				|| IsMarkedForMerge(ContextType);
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
