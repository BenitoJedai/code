using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "Worker")]
    public class Worker : IEventTarget
    {
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
        public static readonly List<Action<DedicatedWorkerGlobalScope>> Handlers = new List<Action<DedicatedWorkerGlobalScope>>();

        public static void InternalAdd(Action<DedicatedWorkerGlobalScope> yield)
        {
            Console.WriteLine("InternalInlineWorker InternalAdd");

            Handlers.Add(yield);

        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130816-web-worker
        public static Worker InternalConstructor(Action<DedicatedWorkerGlobalScope> yield)
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

            var w = new Worker(Worker.ScriptApplicationSource + "#" + index);

            // we need some kind of per Application activation index
            // so multiple inline workers could know which they are.

            return w;
        }

        [System.ComponentModel.Description("Will run as JavaScript Web Worker")]
        public static void InternalInvoke(Action default_yield)
        {
            var href = Native.worker.location.href;
            var i = href.IndexOf("#");

            if (i > 0)
            {
                var s = href.Substring(i + 1);
                if (!string.IsNullOrEmpty(s))
                {
                    var index = int.Parse(s);
                    if (index >= 0)
                        if (index < Handlers.Count)
                        {
                            var yield = Handlers[index];
                            yield(Native.worker);


                            return;
                        }

                }
            }


            default_yield();
        }
    }
}
