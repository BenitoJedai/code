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


        //X:\jsc.svn\examples\rewrite\Test\TestAsyncFinally\TestAsync\bin\Debug>xTestAsync.exe
        //TestAsync.Program+<Invoke>d__0 <0000> ldc.i4.1
        //TestAsync.Program+<Invoke>d__0 <000e> ldloc.2
        //TestAsync.Program+<Invoke>d__0 <0012> br.s, to be optimized away
        //TestAsync.Program+<Invoke>d__0 <001b> br.s, to be optimized away
        //TestAsync.Program+<Invoke>d__0 <001d> nop
        //enter try
        //TestAsync.Program+<Invoke>d__0 <004d> ldarg.0
        //TestAsync.Program+<Invoke>d__0 <00eb> nop
        //TestAsync.Program+<Invoke>d__0 <0000> ldc.i4.1
        //TestAsync.Program+<Invoke>d__0 <000e> ldloc.2
        //TestAsync.Program+<Invoke>d__0 <0019> br.s, to be optimized away
        //TestAsync.Program+<Invoke>d__0 <006e> ldarg.0
        //TestAsync.Program+<Invoke>d__0 <008c> ldloca.s
        //exit try { ElapsedMilliseconds = 1201 }
        //TestAsync.Program+<Invoke>d__0 <00bc> leave.s, to be optimized away
        //TestAsync.Program+<Invoke>d__0 <00d6> nop
        //TestAsync.Program+<Invoke>d__0 <00eb> nop

        static async Task Invoke()
        {
            // X:\jsc.svn\examples\javascript\forms\async\AsyncFinally\AsyncFinally\ApplicationControl.cs
            // X:\jsc.svn\examples\javascript\test\TestByRefThis1\TestByRefThis1\Application.cs

            //enter try
            //exit try { ElapsedMilliseconds = 1209 }
            //enter finally
            //exit finally


            //var s = Stopwatch.StartNew();

            Console.WriteLine("enter try");

            //await Task.Delay(1200);

            //Console.WriteLine("exit try " + new { s.ElapsedMilliseconds });

            // what makes us hang?
        }
    }
}
