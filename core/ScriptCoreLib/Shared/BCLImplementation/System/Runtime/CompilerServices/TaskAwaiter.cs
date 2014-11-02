using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/TaskAwaiter.cs
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
        // X:\jsc.svn\examples\actionscript\async\Test\TestTaskDelay\TestTaskDelay\ApplicationSprite.cs
        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Tasks\Task\Task.cs

        public Func<bool> InternalIsCompleted;
        public bool IsCompleted { get { return InternalIsCompleted(); } }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Runtime.CompilerServices.TaskAwaiter.GetResult()]
        // http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.taskawaiter.getresult.aspx
        public void GetResult()
        {

        }


        public Action InternalOnCompleted;

        public void OnCompleted(Action continuation)
        {
            //Console.WriteLine("__TaskAwaiter.OnCompleted " + new { IsCompleted });
            if (IsCompleted)
            {
                continuation();
                return;
            }

            InternalOnCompleted += continuation;
        }
    }

}
