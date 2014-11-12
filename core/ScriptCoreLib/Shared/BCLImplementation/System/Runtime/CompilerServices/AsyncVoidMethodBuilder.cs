using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/AsyncMethodBuilder.cs
    // https://github.com/mono/mono/tree/master/mcs/class/corlib/System.Runtime.CompilerServices/AsyncVoidMethodBuilder.cs
    // see: http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.asyncvoidmethodbuilder(v=vs.110).aspx

    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncVoidMethodBuilder")]
    internal class __AsyncVoidMethodBuilder : __IAsyncMethodBuilder
    {
        // tested by
        // X:\jsc.svn\examples\actionscript\async\Test\TestAsync\TestAsync\ApplicationSprite.cs
        // X:\jsc.svn\examples\java\async\Test\JVMCLRAsync\JVMCLRAsync\Program.cs

        // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Runtime.CompilerServices/AsyncVoidMethodBuilder.cs

        // struct!

        // X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs

        public static __AsyncVoidMethodBuilder Create()
        {
            //Console.WriteLine("__AsyncVoidMethodBuilder.Create");

            return new __AsyncVoidMethodBuilder { };
        }


        public void SetStateMachine(
            __IAsyncStateMachine stateMachine
        )
        {
            Console.WriteLine("__AsyncVoidMethodBuilder.SetStateMachine");

        }

        public void SetResult()
        {
            //Console.WriteLine("__AsyncVoidMethodBuilder.SetResult");

        }

        public void SetException(Exception exception)
        {
            Console.WriteLine("__AsyncVoidMethodBuilder.SetException " + new { exception });

            Debugger.Break();
        }



        public void Start<TStateMachine>(
     ref  TStateMachine stateMachine
    )
        // script: error JSC1000: Method: <.ctor>b__2, Type: AsyncButtonExperiment.Application; emmiting failed : System.ArgumentException: GenericArguments[0], 'AsyncButtonExperiment.Application+ctor>b__2>d__6', 
        // on 'Void Start[TStateMachine](TStateMachine ByRef)' violates the constraint of type 'TStateMachine'. ---> System.Security.VerificationException: 
        // Method ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices.__AsyncVoidMethodBuilder.Start: 
        // type argument 'AsyncButtonExperiment.Application+ctor>b__2>d__6' violates the constraint of type parameter 'TStateMachine'.

    //where TStateMachine : __IAsyncStateMachine
        {
            //Console.WriteLine("__AsyncVoidMethodBuilder.Start, call MoveNext");

            // jsc does not yet know how to dereference here
            //var x = (__IAsyncStateMachine)stateMachine;
            var xstateMachine = (__IAsyncStateMachine)stateMachine;
            xstateMachine.MoveNext();
        }






        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
                 ref  TAwaiter awaiter,
                 ref  TStateMachine stateMachine
            )
        {
            //Console.WriteLine("__AsyncTaskMethodBuilder AwaitOnCompleted");

            // whats the difference?
            AwaitUnsafeOnCompleted(
                ref awaiter,
                ref stateMachine
            );
        }


        // X:\jsc.svn\examples\actionscript\async\Test\TestTaskDelay\TestTaskDelay\ApplicationSprite.cs
        // ApplicationSprite____ctor_b__4_d__0__MoveNext_06000024.__forwardref_583b1500_06000032(this);
        // public static function __forwardref_583b1500_06000012(ref_arg1:*):void
        //  ref_arg2.__t__builder.AwaitUnsafeOnCompleted_4ebbe596_06000cd1(ref_arg3, ref_arg2);
        // machine0 = (__IAsyncStateMachine(ref_arg2[0]));
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
             ref  TAwaiter awaiter,
             ref  TStateMachine stateMachine
        )
        //where TAwaiter : global::System.Runtime.CompilerServices.INotifyCompletion
        //where TStateMachine : __IAsyncStateMachine
        {
            // tested by
            // X:\jsc.svn\examples\java\Test\TestByRefAwaitUnsafeOnCompleted\TestByRefAwaitUnsafeOnCompleted\Class1.cs

            //Console.WriteLine("enter __AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted " + new { awaiter, stateMachine });

            var xstateMachine = (__IAsyncStateMachine)stateMachine;
            var zstateMachine = xstateMachine;


            //var xawaiter = (__INotifyCompletion)(object)awaiter;
            var xawaiter = (__INotifyCompletion)awaiter;



            //Action yield = () => zstateMachine.MoveNext();
            Action yield = zstateMachine.MoveNext;

            xawaiter.OnCompleted(
                yield

                // X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs
                //delegate
                //{
                //    //Console.WriteLine("__AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted  xawaiter.OnCompleted");

                //    yield();
                //}
            );
        }

        [Obsolete]
        public void PreBoxInitialization()
        {
            Console.WriteLine("__AsyncVoidMethodBuilder.PreBoxInitialization");
        }
    }

}
