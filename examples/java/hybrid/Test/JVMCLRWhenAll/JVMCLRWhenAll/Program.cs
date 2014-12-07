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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRWhenAll
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            //java.lang.Object, rt
            //{{ x = {{ ProcessorCount = 4 }} }}

            var x = new { Thread.CurrentThread.ManagedThreadId, Environment.ProcessorCount };

            Console.WriteLine(new { x });

            //{ x = { ManagedThreadId = 1, ProcessorCount = 4 } }
            //worker1 { ManagedThreadId = 3 }
            //WhenAll { ManagedThreadId = 3, ProcessorCount = 4 }
            // X:\jsc.svn\examples\javascript\appengine\XSLXAssetWithXElement\XSLXAssetWithXElement\ApplicationWebService.cs

            var t0 = Task.Run(
                //async 
                delegate
                {
                    Console.WriteLine("worker1 start " + new { Thread.CurrentThread.ManagedThreadId });

                    Thread.Sleep(1000);
                    Console.WriteLine("worker1 end " + new { Thread.CurrentThread.ManagedThreadId });
                }
            );


            //{ x = { ManagedThreadId = 1, ProcessorCount = 4 } }
            //worker1 start { ManagedThreadId = 3 }
            //worker1 end { ManagedThreadId = 3 }
            //WhenAll { ManagedThreadId = 4, ProcessorCount = 4 }
            //after Wait { ManagedThreadId = 1, ProcessorCount = 4 }


            // CLR seems to continue the worker thread to continuewith

            var ttt = Task.WhenAll(t0).ContinueWith(
                tt =>
                {
                    Console.WriteLine(
                        "WhenAll " +
                        new { Thread.CurrentThread.ManagedThreadId, Environment.ProcessorCount }
                    );


                }
            );


            ttt.Wait();

            Console.WriteLine(
                   "after Wait " +
                   new { Thread.CurrentThread.ManagedThreadId, Environment.ProcessorCount }
               );

            //{ x = { ManagedThreadId = 1, ProcessorCount = 4 } }
            //worker1 start { ManagedThreadId = 8 }
            //worker1 end { ManagedThreadId = 8 }
            //WhenAll { ManagedThreadId = 8, ProcessorCount = 4 }
            //after Wait { ManagedThreadId = 1, ProcessorCount = 4 }

            CLRProgram.CLRMain();
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
