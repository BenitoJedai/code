using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TestJVMCLRException
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // once this syntax thingy compiles            // lets run it under appengine, and android too..
            try
            {
                TryMain();
            }
            catch (Exception ex)
            {
                // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Exception.cs
                MainErrorHandler(ex);

            }

        }

        private static void MainErrorHandler(Exception ex)
        {
            //            System.Object, mscorlib, Version = 4.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089
            //enter MainErrorHandler
            //{
            //                ex = System.Exception: ???
            //                 at TestJVMCLRException.Program.TryMain() in X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRException\TestJVMCLRException\Program.cs:line 84
            //   at TestJVMCLRException.Program.Main(String[] args) in X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRException\TestJVMCLRException\Program.cs:line 30 }
            //            { Message = ??? }
            //            {
            //                StackTrace = at TestJVMCLRException.Program.TryMain() in X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRException\TestJVMCLRException\Program.cs:line 84
            //               at TestJVMCLRException.Program.Main(String[] args) in X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRException\TestJVMCLRException\Program.cs:line 30 }
            //            exit MainErrorHandler
            //Press any key to continue . . .

            Console.WriteLine("enter MainErrorHandler");

            try
            {

                Console.WriteLine(new { ex });
                // /* 0000:0003 */   /* let */{{ ex = java.lang.RuntimeException }}

                Console.WriteLine(new
                {
                    ex.Message
                });

                Console.WriteLine(new
                {
                    ex.StackTrace
                });
            }
            catch (Exception ex2)
            {
                // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Exception.cs
                Console.WriteLine(new { ex2 });

            }
            Console.WriteLine("exit MainErrorHandler");
        }

        // jsc needs to see args to make Main into main for javac..


        // see also>
        // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

        private static void TryMain()
        {

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            throw new Exception("???");


            //CLRProgram.CLRMain();
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
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );



            MessageBox.Show("click to close");

        }
    }


}
