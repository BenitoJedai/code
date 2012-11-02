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
        public static __AsyncVoidMethodBuilder Create()
        {
            return default(__AsyncVoidMethodBuilder);
        }

        public void Start<TStateMachine>(
            /* ref */ TStateMachine stateMachine
            )
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

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            /* ref */ TAwaiter awaiter,
            /* ref */ TStateMachine stateMachine
        )
        {
        }

        public void PreBoxInitialization()
        {
        }
    }

}
