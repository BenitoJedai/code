using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Reflection.Emit;
using ScriptCoreLib;
using System.Media;
using System.Xml.Linq;
using System.Collections;
using jsc.Languages.IL;

namespace jsc.meta.Library
{
	public delegate void Action<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
	public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);

	public static class MyExtensions
	{

		public static void DefineAutomaticProperty(this TypeBuilder Page, string PropertyName, Type PropertyType)
		{
			var Page_Tag = Page.DefineProperty(PropertyName, PropertyAttributes.None, PropertyType, null);
			var Page_TagField = Page.DefineField("_" + PropertyName, PropertyType, FieldAttributes.Private);

			{
				var get_Page_Tag = Page.DefineMethod("get_" + PropertyName, MethodAttributes.Public, CallingConventions.Standard, PropertyType, null);

				var get_Page_Tag_il = get_Page_Tag.GetILGenerator();

				get_Page_Tag_il.Emit(OpCodes.Ldarg_0);
				get_Page_Tag_il.Emit(OpCodes.Ldfld, Page_TagField);
				get_Page_Tag_il.Emit(OpCodes.Ret);

				Page_Tag.SetGetMethod(get_Page_Tag);
			}

			{
				var set_Page_Tag = Page.DefineMethod("set_" + PropertyName, MethodAttributes.Public, CallingConventions.Standard, typeof(void), new[] { PropertyType });

				var set_Page_Tag_il = set_Page_Tag.GetILGenerator();

				set_Page_Tag_il.Emit(OpCodes.Ldarg_0);
				set_Page_Tag_il.Emit(OpCodes.Ldarg_1);
				set_Page_Tag_il.Emit(OpCodes.Stfld, Page_TagField);
				set_Page_Tag_il.Emit(OpCodes.Ret);

				Page_Tag.SetSetMethod(set_Page_Tag);
			}
		}


		public static void NotImplemented(this MethodBuilder m)
		{
			m.GetILGenerator().EmitCode(() => { throw new NotImplementedException(); });
		}

		public static LocalBuilder EmitStringArgumentsAsArray(this ILGenerator il, bool IsInstance, Type[] e)
		{
			var loc0 = il.DeclareLocal(typeof(string[]));
			var loc1 = il.DeclareLocal(typeof(string));

			il.Emit(OpCodes.Ldc_I4, e.Length);
			il.Emit(OpCodes.Newarr, typeof(string));
			il.Emit(OpCodes.Stloc, (short)loc0.LocalIndex);

			foreach (var item in e.Select((k, i) => new { k, i }))
			{

				il.Emit(OpCodes.Ldloc, (short)loc0.LocalIndex);
				il.Emit(OpCodes.Ldc_I4, item.i);
				il.Emit(OpCodes.Ldarg, (short)(item.i + (IsInstance ? 1 : 0)));
				il.Emit(OpCodes.Stelem_Ref);
			}

			return loc0;
		}

		public static LocalBuilder EmitStringArgumentsAsArray(this ILGenerator il, bool IsInstance, ParameterInfo[] e)
		{
			var loc0 = il.DeclareLocal(typeof(string[]));
			var loc1 = il.DeclareLocal(typeof(string));

			il.Emit(OpCodes.Ldc_I4, e.Length);
			il.Emit(OpCodes.Newarr, typeof(string));
			il.Emit(OpCodes.Stloc, (short)loc0.LocalIndex);

			foreach (var item in e.Select((k, i) => new { k, i }))
			{

				il.Emit(OpCodes.Ldloc, (short)loc0.LocalIndex);
				il.Emit(OpCodes.Ldc_I4, item.i);
				il.Emit(OpCodes.Ldarg, (short)(item.k.Position + (IsInstance ? 1 : 0)));
				il.Emit(OpCodes.Stelem_Ref);
			}

			return loc0;
		}

		public static void EmitReturnSerializedArray<T>(this ILGenerator il, T[] e,
			Func<Type, Type> TypeCache,
			Func<ConstructorInfo, ConstructorInfo> ConstructorCache,
			Func<FieldInfo, FieldInfo> FieldCache
			)
		{
			var loc0 = InternalCreateArray(il, typeof(T), e, TypeCache, ConstructorCache, FieldCache);

			il.Emit(OpCodes.Ldloc, (short)(loc0.LocalIndex));
			il.Emit(OpCodes.Ret);
		}

		private static LocalBuilder InternalCreateArray(ILGenerator il, Type ElementType, Array e, Func<Type, Type> TypeCache, Func<ConstructorInfo, ConstructorInfo> ConstructorCache, Func<FieldInfo, FieldInfo> FieldCache)
		{
			Type ArrayType = ElementType.MakeArrayType();

			var loc0 = il.DeclareLocal(TypeCache(ArrayType));
			var loc1 = il.DeclareLocal(TypeCache(ElementType));

			il.Emit(OpCodes.Ldc_I4, e.Length);
			il.Emit(OpCodes.Newarr, TypeCache(ElementType));
			il.Emit(OpCodes.Stloc, (short)(loc0.LocalIndex));

			var i = -1;
			foreach (var item in e)
			{
				i++;

				il.Emit(OpCodes.Newobj, ConstructorCache(ElementType.GetConstructor()));
				il.Emit(OpCodes.Stloc, (short)(loc1.LocalIndex));

				foreach (var f in ElementType.GetFields())
				{
					if (f.GetValue(item) != null)
						if (f.FieldType.IsArray)
						{
							var loc2 = InternalCreateArray(
								il, f.FieldType.GetElementType(), (Array)f.GetValue(item), TypeCache, ConstructorCache, FieldCache);

							il.Emit(OpCodes.Ldloc, (short)(loc1.LocalIndex));
							il.Emit(OpCodes.Ldloc, (short)(loc2.LocalIndex));
							il.Emit(OpCodes.Stfld, FieldCache(f));
						}
						else if (f.FieldType == typeof(string))
						{
							il.Emit(OpCodes.Ldloc, (short)(loc1.LocalIndex));
							il.Emit(OpCodes.Ldstr, (string)f.GetValue(item));
							il.Emit(OpCodes.Stfld, FieldCache(f));
						}
						else if (f.FieldType == typeof(bool))
						{
							il.Emit(OpCodes.Ldloc, (short)(loc1.LocalIndex));
							il.Emit(((bool)f.GetValue(item)) ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
							il.Emit(OpCodes.Stfld, FieldCache(f));
						}
						else throw new NotImplementedException();
				}

				il.Emit(OpCodes.Ldloc, (short)(loc0.LocalIndex));
				il.Emit(OpCodes.Ldc_I4, i);
				il.Emit(OpCodes.Ldloc, (short)(loc1.LocalIndex));
				il.Emit(OpCodes.Stelem_Ref);
			}
			return loc0;
		}

		public static IEnumerable<FileInfo> GetAllFilesByPattern(this DirectoryInfo e, params string[] p)
		{
			return p.SelectMany(i => e.GetFiles(i, SearchOption.AllDirectories));
		}

		public static IEnumerable<FileInfo> GetFilesByPattern(this DirectoryInfo e, params string[] p)
		{
			return p.SelectMany(i => e.GetFiles(i));
		}

		public static bool IsEventMethod(this MethodInfo kk)
		{
			return kk.ReturnType == typeof(void) && kk.GetParameterTypes().Length == 1 && typeof(Delegate).IsAssignableFrom(kk.GetParameterTypes()[0]);
		}

		public static void CopyTo(this ParameterInfo[] p, MethodBuilder m)
		{
			foreach (var item in p)
			{
				// http://msdn.microsoft.com/en-us/library/system.reflection.emit.methodbuilder.defineparameter.aspx

				// The position of the parameter in the parameter list. 
				// Parameters are indexed beginning with the number 1 for the first parameter; the number 0 represents the return value of the method. 


				m.DefineParameter(item.Position + 1, item.Attributes, item.Name);
			}
		}

		public static void CopyTo(this ParameterInfo[] p, ConstructorBuilder m)
		{
			foreach (var item in p)
			{

				m.DefineParameter(item.Position + 1, item.Attributes, item.Name);
			}
		}

		public static void EmitCode(this ILGenerator il, Action e)
		{
			e.Method.EmitTo(il);
		}

		public static LocalBuilder DeclareInitializedLocal(this ILGenerator il, Type t)
		{
			var loc = il.DeclareLocal(t);

			il.Emit(OpCodes.Newobj, t.GetConstructor(new Type[0]));

			il.Emit(OpCodes.Stloc, (short)loc.LocalIndex);

			return loc;
		}


		public static LocalBuilder DeclareInitializedLocal(this ILGenerator il, Type t, ConstructorInfo ctor)
		{
			var loc = il.DeclareLocal(t);

			il.Emit(OpCodes.Newobj, ctor);

			il.Emit(OpCodes.Stloc, (short)loc.LocalIndex);

			return loc;
		}

		public static void DefineManifestResource(this ModuleBuilder m, string name, XElement e)
		{
			m.DefineManifestResource(name, new MemoryStream(Encoding.UTF8.GetBytes(e.ToString())), ResourceAttributes.Public);
		}

		public static MethodBuilder DefineByteArrayToSoundPlayerConversion(this TypeBuilder t)
		{

			Func<byte[], int, SoundPlayer> msource =
				(data, length) =>
				{
					var m = new MemoryStream();

					m.Write(data, 0, length);
					m.Seek(0, SeekOrigin.Begin);

					return new SoundPlayer(m);
				};

			var TypeCache = new jsc.Library.VirtualDictionary<Type, Type>();
			var FieldCache = new jsc.Library.VirtualDictionary<FieldInfo, FieldInfo>();
			var ConstructorCache = new jsc.Library.VirtualDictionary<ConstructorInfo, ConstructorInfo>();
			var MethodCache = new jsc.Library.VirtualDictionary<MethodInfo, MethodInfo>();
			var NameObfuscation = new jsc.Library.VirtualDictionary<string, string>();

			TypeCache.Resolve +=
				source =>
				{
					TypeCache[source] = source;
				};

			FieldCache.Resolve +=
				source =>
				{
					FieldCache[source] = source;
				};

			ConstructorCache.Resolve +=
				source =>
				{
					ConstructorCache[source] = source;
				};

			NameObfuscation.Resolve +=
				source =>
				{
					NameObfuscation[source] = source;
				};

			MethodCache.Resolve +=
				source =>
				{
					MethodCache[source] = source;
				};

			jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyMethod(
				null,
				null,
				msource.Method,
				t,
				TypeCache,
				FieldCache,
				ConstructorCache,
				MethodCache,
				NameObfuscation,
				null,
				null, null, null, null
			);

			return (MethodBuilder)MethodCache[msource.Method];
		}

		class DefineDefaultPropertyMarker
		{
			static DefineDefaultPropertyMarker __DefaultInstance;

			public static DefineDefaultPropertyMarker Default
			{
				get
				{
					if (__DefaultInstance == null)
						__DefaultInstance = new DefineDefaultPropertyMarker();

					return __DefaultInstance;
				}
			}
		}

		public static void DefineDefaultProperty(this TypeBuilder t, ConstructorBuilder ctor)
		{


			var TypeCache = new jsc.Library.VirtualDictionary<Type, Type>();
			var ConstructorCache = new jsc.Library.VirtualDictionary<ConstructorInfo, ConstructorInfo>();
			var FieldCache = new jsc.Library.VirtualDictionary<FieldInfo, FieldInfo>();
			var MethodCache = new jsc.Library.VirtualDictionary<MethodInfo, MethodInfo>();
			var NameObfuscation = new jsc.Library.VirtualDictionary<string, string>();

			ConstructorCache[typeof(DefineDefaultPropertyMarker).GetConstructor(new Type[0])] = ctor;
			TypeCache[typeof(DefineDefaultPropertyMarker)] = t;

			TypeCache.Resolve +=
				source =>
				{
					TypeCache[source] = source;
				};

			// test me!
			FieldCache.Resolve +=
				source =>
				{
					FieldCache[source] = source;
				};

			NameObfuscation.Resolve +=
				source =>
				{
					NameObfuscation[source] = source;
				};

			MethodCache.Resolve +=
				source =>
				{
					jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyMethod(
						null,
						null,
						source,
						t,
						TypeCache,
						FieldCache,
						ConstructorCache,
						MethodCache,
						NameObfuscation,
						null,
						null,
						null,
						null,
						null
					);
				};

			jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyTypeMembers(typeof(DefineDefaultPropertyMarker),
				TypeCache,
				FieldCache,
				ConstructorCache,
				MethodCache,
				NameObfuscation,
				t
			);



		}

		public static T Initialize<T>(this object value, T e)
			where T : class
		{
			if (value == null)
				return e;

			if (value is T)
				return value as T;

			return e;
		}

		public static T Apply<T>(this T e, Action<T> handler)
			where T : class
		{
			if (e == default(T))
				return e;

			handler(e);

			return e;
		}

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

		public static void DefineAttribute<T>(this MethodBuilder a, object z)
		{
			// yay attributes
			var Attribute = typeof(T);
			var Properties = z.GetType().GetProperties();

			var data = Enumerable.ToArray(
				from k in Properties
				let Field = Attribute.GetField(k.Name)
				let Property = Attribute.GetProperty(k.Name)
				let PropertyCanWrite = Property != null && Property.CanWrite
				let Value = k.GetValue(z, null)
				select new { Field, Property, Value, PropertyCanWrite }
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

		public static void DefineAttribute<T>(this TypeBuilder a, object z)
		{
			// yay attributes
			var Attribute = typeof(T);
			var Properties = z.GetType().GetProperties();

			var data = Enumerable.ToArray(
				from k in Properties
				let Field = Attribute.GetField(k.Name)
				let Property = Attribute.GetProperty(k.Name)
				let PropertyCanWrite = Property != null && Property.CanWrite
				let Value = k.GetValue(z, null)
				select new { Field, Property, Value, PropertyCanWrite }
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
				let PropertyCanWrite = Property != null && Property.CanWrite
				let Value = k.GetValue(z, null)
				select new { Field, Property, Value, PropertyCanWrite }
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

		internal static bool Equals(object a, object b, Type t)
		{
			// are we doing the right thing here?
			// we need to compare primitives and objects...

			var Comparer = typeof(Comparer<>).MakeGenericType(t);
			var Default = (IComparer)Comparer.GetProperty("Default").GetValue(null, null);

			return Default.Compare(a, b) == 0;
		}

		public static CustomAttributeBuilder ToCustomAttributeBuilder(this Attribute z)
		{
			var Attribute = z.GetType();

			// we should not be storing values which are default!
			var DefaultAttribute = Activator.CreateInstance(Attribute);

			// We may need to track down some fields from ctor
			// Not a writable property

			var Properties = Enumerable.ToArray(
				from q in
					Enumerable.Concat(
						from k in Attribute.GetProperties()
						let Value = k.GetValue(z, null)
						let Name = k.Name

						where !Equals(Value, k.GetValue(DefaultAttribute, null), k.PropertyType)

						select new { Name, Value },

						from f in Attribute.GetFields(BindingFlags.Instance | BindingFlags.Public)
						let Value = f.GetValue(z)
						let Name = f.Name
						where !Equals(Value, f.GetValue(DefaultAttribute), f.FieldType)
						select new { Name, Value }
					)
				let Field = Attribute.GetField(q.Name)
				let Property = Attribute.GetProperty(q.Name)
				let PropertyCanWrite = Property != null && Property.CanWrite
				select new { q.Name, Field, Property, PropertyValue = q.Value, PropertyCanWrite }
			);

			return new CustomAttributeBuilder(

				// ctor
				Attribute.GetConstructor(new Type[0]),

				// ctor arguments
				new object[0],

				// properties
				Enumerable.ToArray(from k in Properties where k.PropertyCanWrite select k.Property),
				Enumerable.ToArray(from k in Properties where k.PropertyCanWrite select k.PropertyValue),

				// fields
				Enumerable.ToArray(from k in Properties where k.Field != null select k.Field),
				Enumerable.ToArray(from k in Properties where k.Field != null select k.PropertyValue)
			);
		}

		public static void DefineAttribute(this MethodBuilder a, object z, Type Attribute)
		{
			a.SetCustomAttribute(
				((Attribute)z).ToCustomAttributeBuilder()
			);
		}

		public static void DefineAttribute(this TypeBuilder a, object z, Type Attribute)
		{
			a.SetCustomAttribute(
				((Attribute)z).ToCustomAttributeBuilder()
			);
		}

		public static void DefineAttribute(this AssemblyBuilder a, object z, Type Attribute)
		{
			a.SetCustomAttribute(
				((Attribute)z).ToCustomAttributeBuilder()
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
