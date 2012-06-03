using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace TestGetFields
{
    class Foo1 : Foo0
    {
        public static int CLOSE_CURRENT_RESULT = 0;
    }

    class Foo2
    {
        public static int X = 0;
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("this code is running inside JVM");
            Console.WriteLine();

            TestForBothRuntimes.XMain();
            CLRProgram.CLRMain();
        }
    }

    class TestForBothRuntimes
    {
        public static void XMain()
        {
            Console.WriteLine("TestForBothRuntimes");
            Console.WriteLine();

            var Foo2 = new Foo2();

            var t = typeof(Foo2);

            var a = t.GetFields();

            foreach (var item in a)
            {
                Console.WriteLine("a: " + item);
            }

            Console.WriteLine("/TestForBothRuntimes");
            Console.WriteLine();
        }
    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running inside CLR");

            TestForBothRuntimes.XMain();
        }
    }
}
