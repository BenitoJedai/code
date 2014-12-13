using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;
using System.IO;
using ScriptCoreLibNative.BCLImplementation.System.Reflection;
using System.Reflection;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestTaskRun
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
            // http://msdn.microsoft.com/en-us/library/3y1sfaz2.aspx

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141116
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141122

            // http://www.sockets.com/winsock.htm#Bind
            // http://msdn.microsoft.com/en-us/library/wf2w9f6x.aspx

            Console.WriteLine("20141104 does this device support speaker music?");

            // System.Collections.Generic.EqualityComparer`1[<foo>j__TPar] for Boolean Equals(<foo>j__TPar, <foo>j__TPar) used at
            //     LP<>f__AnonymousType0_1 type0_10;
            var xx = new { foo = "xhello from outer scope" };

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
                    Console.WriteLine("cannot return long, but can return delegate! " + xx.foo);

                };
            };

            // threadpool!


            var x = y("goo, delegate invoke now works?");

            //Console.WriteLine(x);

            x();


            Action a = new MyClass { Field1 = "Field1" }.Invoke;
            a();


            // http://bartoszmilewski.com/2011/10/10/async-tasks-in-c11-not-quite-there-yet/
            //var ttt = ThreadPool.QueueUserWorkItem(
            //    new WaitCallback(
            //        delegate
            //        {
            //            Console.WriteLine("at QueueUserWorkItem");

            //        }
            //    )
            //);

            // X:\jsc.svn\examples\java\hybrid\Test\JVMCLRThreadPool\JVMCLRThreadPool\Program.cs

            // .net 4.5

            // script: error JSC1000: type not supported: System.Threading.Tasks.Task ; consider adding [ScriptAttribute]
            // System.Threading.Tasks.Task for System.Threading.Tasks.Task Run(System.Action) used at
            var t = Task.Run(
                delegate
                {
                    // can we do it for android NDK too?

                    Console.WriteLine("can we do  Task.Run?");

                }
            );

            ////TaskScheduler.Current.
            //var tt = new Thread(
            //    delegate()
            //    {
            //        Console.WriteLine("can the thread access outer scope?");
            //        Console.WriteLine("!");
            //        Console.WriteLine(xx.foo);

            //        // http://stackoverflow.com/questions/13322709/use-of-undeclared-identifier-true
            //        // TestTaskRun.exe.c(1154) : error C2065: 'true' : undeclared identifier
            //        var ok = 1;
            //        while (ok == 1)
            //        {


            //            // this keeps going even if the main thread quits?
            //            Console.Write(".");
            //            Thread.Sleep(100);
            //        }
            //    }
            //);


            //tt.Start();
            //Console.Beep(1200, duration: 1000);
            //tt.Suspend();
            //Console.Beep(800, duration: 1000);
            //tt.Resume();
            Console.Beep(1200, duration: 1000);

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
