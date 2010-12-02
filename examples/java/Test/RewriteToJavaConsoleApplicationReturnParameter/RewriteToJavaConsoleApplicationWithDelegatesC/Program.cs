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
            {
                var x = "from jvm".Method1(ExtensionsToSwitchToCLRContext2.Method2, "");

                Console.WriteLine("x: " + x);
            }

            {
                var x = "from jvm".Method1(ExtensionsToSwitchToCLRContext2.Method2);

                Console.WriteLine("x: " + x);
            }


        }
    }

    delegate Data1 Method1Func(string e);

    public class Data1
    {
        public string U;

        public static implicit operator Data1(string e)
        {
            return new Data1 { U = e };
        }

        public override string  ToString()
        {
 	        return this.U;
        }
    }

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        public static string Method1(
            this string data,
            Method1Func Handler1 = null,
            string n = null
        )
        {
            if (n == null)
                Console.WriteLine("n is null");
            else
                Console.WriteLine("n is not null");

            Console.WriteLine("Method1: " + data);

            if (Handler1 != null)
            {
                var y = Handler1(data + " from clr");

                Console.WriteLine("y: " + y);
            }

            return "!";
        }
    }


    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext2
    {
        public static Data1 Method2(
            this string data
        )
        {
            Console.WriteLine("Method2: " + data);

            return "clr";
        }
    }
}
