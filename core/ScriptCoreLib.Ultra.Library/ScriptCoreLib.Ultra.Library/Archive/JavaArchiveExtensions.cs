using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Archive
{
    public static class JavaArchiveExtensions
    {
        public static string ResolveJavaArchiveLoadRequest(string context, string name)
        {
            var list = Directory.GetFiles(Path.GetDirectoryName(context), "*.jar");

            var r = name.Replace(".", "/") + ".class";

            foreach (var item in list)
            {
                Console.WriteLine(".jar " + Path.GetFileNameWithoutExtension(item));

                var zip = ZIPArchive.GetFiles(item);

                var u = zip.FirstOrDefault(k => k.Name == r);

                if (u != null)
                    return item;
            }

            //MessageBox.Show("@ JavaArchiveResolve: " + name);

            //if (name == "com.amazonaws.AmazonWebServiceRequest")
            //    return @"C:\util\aws-android-sdk-0.2.0\lib\aws-android-sdk-0.2.0-core.jar";

            return null;
        }
    }
}
