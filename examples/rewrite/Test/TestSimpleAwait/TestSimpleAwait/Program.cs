using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestSimpleAwait
{
    // { ExceptionObject = System.TypeLoadException: Type 'System.Runtime.CompilerServices.AsyncTaskMethodBuilder' from assembly 'TestSimpleAwait.merged, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' tried to override method 'System.Runtime.CompilerServices.IAsyncMethodBuilder.PreBoxInitialization' but does not implement or inherit that method.
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121106-await

    class Program
    {
        static void Main(string[] args)
        {
            Z.Foo().Wait();


            Console.WriteLine("any key to exit ");
            Console.ReadKey();
        }


    }

    class Z
    {
        public static async Task Foo()
        {
            Console.WriteLine("before await ");

            await X.WebMethod2();

            Console.WriteLine("after await ");
        }
    }

    class X
    {
        public static async Task WebMethod2()
        {
            var s = new TaskCompletionSource<object>();

            new Thread(
                delegate()
                {
                    Thread.Sleep(1500);

                    s.SetResult(null);
                }
            ).Start();

            await s.Task;
        }
    }
}
