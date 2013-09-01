using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using System.Threading;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx
    [Script(Implements = typeof(global::System.Threading.Tasks.Task))]
    internal class __Task
    {
        // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter.aspx
        // !supported in: 4.5
        public __TaskAwaiter GetAwaiter()
        {
            return default(__TaskAwaiter);
        }


        public static TaskFactory Factory
        {
            get
            {
                return new TaskFactory();
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
        public Action InternalYield;

        public void Start()
        {
            //Console.WriteLine("__Task.Start");

            if (InternalStart == null)
                throw new InvalidOperationException("Start may not be called on a continuation task.");


            InternalStart();
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
    internal class __Task<TResult> : __Task
    {
        // see also: http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter(v=vs.110).aspx


        public __TaskAwaiter<TResult> GetAwaiter()
        {
            // see also: X:\jsc.svn\examples\javascript\forms\FormsAsyncButtonExperiment\FormsAsyncButtonExperiment\ApplicationControl.cs

            var awaiter = new __TaskAwaiter<TResult> { };

            this.InternalYield += delegate
            {
                awaiter.InternalResult = this.Result;
                awaiter.IsCompleted = true;

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

        public __Task()
        {

        }

        public __Task(Func<object, TResult> function, object state)
        {
            InternalInitialize(function, state, default(CancellationToken), default(TaskCreationOptions), TaskScheduler.Default);
        }

        public void InternalInitialize(Func<object, TResult> function, object state, CancellationToken c, TaskCreationOptions o, TaskScheduler s)
        {
            
            // what if this is a GUI task?

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run
            var MethodType = typeof(FuncOfObjectToObject).Name;

            #region MethodToken
            var MethodToken = ((__MethodInfo)function.Method).MethodToken;

            if (function.Target != null)
                if (function.Target != Native.self)
                {
                    Delegate InternalTaskExtensionsScope_function = (function.Target as dynamic).InternalTaskExtensionsScope_function;

                    if (InternalTaskExtensionsScope_function == null)
                        throw new InvalidOperationException("inline scope sharing not yet implemented");

                    MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).MethodToken;
                }
            #endregion

            this.InternalStart = delegate
            {
                #region xdata___string
                dynamic xdata___string = new object();

                // how much does this slow us down?
                // connecting to a new scope, we need a fresh copy of everything
                // we can start with strings
                foreach (ExpandoMember nn in Expando.Of(InternalInlineWorker.__string).GetMembers())
                {
                    if (nn.Value != null)
                        xdata___string[nn.Name] = nn.Value;
                }
                #endregion


                var w = new global::ScriptCoreLib.JavaScript.DOM.Worker(
                       global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSourceForInlineWorker
                   );

                #region postMessage
                w.postMessage(
                    new
                    {
                        InternalInlineWorker.InternalThreadCounter,
                        MethodToken,
                        MethodType,
                        state = state,
                        __string = (object)xdata___string
                    }
                    ,
                     e =>
                     {
                         // what kind of write backs do we expect?
                         // for now it should be console only

                         dynamic zdata = e.data;

                         #region AtWrite
                         string AtWrite = zdata.AtWrite;

                         if (!string.IsNullOrEmpty(AtWrite))
                             Console.Write(AtWrite);
                         #endregion

                         #region __string
                         var zdata___string = (object)zdata.__string;
                         if (zdata___string != null)
                         {
                             #region __string
                             dynamic target = InternalInlineWorker.__string;
                             var m = Expando.Of(zdata___string).GetMembers();

                             foreach (ExpandoMember nn in m)
                             {
                                 Console.WriteLine("Worker has sent changes " + new { nn.Name });

                                 target[nn.Name] = nn.Value;
                             }
                             #endregion
                         }
                         #endregion

                         #region yield
                         dynamic yield = zdata.yield;
                         if ((object)yield != null)
                         {

                             object value = yield.value;

                             //Console.WriteLine("__Task.InternalStart inner complete " + new { yield = new { value } });

                             this.Result = (TResult)value;

                             //w.terminate();

                             if (this.InternalYield != null)
                                 this.InternalYield();
                         }
                         #endregion

                     }
                );
                #endregion

                InternalInlineWorker.InternalThreadCounter++;
            };
        }

        public Task ContinueWith(Action<Task<TResult>> continuationAction)
        {
            return ContinueWith(continuationAction, default(TaskScheduler));
        }

        public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler)
        {
            //Console.WriteLine("__Task.ContinueWith");

            var t = new __Task { InternalStart = null };

            this.InternalYield += delegate
            {
                //Console.WriteLine("__Task.InternalStart outer " + new { this.Result });

                // inner task complete

                // null means need to use worker

                continuationAction(this);

                //Console.WriteLine("__Task.InternalStart outer complete");

                if (t.InternalYield != null)
                    t.InternalYield();
            };

            return t;
        }

        public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction)
        {
            return ContinueWith(continuationFunction, default(TaskScheduler));
        }

        public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler)
        {
            var function = continuationFunction;
            var MethodType = typeof(FuncOfTaskToObject).Name;

            #region MethodToken
            var MethodToken = ((__MethodInfo)function.Method).MethodToken;

            if (function.Target != null)
                if (function.Target != Native.self)
                {
                    Delegate InternalTaskExtensionsScope_function = (function.Target as dynamic).InternalTaskExtensionsScope_function;

                    if (InternalTaskExtensionsScope_function == null)
                        throw new InvalidOperationException("inline scope sharing not yet implemented");

                    MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).MethodToken;
                }
            #endregion

            var t = new __Task<TNewResult> { InternalStart = null };

            this.InternalYield += delegate
            {
                Console.WriteLine("ContinueWith " + new { this.Result, scheduler });

                // what if only GUI scheduler is available?
                if (scheduler != null)
                {

                    var r = continuationFunction(this);

                    t.Result = r;

                    //Console.WriteLine("__Task.InternalStart outer complete");

                    if (t.InternalYield != null)
                        t.InternalYield();

                    return;
                }

                #region xdata___string
                dynamic xdata___string = new object();

                // how much does this slow us down?
                // connecting to a new scope, we need a fresh copy of everything
                // we can start with strings
                foreach (ExpandoMember nn in Expando.Of(InternalInlineWorker.__string).GetMembers())
                {
                    if (nn.Value != null)
                        xdata___string[nn.Name] = nn.Value;
                }
                #endregion


                var w = new global::ScriptCoreLib.JavaScript.DOM.Worker(
                       global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSourceForInlineWorker
                   );

                #region postMessage
                w.postMessage(
                    new
                    {
                        InternalInlineWorker.InternalThreadCounter,
                        MethodToken,
                        MethodType,

                        //state = state,

                        // pass the result for reconstruction

                        // task[0].Result

                        Task = new[] { new { this.Result } },

                        __string = (object)xdata___string
                    }
                    ,
                     e =>
                     {
                         // what kind of write backs do we expect?
                         // for now it should be console only

                         dynamic zdata = e.data;

                         #region AtWrite
                         string AtWrite = zdata.AtWrite;

                         if (!string.IsNullOrEmpty(AtWrite))
                             Console.Write(AtWrite);
                         #endregion

                         #region __string
                         var zdata___string = (object)zdata.__string;
                         if (zdata___string != null)
                         {
                             #region __string
                             dynamic target = InternalInlineWorker.__string;
                             var m = Expando.Of(zdata___string).GetMembers();

                             foreach (ExpandoMember nn in m)
                             {
                                 Console.WriteLine("Worker has sent changes " + new { nn.Name });

                                 target[nn.Name] = nn.Value;
                             }
                             #endregion
                         }
                         #endregion

                         #region yield
                         dynamic yield = zdata.yield;
                         if ((object)yield != null)
                         {

                             object value = yield.value;

                             //Console.WriteLine("__Task.InternalStart inner complete " + new { yield = new { value } });

                             t.Result = (TNewResult)value;

                             //w.terminate();

                             if (t.InternalYield != null)
                                 t.InternalYield();
                         }
                         #endregion

                     }
                );
                #endregion

                InternalInlineWorker.InternalThreadCounter++;
            };

            return t;
        }



        [Obsolete("4.5")]
        public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler)
        {
            //Console.WriteLine("__Task.ContinueWith");

            var t = new __Task<TNewResult> { InternalStart = null };

            this.InternalYield += delegate
            {
                Console.WriteLine("__Task.InternalStart outer " + new { this.Result });

                // inner task complete

                // null means need to use worker

                var r = continuationFunction(this, state);

                t.Result = r;

                //Console.WriteLine("__Task.InternalStart outer complete");

                if (t.InternalYield != null)
                    t.InternalYield();
            };

            return t;
        }



        public TResult Result { get; internal set; }


        public static implicit operator Task<TResult>(__Task<TResult> e)
        {
            return (Task<TResult>)(object)e;
        }
    }
}
