using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx
    [Script(Implements = typeof(global::System.Threading.Tasks.Task))]
    internal class __Task
    {
        public Action InternalDispose;
        public void Dispose()
        {
            if (InternalDispose != null)
                InternalDispose();

        }

        [Obsolete("jsc would have to write all application into a global async")]
        public void Wait()
        {

            throw new NotImplementedException();
        }

        public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks)
        {
            return WhenAll(
                tasks.ToArray()
            );
        }

        // public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks);

        public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\css\CSSTransform\CSSTransform\Application.cs

            var x = new TaskCompletionSource<Task<TResult>>();

            foreach (var item in tasks)
            {
                // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.Task.ContinueWith(System.Action`1[[System.Threading.Tasks.Task, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

                item.ContinueWith(
                    c =>
                    {
                        if (x == null)
                            return;

                        x.SetResult(c);
                        x = null;
                    }
                );

            }

            return x.Task;
        }

        public static Task<Task> WhenAny(params Task[] tasks)
        {
            var x = new TaskCompletionSource<Task>();

            foreach (var item in tasks)
            {
                // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.Task.ContinueWith(System.Action`1[[System.Threading.Tasks.Task, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

                item.ContinueWith(
                    c =>
                    {
                        if (x == null)
                            return;

                        x.SetResult(c);
                        x = null;
                    }
                );

            }

            return x.Task;
        }

        public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
        {
            // tested by 
            // X:\jsc.svn\examples\javascript\forms\VBAsyncExperiment\VBAsyncExperiment\ApplicationControl.vb

            // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.TaskFactory.ContinueWhenAll(System.Threading.Tasks.Task[], System.Func`2[[System.Threading.Tasks.Task[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

            //Console.WriteLine("__Task.WhenAll " + new { tasks.Length });

            return __Task.InternalFactory.ContinueWhenAll(tasks,
                u =>
                {
                    //Console.WriteLine("__Task.WhenAll yield " + new { tasks = tasks.Length, u = u.Length });

                    // nop

                    var a = u.Select(k => k.Result).ToArray();

                    //Console.WriteLine("__Task.WhenAll yield " + new { a = a.Length, Thread.CurrentThread.ManagedThreadId });

                    return a;
                },
                cancellationToken: default(CancellationToken),
                continuationOptions: default(TaskContinuationOptions),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }


        // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter.aspx
        // !supported in: 4.5
        public __TaskAwaiter GetAwaiter()
        {
            //Console.WriteLine("__Task.GetAwaiter");

            // see also: X:\jsc.svn\examples\javascript\forms\FormsAsyncButtonExperiment\FormsAsyncButtonExperiment\ApplicationControl.cs

            var awaiter = new __TaskAwaiter
            {
                InternalIsCompleted = () => this.IsCompleted,
            };

            this.InternalYield += delegate
            {
                //Console.WriteLine("__Task.GetAwaiter InternalYield");

                if (awaiter.InternalOnCompleted != null)
                    awaiter.InternalOnCompleted();
            };

            return awaiter;
        }

        public bool IsCompleted { get; internal set; }

        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            var t = new __Task<TResult>();

            t.InternalSetCompleteAndYield(result);

            return t;
        }

        public static __TaskFactory InternalFactory
        {
            get
            {
                return new __TaskFactory();
            }
        }


        public static TaskFactory Factory
        {
            get
            {
                return InternalFactory;
            }
        }



        public __Task()
        {

        }

        public __Task(Action action)
        {
            this.InternalStart = delegate
            {
                // worker?
                action();
                // OnComplete
            };
        }

        public Action InternalStart;



        public Task ContinueWith(Action<Task> continuationAction)
        {
            InternalOnCompleted(
                delegate
                {
                    continuationAction(this);
                }
            );

            // ?
            return this;
        }

        public void InternalOnCompleted(Action continuation)
        {
            //Console.WriteLine("__Task<TResult>.InternalOnCompleted " + new { IsCompleted });
            if (IsCompleted)
            {
                continuation();
                return;
            }

            InternalYield += continuation;
        }


        public Action InternalYield;

        public void Start()
        {
            //Console.WriteLine("__Task.Start");

            if (InternalStart == null)
                throw new InvalidOperationException("Start may not be called on a continuation task.");


            InternalStart();
        }


        public static Task Delay(int millisecondsDelay)
        {
            var t = new __Task { };

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    t.InternalSetCompleteAndYield();
                }
            ).StartTimeout(millisecondsDelay);


            return t;
        }

        public void InternalSetCompleteAndYield()
        {
            this.IsCompleted = true;

            if (this.InternalYield != null)
                this.InternalYield();
        }

        public static implicit operator Task(__Task e)
        {
            return (Task)(object)e;
        }
    }




    // until we support generic type info
    [Script]
    internal delegate object FuncOfObjectToObject(object e);

    // Func<Task<TResult>, object, TNewResult>
    // Func<Task<TResult>, TNewResult>
    [Script]
    internal delegate object FuncOfTaskToObject(Task task);

    [Script]
    internal delegate object FuncOfTaskOfObjectArrayToObject(Task<object>[] task);


    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal partial class __Task<TResult> : __Task
    {
        // see also: http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter(v=vs.110).aspx


        public __TaskAwaiter<TResult> GetAwaiter()
        {
            // see also: X:\jsc.svn\examples\javascript\forms\FormsAsyncButtonExperiment\FormsAsyncButtonExperiment\ApplicationControl.cs

            var awaiter = new __TaskAwaiter<TResult>
            {
                InternalIsCompleted = () => this.IsCompleted,
                InternalGetResult = () => this.Result
            };

            this.InternalYield += delegate
            {
                if (awaiter.InternalOnCompleted != null)
                    awaiter.InternalOnCompleted();
            };

            return awaiter;
        }


        public static TaskFactory<TResult> Factory
        {
            get
            {
                return new TaskFactory<TResult>();
            }
        }



        public TResult Result { get; internal set; }


        public void InternalSetCompleteAndYield(TResult value)
        {

            // or throw?
            if (IsCompleted)
                return;

            // http://stackoverflow.com/questions/12100022/taskcompletionsource-when-to-use-setresult-versus-trysetresult-etc

            //Console.WriteLine("__Task<TResult> InternalSetCompleteAndYield");

            this.Result = value;

            this.InternalSetCompleteAndYield();
        }

        public static implicit operator Task<TResult>(__Task<TResult> e)
        {
            return (Task<TResult>)(object)e;
        }

        public static implicit operator __Task<TResult>(Task<TResult> e)
        {
            return (__Task<TResult>)(object)e;
        }
    }
}
