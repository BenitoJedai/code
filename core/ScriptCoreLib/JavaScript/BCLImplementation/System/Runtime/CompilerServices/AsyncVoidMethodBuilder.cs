using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.asyncvoidmethodbuilder(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncVoidMethodBuilder")]
    internal class __AsyncVoidMethodBuilder : __IAsyncMethodBuilder
    {
        // struct!

        public static __AsyncVoidMethodBuilder Create()
        {
            Console.WriteLine("__AsyncVoidMethodBuilder.Create");

            return new __AsyncVoidMethodBuilder { };
        }

        public void Start<TStateMachine>(
             ref  TStateMachine stateMachine
            )
        //where TStateMachine : global::System.Runtime.CompilerServices.IAsyncStateMachine
        {
            Console.WriteLine("__AsyncVoidMethodBuilder.Start");
            // we need ref support in JSC!

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
            Console.WriteLine("__AsyncVoidMethodBuilder.SetException");

        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
             ref  TAwaiter awaiter,
             ref  TStateMachine stateMachine
        )
        //where TAwaiter : global::System.Runtime.CompilerServices.INotifyCompletion
        //where TStateMachine : global::System.Runtime.CompilerServices.IAsyncStateMachine
        {
            Console.WriteLine("__AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted");
        }

        [Obsolete]
        public void PreBoxInitialization()
        {
            Console.WriteLine("__AsyncVoidMethodBuilder.PreBoxInitialization");
        }
    }

}
