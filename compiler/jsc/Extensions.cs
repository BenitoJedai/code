using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using ScriptCoreLib;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace jsc //.Extensions
{
    static class Extensions
    {
        public static T PopOrDefault<T>(this Stack<T> e)
        {
            if (e.Count > 0)
                return e.Pop();

            return default(T);
        }

        public static Stack<Type> DeclaringTypesToStack(this Type e)
        {
            var s = new Stack<Type>();

            var p = e;

            while (p.DeclaringType != null)
            {
                p = p.DeclaringType;

                s.Push(p);
            }

            return s;
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

        public static ScriptAttribute ToScriptAttribute(this ICustomAttributeProvider p)
        {
            return ScriptAttribute.OfProvider(p);
        }

        public static ScriptAttribute ToScriptAttributeOrDefault(this ICustomAttributeProvider p)
        {
            return ScriptAttribute.OfProvider(p) ?? new ScriptAttribute();
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

        public static T[] GetCustomAttributes<T>(this Assembly e)
            where T : System.Attribute
        {
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
