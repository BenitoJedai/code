using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System.Threading;
using ScriptCoreLibNative.BCLImplementation.System.Reflection;

namespace ScriptCoreLibNative.BCLImplementation.System.Threading
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/thread.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/Thread.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Thread.cs

    [Script(Implements = typeof(global::System.Threading.Thread))]
    internal class __Thread
    {
        // http://stackoverflow.com/questions/796217/what-is-the-difference-between-a-thread-and-a-fiber

        // http://msdn.microsoft.com/en-us/library/xcb2z8hs.aspx

        ThreadStart __ThreadStart;
        ParameterizedThreadStart __ParameterizedThreadStart;


        public __Thread(ParameterizedThreadStart start)
        {
            // X:\jsc.svn\examples\c\Test\TestThreadStart\TestThreadStart\Program.cs
            __ParameterizedThreadStart = start;
        }


        public void Start(object parameter)
        {
            Console.WriteLine("__Thread ParameterizedThreadStart");

            ///*newobj*/
            //malloc(4)[0] = parameter;
            //objectArray0 = ((void**)/*newobj*/ malloc(4));

            //var arglist = new object[1];

            //arglist[0] = parameter;



            //var arglist = new object[] { parameter };

            //process_h._beginthread(__ParameterizedThreadStart._method, stack_size: 0, arglist: parameter);


            var __MethodInfo = (__MethodInfo)(object)__ParameterizedThreadStart.Method;

            process_h._beginthread(
                __MethodInfo.MethodToken, stack_size: 0, arglist: parameter);
        }


        public __Thread(ThreadStart e)
        {
            //if (e.Target != null)
            //{
            //    //throw new NotImplementedException("for now ScriptCoreLibNative supports only static thread starts..");
            //    Console.WriteLine("for now ScriptCoreLibNative supports only static thread starts..");

            //    return;
            //}
            //Console.WriteLine("__Thread ");
            __ThreadStart = e;
        }

        [Script]
        class InternalThreadStartArguments
        {
            // in C we can pass the delegate object and call it
            public ThreadStart __ThreadStart;
        }

        static void Start(InternalThreadStartArguments e)
        {
            //Console.WriteLine("__Thread Start");

            e.__ThreadStart();
        }

        public void Start()
        {
            // X:\jsc.svn\examples\c\Test\TestFunc\TestFunc\Program.cs

            //Console.WriteLine("__Thread Start");


            Action<InternalThreadStartArguments> r = Start;

            var __MethodInfo = (__MethodInfo)(object)r.Method;

            // If successful, _beginthread returns the thread ID number of the new thread. It returns -1 to indicate an error.

            InternalHandle = process_h._beginthread(
                __MethodInfo.MethodToken, stack_size: 0, arglist: new InternalThreadStartArguments { __ThreadStart = this.__ThreadStart });
        }

        public object InternalHandle;

        public static void Sleep(int p)
        {
            windows_h.Sleep(p);
        }

        public void Resume()
        {

            windows_h.ResumeThread(
                InternalHandle
            );
        }


        public void Suspend()
        {

            windows_h.SuspendThread(
                InternalHandle
            );
        }

    }
}
