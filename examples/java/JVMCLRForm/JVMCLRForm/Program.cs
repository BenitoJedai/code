using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System.Xml.Linq;

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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain(
             StringAction ListMethods = null,
             Action SwitchVM = null
            )
        {
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
