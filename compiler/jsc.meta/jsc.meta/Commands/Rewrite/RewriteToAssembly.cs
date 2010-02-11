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
using System.Diagnostics;

namespace jsc.meta.Commands.Rewrite
{
	public partial class RewriteToAssembly
	{
		// usage: c:\util\jsc\bin\jsc.meta.exe RewriteToAssembly /assembly:"$(TargetPath)"
		//

		// todo: generics, interfaces, and opcodes.leave need to be fixed!

		public string product;

		/// <summary>
		/// Types within these assemblies will be merged to the new primary assembly
		/// </summary>
		public MergeInstruction[] merge = new MergeInstruction[0];

		public class MergeInstruction
		{
			public string name;

			public static implicit operator MergeInstruction(string e)
			{
				return new MergeInstruction { name = e };
			}
		}

		public FileInfo assembly;


		public DirectoryInfo staging;

		public string type;


		/// <summary>
		/// We should be translating complex IL to more simple IL.
		/// For example switch statements could be translated to
		/// if statements. We might want to use jsx and then after simplifing 
		/// IL run jsc on the generated assembly.
		/// 
		/// jsc is the default translation provider which just happens
		/// to understand our ScriptAttribute
		/// 
		/// jsx happens to be a more advanced IL reader than jsc
		/// 
		/// if in the future any vendor comes up with a better solution
		/// which we can implement we will consider them too.
		/// 
		/// Some target languages wont implement specific features
		/// for which we will need to simplify IL anyhow.
		/// 
		/// We could also be inlining methods and delete them.
		/// </summary>
		public bool simplify;

		/// <summary>
		/// We can provide obfuscation features. Simply by renaming all
		/// methods would do. We could also make the IL harder for disassamblers
		/// like reflector.
		/// </summary>
		public bool obfuscate = false;

		internal Delegate codeinjecton;
		internal Func<Assembly, object[]> codeinjectonparams;

		public class NamespaceRenameInstructions
		{
			// we could provide namespace renaming to provide 
			// brand support
			public string rule;

			public static implicit operator NamespaceRenameInstructions(string e)
			{
				return new NamespaceRenameInstructions { rule = e };
			}

			public string From
			{
				get
				{
					return rule.Substring(0, rule.IndexOf("->"));
				}
			}

			public string To
			{
				get
				{
					return rule.Substring(rule.IndexOf("->") + 2);
				}
			}
		}

		public NamespaceRenameInstructions[] rename;

		internal Assembly _assembly;

		VirtualDictionary<string, string> NameObfuscation = new VirtualDictionary<string, string>();

		public Type[] PrimaryTypes = new Type[0];


		public class AssemblyRewriteArguments
		{
			public jsc.Languages.IL.ILTranslationContext context;
			public ModuleBuilder Module;
			public AssemblyBuilder Assembly;
		}

		public Action<AssemblyRewriteArguments> PostRewrite;
		public Action<AssemblyRewriteArguments> PreRewrite;


		public class TypeRewriteArguments : AssemblyRewriteArguments
		{
			public Type SourceType;
			public TypeBuilder Type;
		}

		public Action<TypeRewriteArguments> PostTypeRewrite;
		public Action<TypeRewriteArguments> PreTypeRewrite;

		public event Action<TypeRewriteArguments> TypeCreated;

		public class BeforeInstructionsArguments : TypeRewriteArguments
		{
			public MethodInfo SourceMethod;
			public MethodBuilder Method;

			public Func<ILGenerator> GetILGenerator;
		}

		// to be phased out
		public Action<BeforeInstructionsArguments> BeforeInstructions;

		public Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride;

		public FileInfo Output;

		public ILTranslationContext ExternalContext = new ILTranslationContext
		{
			ConstructorCache = new VirtualDictionary<ConstructorInfo, ConstructorInfo>(),
			MethodCache = new VirtualDictionary<MethodInfo, MethodInfo>(),
			TypeCache = new VirtualDictionary<Type, Type>(),
			FieldCache = new VirtualDictionary<FieldInfo, FieldInfo>()
		};

		public void Invoke()
		{
			//Debugger.Launch();

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
					this.PrimaryTypes = new[] {
					(this.type == null ? assembly.EntryPoint.DeclaringType : assembly.GetType(this.type))
				};


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

					if (DeclaringType_ is TypeBuilder)
					{
						var DeclaringType = (TypeBuilder)DeclaringType_;
						var FieldName = NameObfuscation[SourceField.Name];

						if (SourceField.FieldType.StructLayoutAttribute != null && SourceField.FieldType.StructLayoutAttribute.Size > 0)
						{
							var ff = DeclaringType.DefineInitializedData(FieldName, SourceField.GetValue(null).StructAsByteArray(), SourceField.Attributes);

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

			Console.WriteLine("");
			Console.WriteLine("rewriting...");
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
