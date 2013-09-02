using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/hh138386(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.TaskAwaiter`1")]
    //"System.Runtime.CompilerServices.TaskAwaiter`1, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    internal class __TaskAwaiter<TResult> : __INotifyCompletion
    {
        // x:\jsc.svn\examples\javascript\Test\TestGetAwaiter\TestGetAwaiter\Class1.cs

        public bool IsCompleted { get; set; }

        public TResult InternalResult;

        public TResult GetResult()
        {
            return InternalResult;
        }

        public Action InternalOnCompleted;

        public void OnCompleted(Action continuation)
        {
            InternalOnCompleted += continuation;
        }

    }

    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.TaskAwaiter")]
    internal class __TaskAwaiter : __INotifyCompletion
    {
        public bool IsCompleted { get; set; }

        public void GetResult()
        {
        }

        public Action InternalOnCompleted;

        public void OnCompleted(Action continuation)
        {
            InternalOnCompleted += continuation;
        }
    }

}
