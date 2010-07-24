using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;
using ScriptCoreLib.Extensions;

namespace jsc //.Extensions
{
	public static class Extensions
	{
		public static bool IsNonPrimitiveValueType(this Type z)
		{
			ScriptAttribute za = ScriptAttribute.Of(z, true);

			// All BCL structs should have an implementtation class anyhow...
			if (za == null)
				return false;

			var z_Implements = za.Implements;
			var z_NonPrimitiveValueType = z_Implements != null && z_Implements.IsValueType && !z_Implements.IsPrimitive;

			return z_NonPrimitiveValueType;
		}

		internal static void ForEach<T>(this IEnumerable<T> source, Action<T, int> f)
		{
			int c = 0;
			foreach (var k in source)
			{
				f(k, c);
				c++;
			}
		}

		internal static void ForEach<T>(this IEnumerable<T> source, Action<T> f)
		{
			foreach (var k in source)
			{
				f(k);
			}
		}

		public static IEnumerable<ScriptType> ToScriptTypes(this CommandLineOptions options)
		{
			if (options.IsJavaScript)
				yield return ScriptType.JavaScript;

			if (options.IsPHP)
				yield return ScriptType.PHP;

			if (options.IsJava)
				yield return ScriptType.Java;

			if (options.IsActionScript)
				yield return ScriptType.ActionScript;

			if (options.IsCSharp2)
				yield return ScriptType.CSharp2;

			if (options.IsC)
				yield return ScriptType.C;

		}

		/// <summary>
		/// In .net 2.0 the event add and remove methods are marked by the compiler to be synchronized.
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		public static bool IsSynchronized(this MethodBase m)
		{
			int flags = (int)m.GetMethodImplementationFlags();

			// http://blogs.msdn.com/ricom/archive/2004/05/05/126542.aspx
			// http://msdn2.microsoft.com/en-us/library/system.reflection.methodimplattributes.aspx
			if ((flags & (int)MethodImplAttributes.Synchronized) == (int)MethodImplAttributes.Synchronized)
				return true;

			return false;
		}

		public static bool IsInstanceConstructor(this MethodBase e)
		{
			return e.IsConstructor && !e.IsStatic;
		}

		public static Action<T> And<T>(this Action<T> a, Action<T> b)
		{
			return i =>
				{
					a(i);
					b(i);
				};
		}

		public static Action<T> NonNullArgument<T>(this Action<T> e) where T : class
		{
			return i =>
				{
					if (i == default(T))
						return;

					e(i);
				};
		}

		public static bool IsAnonymousType(this Type z)
		{
			return ScriptCoreLib.ScriptAttribute.IsAnonymousType(z);
		}

		public static T PopOrDefault<T>(this Stack<T> e)
		{
			if (e.Count > 0)
				return e.Pop();

			return default(T);
		}

		public static Stack<Type> DeclaringTypesToStack(this Type e)
		{
			return e.DeclaringTypesToStack(false);
		}

		public static Stack<Type> DeclaringTypesToStack(this Type e, bool IncludeThis)
		{
			var s = new Stack<Type>();

			var p = e;

			if (IncludeThis)
				s.Push(p);

			while (p.DeclaringType != null)
			{
				p = p.DeclaringType;

				s.Push(p);
			}

			return s;
		}

        public static ushort[] StructAsUInt16Array(this object data)
        {
            // http://www.vsj.co.uk/articles/display.asp?id=501

            var size = Marshal.SizeOf(data);
            var buf = Marshal.AllocHGlobal(size);


            Marshal.StructureToPtr(data, buf, false);

            var a = new ushort[size / sizeof(ushort)];

            unsafe
            {
                var p = (ushort*)buf;
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = *p;
                    p++;
                }
            }

            Marshal.FreeHGlobal(buf);

            return a;
        }

		public static uint[] StructAsUInt32Array(this object data)
		{
			// http://www.vsj.co.uk/articles/display.asp?id=501

			var size = Marshal.SizeOf(data);
			var buf = Marshal.AllocHGlobal(size);


			Marshal.StructureToPtr(data, buf, false);

			var a = new uint[size / sizeof(int)];

			unsafe
			{
				var p = (uint*)buf;
				for (int i = 0; i < a.Length; i++)
				{
					a[i] = *p;
					p++;
				}
			}

			Marshal.FreeHGlobal(buf);

			return a;
		}

		public static byte[] StructAsByteArray(this object data)
		{
            if (data == null)
                return null;

			// http://www.vsj.co.uk/articles/display.asp?id=501

			var size = Marshal.SizeOf(data);
			var buf = Marshal.AllocHGlobal(size);


			Marshal.StructureToPtr(data, buf, false);

			var a = new byte[size / sizeof(byte)];

			unsafe
			{
				var p = (byte*)buf;
				for (int i = 0; i < a.Length; i++)
				{
					a[i] = *p;
					p++;
				}
			}

			Marshal.FreeHGlobal(buf);

			return a;
		}

		public static sbyte[] StructAsSByteArray(this object data)
		{
			// http://www.vsj.co.uk/articles/display.asp?id=501

			var size = Marshal.SizeOf(data);
			var buf = Marshal.AllocHGlobal(size);


			Marshal.StructureToPtr(data, buf, false);

			var a = new sbyte[size / sizeof(sbyte)];

			unsafe
			{
				var p = (sbyte*)buf;
				for (int i = 0; i < a.Length; i++)
				{
					a[i] = *p;
					p++;
				}
			}

			Marshal.FreeHGlobal(buf);

			return a;
		}

		public static char[] StructAsCharArray(this object data)
		{
			// http://www.vsj.co.uk/articles/display.asp?id=501

			var size = Marshal.SizeOf(data);
			var buf = Marshal.AllocHGlobal(size);


			Marshal.StructureToPtr(data, buf, false);

			var a = new char[size / sizeof(char)];

			unsafe
			{
				var p = (char*)buf;
				for (int i = 0; i < a.Length; i++)
				{
					a[i] = *p;
					p++;
				}
			}

			Marshal.FreeHGlobal(buf);

			return a;
		}

		public static double[] StructAsDoubleArray(this object data)
		{
			// http://www.vsj.co.uk/articles/display.asp?id=501

			var size = Marshal.SizeOf(data);
			var buf = Marshal.AllocHGlobal(size);


			Marshal.StructureToPtr(data, buf, false);

			var a = new double[size / sizeof(double)];

			unsafe
			{
				var p = (double*)buf;
				for (int i = 0; i < a.Length; i++)
				{
					a[i] = *p;
					p++;
				}
			}

			Marshal.FreeHGlobal(buf);

			return a;
		}

		public static int[] StructAsInt32Array(this object data)
		{
			// http://www.vsj.co.uk/articles/display.asp?id=501

			var size = Marshal.SizeOf(data);
			var buf = Marshal.AllocHGlobal(size);


			Marshal.StructureToPtr(data, buf, false);

			var a = new int[size / sizeof(int)];

			unsafe
			{
				int* p = (int*)buf;
				for (int i = 0; i < a.Length; i++)
				{
					a[i] = *p;
					p++;
				}
			}

			Marshal.FreeHGlobal(buf);

			return a;
		}



		public static MethodBase GetMethod(this Type t, MethodBase m)
		{
			return t.GetMethod(m.Name, m.GetParameters().Select(p => p.ParameterType).ToArray());
		}

		public static bool IsNativeTypeExtension(this Type z)
		{
			var za = z.ToScriptAttribute();

			return za != null && za.Implements != null && za.Implements.ToScriptAttributeOrDefault().IsNative;
		}

		public static ConstructorInfo[] GetInstanceConstructors(this Type z)
		{
			if (z == null)
				return null;

			return z.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
		}

		public static MethodInfo[] GetInstanceMethods(this Type z)
		{
			if (z == null)
				return null;

			return z.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
		}

		public static bool IsToString(this MethodInfo e)
		{
			return e.Name == "ToString" && e.GetParameters().Length == 0;
		}

		public static bool IsVirtualMethod(this MethodInfo v)
		{
			if (v.IsVirtual && v.IsHideBySig)
				if ((v.Attributes & MethodAttributes.VtableLayoutMask) == 0)
					return true;

			return false;
		}

		public static IEnumerable<MethodInfo> GetVirtualMethods(this Type t)
		{
			foreach (var v in t.GetInstanceMethods())
			{
				if (v.IsVirtualMethod())
					yield return v;
			}
		}

		public static ConstructorInfo GetStaticConstructor(this Type t)
		{
			return t.GetConstructor(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic, null, System.Type.EmptyTypes, null);
		}

		public static bool ContainsFlags(this int e, int f)
		{
			return (e & f) == f;
		}

		public static MethodInfo[] GetImplicitOperators(this Type e, Type ParameterType, Type ReturnType)
		{
			return (
						from i in e.GetMethods()
						where i.Name == "op_Implicit" && i.IsStatic &&
							(ReturnType == null ? true : i.ReturnType == ReturnType)
						let p = i.GetParameters()
						where p.Length == 1 &&
							(ParameterType == null ? true : p.Single().ParameterType == ParameterType)
						select i
					).ToArray();
		}

		public static MethodInfo[] GetExplicitOperators(this Type e, Type ParameterType, Type ReturnType)
		{
			return (
						from i in e.GetMethods()
						where i.Name == "op_Explicit" && i.IsStatic &&
							(ReturnType == null ? true : i.ReturnType == ReturnType)
						let p = i.GetParameters()
						where p.Length == 1 &&
							(ParameterType == null ? true : p.Single().ParameterType == ParameterType)
						select i
					).ToArray();
		}

	



		public static bool IsDelegate(this Type z)
		{
			return z.BaseType == typeof(MulticastDelegate);
		}

		public static bool IsOverloaded(this MethodBase m)
		{
			if (m is MethodInfo)
			{
				return m.DeclaringType.GetMethods().Count(i => i.Name == m.Name) > 1;
			}

			return false;
		}

		public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider e)
			where T : System.Attribute
		{
			return (T[])e.GetCustomAttributes(typeof(T), false);
		}

		public static T[] GetCustomAttributes<T>(this MemberInfo e)
			where T : System.Attribute
		{
			return (T[])Attribute.GetCustomAttributes(e, typeof(T), false);
		}

		public static T[] GetCustomAttributes<T>(this MemberInfo e, bool inherit)
			where T : System.Attribute
		{
			return (T[])Attribute.GetCustomAttributes(e, typeof(T), inherit);
		}

		public static T[] GetCustomAttributes<T>(this Assembly e)
			where T : System.Attribute
		{
			// "Could not load type

			return (T[])Attribute.GetCustomAttributes(e, typeof(T), false);
		}

		public static IEnumerable<XAttribute> GetPropertiesAsXAttributes(this object e)
		{
			return from p in e.GetProperties(
					BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
				   where p.Value != null
				   select new XAttribute(p.Key, p.Value);
		}

		public static Dictionary<string, object> GetProperties(this object e, BindingFlags f)
		{
			return (
				from p in e.GetType().GetProperties(f)
				let name = p.Name
				let value = p.GetValue(e, null)
				select new { name, value }
			).ToDictionary(i => i.name, i => i.value);
		}

		public static Dictionary<string, object> GetProperties(this object e)
		{
			return (
				from p in e.GetType().GetProperties()
				let name = p.Name
				let value = p.GetValue(e, null)
				select new { name, value }
			).ToDictionary(i => i.name, i => i.value);
		}

	

		public static string GetResourceFileContent(this string name)
		{
			return new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(name)).ReadToEnd();
		}

		public static string ReplaceSpace(this string e, params string[] u)
		{
			foreach (var v in u)
			{
				e = e.ReplaceSpace(v);
			}

			return e;
		}

		public static string ReplaceSpace(this string e, string u)
		{
			return e.Replace(u + " ", u);
		}

		public static string SerializeToJSON(this object e)
		{
			return ScriptCoreLib.Tools.JSONSerializer.Serialize(e);
		}

		public static string SerializeToXML(this object e)
		{
			if (e == null)
				return "";

			var s = new XmlSerializer(e.GetType());
			var w = new StringWriter();
			var z =
				XmlWriter.Create(
					w,
					new XmlWriterSettings
					{

						Indent = true,
						OmitXmlDeclaration = true

					}
				);


			s.Serialize(z, e);

			return w.ToString();
		}

		public static StreamWriter CreateFile(this DirectoryInfo dir, string filename)
		{
			FileInfo f = new FileInfo(dir.FullName + "/" + filename);

			if (f.Exists)
				f.Delete();

			StreamWriter x = new StreamWriter(new FileStream(f.FullName, FileMode.Create));


			return x;
		}


		public static ConstructorInfo GetConstructor(this Type e, params Type[] args)
		{
			return e.GetConstructor(args);
		}
	}
}
