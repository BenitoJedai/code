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

            var x = new CLRProgram();
        }

        [SwitchToCLRContext]
        sealed class CLRProgram
        {
            object __Field1;


            public CLRProgram()
            {
                // CLR is now loaded into JVM via JNI
            }

            public static string StaticMethod1(string e)
            {
                return "hello from CLR - " + e;
            }

            public string Method1(string e)
            {
                // method code is now running inside CLR

                return "hello from CLR - " + e;
            }
        }
    }
}
