using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using ClassLibrary1;
using ScriptCoreLib;
using System.IO;

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
        public static string StaticMethod1(this string args, string message2 = "ok")
        {
            Console.WriteLine("CLR!!: " + DateTime.Now + args);

            try
            {

                Console.WriteLine(new { Environment.CurrentDirectory });
                Console.WriteLine(new { GetExecutingAssembly = Assembly.GetExecutingAssembly().Location });
                Console.WriteLine(new { GetCallingAssembly = Assembly.GetCallingAssembly().Location });

                if (Assembly.GetEntryAssembly() == null)
                    Console.WriteLine("GetEntryAssembly is null");
                else

                    Console.WriteLine(new { GetEntryAssembly = Assembly.GetEntryAssembly().Location });
                Console.WriteLine(message2);




                Foo1.Foo1Method();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }

            return args + " ***";
        }

    }

    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    class Foo1
    {
        public string Bar;

        public static void Foo1Method()
        {
            ClassLibraryForCLR.Class1.Foo();
        }
    }
}
