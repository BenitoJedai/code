using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAsync
{
    //struct __Invoke
    //{
    //    public int state;

    //    public static void __forwardref(ref __Invoke that)
    //    {

    //    }


    //    public void MoveNext()
    //    {
    //        //var loc0 = this;


    //        //0:18ms enter MoveNext { state = 5 }
    //        //0:18ms exit __forwardref { state = 6 }
    //        //0:18ms exit MoveNext { state = 6 }


    //        __forwardref(ref this);
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            var i = Invoke();

            i.Wait();
        }

        static async Task Invoke()
        {
            // X:\jsc.svn\examples\javascript\forms\async\AsyncFinally\AsyncFinally\ApplicationControl.cs
            // X:\jsc.svn\examples\javascript\test\TestByRefThis1\TestByRefThis1\Application.cs

            //enter try
            //exit try { ElapsedMilliseconds = 1209 }
            //enter finally
            //exit finally


            var s = Stopwatch.StartNew();

            Console.WriteLine("enter try");

            await Task.Delay(1200);

            Console.WriteLine("exit try " + new { s.ElapsedMilliseconds });
        }
    }
}
