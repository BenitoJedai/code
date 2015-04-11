using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{

    internal partial class __Task
    {
        // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter.aspx
        // !supported in: 4.5
        public __TaskAwaiter GetAwaiter()
        {
            //Console.WriteLine("__Task.GetAwaiter");


            var awaiter = new __TaskAwaiter
            {
                InternalIsCompleted = () => this.IsCompleted,
            };

            InvokeWhenComplete(
                delegate
                {
                    //Console.WriteLine("__Task.GetAwaiter InternalYield");

                    if (awaiter.InternalOnCompleted != null)
                        awaiter.InternalOnCompleted();
                }
             );

            return awaiter;
        }
    }


    internal partial class __Task<TResult>
    {


        public __TaskAwaiter<TResult> GetAwaiter()
        {
            return InternalTaskOfTResultGetAwaiter(this);
        }

        static __TaskAwaiter<TResult> InternalTaskOfTResultGetAwaiter(__Task<TResult> that)
        {
            // X:\jsc.svn\examples\java\async\Test\JVMCLRTCPServerAsync\JVMCLRTCPServerAsync\Program.cs
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task_1.java:147: error: _1_GetAwaiter_public_ldftn_0017() in __Task_1 cannot override _1_GetAwaiter_public_ldftn_0017() in __Task

            // see also: X:\jsc.svn\examples\javascript\forms\FormsAsyncButtonExperiment\FormsAsyncButtonExperiment\ApplicationControl.cs

            var awaiter = new __TaskAwaiter<TResult>
            {
                InternalIsCompleted = () => that.IsCompleted,
                InternalGetResult = () => that.Result
            };


            that.InvokeWhenComplete(
                delegate
                {
                    if (awaiter.InternalOnCompleted != null)
                        awaiter.InternalOnCompleted();
                }
            );


            return awaiter;
        }
    }

}
