using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/TaskAwaiter.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/CompilerServices/TaskAwaiter.cs
    // see: http://msdn.microsoft.com/en-us/library/hh138386(v=vs.110).aspx


    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.TaskAwaiter`1")]
    //"System.Runtime.CompilerServices.TaskAwaiter`1, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    public class __TaskAwaiter<TResult> : __INotifyCompletion
    {
        // x:\jsc.svn\examples\javascript\Test\TestGetAwaiter\TestGetAwaiter\Class1.cs

        public Func<bool> InternalIsCompleted;
        public bool IsCompleted { get { return InternalIsCompleted(); } }

        public Func<TResult> InternalGetResult;

        public TResult GetResult()
        {
            return InternalGetResult();
        }

        public Action InternalOnCompleted;

        public void OnCompleted(Action continuation)
        {
            //Console.WriteLine("__TaskAwaiter<TResult>.OnCompleted " + new {  IsCompleted });
            if (IsCompleted)
            {
                continuation();
                return;
            }

            InternalOnCompleted += continuation;
        }

    }

    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.TaskAwaiter")]
    public class __TaskAwaiter : __INotifyCompletion
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141103

        // X:\jsc.svn\examples\actionscript\async\Test\TestTaskDelay\TestTaskDelay\ApplicationSprite.cs
        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Tasks\Task\Task.cs

        public Func<bool> InternalIsCompleted;
        public bool IsCompleted
        {
            get
            {

                //Console.WriteLine("__TaskAwaiter.IsCompleted " + new { InternalIsCompleted });
                return InternalIsCompleted();
            }
        }

        // http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.taskawaiter.getresult.aspx
        public void GetResult()
        {

        }


        public Action InternalOnCompleted;

        public void OnCompleted(Action continuation)
        {
            //if (continuation != null)

            //Console.WriteLine("__TaskAwaiter.OnCompleted " + new { IsCompleted, continuation });

            if (IsCompleted)
            {
                continuation();

                return;
            }

            InternalOnCompleted += continuation;
        }
    }

}
