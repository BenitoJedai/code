using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;

namespace RewriteToJavaConsoleApplicationWithDelegatesC
{
    class Program
    {
        public static void Main(string[] args)
        {

            var x = "from jvm".Method1(ExtensionsToSwitchToCLRContext2.Method2);

            Console.WriteLine("x: " + x);
        }
    }

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        public static string Method1(
            this string data,
            StringAction Handler1 = null
        )
        {
            Console.WriteLine("Method1: " + data);

            if (Handler1 != null)
                Handler1(data + " from clr");

            return "!";
        }
    }


    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext2
    {
        public static void Method2(
            this string data
        )
        {
            Console.WriteLine("Method2: " + data);
        }
    }
}
