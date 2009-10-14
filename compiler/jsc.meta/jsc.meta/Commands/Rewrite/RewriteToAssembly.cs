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

namespace jsc.meta.Commands.Rewrite
{
	public partial class RewriteToAssembly
	{
		/// <summary>
		/// Types within these assemblies will be merged to the new primary assembly
		/// </summary>
		public string[] merge = new string[0];

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
		public bool obfuscate;

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

		public void Invoke()
		{


			if (this.staging == null)
				this.staging = this.assembly.Directory.CreateSubdirectory("staging");
			else if (!staging.Exists)
				this.staging.Create();

			var assembly = this.assembly.LoadAssemblyAt(staging);
			_assembly = assembly;

			// load the rest of the references
			assembly.LoadReferencesAt(staging, this.assembly.Directory);


			var type = this.type == null ? assembly.EntryPoint.DeclaringType : assembly.GetType(this.type);


			var name = new AssemblyName(FullNameFixup(type.FullName));

			var Product = new FileInfo(Path.Combine(staging.FullName, name.Name + this.assembly.Extension));
			if (Product.Exists)
				Product.Delete();


			var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave, staging.FullName);
			var m = a.DefineDynamicModule(type.FullName, Product.Name);

			var TypeCache = new VirtualDictionary<Type, Type>();
			var TypeFieldsCache = new VirtualDictionary<Type, List<FieldBuilder>>();

			var ConstructorCache = new VirtualDictionary<ConstructorInfo, ConstructorInfo>();
			var MethodCache = new VirtualDictionary<MethodInfo, MethodInfo>();

			ConstructorCache.Resolve +=
				msource =>
				{
					if (msource.DeclaringType.Assembly == type.Assembly || this.merge.Contains(msource.DeclaringType.Assembly.GetName().Name))
					{
						CopyConstructor(msource, (TypeBuilder)TypeCache[msource.DeclaringType], TypeCache, ConstructorCache, TypeFieldsCache);
						return;
					}

					ConstructorCache[msource] = msource;
				};


			MethodCache.Resolve +=
				msource =>
				{
					if (msource.DeclaringType.Assembly == type.Assembly || this.merge.Contains(msource.DeclaringType.Assembly.GetName().Name))
					{
						CopyMethod(a, m, msource, (TypeBuilder)TypeCache[msource.DeclaringType], TypeCache, MethodCache, TypeFieldsCache, ConstructorCache, MethodCache);
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

					if (source.Assembly == type.Assembly || this.merge.Contains(source.Assembly.GetName().Name))
					{
						CopyType(source, a, m, TypeCache, TypeFieldsCache, ConstructorCache, MethodCache, null);
					}
					else
					{
						TypeCache[source] = source;
					}

				};

			var kt = TypeCache[type];

			a.Save(
				Product.Name
			);


			Product.Refresh();
		}


		public void CopyMethod(
			AssemblyBuilder a,
			ModuleBuilder m,
			MethodInfo source,
			TypeBuilder t,
			VirtualDictionary<Type, Type> tc,
			VirtualDictionary<MethodInfo, MethodInfo> mc,
			VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache,
			VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
			VirtualDictionary<MethodInfo, MethodInfo> MethodCache)
		{
			// sanity check!

			if (mc.BaseDictionary.ContainsKey(source))
				return;

			var km = t.DefineMethod(source.Name, source.Attributes, source.CallingConvention, tc[source.ReturnType], source.GetParameters().Select(kp => tc[kp.ParameterType]).ToArray());

			km.SetImplementationFlags(source.GetMethodImplementationFlags());

			mc[source] = km;

			if (source.GetMethodBody() == null)
				return;

			MethodBase mb = source;

			var kmil = km.GetILGenerator();

			if (source == this._assembly.EntryPoint)
			{
				// we found the entrypoint
				if (this.codeinjecton != null)
				{
					WriteEntryPointCodeInjection(a, m, kmil, t, tc, mc, TypeFieldCache, ConstructorCache, MethodCache);
				}

				a.SetEntryPoint(km);
			}

			mb.EmitTo(kmil,
				new ILTranslationExtensions.EmitToArguments
				{
					// we need to redirect any typerefs and methodrefs!
					TranslateTargetType = TargetType => tc[TargetType],
					TranslateTargetField = TargetField => TypeFieldCache[TargetField.DeclaringType].Single(k => k.Name == TargetField.Name),
					TranslateTargetMethod = TargetMethod => MethodCache[TargetMethod],
					TranslateTargetConstructor = TargetConstructor => ConstructorCache[TargetConstructor],
				}
			);

		}




		public void CopyConstructor(
			ConstructorInfo source, TypeBuilder t, VirtualDictionary<Type, Type> tc, VirtualDictionary<ConstructorInfo, ConstructorInfo> mc,
			VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache)
		{
			var km = t.DefineConstructor(
				source.Attributes,
				source.CallingConvention,
				source.GetParameters().Select(kp => tc[kp.ParameterType]).ToArray()
			);

			km.SetImplementationFlags(source.GetMethodImplementationFlags());

			mc[source] = km;

			if (source.GetMethodBody() == null)
				return;

			MethodBase mb = source;

			mb.EmitTo(km.GetILGenerator(),
				new ILTranslationExtensions.EmitToArguments
				{
					// we need to redirect any typerefs and methodrefs!

					TranslateTargetField = TargetField => TypeFieldCache[TargetField.DeclaringType].Single(k => k.Name == TargetField.Name),

				}
			);

		}

		public string FullNameFixup(string n)
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
