﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "Worker")]
    public class Worker : IEventTarget
    {
        // http://msdn.microsoft.com/en-us/library/windows/apps/hh453409.aspx

        public const string ScriptApplicationSource = "view-source";

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


        [Obsolete("https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130816-web-worker")]
        public Worker(Action<DedicatedWorkerGlobalScope> yield)
        {

        }
    }


    [Script]
    public class InternalInlineWorker
    {
        // WorkerGlobalScope

        static readonly List<Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope>> Handlers = new List<Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope>>();
        static readonly List<Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope>> SharedWorkerHandlers = new List<Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope>>();

        public static void InternalAddSharedWorker(Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope> yield)
        {
            Console.WriteLine("InternalInlineWorker InternalAddSharedWorker");

            SharedWorkerHandlers.Add(yield);
        }

        public static void InternalAdd(Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope> yield)
        {
            Console.WriteLine("InternalInlineWorker InternalAdd");

            Handlers.Add(yield);
        }


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
        public static global::ScriptCoreLib.JavaScript.DOM.Worker InternalConstructor(Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope> yield)
        {
            // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.TakeWhile(System.Collections.Generic.IEnumerable`1[[System.Action`1[[ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope, ScriptCoreLib, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], System.Func`2[[System.Action`1[[ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope, ScriptCoreLib, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]
            var index = -1;

            for (int i = 0; i < Handlers.Count; i++)
            {
                if (Handlers[i] == yield)
                    index = i;
            }

            Console.WriteLine("InternalInlineWorker InternalConstructor " + new { index });

            // discard params

            var w = new global::ScriptCoreLib.JavaScript.DOM.Worker(
                global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSource
                + "#" + index
                + "#worker"
            );

            // we need some kind of per Application activation index
            // so multiple inline workers could know which they are.


            //var c = new global::ScriptCoreLib.JavaScript.DOM.MessageChannel();

            //c.port1.onmessage +=
            //    e =>
            //    {
            //        Console.Write("" + e.data);
            //    };

            //c.port1.start();
            //c.port2.start();

            w.postMessage("" + index,
                 e =>
                 {
                     Console.Write("" + e.data);
                 }
            );

            return w;
        }

        [System.ComponentModel.Description("Will run as JavaScript Web Worker")]
        public static void InternalInvoke(Action default_yield)
        {
            // called by X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.InjectJavaScriptBootstrap.cs
            // tested by X:\jsc.svn\examples\javascript\SharedWorkerExperiment\Application.cs

            if (Native.worker != null)
            {

                #region #worker
                var href = Native.worker.location.href;
                if (href.EndsWith("#worker"))
                {
                    Native.worker.onmessage +=
                        e =>
                        {
                            if (default_yield == null)
                                return;

                            var s = "" + e.data;
                            if (!string.IsNullOrEmpty(s))
                            {
                                var index = int.Parse(s);
                                if (index >= 0)
                                    if (index < Handlers.Count)
                                    {
                                        var yield = Handlers[index];


                                        #region ConsoleFormWriter
                                        var w = new InternalInlineWorkerTextWriter();

                                        var o = Console.Out;

                                        Console.SetOut(w);

                                        w.AtWrite =
                                             x =>
                                             {
                                                 foreach (MessagePort port in e.ports)
                                                 {

                                                     port.postMessage(x, new MessagePort[0]);
                                                 }

                                             };

                                        #endregion

                                        default_yield = null;

                                        yield(Native.worker);
                                    }

                            }
                        };

                    return;
                }
                #endregion

            }
            else if (Native.sharedworker != null)
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
