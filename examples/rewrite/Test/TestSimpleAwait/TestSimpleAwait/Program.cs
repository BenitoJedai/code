using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestSimpleAwait
{
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
