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

namespace JVMCLRTaskStartNew
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


            //TaskScheduler.FromCurrentSynchronizationContext().


            {
                var s = new TaskCompletionSource<string>(
                    //TaskCreationOptions.AttachedToParent


                    );

                //y { ManagedThreadId = 3, IsCompleted = True, Result = done }
                //x { ManagedThreadId = 4, IsCompleted = True, Result = done }
                //{ ManagedThreadId = 6 } and then some

                var y = s.Task.ContinueWith(
                    t =>
                    {
                        Console.WriteLine("y " + new { Thread.CurrentThread.ManagedThreadId, t.IsCompleted, t.Result });
                    }
                    , TaskContinuationOptions.ExecuteSynchronously
                );

                var x = s.Task.ContinueWith(
                    t =>
                    {
                        Console.WriteLine("x " + new { Thread.CurrentThread.ManagedThreadId, t.IsCompleted, t.Result });
                    }
                    , TaskContinuationOptions.ExecuteSynchronously
                );

                var z = x.ContinueWith(
                    t =>
                    {
                        Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId } + " and then some");

                    }
                    , TaskContinuationOptions.ExecuteSynchronously
                );

                //s.Task.
                s.SetResult("done");

                //            y { ManagedThreadId = 1, IsCompleted = true, Result = done }
                //x { ManagedThreadId = 1, IsCompleted = true, Result = done }
                //{ ManagedThreadId = 1 } and then some
            }

            {
                var s = new TaskCompletionSource<string>(
                    //TaskCreationOptions.AttachedToParent


                    );

                //y { ManagedThreadId = 4, IsCompleted = True, Result = done }
                //x { ManagedThreadId = 3, IsCompleted = True, Result = done }
                //{ ManagedThreadId = 3 } and then some

                var y = s.Task.ContinueWith(
                    t =>
                    {
                        Console.WriteLine("y " + new { Thread.CurrentThread.ManagedThreadId, t.IsCompleted, t.Result });
                    }
                    , TaskContinuationOptions.None
                );

                var x = s.Task.ContinueWith(
                    t =>
                    {
                        Console.WriteLine("x " + new { Thread.CurrentThread.ManagedThreadId, t.IsCompleted, t.Result });
                    }
                    , TaskContinuationOptions.None
                );

                var z = x.ContinueWith(
                    t =>
                    {
                        Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId } + " and then some");

                    }
                    , TaskContinuationOptions.None
                );

                //s.Task.
                s.SetResult("done");

                Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId } + " before wait");

                x.Wait();

                //{ ManagedThreadId = 1 } before wait
                //y { ManagedThreadId = 8, IsCompleted = true, Result = done }
                //x { ManagedThreadId = 9, IsCompleted = true, Result = done }
                //{ ManagedThreadId = 9 } and then some
                //{ ManagedThreadId = 1 } after wait

                Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId } + " after wait");

                //{ ManagedThreadId = 1 } before wait
                //y { ManagedThreadId = 4, IsCompleted = True, Result = done }
                //x { ManagedThreadId = 3, IsCompleted = True, Result = done }
                //{ ManagedThreadId = 4 } and then some
                //{ ManagedThreadId = 1 } after wait
            }



            //Additional information: RunSynchronously may not be called on a task not bound to a delegate, such as the task returned from an asynchronous method.
            //s.Task.RunSynchronously();

            // Additional information: Start may not be called on a task that has completed.
            //s.Task.Start();

            //TaskScheduler.FromCurrentSynchronizationContext().When

            //s.Task.Wait();
            //z.Wait();

            //Task.Factory.StartNew(


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
