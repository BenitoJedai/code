using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;
using System.IO;
using ScriptCoreLibNative.BCLImplementation.System.Reflection;

namespace TestAction
{
    //[Script]
    //public class ColoredText
    //{
    //    public ConsoleColor Color;
    //    public string Text;
    //}

    //[Script]
    //public static class ColoredTextExtensions
    //{
    //    public static void ToConsole(this ColoredText e)
    //    {
    //        Console.ForegroundColor = e.Color;
    //        Console.WriteLine(e.Text);
    //    }
    //}




    [Script]
    public unsafe class NativeClass1 : ScriptCoreLibNative.IAssemblyReferenceToken
    {
        // http://msdn.microsoft.com/en-us/library/windows/desktop/ms679277(v=vs.85).aspx

        //Setting environment for using Microsoft Visual Studio 2010 x86 tools.
        //ERROR: Cannot determine the location of the VS Common Tools folder.





        //public static void TheOtherThread()
        //{
        //    // what about thread pool
        //    // we have our own BCL with us, like we did with android.

        //    new ColoredText { Color = ConsoleColor.Cyan, Text = "TheOtherThread" }.ToConsole();

        //    //Console.WriteLine("TheOtherThread");

        //    Thread.Sleep(500);
        //    Console.WriteLine("TheOtherThread done");
        //}

        [Script(NoDecoration = true)]
        public static int main()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141116
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141122


            Console.WriteLine("20141104 does this device support speaker music?");

            //action0 = ((void(*)())TestAction_NativeClass1_CS___9__CachedAnonymousMethodDelegate1);
            //(/* typecast */(void(*)())action0)();

            // jsc is not doing the right thing here is it.

            // TestAction_NativeClass1_CS___9__CachedAnonymousMethodDelegate1 = ScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart__ctor_6000031(__new_ScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart(1), (void*)NULL, (void*)TestAction_NativeClass1__main_b__0);

            //// instance ScriptCoreLibNative.BCLImplementation.System.__ParameterizedThreadStart..ctor
            //LPScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart ScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart__ctor_6000031(LPScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart __that, void* object, void* method)
            //{
            //}

            ParameterizedThreadStart y = o =>
            {

                Console.WriteLine("hello there:");
                Console.WriteLine((string)o);
                //Console.WriteLine("hello there");
            };

            // type not supported: System.Reflection.MethodInfo ; consider adding [ScriptAttribute]
            //var yMethod = y.Method;

            Console.WriteLine("y.Method");

            Delegate yy = y;

            // System.Reflection.MethodInfo for Boolean op_Equality(System.Reflection.MethodInfo, System.Reflection.MethodInfo) used at
            // x:\jsc.svn\examples\c\test\testaction\testaction\bin\release\web\testaction.exe.c(257) : warning C4716: 'ScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart__ctor_6000031' : must return a value
            // // instance ScriptCoreLibNative.BCLImplementation.System.__ParameterizedThreadStart..ctor
            //LPScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart ScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart__ctor_6000031(LPScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart __that, void* object, void* method)
            //{
            //}

            // TestAction.exe.c(265) : warning C4133: 'function' : incompatible types - from 'LPScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart' to 'LPScriptCoreLibNative_BCLImplementation_System___MulticastDelegate'

            if ((object)yy.Method == null)
            {
                Console.WriteLine("y.Method is null?");
            }
            else
            {
                Console.WriteLine("has y.Method");

                if (((__MethodInfo)yy.Method).MethodToken == null)
                {
                    Console.WriteLine("y.Method.MethodToken is null?");
                }
                else
                {
                    Console.WriteLine("has y.Method.MethodToken");
                }
            }


            //yy.Method

            //Console.WriteLine(y.Method);

            // void* start0;
            //    (/* typecast */(void(*)(void*))start0)((void*)"goo");

            // TestAction.exe.obj : error LNK2019: unresolved external symbol ScriptCoreLibNative_BCLImplementation_System___ParameterizedThreadStart_Invoke referenced in function main
            //y("goo");


            //// you really should not use headphones with PC speakers
            //Console.Beep();

            //// http://stackoverflow.com/questions/331536/windows-threading-beginthread-vs-beginthreadex-vs-createthread-c
            //// http://support.microsoft.com/kb/104641
            //// alchemy?
            //// javacard
            //// http://msdn.microsoft.com/en-us/library/kdzttdcb(v=vs.120).aspx

            //new ColoredText { Color = ConsoleColor.Yellow, Text = "hello world" }.ToConsole();

            //// The _beginthread function creates a thread that begins execution of a routine at start_address. The routine at start_address must use the __cdecl (for native code) 
            //// The _beginthreadex function gives you more control over how the thread is created than _beginthread does. 


            //Console.Write('j');
            //Console.Write('s');
            //Console.Write('c');

            //Console.WriteLine();

            //// http://stackoverflow.com/questions/331536/windows-threading-beginthread-vs-beginthreadex-vs-createthread-c
            //// C : unable to emit pop at 'TestAction.NativeClass1.main'#005d: C : unable to emit call at 'TestAction.NativeClass1.main'#0058: C : failure at TestAction.process_h._beginthread : type not supported: System.Action ; consider adding [ScriptAttribute]
            //// _beginthreadex initializes Certain CRT (C Run-Time) internals that CreateThread API would not do.
            //// http://wayback.archive.org/web/20100801061740/http://www.microsoft.com/msj/0799/win32/win320799.aspx
            //// http://stackoverflow.com/questions/12603407/how-to-return-a-value-from-beginthread-thread

            //// didnt jsc include opcode.ldftn for c yet?
            //// no not yet?
            ////process_h._beginthread(TheOtherThread, 0, null);
            //// http://www.digitalmars.com/rtl/process.html
            //var x = new Thread(TheOtherThread);

            //x.Start();



            //Console.Beep(1200, duration: 2000);
            //Console.Beep(1000, duration: 2000);
            //Console.Beep(1200, duration: 2000);

            //Console.WriteLine("done");

            return 0;
        }



    }

    class Program
    {
        static void Main(string[] args)
        {
            NativeClass1.main();
        }
    }
}
