using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace HybridJavaApplication
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("this code is running inside JVM");

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

            // if jsc supported pdb rewrite we could have a break point over here!

            MessageBox.Show("hello");

            Console.WriteLine("done!");
        }
    }
}
