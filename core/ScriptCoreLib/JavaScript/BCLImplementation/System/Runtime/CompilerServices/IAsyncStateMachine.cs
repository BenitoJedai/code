using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.iasyncstatemachine(v=vs.110).aspx
    internal interface __IAsyncStateMachine
    {
        void MoveNext();

        void SetStateMachine(
            __IAsyncStateMachine stateMachine
        );
    }

}
