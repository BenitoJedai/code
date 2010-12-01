using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using System.Collections;
using System.Runtime.InteropServices;

namespace RewriteToJavaConsoleApplicationWithDelegatesB
{
    class Program
    {
        public static void Main(string[] args)
        {
            var h = new Hashtable();
            var a = Marshal.AllocHGlobal(1);
            var b = Marshal.AllocHGlobal(1);

            h[a] = "hello";
            h[b] = "world";


            Console.WriteLine((string)h[a]);
            Console.WriteLine((string)h[b]);

            var x = ExtensionsToSwitchToCLRContext.Method1(
                Handler1:
                    text =>
                    {
                        Console.WriteLine("Handler1: " + text);
                    },
                Handler2:
                    text =>
                    {
                        Console.WriteLine("Handler2: " + text);
                    }
            );

            Console.WriteLine("x: " + x);
        }
    }

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        public static string Method1(
            StringAction Handler1,
            StringAction Handler2,
            string P1 = "P1",
            string P2 = "p2"
        )
        {
            Console.WriteLine("Method1 0");

            if (Handler1 != null)
                Handler1("A " + P1);
            else
                Console.WriteLine("Handler1 is null - " + P1);

            Console.WriteLine("Method1 1");

            if (Handler2 != null)
                Handler2("B" + P2);
            else
                Console.WriteLine("Handler2 is null - " + P2);

            Console.WriteLine("Method1 2");

            return "!";
        }
    }
}
