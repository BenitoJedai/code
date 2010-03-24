using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace jsc.meta.Library
{
	public static class BCLExtensions
	{
		public static void EmitStoreFields(this ILGenerator il, PropertyInfo[] fields, object e)
		{
			foreach (var item in
				from p in e.GetProperties()
				let f = fields.Single(k => k.Name == p.Key)
				select new { f, p.Value }
				)
			{
				il.Emit(OpCodes.Ldarg_0);
				if (item.Value is string)
				{
					il.Emit(OpCodes.Ldstr, (string)item.Value);
				}
				else if (item.Value is int)
				{
					il.Emit(OpCodes.Ldc_I4, (int)item.Value);
				}
				else if (item.Value is MethodInfo)
				{
					il.Emit(OpCodes.Ldnull);
					il.Emit(OpCodes.Ldftn, ((MethodInfo)item.Value));
					il.Emit(OpCodes.Newobj, item.f.PropertyType.GetConstructors().Single());
				}
				else throw new NotImplementedException();
				il.Emit(OpCodes.Call, item.f.GetSetMethod(true));
			}
		}

		public static FieldInfo ToReferencedField(this Delegate t)
		{
			var xb = new ILBlock(t.Method);

			return xb.Instructrions.Where(k =>
				new[] {
					OpCodes.Ldfld ,
					OpCodes.Stfld,
				}.Contains(k.OpCode)

			).Select(k => k.TargetField).Single();
		}

		public static MethodInfo ToReferencedMethod(this Delegate t)
		{
			var xb = new ILBlock(t.Method);

			return xb.Instructrions.Where(k =>
				new[] {
					OpCodes.Ldftn ,
					OpCodes.Ldvirtftn,
					OpCodes.Call,
					OpCodes.Callvirt
				}.Contains(k.OpCode)

			).Select(k => k.TargetMethod).Single();
		}

		public static IEnumerable<Type> GetSubTypesFromAssembly(this Type t)
		{
			foreach (var item in t.Assembly.GetTypes())
			{
				if (item != t && t.IsAssignableFrom(item))
					yield return item;
			}
		}

		public static bool IsInitializedDataFieldType(this Type SourceType)
		{
			return (SourceType.StructLayoutAttribute != null && SourceType.StructLayoutAttribute.Size > 0);
		}

		public static T ToConsole<T>(this T e) where T : class
		{
			Console.WriteLine(e);

			return e;
		}

		public static DirectoryInfo CreateTemp(this DirectoryInfo e)
		{
			return e.Create(() => new DirectoryInfo(Path.GetTempPath()).CreateSubdirectory(Path.GetRandomFileName()));
		}

		public static DirectoryInfo Create(this DirectoryInfo e, Func<DirectoryInfo> f)
		{
			if (e == null)
				return f();

			if (!e.Exists)
				e.Create();

			return e;
		}

		public static Type[] GetParameterTypes(this MethodBase e)
		{
			return e.GetParameters().Select(k => k.ParameterType).ToArray();
		}

		public static Type[] GetSignatureTypes(this MethodInfo e)
		{
			return e.GetParameters().Select(k => k.ParameterType).Concat(new[] { e.ReturnType }).ToArray();
		}

	}
}
