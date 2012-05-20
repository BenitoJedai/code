using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Archive
{
    public static class JavaArchiveExtensions
    {
        static Dictionary<string, string[]> FileContentLookup = new Dictionary<string, string[]>();


        public static string ResolveJavaArchiveLoadRequest(string context, string name, FileInfo[] ImplicitReferences)
        {
            var list = ContextToFileArray(context);

            list = list.Concat(ImplicitReferences.Select(k => k.FullName)).Distinct().ToArray();

            var r = name.Replace(".", "/") + ".class";

            foreach (var item in list)
            {
                var c = default(string[]);

                if (!FileContentLookup.ContainsKey(item))
                {
                    Console.WriteLine(".jar " + Path.GetFileNameWithoutExtension(item));

                    var zip = ZIPArchive.GetFiles(item);

                    FileContentLookup[item] = zip.Select(k => k.Name).ToArray();    
                }

                c = FileContentLookup[item];

                var u = c.FirstOrDefault(k => k == r);

                if (u != null)
                    return item;
            }

            //MessageBox.Show("@ JavaArchiveResolve: " + name);

            //if (name == "com.amazonaws.AmazonWebServiceRequest")
            //    return @"C:\util\aws-android-sdk-0.2.0\lib\aws-android-sdk-0.2.0-core.jar";

            return null;
        }

        private static string[] ContextToFileArray(string context)
        {
            var ContextPath = Path.GetDirectoryName(context);
            var WiderContextPath = Directory.GetParent(ContextPath).FullName;

            var list = Directory.GetFiles(WiderContextPath, "*.jar", SearchOption.AllDirectories).OrderByDescending(k => Path.GetDirectoryName(k) == ContextPath).ToArray();
            return list;
        }
    }
}
