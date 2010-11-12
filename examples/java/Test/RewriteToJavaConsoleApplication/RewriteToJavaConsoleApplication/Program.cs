using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using ClassLibrary1;
using ScriptCoreLib;

namespace RewriteToJavaConsoleApplication
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hello world");
            Class1.Foo();

            //var x = new CLRProgram();
            ExtensionsToSwitchToCLRContext.StaticMethod1("hello");

            Console.WriteLine("jvm".StaticMethod1());
        }

        
    }


    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        //object __Field1;


        //public CLRProgram()
        //{
        //    // CLR is now loaded into JVM via JNI
        //}


        //public string Method1(string e)
        //{
        //    // method code is now running inside CLR

        //    return "hello from CLR - " + e;
        //}

        public static string StaticMethod1(this string args, string message2 = "ok")
        {
            Console.WriteLine("CLR!!: " + DateTime.Now + args);

            //Console.WriteLine("Enter!");
            //Console.ReadLine();
            Console.WriteLine(message2);

            return args + " ***";
        }

    }
}
