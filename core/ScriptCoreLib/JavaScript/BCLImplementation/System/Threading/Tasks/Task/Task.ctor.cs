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
    internal partial class __Task<TResult> : __Task
    {

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
            var MethodToken = ((__MethodInfo)function.Method).InternalMethodToken;

            if (function.Target != null)
                if (function.Target != Native.self)
                {
                    // X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs

                    Delegate InternalTaskExtensionsScope_function = (function.Target as dynamic).InternalTaskExtensionsScope_function;

                    if (InternalTaskExtensionsScope_function == null)
                        throw new InvalidOperationException("InternalInitializeInlineWorker: inline scope sharing not yet implemented");

                    MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).InternalMethodToken;
                }
            #endregion



            #region CreateWorker
            Action<string> CreateWorker = uri =>
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


                var worker = new global::ScriptCoreLib.JavaScript.DOM.Worker(
                    uri
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
                worker.postMessage(
                    new
                    {
                        InternalInlineWorker.InternalThreadCounter,
                        MethodToken,
                        MethodType,



                        // X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
                        // fields
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
                             // thread is writing to console isnt it.
                             // we have requested to be notified of it on the main thread instead.


                             if (AtWrite == "\r\n")
                                 Console.WriteLine();
                             else
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
                                     worker.terminate();
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



            #region InternalStart
            this.InternalStart = delegate
            {
                // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs



                #region GetScriptApplicationSourceForInlineWorker
                var u = InternalInlineWorker.GetScriptApplicationSourceForInlineWorker();

                //GetScriptApplicationSourceForInlineWorker { value = view-source#worker }

                if (u == Worker.ScriptApplicationSource + "#worker")
                {
                    if (Native.document.baseURI == Native.document.location.href)
                    {
                        // X:\jsc.svn\examples\javascript\async\Test\TestDownloadStringTaskAsync\TestDownloadStringTaskAsync\Application.cs

                        // X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
                        //Console.WriteLine("Document base not redirected...");
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
                #endregion


                CreateWorker(u);
            };
            #endregion

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

    }
}
