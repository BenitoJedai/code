using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/System.Runtime.CompilerServices.AsyncTaskMethodBuilder.aspx
#if NET45
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.AsyncTaskMethodBuilder))]
#else
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncTaskMethodBuilder")]
#endif
    internal class __AsyncTaskMethodBuilder : __IAsyncMethodBuilder
    {
        public __Task Task { get; set; }

        public static __AsyncTaskMethodBuilder Create()
        {
            Console.WriteLine("__AsyncTaskMethodBuilder Create");

            return default(__AsyncTaskMethodBuilder);
        }

        public void Start<TStateMachine>(
            ref  TStateMachine stateMachine
        )
        // script: error JSC1000: Method: FooAsync, Type: VBAsyncExperiment.ApplicationControl; emmiting failed : System.ArgumentException: GenericArguments[0], 'VBAsyncExperiment.ApplicationControl+VB$StateMachine_3_FooAsync', on 'Void Start[TStateMachine](TStateMachine ByRef)' violates the constraint of type 'TStateMachine'. ---> System.Security.VerificationException: Method ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder.Start: type argument 'VBAsyncExperiment.ApplicationControl+VB$StateMachine_3_FooAsync' violates the constraint of type parameter 'TStateMachine'.
        //where TStateMachine : __IAsyncStateMachine
        {
            // we need ref support in JSC!
            Console.WriteLine("__AsyncTaskMethodBuilder Start");

        }

        public void SetStateMachine(
               __IAsyncStateMachine stateMachine
           )
        {

        }

        public void SetResult()
        {

        }

        public void SetException(
            Exception exception
        )
        {

        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
             ref  TAwaiter awaiter,
             ref  TStateMachine stateMachine
)
        {
            Console.WriteLine("__AsyncTaskMethodBuilder AwaitOnCompleted");
        }


        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref  TAwaiter awaiter,
            ref  TStateMachine stateMachine
)
        {
            Console.WriteLine("__AsyncTaskMethodBuilder AwaitUnsafeOnCompleted");
        }

        public void PreBoxInitialization()
        {
        }
    }

    // see: http://msdn.microsoft.com/en-us/library/hh138506(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1")]
    internal class __AsyncTaskMethodBuilder<TResult>
    {
        public __Task<TResult> Task { get; set; }

        public void Start<TStateMachine>(
             ref TStateMachine stateMachine
        )
        {
            Console.WriteLine("__AsyncTaskMethodBuilder<TResult> Start");
            // we need ref support in JSC!

        }

        public static __AsyncTaskMethodBuilder<TResult> Create()
        {
            // how will this work for JSC?
            Console.WriteLine("__AsyncTaskMethodBuilder<TResult> Create");

            return default(__AsyncTaskMethodBuilder<TResult>);
        }
    }
}
