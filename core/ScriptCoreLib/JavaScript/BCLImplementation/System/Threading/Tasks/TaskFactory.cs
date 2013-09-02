using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    [Script(Implements = typeof(global::System.Threading.Tasks.TaskFactory))]
    internal partial class __TaskFactory
    {
        public static implicit operator TaskFactory(__TaskFactory e)
        {
            return (TaskFactory)(object)e;
        }





        public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(
            Task<TAntecedentResult>[] tasks,
            Func<Task<TAntecedentResult>[], TResult> continuationFunction)
        {
            return ContinueWhenAll(
                tasks,
                continuationFunction,
                cancellationToken: default(CancellationToken),
                continuationOptions: default(TaskContinuationOptions),
                scheduler: TaskScheduler.Default
            );
        }

        public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(
            Task<TAntecedentResult>[] tasks,
            Func<Task<TAntecedentResult>[], TResult> continuationFunction,
            CancellationToken cancellationToken,
            TaskContinuationOptions continuationOptions,
            TaskScheduler scheduler)
        {


            var t = new __Task<TResult> { InternalStart = null };

            var c = 0;

            Action ContinueWhenAll_yield = delegate
            {
                // how can we pass array of tasks to background?

                Console.WriteLine("ContinueWhenAll " + new { scheduler });

                #region GUI
                if (scheduler != null)
                {

                    var r = continuationFunction(tasks);

                    t.Result = r;

                    //Console.WriteLine("__Task.InternalStart outer complete");

                    if (t.InternalYield != null)
                        t.InternalYield();

                    return;
                }
                #endregion



                var function = continuationFunction;
                var MethodType = typeof(FuncOfTaskOfObjectArrayToObject).Name;

                #region MethodToken
                var MethodToken = ((__MethodInfo)function.Method).MethodToken;

                if (function.Target != null)
                    if (function.Target != Native.self)
                    {
                        Delegate InternalTaskExtensionsScope_function = (function.Target as dynamic).InternalTaskExtensionsScope_function;

                        if (InternalTaskExtensionsScope_function == null)
                        {
                            var message = "inline scope sharing not yet implemented";
                            Console.WriteLine(message);
                            throw new InvalidOperationException(message);
                        }

                        MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).MethodToken;
                    }
                #endregion



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

                var TaskArray = tasks.Select(k => new { k.Result }).ToArray();

                Console.WriteLine(new { TaskArray = TaskArray.Length });
                //Debugger.Break();


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

                        TaskArray,

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


                             t.InternalSetCompleteAndYield((TResult)value);

                             //w.terminate();
                         }
                         #endregion

                     }
                );
                #endregion

                InternalInlineWorker.InternalThreadCounter++;
            };

            #region ContinueWhenAll_yield
            foreach (Task<TAntecedentResult> item in tasks)
            {
                item.ContinueWith(
                    task =>
                    {
                        c++;

                        if (c == tasks.Length)
                        {
                            ContinueWhenAll_yield();
                        }
                    },

                    // just for the watchdog
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
            #endregion

            return t;
        }

        public Task ContinueWhenAll<TAntecedentResult>(
            Task<TAntecedentResult>[] tasks,
            Action<Task<TAntecedentResult>[]> continuationAction,
            CancellationToken cancellationToken,
            TaskContinuationOptions continuationOptions,
            TaskScheduler scheduler)
        {
            var t = new __Task { InternalStart = null };

            var c = 0;

            foreach (Task<TAntecedentResult> item in tasks)
            {
                item.ContinueWith(
                    task =>
                    {
                        c++;

                        if (c == tasks.Length)
                        {
                            if (continuationAction != null)
                                continuationAction(tasks);
                        }
                    },
                    scheduler: scheduler
                );
            }

            return t;
        }
    }

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskFactory<>))]
    internal class __TaskFactory<TResult>
    {
        public Task<TResult> StartNew(Func<object, TResult> function, object state)
        {
            var x = new Task<TResult>(function, state);

            x.Start();

            return x;
        }

        public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken c, TaskCreationOptions o, TaskScheduler s)
        {
            var x = new __Task<TResult>();

            x.InternalInitialize(
                function, state, c, o, s
            );


            x.Start();

            return x;
        }
    }
}
