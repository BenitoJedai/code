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

namespace JVMCLRThreadPool
{

    static class Program
    {

        // jsc should keep volatile in sync between threads
        // while threadLocal would not be in sync
        static volatile int state = 1;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // X:\jsc.svn\examples\c\Test\TestFunc\TestFunc\Program.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            //ExecutionContext.Run(

            Console.WriteLine("at QueueUserWorkItem " + new
            {
                Thread.CurrentThread.ManagedThreadId,

                TaskScheduler.Current.Id

            });

            var done = new AutoResetEvent(false);

            //at QueueUserWorkItem { ManagedThreadId = 1 }
            //at QueueUserWorkItem { state = , ManagedThreadId = 4 }
            //at QueueUserWorkItem { state = , ManagedThreadId = 5 }



            ThreadPool.RegisterWaitForSingleObject(
                done,
                new WaitOrTimerCallback(
                    (object state, bool timedOut) =>
                    {
                        Console.WriteLine("at QueueUserWorkItem " + new { state, TaskScheduler.Current.Id, Thread.CurrentThread.ManagedThreadId });

                        // how do we get back to UI thread?
                    }
                ),
                null,
                -1,
                true
            );

            //at QueueUserWorkItem { ManagedThreadId = 1, Id = 1 }
            //at QueueUserWorkItem { state = , Id = 1, ManagedThreadId = 4 }
            //at QueueUserWorkItem { state = , Id = 1, ManagedThreadId = 5 }


            // Task Run
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(
                    state =>
                    {
                        Console.WriteLine("at QueueUserWorkItem " + new { state, TaskScheduler.Current.Id, Thread.CurrentThread.ManagedThreadId });



                    }
                )
            );



            done.Set();


            Thread.Yield();
            //Thread.SpinWait(32);
            Thread.Sleep(100);

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
