using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "Worker")]
    public class Worker : IEventTarget
    {
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

        public Worker(string uri = "view-source")
        {

        }

        [Obsolete("not implemented yet, jsc will have to split your Application here. https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130816-web-worker")]
        public Worker(Action<DedicatedWorkerGlobalScope> yield)
        {

        }
    }

    [Script]
    public class InternalInlineWorker
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130816-web-worker
        public static Worker InternalConstructor(Action<DedicatedWorkerGlobalScope> yield)
        {
            Console.WriteLine("InternalInlineWorker InternalConstructor");

            // discard params

            var w = new Worker();

            // we need some kind of per Application activation index
            // so multiple inline workers could know which they are.

            return w;
        }
    }
}
