using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abstractatech.Async
{
    // X:\jsc.svn\examples\rewrite\Test\TestSimpleAwait\TestSimpleAwait\Program.cs

    class Program
    {
        static void Main(string[] args)
        {
            // see also
            // http://www.nuget.org/packages/Microsoft.CompilerServices.AsyncTargetingPack

            Task.Factory.StartNew(
                async delegate
                {
                    Console.WriteLine("before WebMethod2");
                    var x = await X.WebMethod2();
                    Console.WriteLine("after WebMethod2");

                    var y = await x;

                    Console.WriteLine("any key to exit " + new { y });
                    Console.ReadKey();
                }
            ).Wait();



        }



    }

    class X
    {
        public static async Task<Task<int>> WebMethod2()
        {
            var s = new TaskCompletionSource<int>();

            new Thread(
                delegate()
                {
                    Thread.Sleep(1500);

                    s.SetResult(5);
                }
            ).Start();

            //await s.Task;

            return s.Task;
        }
    }

}
