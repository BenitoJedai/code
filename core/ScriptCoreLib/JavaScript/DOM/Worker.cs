﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Extensions;
using System.Threading;
using ScriptCoreLib.JavaScript.BCLImplementation.System;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script]
    public delegate void ActionOfDedicatedWorkerGlobalScope(DedicatedWorkerGlobalScope scope);

    [Script(HasNoPrototype = true, ExternalTarget = "Worker")]
    public class Worker : IEventTarget
    {
        // http://msdn.microsoft.com/en-us/library/windows/apps/hh453409.aspx

        public const string ScriptApplicationSource = "view-source";
        public const string ScriptApplicationSourceForInlineWorker = ScriptApplicationSource + "#worker";

        #region event onmessage
        public event System.Action<MessageEvent> onmessage
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "message");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "message");
            }
        }
        #endregion

        public void postMessage(object message, MessagePort[] transfer) { }

        public Worker(string uri = ScriptApplicationSource)
        {

        }




        //[Obsolete("https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130816-web-worker")]
        public Worker(Action<DedicatedWorkerGlobalScope> yield)
        {

        }
    }


    [Script]
    public class InternalInlineWorker
    {
        // WorkerGlobalScope

        //static readonly List<Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope>> Handlers = new List<Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope>>();
        static readonly List<Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope>> SharedWorkerHandlers = new List<Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope>>();

        [Obsolete]
        public static void InternalAddSharedWorker(Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope> yield)
        {
            Console.WriteLine("InternalInlineWorker InternalAddSharedWorker");

            SharedWorkerHandlers.Add(yield);
        }

        [Obsolete("the hacky way to share static string fields..")]
        internal static object __string
        {
            get
            {
                return new IFunction("return __string;").apply(Native.window);
            }
        }

        [Obsolete]
        public static void InternalAdd(Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope> yield)
        {
            // thanks compiler, but we are doing this now on runtime
        }

        // how many threads have we created? lets start at ten
        internal static int InternalThreadCounter = 10;

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130812-sharedworker
        public static global::ScriptCoreLib.JavaScript.DOM.SharedWorker InternalSharedWorkerConstructor(Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope> yield)
        {
            var index = -1;

            for (int i = 0; i < SharedWorkerHandlers.Count; i++)
            {
                if (SharedWorkerHandlers[i] == yield)
                    index = i;
            }

            Console.WriteLine("InternalInlineWorker InternalSharedWorkerConstructor " + new { index });


            var w = new global::ScriptCoreLib.JavaScript.DOM.SharedWorker(
                    global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSource
                    + "#" + index
                    + "#sharedworker"
            );

            //w.port.start();
            //w.port.postMessage("" + index,
            //      e =>
            //      {
            //          // since this is shared, we actually need it only once
            //          // need to deduplicate

            //          Console.Write("" + e.data);
            //      }
            // );


            return w;
        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130816-web-worker
        public static global::ScriptCoreLib.JavaScript.DOM.Worker InternalConstructor(Action<DedicatedWorkerGlobalScope> yield)
        {
            var MethodToken = ((__MethodInfo)yield.Method).MethodToken;

            Console.WriteLine("InternalInlineWorker InternalConstructor " + new { MethodToken, InternalThreadCounter });

            // discard params




            // we need some kind of per Application activation index
            // so multiple inline workers could know which they are.

            // x:\jsc.svn\examples\javascript\Test\TestThreadStart\TestThreadStart\Application.cs
            // share scope


            dynamic xdata = new object();

            xdata.InternalThreadCounter = InternalInlineWorker.InternalThreadCounter;
            xdata.MethodToken = MethodToken;
            xdata.MethodType = typeof(ActionOfDedicatedWorkerGlobalScope).Name;

            #region xdata___string
            dynamic xdata___string = new object();

            // how much does this slow us down?
            // connecting to a new scope, we need a fresh copy of everything
            // we can start with strings
            foreach (ExpandoMember nn in Expando.Of(__string).GetMembers())
            {
                if (nn.Value != null)
                    xdata___string[nn.Name] = nn.Value;
            }
            #endregion

            xdata.__string = xdata___string;

            InternalInlineWorker.InternalThreadCounter++;

            var w = new global::ScriptCoreLib.JavaScript.DOM.Worker(
                global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSourceForInlineWorker
            );

            w.postMessage((object)xdata,
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
                         dynamic target = __string;
                         var m = Expando.Of(zdata___string).GetMembers();

                         foreach (ExpandoMember nn in m)
                         {
                             Console.WriteLine("Worker has sent changes " + new { nn.Name });

                             target[nn.Name] = nn.Value;
                         }
                         #endregion
                     }
                     #endregion

                 }
            );

            return w;
        }




        static void __worker_onfirstmessage(MessageEvent e,
            int InternalThreadCounter,
             object data___string,
              string MethodToken,
            string MethodType,
            object state,
            __Task<object>[] TaskArray
            )
        {
            #region ConsoleFormWriter
            var w = new InternalInlineWorkerTextWriter();

            var o = Console.Out;

            Console.SetOut(w);

            w.AtWrite =
                 x =>
                 {
                     dynamic zdata = new object();

                     zdata.AtWrite = x;


                     foreach (MessagePort port in e.ports)
                     {


                         port.postMessage((object)zdata, new MessagePort[0]);
                     }

                 };

            #endregion

            __Thread.InternalCurrentThread.ManagedThreadId = InternalThreadCounter;
            __Thread.InternalCurrentThread.IsBackground = true;


            Console.WriteLine(
                new
                {
                    Native.worker.location.href,
                    MethodToken,
                    MethodType,
                    Thread.CurrentThread.ManagedThreadId
                }
            );

            var MethodTokenReference = IFunction.Of(MethodToken);


            Console.WriteLine(
                 new
                 {
                     MethodTokenReference,
                     Thread.CurrentThread.ManagedThreadId
                 }
             );

            // whats the type?



            #region __string
            dynamic target = __string;
            var m = Expando.Of(data___string).GetMembers();
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130826-domainmemory
            foreach (ExpandoMember nn in m)
            {
                target[nn.Name] = nn.Value;

                var trigger = "set_" + nn.Name;
                var trigger_default = IFunction.Of(trigger);

                (Native.self as dynamic)[trigger] = IFunction.OfDelegate(
                    new Action<string>(
                        Value =>
                        {
                            if (nn.Value == Value)
                                return;

                            trigger_default.apply(null, Value);

                            #region sync one field only

                            {
                                dynamic zdata = new object();
                                dynamic zdata___string = new object();

                                zdata.__string = zdata___string;


                                zdata___string[nn.Name] = Value;

                                // prevent sync via diff
                                nn.Value = Value;

                                foreach (MessagePort port in e.ports)
                                {
                                    port.postMessage((object)zdata, new MessagePort[0]);
                                }

                            }


                            #endregion
                        }
                    )
                );
            }
            #endregion


            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run
                // for now we only support static calls

                dynamic zdata = new object();


                if (MethodType == typeof(ActionOfDedicatedWorkerGlobalScope).Name)
                {
                    MethodTokenReference.apply(null, Native.worker);
                }
                else if (MethodType == typeof(FuncOfObjectToObject).Name)
                {
                    var value = MethodTokenReference.apply(null, state);
                    var yield = new { value };

                    //Console.WriteLine(new { yield });

                    zdata.yield = yield;

                    // now what?
                }
                else if (MethodType == typeof(FuncOfTaskToObject).Name)
                {
                    // need to reconstruct the caller task?


                    var value = MethodTokenReference.apply(null, TaskArray.Single());
                    var yield = new { value };

                    //Console.WriteLine(new { yield });

                    zdata.yield = yield;

                    // now what?
                }
                else if (MethodType == typeof(FuncOfTaskOfObjectArrayToObject).Name)
                {
                    // need to reconstruct the caller task?

                    Console.WriteLine("__worker_onfirstmessage: " + new { TaskArray = TaskArray.Length });

                    //Debugger.Break();

                    var args = new object[] { TaskArray };

                    var value = MethodTokenReference.apply(
                        o: null,

                        // watch out
                        args: args
                    );

                    var yield = new { value };

                    //Console.WriteLine(new { yield });

                    zdata.yield = yield;

                    // now what?
                }

                #region [sync] diff and upload changes to DOM context, the latest now
                {
                    dynamic zdata___string = new object();

                    zdata.__string = zdata___string;

                    foreach (ExpandoMember nn in m)
                    {
                        string Value = (string)Expando.InternalGetMember((object)target, nn.Name);
                        // this is preferred:
                        //string Value = target[nn.Name];

                        if (Value != nn.Value)
                        {
                            zdata___string[nn.Name] = Value;
                        }
                    }




                }
                #endregion


                foreach (MessagePort port in e.ports)
                {
                    port.postMessage((object)zdata, new MessagePort[0]);
                }
            }
        }

        [System.ComponentModel.Description("Will run as JavaScript Web Worker")]
        public static void InternalInvoke(Action default_yield)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run

            // called by X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.InjectJavaScriptBootstrap.cs
            // tested by X:\jsc.svn\examples\javascript\SharedWorkerExperiment\Application.cs

            if (Native.worker != null)
                if (Native.worker.location.href.EndsWith("#worker"))
                {
                    Native.worker.onfirstmessage +=
                        e =>
                        {
                            dynamic e_data = e.data;

                            int InternalThreadCounter = e_data.InternalThreadCounter;
                            object data___string = e_data.__string;
                            string MethodToken = e_data.MethodToken;
                            string MethodType = e_data.MethodType;


                            // used byTask.ctor 
                            object state = e_data.state;


                            //                            if (!(WwoABMesEj6KKMa59FrqOw))
                            //{
                            //  WwoABMesEj6KKMa59FrqOw = _6QIABkmHWjqHBHzPjs4Qsg(kAIABiO2_aTySKN41_aKL3ew(0, sBkABtC6ljmbrk8x5kK6iA(new ctor$UBkABhfpfj6IFLf_a4gLSZg(type$AAAAAAAAAAAAAAAAAAAAAA)), sBkABtC6ljmbrk8x5kK6iA(new ctor$UBkABhfpfj6IFLf_a4gLSZg(type$G74_bZyECQzqq6_bVD_ak58Wg))));
                            //}
                            // X:\jsc.svn\examples\javascript\forms\ParallelTaskExperiment\ParallelTaskExperiment\ApplicationControl.cs


                            // used by ContinueWith

                            // jsc, why cant i do arrays?
                            var __TaskArray = (object[])(object)e_data.TaskArray;

                            //Console.WriteLine(new { __TaskArray });


                            __Task<object>[] TaskArray = null;

                            if (__TaskArray != null)
                            {
                                // reviwing parent tasks the primitive way
                                TaskArray = __TaskArray.Select(
                                    (dynamic k) =>
                                    {
                                        object Result = k.Result;

                                        return new __Task<object> { Result = Result };
                                    }
                                ).ToArray();

                            }
                            //var task = new __Task<object> { Result  = ResuWot };


                            // 3 dynamic uses messes up jsc? why?

                            __worker_onfirstmessage(
                                e,
                                InternalThreadCounter,
                                data___string,
                                MethodToken,
                                MethodType,
                                state,
                                TaskArray
                                );
                        };

                    return;
                }


            if (Native.sharedworker != null)
            {

                #region #sharedworker
                var href = Native.sharedworker.location.href;
                if (href.EndsWith("#sharedworker"))
                {
                    var s = href.Substring(0, href.Length - "#sharedworker".Length);
                    var si = s.LastIndexOf("#");

                    s = s.Substring(si + 1);

                    if (!string.IsNullOrEmpty(s))
                    {
                        var index = int.Parse(s);
                        if (index >= 0)
                            if (index < SharedWorkerHandlers.Count)
                            {
                                var yield = SharedWorkerHandlers[index];


                                // do we have to regenerate onconnect event?
                                yield(Native.sharedworker);
                            }

                    }




                    return;
                }
                #endregion

            }

            default_yield();

        }
    }

    [Script]
    public class InternalInlineWorkerTextWriter : TextWriter
    {
        public Action<string> AtWrite;
        public Action<string> AtWriteLine;

        public override void Write(object value)
        {
            AtWrite("" + value);
        }


        public override void Write(string value)
        {
            AtWrite(value);
        }

        public override void WriteLine(string value)
        {
            AtWriteLine(value);

        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
