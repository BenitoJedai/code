using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace HybridJavaApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("this code is running inside JVM");
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
