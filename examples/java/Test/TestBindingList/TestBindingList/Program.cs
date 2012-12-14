using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ScriptCoreLibJava.Extensions;
using System.ComponentModel;

namespace TestBindingList
{


    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Console.WriteLine("" + typeof(object).FullName);
                Console.WriteLine("vm: " + typeof(object).FullName);
                //Process is terminated due to StackOverflowException.

                var b = new BindingList<object>();

                b.ListChanged +=
                    delegate
                    {
                        Console.WriteLine("ListChanged");
                    };

                b.Add(new object());


            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
            }

            CLRProgram.CLRMain();
        }
    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running inside CLR");

            Console.WriteLine(Path.GetFileName(Assembly.GetExecutingAssembly().Location));

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(Path.GetFullPath(item.Location) + " types: " + item.GetTypes().Length);
            }

            Console.ReadLine();
            //Debugger.Break();
        }
    }
}
