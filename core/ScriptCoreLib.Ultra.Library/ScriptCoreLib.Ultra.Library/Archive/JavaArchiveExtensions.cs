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
            Func<string[], string> f =
                list =>
                {
                    var r = name.Replace(".", "/") + ".class";

                    foreach (var item in list)
                    {
                        var c = default(string[]);

                        if (!FileContentLookup.ContainsKey(item))
                        {
                            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140209

                            Console.WriteLine("JavaArchiveExtensions.ResolveJavaArchiveLoadRequest " +
                                new
                                {
                                    jar = Path.GetFileNameWithoutExtension(item),
                                    context,
                                    name
                                }
                            );

                            var zip = ZIPArchive.GetFiles(item);

                            FileContentLookup[item] = zip.Select(k => k.Name).ToArray();
                        }

                        c = FileContentLookup[item];

                        var u = c.FirstOrDefault(k => k == r);

                        if (u != null)
                            return item;
                    }

                    return null;
                };

            var x0 = f(ImplicitReferences.Select(k => k.FullName).ToArray());

            if (x0 != null)
                return x0;

            var x1 = f(ContextToFileArray(context));



            //MessageBox.Show("@ JavaArchiveResolve: " + name);

            //if (name == "com.amazonaws.AmazonWebServiceRequest")
            //    return @"C:\util\aws-android-sdk-0.2.0\lib\aws-android-sdk-0.2.0-core.jar";

            return x1;
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
