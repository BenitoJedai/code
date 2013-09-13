using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAsyncRewrite
{
    //struct foo
    //{
    //    public int x;
    //}

    //static class X
    //{
    //    public static void WriteHashCode<T>(ref T x)
    //    {
    //        var HashCode = x.GetHashCode();

    //        Console.WriteLine(new { HashCode });
    //    }

    //}
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

        // the problem happens with debug build of jsc





        static void Main(string[] args)
        {
            //enter goo
            //exit goo
            //exit Main

            //foo foo1;
            //foo1.x = 7;
            //X.WriteHashCode(ref foo1);

            //foo foo2 = foo1;
            //foo1.x = 2;
            //X.WriteHashCode(ref foo1);
            //X.WriteHashCode(ref foo2);

            ////{ HashCode = 346948955 }
            ////{ HashCode = 346948958 }
            ////{ HashCode = 346948955 }


            //Console.WriteLine(new { foo2.x });


            goo().Wait();

            Console.WriteLine("exit Main");

            //Console.ReadKey(true);
        }

        public async static Task goo()
        {
            //Console.WriteLine("enter goo");

            //await Task.Delay(100);

            Console.WriteLine("exit goo");
        }
    }
}
