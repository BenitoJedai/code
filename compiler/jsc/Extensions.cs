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

namespace jsc //.Extensions
{
    static class Extensions
    {
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
