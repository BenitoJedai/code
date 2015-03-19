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
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/Tasks/TaskContinuation.cs

	public partial class __Task 
    {
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

    }

    internal partial class __Task<TResult> : __Task
    {


        #region ContinueWith
        public Task ContinueWith(Action<Task<TResult>> continuationAction)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\android\com.abstractatech.adminshell\com.abstractatech.adminshell\Application.cs

            return ContinueWith(continuationAction,
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
        }

        public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler)
        {
            //Console.WriteLine("__Task.ContinueWith " + new { scheduler });

            var t = new __Task { InternalStart = null };

            this.InternalOnCompleted(
                delegate
                {
                    //Console.WriteLine("__Task.ContinueWith yield");

                    continuationAction(this);

                    t.InternalSetCompleteAndYield();
                }
            );

            return t;
        }

        public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction)
        {
            return ContinueWith(continuationFunction, default(TaskScheduler));
        }

        public Task<TNewResult> ContinueWith<TNewResult>(
            Func<Task<TResult>, TNewResult> continuationFunction,
            TaskScheduler scheduler)
        {
            // X:\jsc.svn\examples\javascript\appengine\Test\AppEngineFirstEverWebServiceTask\AppEngineFirstEverWebServiceTask\ApplicationWebService.cs
            // android webview dislikes keywords as fields
            var xfunction = continuationFunction;
            var MethodType = typeof(FuncOfTaskToObject).Name;

            #region MethodToken
            var MethodToken = ((__MethodInfo)xfunction.Method).InternalMethodToken;

            if (xfunction.Target != null)
                if (xfunction.Target != Native.self)
                {
                    // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                    Delegate InternalTaskExtensionsScope_function = (xfunction.Target as dynamic).InternalTaskExtensionsScope_function;

                    if (InternalTaskExtensionsScope_function == null)
                        throw new InvalidOperationException("inline scope sharing not yet implemented");

                    MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).InternalMethodToken;
                }
            #endregion

            var t = new __Task<TNewResult> { InternalStart = null };

            this.InternalOnCompleted(
                delegate
                {
                    Console.WriteLine("ContinueWith " + new { this.Result, scheduler });

                    #region what if only GUI scheduler is available?
                    if (scheduler != null)
                    {
                        var r = continuationFunction(this);

                        t.InternalSetCompleteAndYield(r);

                        return;
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


                    // dont think this is correct?
                    var w = new global::ScriptCoreLib.JavaScript.DOM.Worker(
                    InternalInlineWorker.GetScriptApplicationSourceForInlineWorker()
                        //global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSourceForInlineWorker
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
                }
            );

            return t;
        }



        [Obsolete("4.5")]
        public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler)
        {
            //Console.WriteLine("__Task.ContinueWith");

            var t = new __Task<TNewResult> { InternalStart = null };

            t.InternalOnCompleted(
                delegate
                {
                    Console.WriteLine("__Task.InternalStart outer " + new { this.Result });

                    // inner task complete

                    // null means need to use worker

                    var r = continuationFunction(this, state);


                    t.InternalSetCompleteAndYield(r);
                }
            );

            return t;
        }
        #endregion




    }
}
