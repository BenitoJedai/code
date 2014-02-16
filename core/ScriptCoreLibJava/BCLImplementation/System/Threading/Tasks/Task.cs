using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx
    [Script(Implements = typeof(global::System.Threading.Tasks.Task))]
    internal class __Task
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216/task


        public bool IsCompleted { get; set; }


        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            return new __Task<TResult> { Result = result, IsCompleted = true };
        }
    }

    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal class __Task<TResult> : __Task
    {
        public TResult Result { get; set; }



        public static implicit operator __Task<TResult>(Task<TResult> e)
        {
            return (__Task<TResult>)(object)e;
        }

        public static implicit operator Task<TResult>(__Task<TResult> e)
        {
            return (Task<TResult>)(object)e;
        }


        //Implementation not found for type import :
        //type: System.Threading.Tasks.Task`1[[System.Data.DataTable, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        //method: System.Runtime.CompilerServices.TaskAwaiter`1[System.Data.DataTable] GetAwaiter()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


        public __TaskAwaiter<TResult> GetAwaiter()
        {
            // see also: X:\jsc.svn\examples\javascript\forms\FormsAsyncButtonExperiment\FormsAsyncButtonExperiment\ApplicationControl.cs

            var awaiter = new __TaskAwaiter<TResult>
            {
                InternalIsCompleted = () => this.IsCompleted,
                InternalGetResult = () => this.Result
            };

            //this.InternalYield += delegate
            //{
            //    if (awaiter.InternalOnCompleted != null)
            //        awaiter.InternalOnCompleted();
            //};

            return awaiter;
        }


    }
}
