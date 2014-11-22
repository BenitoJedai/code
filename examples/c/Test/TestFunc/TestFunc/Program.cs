using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;
using System.IO;
using ScriptCoreLibNative.BCLImplementation.System.Reflection;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestFunc
{
    //[Script]
    class MyClass
    {
        public string Field1;

        public void Invoke()
        {
            Console.WriteLine(Field1);
        }
    }

    //[Script]
    public unsafe class NativeClass1 : ScriptCoreLibNative.IAssemblyReferenceToken
    {
        // http://msdn.microsoft.com/en-us/library/windows/desktop/ms679277(v=vs.85).aspx

        //Setting environment for using Microsoft Visual Studio 2010 x86 tools.
        //ERROR: Cannot determine the location of the VS Common Tools folder.



        [Script(NoDecoration = true)]
        public static int main()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141116
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141122


            Console.WriteLine("20141104 does this device support speaker music?");

            // System.Collections.Generic.EqualityComparer`1[<foo>j__TPar] for Boolean Equals(<foo>j__TPar, <foo>j__TPar) used at
            //     LP<>f__AnonymousType0_1 type0_10;
            var xx = new { foo = "hello " };

            // we dont do virtuals yet
            // yet. why wouldnt jsc call the correct ToString instead without virtual?
            Console.WriteLine(xx.ToString());


            Func<string, Action> y = o =>
            {

                Console.WriteLine("hello there:");
                Console.WriteLine((string)o);
                //Console.WriteLine("hello there");

                //return "done";

                return delegate
                {
                    Console.WriteLine("cannot return long, but can return delegate!");

                };
            };

            // threadpool!


            var x = y("goo, delegate invoke now works?");

            //Console.WriteLine(x);

            x();


            Action a = new MyClass { Field1 = "Field1" }.Invoke;


            if ((object)a.Target == null)
            {
                Console.WriteLine("target is null");
            }
            else
            {
                Console.WriteLine("target is not null");
            }

            a();




            Console.Beep(1200, duration: 2000);

            return 0;
        }



    }



    [Script(IsNative = true)]
    class Program
    {
        static int Main(string[] args)
        {

            NativeClass1.main();
            return 0;
        }
    }
}
