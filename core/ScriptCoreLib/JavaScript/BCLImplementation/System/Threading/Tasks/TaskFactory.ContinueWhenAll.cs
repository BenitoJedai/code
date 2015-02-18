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
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/TaskFactory.cs

    internal partial class __TaskFactory
    {
        public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
        {
            var z = new TaskCompletionSource<Task[]>();


            var c = tasks.Length;

            foreach (var item in tasks)
            {
                item.ContinueWith(
                    t =>
                    {
                        c--;

                        if (c == 0)
                        {
                            // its time!
                            if (continuationAction != null)
                                continuationAction(tasks);

                            // is it the CLR ordering of events?
                            z.SetResult(tasks);
                        }
                    }
                );
            }

            return z.Task;
        }

        #region ContinueWhenAll
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
            //Console.WriteLine("ContinueWhenAll " + new { tasks.Length, scheduler, Thread.CurrentThread.ManagedThreadId });


            var t = new __Task<TResult> { InternalStart = null };

            var cstart = 0;
            var cstop = 0;

            #region ContinueWhenAll_yield
            Action ContinueWhenAll_yield = delegate
            {
                // how can we pass array of tasks to background?

                //Console.WriteLine("ContinueWhenAll_yield " + new { scheduler });

                #region GUI
                if (scheduler != null)
                {
                    var r = continuationFunction(tasks);

                    t.InternalSetCompleteAndYield(r);
                    return;
                }
                #endregion


                // X:\jsc.svn\examples\javascript\appengine\Test\AppEngineFirstEverWebServiceTask\AppEngineFirstEverWebServiceTask\ApplicationWebService.cs

                var xfunction = continuationFunction;
                var MethodType = typeof(FuncOfTaskOfObjectArrayToObject).Name;

                #region MethodToken
                var MethodToken = ((__MethodInfo)xfunction.Method).InternalMethodToken;

                if (xfunction.Target != null)
                    if (xfunction.Target != Native.self)
                    {
                        // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                        Delegate InternalTaskExtensionsScope_function = (xfunction.Target as dynamic).InternalTaskExtensionsScope_function;

                        if (InternalTaskExtensionsScope_function == null)
                        {
                            var message = "inline scope sharing not yet implemented";
                            Console.WriteLine(message);
                            throw new InvalidOperationException(message);
                        }

                        MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).InternalMethodToken;
                    }
                #endregion



                #region xdata___string
                // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
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

                // is this correct?
                // OBSOLETE ! needs more tests. what about scope sharing?
                var w = new global::ScriptCoreLib.JavaScript.DOM.Worker(
                    InternalInlineWorker.GetScriptApplicationSourceForInlineWorker()
                    //global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSourceForInlineWorker
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

                         // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
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
            #endregion


            #region ContinueWhenAll_yield
            foreach (__Task<TAntecedentResult> item in tasks)
            {
                cstart++;
                //Console.WriteLine("ContinueWhenAll before ContinueWith " + new { cstart, tasks.Length, Thread.CurrentThread.ManagedThreadId });

                item.ContinueWith(
                    task =>
                    {
                        cstop++;
                        //Console.WriteLine("ContinueWhenAll ContinueWith yield " + new { cstop, tasks.Length, Thread.CurrentThread.ManagedThreadId });


                        if (cstop == tasks.Length)
                        {
                            //Console.WriteLine("before ContinueWhenAll_yield");

                            ContinueWhenAll_yield();
                            //Console.WriteLine("after ContinueWhenAll_yield");
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
        #endregion


    }


}
