using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.asyncvoidmethodbuilder(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncVoidMethodBuilder")]
    internal class __AsyncVoidMethodBuilder : __IAsyncMethodBuilder
    {
        // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Runtime.CompilerServices/AsyncVoidMethodBuilder.cs

        // struct!

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
            Console.WriteLine("__AsyncVoidMethodBuilder.SetResult");

        }

        public void SetException(Exception exception)
        {
            Console.WriteLine("__AsyncVoidMethodBuilder.SetException " + new { exception.Message });

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


        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
             ref  TAwaiter awaiter,
             ref  TStateMachine stateMachine
        )
        //where TAwaiter : global::System.Runtime.CompilerServices.INotifyCompletion
        //where TStateMachine : __IAsyncStateMachine
        {
            //Console.WriteLine("__AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted");

            var xstateMachine = (__IAsyncStateMachine)stateMachine;
            var zstateMachine = xstateMachine;

            Action yield = () => zstateMachine.MoveNext();

            var xawaiter = (__INotifyCompletion)(object)awaiter;

            xawaiter.OnCompleted(
                delegate
                {
                    //Console.WriteLine("__AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted  xawaiter.OnCompleted");

                    yield();
                }
            );
        }

        [Obsolete]
        public void PreBoxInitialization()
        {
            Console.WriteLine("__AsyncVoidMethodBuilder.PreBoxInitialization");
        }
    }

}
