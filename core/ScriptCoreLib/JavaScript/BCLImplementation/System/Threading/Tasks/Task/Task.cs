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
    internal class __Task<TResult> : __Task
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

        public __Task()
        {

        }

        public __Task(Func<object, TResult> function, object state)
        {
            InternalInitializeInlineWorker(
                function,
                state,
                default(CancellationToken),
                default(TaskCreationOptions),
                TaskScheduler.Default
             );
        }

        public void InternalInitializeInlineWorker(
            Func<object, TResult> function,
            object state,
            CancellationToken c,
            TaskCreationOptions o,
            TaskScheduler s)
        {
            if (state == null)
            {
                // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
                // what happened? also, as interface cannot handle ull yet
                Debugger.Break();
            }

            // what if this is a GUI task?

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run
            var MethodType = typeof(FuncOfObjectToObject).Name;

            #region MethodToken
            var MethodToken = ((__MethodInfo)function.Method).MethodToken;

            if (function.Target != null)
                if (function.Target != Native.self)
                {
                    // X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs

                    Delegate InternalTaskExtensionsScope_function = (function.Target as dynamic).InternalTaskExtensionsScope_function;

                    if (InternalTaskExtensionsScope_function == null)
                        throw new InvalidOperationException("InternalInitializeInlineWorker: inline scope sharing not yet implemented");

                    MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).MethodToken;
                }
            #endregion



            #region CreateWorker
            Action<string> CreateWorker = u =>
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
                    u
                    //InternalInlineWorker.GetScriptApplicationSourceForInlineWorker()
                    //global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSourceForInlineWorker
                   );


                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress

                // InternalInitializeInlineWorker { IsIProgress = false, state = [object Object] }
                // e = (a.state instanceof PWjgSJKGsjiGudzxTfGfaA);

                // jsc does not yet support is interface
                // function PWjgSJKGsjiGudzxTfGfaA() {}  PWjgSJKGsjiGudzxTfGfaA.TypeName = "IProgress_1";
                // we should add .Interfaces = []

                //Action<object> OnReportAction = default(__IProgress<object>).Report;
                //var OnReportMethod = OnReportAction.Method;


                var IsIProgress = state is __IProgress<object>;

                var AsTuple2 = state as __Tuple<object, object>;
                var IsTuple2_Item1_IsIProgress = default(bool);

                if (AsTuple2 != null)
                {
                    IsTuple2_Item1_IsIProgress = AsTuple2.Item1 is __IProgress<object>;
                }


                // InternalInitializeInlineWorker: { IsIProgress = true, state = [object Object] }
                var x = default(__IProgress<object>);

                if (IsIProgress)
                {
                    x = (__IProgress<object>)state;
                    state = null;
                }

                if (IsTuple2_Item1_IsIProgress)
                {
                    x = (__IProgress<object>)AsTuple2.Item1;
                    AsTuple2.Item1 = null;
                }

                Console.WriteLine("InternalInitializeInlineWorker: " + new { IsIProgress, IsTuple2_Item1_IsIProgress, state });

                #region postMessage
                w.postMessage(
                    new
                    {
                        InternalInlineWorker.InternalThreadCounter,
                        MethodToken,
                        MethodType,

                        state = state,


                        IsIProgress,
                        IsTuple2_Item1_IsIProgress,

                        __string = (object)xdata___string
                    }
                    ,
                     e =>
                     {
                         // what kind of write backs do we expect?
                         // for now it should be console only


                         //Console.WriteLine(
                         //    "InternalInitializeInlineWorker: new message! "
                         //    + new
                         //    {
                         //        data = string.Join(
                         //           ", ",
                         //           Expando.Of(e.data).GetMemberNames().Select(k => (string)k).ToArray()
                         //        )
                         //    }
                         //);


                         dynamic zdata = e.data;


                         #region AtWrite
                         string AtWrite = zdata.AtWrite;

                         if (!string.IsNullOrEmpty(AtWrite))
                         {
                             Console.Write(AtWrite);
                         }
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
                         {
                             dynamic yield = zdata.yield;
                             if ((object)yield != null)
                             {

                                 object value = yield.value;

                                 //Console.WriteLine("__Task.InternalStart inner complete " + new { yield = new { value } });

                                 this.InternalDispose = delegate
                                 {
                                     Console.WriteLine("at InternalDispose");
                                     w.terminate();
                                 };

                                 this.InternalSetCompleteAndYield((TResult)value);



                                 // when to terminate???
                                 //w.terminate();

                             }
                         }
                         #endregion


                         #region __IProgress_Report
                         if (x != null)
                         {
                             dynamic __IProgress_Report = zdata.__IProgress_Report;

                             if ((object)__IProgress_Report != null)
                             {
                                 object value = __IProgress_Report.value;




                                 //Console.WriteLine("InternalInitializeInlineWorker Report: " + new { __IProgress_Report = new { value } });


                                 x.Report(value);
                             }
                         }
                         #endregion
                     }
                );
                #endregion

                InternalInlineWorker.InternalThreadCounter++;
            };
            #endregion

            this.InternalStart = delegate
            {
                // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs

                var u = InternalInlineWorker.GetScriptApplicationSourceForInlineWorker();

                //GetScriptApplicationSourceForInlineWorker { value = view-source#worker }

                if (u == Worker.ScriptApplicationSource + "#worker")
                {
                    if (Native.document.baseURI == Native.document.location.href)
                    {
                        // X:\jsc.svn\examples\javascript\async\Test\TestDownloadStringTaskAsync\TestDownloadStringTaskAsync\Application.cs

                        Console.WriteLine("Document base not redirected...");

                    }
                    else
                    {
                        Console.WriteLine("Document base redirected...");

                        var w = new WebClient();

                        w.DownloadStringCompleted +=
                            (sender, args) =>
                            {

                                var aFileParts = new[] { args.Result };
                                var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" }); // the blob


                                var url = oMyBlob.ToObjectURL();

                                InternalInlineWorker.ScriptApplicationSourceForInlineWorker = url;

                                u = InternalInlineWorker.GetScriptApplicationSourceForInlineWorker();

                                CreateWorker(u);
                            };

                        // Failed to load resource: the server responded with a status of 400 (Bad Request) http://192.168.1.75:24275/:view-source
                        w.DownloadStringAsync(
                            new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
                        );

                        return;
                    }
                }

                CreateWorker(u);
            };
        }

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
            var MethodToken = ((__MethodInfo)xfunction.Method).MethodToken;

            if (xfunction.Target != null)
                if (xfunction.Target != Native.self)
                {
                    Delegate InternalTaskExtensionsScope_function = (xfunction.Target as dynamic).InternalTaskExtensionsScope_function;

                    if (InternalTaskExtensionsScope_function == null)
                        throw new InvalidOperationException("inline scope sharing not yet implemented");

                    MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).MethodToken;
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
