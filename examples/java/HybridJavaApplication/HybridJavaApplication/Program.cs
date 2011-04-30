using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

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
        }
    }
}
