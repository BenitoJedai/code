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

        //Command-line build environment
        //The Windows SDK no longer ships with a complete command-line build environment.Instead, the Windows SDK requires a compiler and build environment to be installed separately.
        // smart. sdk without being sdk

        // http://blogs.msdn.com/b/visualstudio/archive/2012/06/08/visual-studio-express-2012-for-windows-desktop.aspx
        // http://masm32.com/board/index.php?topic=204.0
        //        The reason is that VS 11 Express doesn't support building native Windows Application anymore.
        //Users of VS 11 Express can only build Metro Apps, nothing else.
        //And to prevent the possibility to build native Windows Applications by just using the Win8 SDK via command line or with an alternative IDE, they removed the compilers, linkers and so on.
        // "C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\WPSDK\WP80\bin\cl.exe"

        static void AtTaskRun()
        {
            // The Windows Software Development Kit (SDK) for Windows 8.1 contains headers, libraries, and tools you can use when you create apps that run on Windows operating systems. You can use the Windows SDK, along with your chosen development environment, to write Windows Store apps and desktop apps for Windows 8.1 as well as Windows 8, Windows 7, Windows Vista, Windows Server 2012, Windows Server 2008 R2, and Windows Server 2008.
            // call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.Cmd"
            // The Windows SDK no longer ships with a complete command-line build environment. You must install a compiler and build environment separately. If you require a complete development environment that includes compilers and a build environment, you can download Visual Studio 2013, which includes the appropriate components of the Windows SDK. To download the SDK and install it on another computer, click the download link and run the setup.

            Console.WriteLine("can we do  Task.Run?");
        }

        [Script(NoDecoration = true)]
        public static int main()
        {
            // TestTaskRun.exe.h(81) : error C2061: syntax error : identifier 'ScriptCoreLibNative_BCLImplementation_System_Collections_Generic___List_1_T'

            // http://msdn.microsoft.com/en-us/library/3y1sfaz2.aspx

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141116
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141122

            // http://www.sockets.com/winsock.htm#Bind
            // http://msdn.microsoft.com/en-us/library/wf2w9f6x.aspx

            Console.WriteLine("20141104 does this device support speaker music?");

            // System.Collections.Generic.EqualityComparer`1[<foo>j__TPar] for Boolean Equals(<foo>j__TPar, <foo>j__TPar) used at
            //     LP<>f__AnonymousType0_1 type0_10;
            //var xx = new { foo = "xhello from outer scope" };

            //        method: Boolean Equals(System.Object) }
            //        internal compiler error at method
            // assembly: X:\jsc.svn\examples\c\Test\TestTask
            //skRun.exe at
            // type: <>f__AnonymousType0`1, TestTaskRun, Ver
            //licKeyToken = null
            // method: Equals
            // multiple stack entries instead of one

            // we dont do virtuals yet
            // yet. why wouldnt jsc call the correct ToString instead without virtual?
            //Console.WriteLine(xx.ToString());


            //Func<string, Action> y = o =>
            //{

            //    Console.WriteLine("hello there:");
            //    Console.WriteLine((string)o);
            //    //Console.WriteLine("hello there");

            //    //return "done";

            //    return delegate
            //    {
            //        //Console.WriteLine("cannot return long, but can return delegate! " + xx.foo);
            //        Console.WriteLine("cannot return long, but can return delegate! " );

            //    };
            //};

            //// threadpool!


            //var x = y("goo, delegate invoke now works?");

            ////Console.WriteLine(x);

            //x();


            //Action a = new MyClass { Field1 = "Field1" }.Invoke;
            //a();


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

            //internal compiler error at method
            // assembly: C:\util\jsc\bin\ScriptCoreLibNative.dll at
            // type: ScriptCoreLibNative.BCLImplementation.System.IO.__FileStream,
            //ibNative, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // method: .ctor
            // recursion detected at stack 32
            var t = Task.Run(
                (Action)AtTaskRun
            // can we do it for android NDK too?


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
