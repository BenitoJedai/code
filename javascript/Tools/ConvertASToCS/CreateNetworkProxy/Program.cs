using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;

using ScriptCoreLib.Shared.Lambda;

using ConvertASToCS.js.Any;

namespace CreateNetworkProxy
{
    class Program
    {
        // this tool uses script library

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var a = Assembly.GetExecutingAssembly();

                Console.WriteLine("usage: " + new FileInfo(a.Location).Name + " source_1.cs source_2.cs source_n.cs");
                Console.WriteLine();

                Console.WriteLine("Below is an example source file for this tool.");
                Console.WriteLine(
                    new StreamReader(a.GetManifestResourceStream(a.GetManifestResourceNames().FirstOrDefault(i => i.EndsWith("SharedClass1.cs")))).ReadToEnd()
                );

                return;
            }

            foreach (var a in from i in args
                              let f = new FileInfo(i)
                              where f.Exists
                              select f.FixParam(CreateSingleFile))
                a();
        }

        static void CreateSingleFile(FileInfo f)
        {
            Console.WriteLine(f.Name);

            // first we need the networking domain eg "SharedClass1"
            // it needs to define an interface IMessages

            #region Read Input
            var input = File.ReadAllText(f.FullName);

            var ns = input.IndexInfoOf("namespace ");
            var ns_END = ns.IndexInfoOf(Environment.NewLine);

            // find a class

            var type = ns_END.IndexInfoOf("class ");
            var type_EOL = type.IndexInfoOf(Environment.NewLine);

            // at this time the interface name is fixed
            var messages = type_EOL.IndexInfoOf("interface IMessages");
            var messages_BEGIN = messages.IndexInfoOf("{");
            var messages_END = messages_BEGIN.IndexInfoOf("}");
            #endregion

            var NamespaceName = ns.SubString(ns_END).Trim();
            var TypeName = type.SubString(type_EOL).Trim();
            var List = messages_BEGIN.SubString(messages_END);

            var Extension = ".cs";

            var TargetFile = new FileInfo(f.FullName.Substring(0, f.FullName.Length - Extension.Length) + ".Generated" + Extension);

            if (TargetFile.Exists)
                TargetFile.Delete();

            using (var w = new StreamWriter(TargetFile.OpenWrite()))
            {
                ProxyConverter.RenderProxyTo(
                    new ProxyProvider(List), w.Write,
                    new ConverterBase.TypeInfo
                    {
                        Namespace = NamespaceName,
                        Name = TypeName,
                        NoAttributes = true
                    }
                );


                w.WriteLine("// " + DateTime.Now);
            }
        }
    }
}
