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

		public Type PrimaryType;

		public void Invoke()
		{
			//Debugger.Launch();

			var NameObfuscationRandom = new Random();

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

			if (this.staging == null)
				this.staging = this.assembly.Directory.CreateSubdirectory("staging");
			else if (!staging.Exists)
				this.staging.Create();

			var assembly = this.assembly.LoadAssemblyAt(staging);
			_assembly = assembly;

			// load the rest of the references
			assembly.LoadReferencesAt(staging, this.assembly.Directory);


			this.PrimaryType = this.type == null ? assembly.EntryPoint.DeclaringType : assembly.GetType(this.type);


			var Product = new FileInfo(Path.Combine(staging.FullName,
			(string.IsNullOrEmpty(this.product) ? this.assembly.Name + "Rewrite" : this.product) + this.assembly.Extension));

			var name = new AssemblyName(Path.GetFileNameWithoutExtension(Product.Name));


			if (Product.Exists)
				Product.Delete();


			var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave, staging.FullName);
			var m = a.DefineDynamicModule(Path.GetFileNameWithoutExtension(Product.Name), Product.Name);




			foreach (var ka in assembly.GetCustomAttributes<ObfuscationAttribute>())
			{
				a.DefineAttribute<ObfuscationAttribute>(ka);
			}

			var TypeCache = new VirtualDictionary<Type, Type>();
			var TypeFieldsCache = new VirtualDictionary<Type, List<FieldBuilder>>();

			var ConstructorCache = new VirtualDictionary<ConstructorInfo, ConstructorInfo>();
			var MethodCache = new VirtualDictionary<MethodInfo, MethodInfo>();



			ConstructorCache.Resolve +=
				msource =>
				{
					if (msource.DeclaringType.Assembly == PrimaryType.Assembly || this.merge.Any(k => k.name == msource.DeclaringType.Assembly.GetName().Name))
					{
						var DeclaringType = (TypeBuilder)TypeCache[msource.DeclaringType];

						if (ConstructorCache.BaseDictionary.ContainsKey(msource))
							return;

						CopyConstructor(msource,
							DeclaringType,
							TypeCache, ConstructorCache, TypeFieldsCache,
							ConstructorCache, MethodCache, NameObfuscation);
						return;
					}

					ConstructorCache[msource] = msource;
				};


			MethodCache.Resolve +=
				msource =>
				{
					if (msource.DeclaringType.Assembly == PrimaryType.Assembly || this.merge.Any(k => k.name == msource.DeclaringType.Assembly.GetName().Name))
					{
						CopyMethod(a, m, msource, (TypeBuilder)TypeCache[msource.DeclaringType], TypeCache, MethodCache, TypeFieldsCache, ConstructorCache, MethodCache, NameObfuscation);
						return;
					}

					MethodCache[msource] = msource;

				};

			TypeFieldsCache.Resolve +=
				source =>
				{
					var k = TypeCache[source];

					if (TypeFieldsCache.BaseDictionary.ContainsKey(source))
						return;

					TypeFieldsCache[source] = new List<FieldBuilder>();
				};

			TypeCache.Resolve +=
				source =>
				{
					if (source.IsArray)
					{
						TypeCache[source] = TypeCache[source.GetElementType()].MakeArrayType();
						return;
					}

					// should we actually copy the field type?
					// simple rule - same assembly equals must copy

					var ContextType = source;

					if (ShouldCopyType(PrimaryType, ContextType))
					{
						CopyType(source, a, m, TypeCache, TypeFieldsCache, ConstructorCache, MethodCache, null, NameObfuscation);
					}
					else
					{
						TypeCache[source] = source;
					}

				};

			var kt = TypeCache[PrimaryType];

			a.Save(
				Product.Name
			);


			Product.Refresh();
		}

		private bool ShouldCopyType(Type PrimaryType, Type ContextType)
		{
			return ContextType.Assembly == PrimaryType.Assembly || this.merge.Any(k => k.name == ContextType.Assembly.GetName().Name);
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
