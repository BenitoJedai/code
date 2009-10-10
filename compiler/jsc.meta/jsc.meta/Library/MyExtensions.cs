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
	public delegate void Action<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);

	public static class MyExtensions
	{
		public static int GetLiteralInt32(this Type t, string name, int Default)
		{
			var f = t.GetField(name);


			if (f != null)
				if (f.IsLiteral)
					if (f.FieldType == typeof(int))
						return (int)f.GetRawConstantValue();

			return Default;
		}

		public static string ToCamelCase(this string e)
		{
			var w = new StringBuilder();

			for (int i = 0; i < e.Length; i++)
			{
				if (i == 0)
					w.Append(e.Substring(i, 1).ToUpper());
				else if (char.IsLetter(e[i]) || char.IsDigit(e[i]))
				{
					if (char.IsLetter(e[i - 1]) || char.IsDigit(e[i - 1]))
						w.Append(e[i]);
					else
						w.Append(e.Substring(i, 1).ToUpper());
				}
			}

			return w.ToString();
		}

		public static EventBuilder DefineWorkingEvent(this TypeBuilder t, string EventName, Type EventType, Action<MethodInfo> RaiseEvent)
		{
			var f = t.DefineField("event_" + EventName, EventType, FieldAttributes.Private);

			var e = t.DefineEvent(EventName, EventAttributes.None, EventType);

			var _add = t.DefineMethod("add_" + EventName, MethodAttributes.Public, CallingConventions.HasThis, typeof(void), new[] { typeof(Action) });
			e.SetAddOnMethod(_add);
			{
				var il = _add.GetILGenerator();

				il.Emit(OpCodes.Ldarg_0);


				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldfld, f);
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Call, typeof(Delegate).GetMethod("Combine", new[] { typeof(Delegate), typeof(Delegate) }));

				il.Emit(OpCodes.Castclass, EventType);

				il.Emit(OpCodes.Stfld, f);

				il.Emit(OpCodes.Ret);
			}

			var _remove = t.DefineMethod("remove_" + EventName, MethodAttributes.Public, CallingConventions.HasThis, typeof(void), new[] { typeof(Action) });
			e.SetRemoveOnMethod(_remove);


			{
				var il = _remove.GetILGenerator();

				il.Emit(OpCodes.Ldarg_0);


				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldfld, f);
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Call, typeof(Delegate).GetMethod("Remove", new[] { typeof(Delegate), typeof(Delegate) }));

				il.Emit(OpCodes.Castclass, EventType);
				il.Emit(OpCodes.Stfld, f);

				il.Emit(OpCodes.Ret);
			}

			var _raise = t.DefineMethod("raise_" + EventName, MethodAttributes.Private, CallingConventions.HasThis, typeof(void), new[] { typeof(object), typeof(EventArgs) });
			{
				var il = _raise.GetILGenerator();

				#region if (event_ == null) return;
				var notnull = il.DefineLabel();

				// jsc needs a temporal variable it seems...
				var isnull = il.DeclareLocal(typeof(bool));

				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldfld, f);
				il.Emit(OpCodes.Ldnull);
				il.Emit(OpCodes.Ceq);
				il.Emit(OpCodes.Stloc_S, (byte)isnull.LocalIndex);

				il.Emit(OpCodes.Ldloc_S, (byte)isnull.LocalIndex);
				il.Emit(OpCodes.Brfalse, notnull);

				il.Emit(OpCodes.Ret);

				il.MarkLabel(notnull);
				#endregion

				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldfld, f);
				il.Emit(OpCodes.Call, typeof(Action).GetMethod("Invoke"));
				il.Emit(OpCodes.Ret);
			}

			e.SetRaiseMethod(_raise);

			RaiseEvent(_raise);

			return e;
		}

		public static void EmitSetProperty(this ILGenerator il, PropertyInfo p, string value)
		{
			il.Emit(OpCodes.Ldstr, value);
			il.Emit(OpCodes.Call, p.GetSetMethod());
		}

		public static void SetCustomAttribute(this MethodBuilder a, ConstructorInfo ctor, params object[] p)
		{
			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					ctor, p
				)
			);
		}

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

		public static Action DefineAttributeAt<T1>(this Func<T1> signature, MethodBuilder a)
		{
			var ctor = signature.ToConstructorInfo();

			return () => a.SetCustomAttribute(ctor);
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

			//var ScriptTypeFilterAttribute = default(Func<ScriptType, ScriptTypeFilterAttribute>).ToConstructorInfo();


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

		public static void DefineAttribute<T>(this TypeBuilder a, object z)
		{
			// yay attributes
			var Attribute = typeof(T);
			var Properties = z.GetType().GetProperties();

			var data = Enumerable.ToArray(
				from k in Properties
				let Field = Attribute.GetField(k.Name)
				let Property = Attribute.GetProperty(k.Name)
				let Value = k.GetValue(z, null)
				select new { Field, Property, Value }
			);


			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					Attribute.GetConstructor(new Type[0]),
					new object[0],

					Enumerable.ToArray(from k in data where k.Property != null select k.Property),
					Enumerable.ToArray(from k in data where k.Property != null select k.Value),

					Enumerable.ToArray(from k in data where k.Field != null select k.Field),
					Enumerable.ToArray(from k in data where k.Field != null select k.Value)
				)
			);
		}

		public static void DefineAttribute<T>(this AssemblyBuilder a, object z)
		{
			// yay attributes
			var Attribute = typeof(T);
			var Properties = z.GetType().GetProperties();

			var data = Enumerable.ToArray(
				from k in Properties
				let Field = Attribute.GetField(k.Name)
				let Property = Attribute.GetProperty(k.Name)
				let Value = k.GetValue(z, null)
				select new { Field, Property, Value }
			);


			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					Attribute.GetConstructor(new Type[0]),
					new object[0],

					Enumerable.ToArray(from k in data where k.Property != null select k.Property),
					Enumerable.ToArray(from k in data where k.Property != null select k.Value),

					Enumerable.ToArray(from k in data where k.Field != null select k.Field),
					Enumerable.ToArray(from k in data where k.Field != null select k.Value)
				)
			);
		}

		public static void DefineScriptAttribute<T>(this AssemblyBuilder a, T z)
		{
			// yay attributes
			var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);

			var p = typeof(T).GetProperties();



			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					ScriptAttribute.GetConstructors().Single(),
					new object[0],

					p.Select(k => ScriptAttribute.GetField(k.Name)).ToArray(),
					p.Select(k => k.GetValue(z, null)).ToArray()

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

		public static Assembly LoadReferencesAt(this Assembly a, DirectoryInfo target, DirectoryInfo source)
		{

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
						var staging_reference = reference.LoadAssemblyAt(source, target);
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

		/// <summary>
		/// All referenced types including their defining assemblies will be copied
		/// to the target possibily the staging area
		/// </summary>
		/// <param name="target"></param>
		/// <param name="z"></param>
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
