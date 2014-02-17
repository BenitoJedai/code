﻿using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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




        public static implicit operator Task(__Task e)
        {
            return (Task)(object)e;
        }


        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            return new __Task<TResult> { Result = result, IsCompleted = true };
        }


        public Task ContinueWith(Action<Task> continuationAction)
        {
            return ContinueWith(continuationAction, default(TaskContinuationOptions));
        }

        public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
        {
            var u = new TaskCompletionSource<object>();

            InvokeWhenComplete(
                delegate
                {
                    continuationAction(this);

                    u.SetResult(null);
                }
            );


            return u.Task;
        }



        public void InvokeWhenComplete(Action e)
        {
            if (this.IsCompleted)
            {
                e();
                return;
            }

            InvokeWhenCompleteLater += e;
        }

        public Action InvokeWhenCompleteLater;


        public AutoResetEvent WaitEvent = new AutoResetEvent(false);

        public void Wait()
        {
            if (this.IsCompleted)
                return;

            WaitEvent.WaitOne();
        }
    }

    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal class __Task<TResult> : __Task
    {
        public TResult InternalResult;
        public TResult Result
        {
            get
            {

                // in js we cannot wait, throw instead?
                this.Wait();
                return this.InternalResult;
            }
            set { this.InternalResult = value; }
        }



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


            InvokeWhenComplete(
                delegate
                {
                    if (awaiter.InternalOnCompleted != null)
                        awaiter.InternalOnCompleted();
                }
            );


            return awaiter;
        }



        public Task ContinueWith(Action<Task<TResult>> continuationAction)
        {
            return ContinueWith(continuationAction, default(TaskContinuationOptions));
        }

        public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskContinuationOptions continuationOptions)
        {
            var u = new TaskCompletionSource<object>();

            // X:\jsc.svn\examples\java\Test\JVMCLRTaskStartNew\JVMCLRTaskStartNew\Program.cs

            //y { ManagedThreadId = 8, IsCompleted = true, Result = done }
            //x { ManagedThreadId = 9, IsCompleted = true, Result = done }
            //{ ManagedThreadId = 9 } and then some

            InvokeWhenComplete(
                delegate
                {
                    if (continuationOptions == TaskContinuationOptions.ExecuteSynchronously)
                    {
                        continuationAction(this);
                        u.SetResult(null);
                        return;
                    }

                    // shall we use threadpool instead?
                    new Thread(
                        delegate()
                        {
                            continuationAction(this);
                            u.SetResult(null);
                        }
                    ) { IsBackground = true }.Start();
                }
            );

            return u.Task;
        }


        public void SetResult(TResult result)
        {
            this.Result = result;
            this.IsCompleted = true;

            if (InvokeWhenCompleteLater != null)
            {
                InvokeWhenCompleteLater();
                InvokeWhenCompleteLater = null;
            }

            // does Wait event get raised after descendant actions are done or just started?

            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task_1.java:121: error: cannot find symbol
            //        this.WaitEvent.Set();
            //                      ^
            //  symbol:   method Set()
            //  location: variable WaitEvent of type __AutoResetEvent

            this.WaitEvent.Set();
        }
    }
}
