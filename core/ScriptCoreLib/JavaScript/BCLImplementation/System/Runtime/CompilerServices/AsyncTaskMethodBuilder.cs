using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/System.Runtime.CompilerServices.AsyncTaskMethodBuilder.aspx
    internal class __AsyncTaskMethodBuilder
    {
        public __Task Task { get; set; }

        public static __AsyncTaskMethodBuilder Create()
        {
            return default(__AsyncTaskMethodBuilder);
        }

        public void Start<TStateMachine>(
            /* ref */ TStateMachine stateMachine
        )
        {
            // we need ref support in JSC!

        }
    }

    // see: http://msdn.microsoft.com/en-us/library/hh138506(v=vs.110).aspx
    internal class __AsyncTaskMethodBuilder<TResult>
    {
        public __Task<TResult> Task { get; set; }

        public void Start<TStateMachine>(
            /* ref */ TStateMachine stateMachine
        )
        {
            // we need ref support in JSC!

        }

        public static __AsyncTaskMethodBuilder<TResult> Create()
        {
            // how will this work for JSC?

            return default(__AsyncTaskMethodBuilder<TResult>);
        }
    }
}
