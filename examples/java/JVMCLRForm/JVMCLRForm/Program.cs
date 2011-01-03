using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;

namespace JVMCLRForm
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..

            var filePath0 = @"C:\util\android-sdk_r08-windows\android-sdk-windows\platforms\android-8\android.jar";

            var x = new JavaArchiveReflector(new FileInfo(filePath0));


            Console.WriteLine("done");



            StringAction ListMethods =
                MethodName =>
                {
                    try
                    {
                        Console.WriteLine();
                        Console.WriteLine(MethodName);

                        // unreported exception java.lang.ClassNotFoundException; must be caught or declared to be thrown
                        var c = java.lang.Class.forName(MethodName);

                        foreach (var item in c.getMethods())
                        {
                            Console.WriteLine("method: " + item.getName());
                        }


                    }
                    catch
                    {
                        Console.WriteLine("error!");
                    }
                };

            //ListMethods("java.lang.Class");


            Console.WriteLine("jvm");

            var SwitchVM = true;

            while (SwitchVM)
            {
                SwitchVM = false;

                CLRProgram.XML = new XElement("hello", "world");
                CLRProgram.CLRMain(
                    ListMethods: ListMethods,
                    SwitchVM: () => SwitchVM = true,
                    jar: x
                );

                //if (SwitchVM)
                //{
                //    SwitchVM = false;
                //    UltraProgram.UltraMain(
                //    ListMethods: ListMethods,
                //        SwitchVM: () => SwitchVM = true
                //   );
                //}
            }
        }


    }

    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain(
            // error?
            //XElementAction xml = null,
             StringAction ListMethods = null,
             Action SwitchVM = null,
             IJavaArchiveReflector jar = null
            )
        {
            Console.WriteLine(XML);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(
                new Form1().With(f =>
                {
                    var n = f.treeView1.Nodes.Add(jar.FileNameString + " (" + jar.Count + ")");

                    var Namespaces = new Dictionary<string, TreeNode>();

                    for (int i = 0; i < jar.Count; i++)
                    {
                        Console.WriteLine("# " + i);

                        var fqn = jar.GetTypeFullName(i);

                        if (!string.IsNullOrEmpty(fqn))
                        {
                            var Namespace = fqn.TakeUntilLastOrEmpty(".");
                            var Name = fqn.SkipUntilLastIfAny(".");

                            if (!Namespaces.ContainsKey(Namespace))
                            {
                                Namespaces[Namespace] = n.Nodes.Add(Namespace);
                            }

                            Namespaces[Namespace].Nodes.Add(Name);

                            //n.Nodes.Add(fqn);
                        }
                    }

                    f.button2.Click +=
                        delegate
                        {
                            if (SwitchVM != null)
                                SwitchVM();

                            f.Close();
                        };
                    f.button1.Click +=
                        delegate
                        {
                            if (ListMethods != null)
                                ListMethods(f.textBox1.Text);
                        };
                }
            ));
        }
    }

    //static class UltraProgram
    //{

    //    public static void UltraMain(
    //         StringAction ListMethods = null,
    //         Action SwitchVM = null
    //        )
    //    {
    //        Application.EnableVisualStyles();
    //        Application.SetCompatibleTextRenderingDefault(false);

    //        // no generics in java yet..
    //        var f = new Form1();


    //        f.button2.Click +=
    //            delegate
    //            {
    //                if (SwitchVM != null)
    //                    SwitchVM();

    //                f.Close();
    //            };

    //        f.button1.Click +=
    //            delegate
    //            {
    //                ListMethods(f.textBox1.Text);
    //            };

    //        Application.Run(f);
    //    }
    //}
}
