using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAsyncFinally
{
    class Program
    {
        static void Main(string[] args)
        {
            // Additional information: RunSynchronously may not be called 
            // on a task not bound to a delegate, such as the task returned
            // from an asynchronous method.

            //Invoke().RunSynchronously();

            var i = Invoke();

            i.Wait();
        }

        static async Task Invoke()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140524

            // X:\jsc.svn\examples\javascript\forms\async\AsyncFinally\AsyncFinally\ApplicationControl.cs

            //enter try
            //exit try { ElapsedMilliseconds = 1209 }
            //enter finally
            //exit finally


            try
            {
                //var s = Stopwatch.StartNew();

                Console.WriteLine("enter try");

                //await Task.Delay(1200);

                //Console.WriteLine("exit try " + new { s.ElapsedMilliseconds });
            }
            finally
            {
                Console.WriteLine("enter finally");

                // cannot do that before roslyn
                // Error	1	Cannot await in the body of a finally clause	X:\jsc.svn\examples\rewrite\Test\TestAsyncFinally\TestAsyncFinally\Program.cs	50	17	TestAsyncFinally
                await Task.Delay(200);
            }
            Console.WriteLine("exit finally");
        }
    }
}
