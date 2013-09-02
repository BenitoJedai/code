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
        ) where TStateMachine : __IAsyncStateMachine
        {
            // we need ref support in JSC!

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
        }


        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref  TAwaiter awaiter,
            ref  TStateMachine stateMachine
)
        {
        }

        public void PreBoxInitialization()
        {
        }
    }

    // see: http://msdn.microsoft.com/en-us/library/hh138506(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncTaskMethodBuilder")]
    internal class __AsyncTaskMethodBuilder<TResult>
    {
        public __Task<TResult> Task { get; set; }

        public void Start<TStateMachine>(
             ref TStateMachine stateMachine
        )
        {
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
