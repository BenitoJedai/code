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
    public class JavaArchiveReflector : IEnumerable
    {
        public class Entry
        {
            public string Name;

            public Type Type;
        }

        public delegate DynamicEnumerator GetDynamicEnumeratorFunc();
        GetDynamicEnumeratorFunc GetDynamicEnumerator;

        public delegate Entry GetNextEntry();



        public class DynamicEnumerator : IEnumerator
        {
            public object Current
            {
                get;
                set;
            }

            public GetNextEntry GetNextEntry;
            public static implicit operator DynamicEnumerator(GetNextEntry e)
            {
                return new DynamicEnumerator { GetNextEntry = e };
            }

            public bool MoveNext()
            {
                if (GetNextEntry == null)
                    return false;

                this.Current = GetNextEntry();

                if (this.Current == null)
                {
                    this.GetNextEntry = null;
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        public JavaArchiveReflector(FileInfo jar)
        {
            var clazzLoader = default(URLClassLoader);

            try
            {
                var filePath = "jar:file://" + jar.FullName + "!/";
                var url = new java.io.File(filePath).toURL();


                clazzLoader = new URLClassLoader(new URL[] { url });
            }
            catch
            {
            }



            this.GetDynamicEnumerator = () =>
            {

                var zip = default(ZipInputStream);

                try
                {
                    zip = new ZipInputStream(new FileInputStream(jar.FullName));
                }
                catch
                {
                }

                return (GetNextEntry)
                    delegate
                    {
                        if (zip == null)
                            return null;

                        var e = default(ZipEntry);

                        try
                        {
                            e = zip.getNextEntry();
                        }
                        catch
                        {
                        }

                        if (e == null)
                            return null;



                        var n = new Entry { Name = e.getName() };

                        if (clazzLoader != null)
                            if (n.Name.EndsWith(".class"))
                            {
                                var fqn = n.Name.Substring(0, n.Name.Length - ".class".Length).Replace("/", ".");

                                var c = default(java.lang.Class);

                                try
                                {
                                    c = clazzLoader.loadClass(fqn);
                                }
                                catch
                                {
                                }

                                n.Type = c.ToType();
                            }

                        return n;
                    };
            };
        }

        public IEnumerator GetEnumerator()
        {
            return GetDynamicEnumerator();
        }
    }

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

            foreach (JavaArchiveReflector.Entry item in x)
            {
                if (item.Type != null)
                {
                    Console.WriteLine(item.Type.FullName);

                    break;

                }
            }

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
                    SwitchVM: () => SwitchVM = true
                );

                if (SwitchVM)
                {
                    SwitchVM = false;
                    UltraProgram.UltraMain(
                    ListMethods: ListMethods,
                        SwitchVM: () => SwitchVM = true
                   );
                }
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
             Action SwitchVM = null
            )
        {
            Console.WriteLine(XML);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(
                new Form1().With(f =>
                {

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

    static class UltraProgram
    {

        public static void UltraMain(
             StringAction ListMethods = null,
             Action SwitchVM = null
            )
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // no generics in java yet..
            var f = new Form1();


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
                    ListMethods(f.textBox1.Text);
                };

            Application.Run(f);
        }
    }
}
