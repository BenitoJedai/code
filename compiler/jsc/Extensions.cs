using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace jsc //.Extensions
{
    static class Extensions
    {
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
