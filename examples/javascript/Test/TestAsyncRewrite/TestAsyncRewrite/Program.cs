using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAsyncRewrite
{
    class Program
    {

        // switch rewrite hangs for wait, why?
        //X:\jsc.svn\examples\javascript\Test\TestAsyncRewrite\TestAsyncRewrite\bin\Debug>TestAsyncRewrite.exe
        //enter goo
        //exit goo
        //exit Main

        //X:\jsc.svn\examples\javascript\Test\TestAsyncRewrite\TestAsyncRewrite\bin\Debug>TestAsyncRewrite.ZRewrite.exe
        //enter goo
        //exit goo








        static void Main(string[] args)
        {
            //enter goo
            //exit goo
            //exit Main


            goo().Wait();

            Console.WriteLine("exit Main");

            //Console.ReadKey(true);
        }

        public async static Task goo()
        {
            Console.WriteLine("enter goo");

            await Task.Delay(100);

            Console.WriteLine("exit goo");
        }
    }
}
