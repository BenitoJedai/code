using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Reflection.Emit;
using ScriptCoreLib;

namespace jsc.meta.Library
{
	public static class MyExtensions
	{
		public static void SetCustomAttribute(this TypeBuilder a, ConstructorInfo ctor, params object[] p)
		{
			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					ctor, p
				)
			);
		}


		public static void SetCustomAttribute(this AssemblyBuilder a, ConstructorInfo ctor, params object[] p)
		{
			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					ctor, p
				)
			);
		}

		public static Action DefineAttributeAt<T1>(this Func<T1> signature, TypeBuilder a)
		{
			var ctor = signature.ToConstructorInfo();

			return () => a.SetCustomAttribute(ctor);
		}

		public static Action DefineAttributeAt<T1>(this Func<T1> signature, AssemblyBuilder a)
		{
			var ctor = signature.ToConstructorInfo();

			return () => a.SetCustomAttribute(ctor);
		}

		public static Action<T1> DefineAttributeAt<T1, T2>(this Func<T1, T2> signature, TypeBuilder a)
		{
			var ctor = signature.ToConstructorInfo();

			return (t1) => a.SetCustomAttribute(ctor, t1);
		}

		public static Action<T1> DefineAttributeAt<T1, T2>(this Func<T1, T2> signature, AssemblyBuilder a)
		{
			var ctor = signature.ToConstructorInfo();

			return (t1) => a.SetCustomAttribute(ctor, t1);
		}

		public static Action<T1, T2> DefineAttributeAt<T1, T2, T3>(this Func<T1, T2, T3> signature, AssemblyBuilder a)
		{
			var ctor = signature.ToConstructorInfo();

			return (t1, t2) => a.SetCustomAttribute(ctor, t1, t2);
		}

		public static void DefineScriptLibraries(this AssemblyBuilder a, params Type[] z)
		{
			// yay attributes
			var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);

			var ScriptTypeFilterAttribute = default(Func<ScriptType, ScriptTypeFilterAttribute>).ToConstructorInfo();


			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					ScriptAttribute.GetConstructors().Single(),
					new object[0],
					new[] { 
										ScriptAttribute.GetField("ScriptLibraries"),
										ScriptAttribute.GetField("IsScriptLibrary")},
					new object[] { 
										z,
										true
									}
				)
			);
		}

		public static Assembly LoadAssemblyAt(this AssemblyName assembly, DirectoryInfo source, DirectoryInfo target)
		{

			var dll = new FileInfo(Path.Combine(source.FullName, assembly.Name + ".dll"));
			var exe = new FileInfo(Path.Combine(source.FullName, assembly.Name + ".exe"));

			return dll.LoadAssemblyAt(target) ?? exe.LoadAssemblyAt(target);
		}


		public static Assembly LoadAssemblyAt(this FileInfo assembly, DirectoryInfo target)
		{
			if (!assembly.Exists)
				return null;

			var target_assembly = Path.Combine(target.FullName, assembly.Name);
			assembly.CopyTo(target_assembly, true);
			return Assembly.LoadFile(target_assembly);
		}
		
		public static Assembly LoadAssemblyAtWithReferences(this FileInfo assembly, DirectoryInfo target)
		{
			var a = assembly.LoadAssemblyAt(target);

			#region DefineReferencedAssemblies
			Action<Assembly> DefineReferencedAssemblies = null;
			var DefineReferencedAssembliesCache = new List<string>();

			DefineReferencedAssemblies =
				k =>
				{
					foreach (var reference in k.GetReferencedAssemblies())
					{
						if (DefineReferencedAssembliesCache.Contains(reference.Name))
						{
							continue;
						}
						DefineReferencedAssembliesCache.Add(reference.Name);
						var staging_reference = reference.LoadAssemblyAt(assembly.Directory, target);
						if (staging_reference != null)
						{
							DefineReferencedAssemblies(staging_reference);
						}
					}
				};
			#endregion

			DefineReferencedAssemblies(a);

			return a;
		}

		public static void DefinesTypes(this DirectoryInfo target, params Type[] z)
		{
			foreach (var item in z)
			{
				item.CopyAssemblyTo(target);
			}
		}

		public static void CopyAssemblyTo(this Type z, DirectoryInfo target)
		{
			var a = new FileInfo(z.Assembly.Location);
			var x = Path.Combine(target.FullName, a.Name);
			a.CopyTo(x, true);
		}

		public static ConstructorInfo ToConstructorInfo<T1>(this Func<T1> signature)
		{
			return typeof(T1).GetConstructor(new Type[] { });
		}


		public static ConstructorInfo ToConstructorInfo<T1, T2>(this Func<T1, T2> signature)
		{
			return typeof(T2).GetConstructor(new[] { typeof(T1) });
		}


		public static ConstructorInfo ToConstructorInfo<T1, T2, T3>(this Func<T1, T2, T3> signature)
		{
			return typeof(T3).GetConstructor(new[] { typeof(T1), typeof(T2) });
		}


	}
}
