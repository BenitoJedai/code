using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/hh138386(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.TaskAwaiter´1")]
    internal class __TaskAwaiter<TResult>
    {
        public bool IsCompleted {get;set;}

        public TResult GetResult()
        {
            return default(TResult);
        }
    }

    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.TaskAwaiter")]
    internal class __TaskAwaiter
    {
        public bool IsCompleted { get; set; }

        public void GetResult()
        {
        }
    }

}
