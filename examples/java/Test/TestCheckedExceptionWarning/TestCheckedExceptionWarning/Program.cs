using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;

namespace TestCheckedExceptionWarning
{
    class FooX : TestCheckedExceptionWarning.Library.Foo
    {
        // http://stackoverflow.com/questions/3465465/how-to-use-java-style-throws-keyword-in-c

        [ScriptMethodThrows(typeof(java.io.IOException))]
        public static string BarX()
        {
            return "BarX";
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..

            Console.WriteLine("hi!");

            Test2();

            try
            {
                Test();
            }
            catch (Exception ex)
            {
                // toString will return JVM style value.

                Console.WriteLine("error!\n\n" + new { ex.Message, ex.StackTrace } + "\n\n");
            }

            System.Console.WriteLine("jvm " + typeof(object).FullName);


            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
            );

        }

        private static void Test2()
        {
            FooX.BarX();
        }

        private static void Test()
        {
            var SourceType = typeof(TestCheckedExceptionWarning.Library.Foo);


            SourceType.GetMethods().WithEach(
                SourceMethod =>
                {
                    Console.WriteLine(
                        new
                        {
                            SourceType,
                            SourceMethod
                        }
                    );

                    SourceMethod.ToMethod().With(
                        JVMMethod =>
                        {
                            JVMMethod.getExceptionTypes().WithEach(
                                c =>
                                {
                                    Console.WriteLine("  throws " + c.ToType().FullName);
                                }
                            );
                        }
                    );
                }
            );

        }


    }

    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain(
             StringAction ListMethods = null
            )
        {
            System.Console.WriteLine(XML);

            MessageBox.Show("it works?!?");
        }
    }

}
