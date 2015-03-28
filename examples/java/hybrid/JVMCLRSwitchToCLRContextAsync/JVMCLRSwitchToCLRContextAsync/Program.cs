using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JVMCLRSwitchToCLRContextAsync
{
    #region HopToThreadPoolAwaitable
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // simple awaitable that allows for hopping to the thread pool
    struct HopToThreadPoolAwaitable : System.Runtime.CompilerServices.INotifyCompletion
    {
        public HopToThreadPoolAwaitable GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }
        public void OnCompleted(Action continuation) { Task.Run(continuation); }
        public void GetResult() { }
    }
    #endregion


    struct HopToCLR : System.Runtime.CompilerServices.INotifyCompletion
    {
        public HopToCLR GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }
        public void OnCompleted(Action continuation)
        {
            Console.WriteLine(typeof(object) + " enter HopToCLR " + new { Thread.CurrentThread.ManagedThreadId });

            //Task.Run(continuation); 

            // we will never complete?
            // if we do, se have to jump? can we jump?

            // do we know how to pass forward our state?
            // continuation = {Method = {Void Run()}}

            //-		((System.Delegate)(continuation))._target	{System.Runtime.CompilerServices.AsyncMethodBuilderCore.MoveNextRunner}	object {System.Runtime.CompilerServices.AsyncMethodBuilderCore.MoveNextRunner}
            //+		m_context	{System.Threading.ExecutionContext}	System.Threading.ExecutionContext
            //-		m_stateMachine	{JVMCLRSwitchToCLRContextAsync.SharedProgram.Invoke}	System.Runtime.CompilerServices.IAsyncStateMachine {JVMCLRSwitchToCLRContextAsync.SharedProgram.Invoke}
            //-		[JVMCLRSwitchToCLRContextAsync.SharedProgram.Invoke]	{JVMCLRSwitchToCLRContextAsync.SharedProgram.Invoke}	JVMCLRSwitchToCLRContextAsync.SharedProgram.Invoke
            //        e	"hi"	string

            Console.WriteLine(new { continuation });
            Console.WriteLine(new { continuation.Method });
            Console.WriteLine(new { continuation.Target });

            // inspect target. can we reactivate it?

            //0001 0200000e JVMCLRSwitchToCLRContextAsync__i__d.jvm::JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0


            // Implementation not found for type import :
            // type: System.Type
            // method: System.Reflection.FieldInfo[] GetFields(System.Reflection.BindingFlags)
            // Did you forget to add the [Script] attribute?
            // Please double check the signature!

            var f = continuation.Target.GetType().GetFields(
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
            );

            f.WithEach(
                SourceField =>
                {
                    var value = SourceField.GetValue(continuation.Target);

                    Console.WriteLine(new { SourceField, value });
                }
            );

            //System.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
            //System.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
            //System.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
            //System.Object enter HopToCLR { ManagedThreadId = 1 }
            //{ continuation = System.Action }
            //{ Method = Void Run() }
            //{ Target = System.Runtime.CompilerServices.AsyncMethodBuilderCore+MoveNextRunner }
            //{ SourceField = System.Threading.ExecutionContext m_context, value = System.Threading.ExecutionContext }
            //{ SourceField = System.Runtime.CompilerServices.IAsyncStateMachine m_stateMachine, value = JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 }

            // will it be the same in roslyn?
            // can we jump back in the future?


            //java.lang.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
            //System.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 3 }
            //java.lang.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
            //java.lang.Object enter HopToCLR { ManagedThreadId = 1 }
            //{ continuation = ScriptCoreLib.Shared.BCLImplementation.System.__Action@123ec81 }
            //{ Method = void _AwaitUnsafeOnCompleted_b__1() }
            //{ Target = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder___c__DisplayClass2_2@f87f48 }
            //{ SourceField = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__IAsyncStateMachine zstateMachine, value = JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0@ffa5d }
            //{ SourceField = ScriptCoreLib.Shared.BCLImplementation.System.__Action yield, value = ScriptCoreLib.Shared.BCLImplementation.System.__Action@117ae12 }
            //java.lang.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
            //java.lang.Object CLRInvoke { ManagedThreadId = 1 }

            // whats the _AwaitUnsafeOnCompleted_b__1 ?

            // both have __IAsyncStateMachine, but CLR has ExecutionContext, while others have .__Action yield
            // shall we test on roslyn, then inspect that __IAsyncStateMachine?

            CLRProgram.CLRInvoke();
        }

        //System.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
        //System.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
        //System.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
        //System.Object enter HopToCLR { ManagedThreadId = 1 }
        //System.Object CLRInvoke { ManagedThreadId = 1 }

        public void GetResult() { }
    }

    static class Program
    {
        static Program()
        {
            Console.WriteLine(typeof(object) + " Program prep for " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {

            var ebytes = CLRProgram.CLRMain(
                 m: null,
                 e: null
            );


            SharedProgram.Invoke("hi").Wait();







            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323
            Thread.Sleep(10000);
        }


    }

    class SharedProgram
    {
        // not roslyn

        public static async Task Invoke(string e)
        {
            Console.WriteLine(typeof(object) + " enter " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

            await default(HopToCLR);

            Console.WriteLine(typeof(object) + " state 1 " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

            await default(HopToThreadPoolAwaitable);

            Console.WriteLine(typeof(object) + " exit " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });
        }
    }

    //java.lang.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
    //System.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 3 }
    //java.lang.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
    //java.lang.Object exit JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 8 }



    //System.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
    //System.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
    //System.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
    //System.Object exit JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 3 }






    [SwitchToCLRContext]
    static class CLRProgram
    {
        static CLRProgram()
        {
            Console.WriteLine(typeof(object) + " CLRProgram prep for " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });
        }

        public static void CLRInvoke()
        {
            Console.WriteLine(typeof(object) + " CLRInvoke " + new { Thread.CurrentThread.ManagedThreadId });


        }

        [STAThread]
        public static byte[] CLRMain(
            byte[] e,
            byte[] m
                )
        {


            return null;
        }
    }



}
