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
using System.Runtime.CompilerServices;
using System.Reflection;

namespace JVMCLRSwitchToCLRContextAsync
{
    // X:\jsc.svn\examples\java\hybrid\test\TestStructFieldDefaults\TestStructFieldDefaults\Program.cs

    #region HopToThreadPoolAwaitable
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // simple awaitable that allows for hopping to the thread pool
    struct HopToJVM : System.Runtime.CompilerServices.INotifyCompletion
    {
        // could we fork ? run in parallerl?

        public HopToJVM GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }

        public void GetResult() { }
    }
    #endregion


    struct HopToCLR : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToCLR GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }

        public void GetResult() { }
    }

    static class Program
    {
        static Program()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150328

            Console.WriteLine(typeof(object) + " Program prep for " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

            HopToCLR.VirtualOnCompleted =
                (Action continuation) =>
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



                    var f = continuation.Target.GetType().GetFields(
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                    );


                    var AsyncStateMachineSource = default(IAsyncStateMachine);
                    var AsyncStateMachineType = default(Type);
                    var AsyncStateMachineFields = default(FieldInfo[]);

                    var AsyncStateMachineStateField = default(FieldInfo);


                    f.WithEach(
                        SourceField =>
                        {
                            var SourceField_value = SourceField.GetValue(continuation.Target);
                            Console.WriteLine(new { SourceField, value = SourceField_value });

                            var m_stateMachine = SourceField_value as IAsyncStateMachine;
                            if (m_stateMachine != null)
                            {
                                AsyncStateMachineSource = m_stateMachine;
                                AsyncStateMachineType = m_stateMachine.GetType();

                                AsyncStateMachineFields = AsyncStateMachineType.GetFields(
                                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                                );

                                AsyncStateMachineFields.WithEach(
                                    AsyncStateMachineSourceField =>
                                    {
                                        var value = AsyncStateMachineSourceField.GetValue(m_stateMachine);

                                        if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
                                        {
                                            AsyncStateMachineStateField = AsyncStateMachineSourceField;
                                        }

                                        Console.WriteLine(new { AsyncStateMachineSourceField, value });
                                    }
                                );
                            }

                        }
                    );

                    // System.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
                    //System.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
                    //System.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
                    //System.Object enter HopToCLR { ManagedThreadId = 1 }
                    //{ continuation = System.Action }
                    //{ Method = Void Run() }
                    //{ Target = System.Runtime.CompilerServices.AsyncMethodBuilderCore+MoveNextRunner }
                    //{ SourceField = System.Threading.ExecutionContext m_context, value = System.Threading.ExecutionContext }
                    //{ SourceField = System.Runtime.CompilerServices.IAsyncStateMachine m_stateMachine, value = JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 }
                    //{ AsyncStateMachineSourceField = Int32 <>1__state, value = 0 }
                    //{ AsyncStateMachineSourceField = System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder, value = System.Runtime.CompilerServices.AsyncTaskMethodBuilder }
                    //{ AsyncStateMachineSourceField = System.String e, value = hi }
                    //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToCLR <>u__$awaiter1, value = JVMCLRSwitchToCLRContextAsync.HopToCLR }
                    //{ AsyncStateMachineSourceField = System.Object <>t__stack, value =  }
                    //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToThreadPoolAwaitable <>u__$awaiter2, value = JVMCLRSwitchToCLRContextAsync.HopToThreadPoolAwaitable }
                    //System.Object CLRInvoke { ManagedThreadId = 1 }

                    //			System.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
                    //System.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
                    //System.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
                    //System.Object enter HopToCLR { ManagedThreadId = 1 }
                    //{ continuation = System.Action }
                    //{ Method = Void Run() }
                    //{ Target = System.Runtime.CompilerServices.AsyncMethodBuilderCore+MoveNextRunner }
                    //{ SourceField = System.Threading.ExecutionContext m_context, value = System.Threading.ExecutionContext }
                    //{ SourceField = System.Runtime.CompilerServices.IAsyncStateMachine m_stateMachine, value = JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 }
                    //{ AsyncStateMachineSourceField = Int32 <>1__state, value = 0 }
                    //{ AsyncStateMachineSourceField = System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder, value = System.Runtime.CompilerServices.AsyncTaskMethodBuilder }
                    //{ AsyncStateMachineSourceField = System.String e, value = hi }
                    //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToCLR <>u__1, value = JVMCLRSwitchToCLRContextAsync.HopToCLR }
                    //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToThreadPoolAwaitable <>u__2, value = JVMCLRSwitchToCLRContextAsync.HopToThreadPoolAwaitable }
                    //System.Object CLRInvoke { ManagedThreadId = 1 }


                    //-Activator.CreateInstance(AsyncStateMachineType) { JVMCLRSwitchToCLRContextAsync.SharedProgram.< Invoke > d__0}
                    //object { JVMCLRSwitchToCLRContextAsync.SharedProgram.< Invoke > d__0}
                    //e   null    string

                    // how can we send the type ref over the wire? encypt it?


                    //var NewStateMachine = Activator.CreateInstance(AsyncStateMachineType);
                    //var NewStateMachineI = NewStateMachine as IAsyncStateMachine;

                    // this will take step into next await.
                    //NewStateMachineI.MoveNext();


                    //CLRProgram.CLRInvoke(
                    var ShadowIAsyncStateMachine = Program.CLRInvoke(
                        new ShadowIAsyncStateMachine
                        {
                            TypeName = AsyncStateMachineType.FullName,
                            state = (int)AsyncStateMachineStateField.GetValue(AsyncStateMachineSource)
                        }
                    );

                    Console.WriteLine("can we skip a step? " + new { AsyncStateMachineStateField, ShadowIAsyncStateMachine.state, AsyncStateMachineSource });

                    //time to jump back? yes
                    //{ continuation = System.Action }
                    //{ Method = Void Run() }
                    //{ Target = System.Runtime.CompilerServices.AsyncMethodBuilderCore+MoveNextRunner }
                    //{ SourceField = System.Threading.ExecutionContext m_context, value = System.Threading.ExecutionContext }
                    //{ SourceField = System.Runtime.CompilerServices.IAsyncStateMachine m_stateMachine, value = JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 }
                    //{ AsyncStateMachineSourceField = Int32 <>1__state, value = 1 }
                    //{ AsyncStateMachineSourceField = System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder, value = System.Runtime.CompilerServices.AsyncTaskMethodBuilder }
                    //{ AsyncStateMachineSourceField = System.String e, value =  }
                    //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToCLR <>u__$awaiter1, value = JVMCLRSwitchToCLRContextAsync.HopToCLR }
                    //{ AsyncStateMachineSourceField = System.Object <>t__stack, value =  }
                    //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToJVM <>u__$awaiter2, value = JVMCLRSwitchToCLRContextAsync.HopToJVM }
                    //time to jump back? { state = 1 }
                    //                   time to jump back? { state = 1 }
                    //can we skip a step? { AsyncStateMachineStateField = int __1__state, state = 1, AsyncStateMachineSource = JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0@4437c4 }
                    // enter catch { mname = <01a8> ldloca.s.try } ClauseCatchLocal:

                    //__AsyncTaskMethodBuilder.SetException { exception =  }
                    //enter catch { mname = <00a8> ldarg.0.try } ClauseCatchLocal:

                    //__AsyncTaskMethodBuilder.SetException { exception =  }
                    //{ Message = System.Diagnostics.Debugger.Break, StackTrace = java.lang.RuntimeException: System.Diagnostics.Debugger.Break
                    //        at ScriptCoreLibJava.BCLImplementation.System.Diagnostics.__Debugger.Break(__Debugger.java:32)
                    //        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.SetException(__AsyncTaskMethodBuilder.java:58)
                    //        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._0214__stloc_1(SharedProgram__Invoke_d__0__MoveNext_06000055.java:253)
                    //        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._00a8__ldarg_0(SharedProgram__Invoke_d__0__MoveNext_06000055.java:425)
                    //        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__workflow(SharedProgram__Invoke_d__0__MoveNext_06000055.java:81)
                    //        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__forwardref(SharedProgram__Invoke_d__0__MoveNext_06000055.java:49)
                    //        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.MoveNext(SharedProgram__Invoke_d__0.java:34)
                    //        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.System_Runtime_CompilerServices_IAsyncStateMachine_MoveNext(SharedProgram__Invoke_d__0.java:51)
                    //        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.Start(__AsyncTaskMethodBuilder.java:43)
                    //        at JVMCLRSwitchToCLRContextAsync.SharedProgram.Invoke(SharedProgram.java:36)
                    //        at JVMCLRSwitchToCLRContextAsync.Program.main(Program.java:128)
                    // }






















                    ////var NewStateMachine = Activator.CreateInstance(AsyncStateMachineType);
                    ////var NewStateMachineI = NewStateMachine as IAsyncStateMachine;


                    ////AsyncStateMachineStateField.SetValue(NewStateMachine, ShadowIAsyncStateMachine.state);

                    //////this will take step into next await.
                    ////NewStateMachineI.MoveNext();

                    AsyncStateMachineStateField.SetValue(AsyncStateMachineSource, ShadowIAsyncStateMachine.state);
                    AsyncStateMachineSource.MoveNext();

                    // will it be the same in roslyn?
                    // can we jump back in the future?


                    //java.lang.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
                    //System.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 3 }
                    //java.lang.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
                    //java.lang.Object enter HopToCLR { ManagedThreadId = 1 }
                    //{ continuation = ScriptCoreLib.Shared.BCLImplementation.System.__Action@123ec81 }
                    //{ Method = void _AwaitUnsafeOnCompleted_b__1() }
                    //{ Target = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder___c__DisplayClass2_2@f87f48 }
                    //{ SourceField = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__IAsyncStateMachine zstateMachine, value = JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0@1405ef7 }
                    //{ AsyncStateMachineSourceField = java.lang.String e, value = hi }
                    //{ AsyncStateMachineSourceField = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder __t__builder, value = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder@1bba400 }
                    //{ AsyncStateMachineSourceField = int __1__state, value = 0 }
                    //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToCLR __u___awaiter1, value = JVMCLRSwitchToCLRContextAsync.HopToCLR@19d3b25 }
                    //{ AsyncStateMachineSourceField = java.lang.Object __t__stack, value =  }
                    //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToThreadPoolAwaitable __u___awaiter2, value =  }
                    //{ SourceField = ScriptCoreLib.Shared.BCLImplementation.System.__Action yield, value = ScriptCoreLib.Shared.BCLImplementation.System.__Action@14989ff }
                    //java.lang.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
                    //java.lang.Object CLRInvoke { ManagedThreadId = 1 }



                    // whats the _AwaitUnsafeOnCompleted_b__1 ?

                    // both have __IAsyncStateMachine, but CLR has ExecutionContext, while others have .__Action yield
                    // shall we test on roslyn, then inspect that __IAsyncStateMachine?

                    //CLRProgram.CLRInvoke();


                };

        }

        public static ShadowIAsyncStateMachine CLRInvoke(ShadowIAsyncStateMachine e)
        {
            // 23e8:02:01 RewriteToAssembly error: System.IO.FileLoadException: Could not load file or assembly 'JVMCLRSwitchToCLRContextAsync.dll' or one of its dependencies. A dynamic link library (DLL) initialization routine failed. (Exception from HRESULT: 0x8007045A)

            return CLRProgram.CLRInvoke(e);
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

            try
            {
                SharedProgram.Invoke("hi").Wait();
                Console.WriteLine("all done");






                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323
            }
            catch (Exception ex)
            {
                Console.WriteLine(new { ex.Message, ex.StackTrace });
            }
            //Thread.Sleep(10000);

            Console.ReadLine();
        }


    }

    class SharedProgram
    {
        // not roslyn

        //Error	1	The type 'JVMCLRSwitchToCLRContextAsync.HopToCLR' cannot be declared const	X:\jsc.svn\examples\java\hybrid\JVMCLRSwitchToCLRContextAsync\JVMCLRSwitchToCLRContextAsync\Program.cs	349	9	JVMCLRSwitchToCLRContextAsync
        // Error	1	The type 'JVMCLRSwitchToCLRContextAsync.HopToCLR' cannot be declared const	X:\jsc.svn\examples\java\hybrid\JVMCLRSwitchToCLRContextAsync\JVMCLRSwitchToCLRContextAsync\Program.cs	348	9	JVMCLRSwitchToCLRContextAsync
        static readonly HopToCLR CLR = default(HopToCLR);

        public static async Task Invoke(string e)
        {
            Console.WriteLine(typeof(object) + " enter " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

            await CLR;

            Console.WriteLine(typeof(object) + " CLR state 1 " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

            await default(HopToJVM);

            Console.WriteLine(typeof(object) + " JVM state 2 " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

            await default(HopToCLR);

            Console.WriteLine(typeof(object) + " CLR state 3 " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

            await default(HopToJVM);

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




    class ShadowIAsyncStateMachine
    {
        public string TypeName;
        public int state;
    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        //static SharedProgram ref0;

        static CLRProgram()
        {
            Console.WriteLine(typeof(object) + " CLRProgram prep for " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });


        }



        public static ShadowIAsyncStateMachine CLRInvoke(
            ShadowIAsyncStateMachine that
            )
        {
            var xAsyncStateMachineTypeName = that.TypeName;

            Console.WriteLine(typeof(object) + " CLRInvoke "
                //+ typeof(SharedProgram) 
                + new
            {
                Thread.CurrentThread.ManagedThreadId,

                that.TypeName,
                that.state
            });

            // do we have the type mentioned loaded?

            //var NewStateMachine = Activator.CreateInstance(AsyncStateMachineType);
            //var NewStateMachineI = NewStateMachine as IAsyncStateMachine;

            //// this will take step into next await.
            ////NewStateMachineI.MoveNext();


            //java.lang.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
            //java.lang.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
            //java.lang.Object enter HopToCLR { ManagedThreadId = 1 }
            //{ continuation = ScriptCoreLib.Shared.BCLImplementation.System.__Action@123ec81 }
            //{ Method = void _AwaitUnsafeOnCompleted_b__1() }
            //{ Target = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder___c__DisplayClass2_2@f87f48 }
            //{ SourceField = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__IAsyncStateMachine zstateMachine, value = JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0@1405ef7 }
            //{ AsyncStateMachineSourceField = java.lang.String e, value = hi }
            //{ AsyncStateMachineSourceField = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder __t__builder, value = ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder@1bba400 }
            //{ AsyncStateMachineSourceField = int __1__state, value = 0 }
            //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToCLR __u___awaiter1, value = JVMCLRSwitchToCLRContextAsync.HopToCLR@19d3b25 }
            //{ AsyncStateMachineSourceField = java.lang.Object __t__stack, value =  }
            //{ AsyncStateMachineSourceField = JVMCLRSwitchToCLRContextAsync.HopToThreadPoolAwaitable __u___awaiter2, value =  }
            //{ SourceField = ScriptCoreLib.Shared.BCLImplementation.System.__Action yield, value = ScriptCoreLib.Shared.BCLImplementation.System.__Action@14989ff }

            //System.Object CLRInvoke { ManagedThreadId = 3, AsyncStateMachineTypeName = JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0 }

            //{ SourceField = System.Runtime.CompilerServices.IAsyncStateMachine m_stateMachine, value = JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 }
            // should java keep the actual type name as an attribute, like we have displayName

            Console.WriteLine("looking for the type...");

            var xAsyncStateMachineType = typeof(CLRProgram).Assembly.GetTypes().FirstOrDefault(
                x =>
                {
                    //Console.WriteLine(new { x.FullName });

                    return x.FullName.Replace("+", "_").Replace("<", "_").Replace(">", "_")
                    == xAsyncStateMachineTypeName.Replace("+", "_").Replace("<", "_").Replace(">", "_");
                }
            );

            //looking for the type...
            //{ FullName = JVMCLRSwitchToCLRContextAsync.Program }
            //{ FullName = <module>.SHA12fc19b38aba0098236ba7fd654b62facbfa4969b@1331799119 }
            //{ FullName = ScriptCoreLib.Desktop.JVM.JVMLauncher }
            //{ FullName = <module>.SHA1e4eb4631b3cf3610d48914d2f4deba4581c74282@275784628$00000006 }
            //{ FullName =  .  }
            //{ FullName =  .  }
            //{ FullName =  .  }
            //{ FullName = ScriptCoreLib.Desktop.JVM.__JVMLauncherInvoke }
            //{ FullName = ScriptCoreLib.Desktop.JVM.__InternalGetEntryPoint }
            //{ FullName = ScriptCoreLib.Desktop.JVM.__InternalGetEntryPoint+    }
            //{ FullName =  .  }
            //{ FullName =  .  }
            //{ FullName =  .? }
            //{ FullName =  .   }
            //{ FullName =  .   }
            //{ FullName =  .   }
            //{ FullName =  .   }
            //{ FullName =  .   }
            //{ FullName =  .   }
            //{ FullName =  .   }
            //{ FullName =  .?  }
            //{ FullName =  .   }
            //{ FullName =  .   }
            //{ FullName = JVMCLRSwitchToCLRContextAsync__i.<02000018>\\\\\\\+export }
            //{ FullName = <module>.SHA14908e43b60b15906983f15d5126d55d794b08a03@516876350$0000001e }
            //{ FullName = JVMCLRSwitchToCLRContextAsync.<02000006>\\\\\\\\\\\\\\\+<interfaceexport> }
            //{ FullName = JVMCLRSwitchToCLRContextAsync.CLRProgram }
            //{ FullName = JVMCLRSwitchToCLRContextAsync.CLRProgram+<>c__DisplayClass1 }
            //{ FullName = <>f__AnonymousType$34$$30$$29$$28$7`1 }
            //{ FullName = ScriptCoreLib.Desktop.AppDomainAssemblyResolve }
            //{ FullName = <module>.SHA1ece49800bcc87751341b0db5b5d43bab4e1973d0@1209331862$00000025 }
            //{ FullName = ScriptCoreLib.Library.StringConversions }
            //{ FullName = <module>.SHA17c927d0ef51414d5913d986a2a0ad52032747944@1023654678$00000027 }
            //{ FullName = ScriptCoreLib.Interop.IntPtrInfo }
            //{ FullName = <module>.SHA19ab5a45e1768fb14172603268fe7f44ab10839ae@1329949876$0000002a$0000002e$00000029 }
            //{ FullName = <>f__AnonymousType$42$$53$$67$$65$6`2 }
            //{ FullName = <>f__AnonymousType$45$$65$$70$$68$8`2 }
            //{ FullName = <module>.SHA111aec3ca07ebcd71f8efa6a7eb37c94e05d294eb@2139898218$00000047$00000030 }
            //{ FullName = <module>.SHA1d04c4f386a94a00ca9ac06112eefc7d4456dc8c4@30829592 }
            //{ FullName = <ExportDirectoryBridge> }

            Console.WriteLine(typeof(object) + " CLRInvoke " + new
            {
                Thread.CurrentThread.ManagedThreadId,

                xAsyncStateMachineType
            });




            // how can we send the type ref over the wire? encypt it?
            var NewStateMachine = Activator.CreateInstance(xAsyncStateMachineType);
            var NewStateMachineI = NewStateMachine as IAsyncStateMachine;

            #region 1__state
            xAsyncStateMachineType.GetFields(
                      System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                  ).WithEach(
                   AsyncStateMachineSourceField =>
                   {
                       if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
                       {
                           AsyncStateMachineSourceField.SetValue(
                               NewStateMachineI,
                               that.state
                            );

                       }
                   }
              );
            #endregion






            var ShadowIAsyncStateMachine = new ShadowIAsyncStateMachine();

            var reset = new AutoResetEvent(false);

            HopToJVM.VirtualOnCompleted =
                (Action continuation) =>
                {
                    Console.WriteLine("time to jump back? yes");

                    Console.WriteLine(new { continuation });
                    Console.WriteLine(new { continuation.Method });
                    Console.WriteLine(new { continuation.Target });

                    // inspect target. can we reactivate it?

                    //0001 0200000e JVMCLRSwitchToCLRContextAsync__i__d.jvm::JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0



                    var f = continuation.Target.GetType().GetFields(
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                    );


                    var AsyncStateMachineSource = default(IAsyncStateMachine);
                    var AsyncStateMachineType = default(Type);
                    var AsyncStateMachineFields = default(FieldInfo[]);

                    // { AsyncStateMachineSourceField = Int32 <>1__state, value = 1 }

                    f.WithEach(
                        SourceField =>
                        {
                            var SourceField_value = SourceField.GetValue(continuation.Target);
                            Console.WriteLine(new { SourceField, value = SourceField_value });

                            var m_stateMachine = SourceField_value as IAsyncStateMachine;
                            if (m_stateMachine != null)
                            {
                                AsyncStateMachineSource = m_stateMachine;
                                AsyncStateMachineType = m_stateMachine.GetType();

                                AsyncStateMachineFields = AsyncStateMachineType.GetFields(
                                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                                );

                                AsyncStateMachineFields.WithEach(
                                    AsyncStateMachineSourceField =>
                                    {
                                        var value = AsyncStateMachineSourceField.GetValue(m_stateMachine);

                                        if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
                                        {
                                            ShadowIAsyncStateMachine.state = (int)value;
                                        }

                                        Console.WriteLine(new { AsyncStateMachineSourceField, value });
                                    }
                                );
                            }

                        }
                    );

                    reset.Set();
                };

            new Thread(
                delegate()
                {
                    // this will take step into next await.
                    NewStateMachineI.MoveNext();
                }
            ).Start();


            reset.WaitOne();

            Console.WriteLine("time to jump back? " + new { ShadowIAsyncStateMachine.state });

            return ShadowIAsyncStateMachine;
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

//time to jump back? { state = 1 }
//can we skip a step? { AsyncStateMachineStateField = int __1__state, state = 1, AsyncStateMachineSource = JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0@4437c4 }
//JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 <0000> ldc.i4.1
//JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 <0035> br
//JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 <0189> ldarg.0
//JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 <01a8> ldloca.s
//enter catch { mname = <01a8> ldloca.s.try } ClauseCatchLocal:
//{ Message = , StackTrace = java.lang.NullPointerException
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._01a8__ldloca_s_try(SharedProgram__Invoke_d__0__MoveNext_06000055.java:604)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._01a8__ldloca_s(SharedProgram__Invoke_d__0__MoveNext_06000055.java:579)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__workflow(SharedProgram__Invoke_d__0__MoveNext_06000055.java:105)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__forwardref(SharedProgram__Invoke_d__0__MoveNext_06000055.java:49)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.MoveNext(SharedProgram__Invoke_d__0.java:34)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.System_Runtime_CompilerServices_IAsyncStateMachine_MoveNext(SharedProgram__Invoke_d__0.java:51)
//        at JVMCLRSwitchToCLRContextAsync.Program.__cctor_b__1(Program.java:106)
//        at sun.reflect.NativeMethodAccessorImpl.invoke0(Native Method)
//        at sun.reflect.NativeMethodAccessorImpl.invoke(Unknown Source)
//        at sun.reflect.DelegatingMethodAccessorImpl.invoke(Unknown Source)
//        at java.lang.reflect.Method.invoke(Unknown Source)
//        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:93)
//        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:71)
//        at ScriptCoreLib.Shared.BCLImplementation.System.__Action_1.Invoke(__Action_1.java:28)
//        at JVMCLRSwitchToCLRContextAsync.HopToCLR.OnCompleted(HopToCLR.java:34)
//        at JVMCLRSwitchToCLRContextAsync.HopToCLR.System_Runtime_CompilerServices_INotifyCompletion_OnCompleted(HopToCLR.java:44)
//        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted(__AsyncTaskMethodBuilder.java:92)
//        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.AwaitOnCompleted(__AsyncTaskMethodBuilder.java:66)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._00a8__ldarg_0_try(SharedProgram__Invoke_d__0__MoveNext_06000055.java:451)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._00a8__ldarg_0(SharedProgram__Invoke_d__0__MoveNext_06000055.java:424)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__workflow(SharedProgram__Invoke_d__0__MoveNext_06000055.java:81)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__forwardref(SharedProgram__Invoke_d__0__MoveNext_06000055.java:49)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.MoveNext(SharedProgram__Invoke_d__0.java:34)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.System_Runtime_CompilerServices_IAsyncStateMachine_MoveNext(SharedProgram__Invoke_d__0.java:51)
//        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.Start(__AsyncTaskMethodBuilder.java:43)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram.Invoke(SharedProgram.java:36)
//        at JVMCLRSwitchToCLRContextAsync.Program.main(Program.java:123)
// }
//JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 <0214> stloc.1
//__AsyncTaskMethodBuilder.SetException { exception =  }
//enter catch { mname = <00a8> ldarg.0.try } ClauseCatchLocal:
//{ Message = , StackTrace = java.lang.RuntimeException
//        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:97)
//        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:71)
//        at ScriptCoreLib.Shared.BCLImplementation.System.__Action_1.Invoke(__Action_1.java:28)
//        at JVMCLRSwitchToCLRContextAsync.HopToCLR.OnCompleted(HopToCLR.java:34)
//        at JVMCLRSwitchToCLRContextAsync.HopToCLR.System_Runtime_CompilerServices_INotifyCompletion_OnCompleted(HopToCLR.java:44)
//        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted(__AsyncTaskMethodBuilder.java:92)
//        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.AwaitOnCompleted(__AsyncTaskMethodBuilder.java:66)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._00a8__ldarg_0_try(SharedProgram__Invoke_d__0__MoveNext_06000055.java:451)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._00a8__ldarg_0(SharedProgram__Invoke_d__0__MoveNext_06000055.java:424)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__workflow(SharedProgram__Invoke_d__0__MoveNext_06000055.java:81)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__forwardref(SharedProgram__Invoke_d__0__MoveNext_06000055.java:49)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.MoveNext(SharedProgram__Invoke_d__0.java:34)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.System_Runtime_CompilerServices_IAsyncStateMachine_MoveNext(SharedProgram__Invoke_d__0.java:51)
//        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.Start(__AsyncTaskMethodBuilder.java:43)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram.Invoke(SharedProgram.java:36)
//        at JVMCLRSwitchToCLRContextAsync.Program.main(Program.java:123)
//Caused by: java.lang.reflect.InvocationTargetException
//        at sun.reflect.NativeMethodAccessorImpl.invoke0(Native Method)
//        at sun.reflect.NativeMethodAccessorImpl.invoke(Unknown Source)
//        at sun.reflect.DelegatingMethodAccessorImpl.invoke(Unknown Source)
//        at java.lang.reflect.Method.invoke(Unknown Source)
//        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:93)
//        ... 15 more
//Caused by: java.lang.RuntimeException: System.Diagnostics.Debugger.Break
//        at ScriptCoreLibJava.BCLImplementation.System.Diagnostics.__Debugger.Break(__Debugger.java:32)
//        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.SetException(__AsyncTaskMethodBuilder.java:58)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._0214__stloc_1(SharedProgram__Invoke_d__0__MoveNext_06000055.java:255)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._01a8__ldloca_s(SharedProgram__Invoke_d__0__MoveNext_06000055.java:586)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__workflow(SharedProgram__Invoke_d__0__MoveNext_06000055.java:105)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__forwardref(SharedProgram__Invoke_d__0__MoveNext_06000055.java:49)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.MoveNext(SharedProgram__Invoke_d__0.java:34)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.System_Runtime_CompilerServices_IAsyncStateMachine_MoveNext(SharedProgram__Invoke_d__0.java:51)
//        at JVMCLRSwitchToCLRContextAsync.Program.__cctor_b__1(Program.java:106)
//        ... 20 more
// }
//JVMCLRSwitchToCLRContextAsync.SharedProgram+<Invoke>d__0 <0214> stloc.1
//__AsyncTaskMethodBuilder.SetException { exception =  }
//{ Message = System.Diagnostics.Debugger.Break, StackTrace = java.lang.RuntimeException: System.Diagnostics.Debugger.Break
//        at ScriptCoreLibJava.BCLImplementation.System.Diagnostics.__Debugger.Break(__Debugger.java:32)
//        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.SetException(__AsyncTaskMethodBuilder.java:58)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._0214__stloc_1(SharedProgram__Invoke_d__0__MoveNext_06000055.java:255)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055._00a8__ldarg_0(SharedProgram__Invoke_d__0__MoveNext_06000055.java:431)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__workflow(SharedProgram__Invoke_d__0__MoveNext_06000055.java:81)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0__MoveNext_06000055.__forwardref(SharedProgram__Invoke_d__0__MoveNext_06000055.java:49)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.MoveNext(SharedProgram__Invoke_d__0.java:34)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram__Invoke_d__0.System_Runtime_CompilerServices_IAsyncStateMachine_MoveNext(SharedProgram__Invoke_d__0.java:51)
//        at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.Start(__AsyncTaskMethodBuilder.java:43)
//        at JVMCLRSwitchToCLRContextAsync.SharedProgram.Invoke(SharedProgram.java:36)
//        at JVMCLRSwitchToCLRContextAsync.Program.main(Program.java:123)
// }







