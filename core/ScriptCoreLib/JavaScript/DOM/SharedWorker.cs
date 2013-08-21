using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "SharedWorker")]
    public class SharedWorker : IEventTarget
    {
        public readonly MessagePort port;

        public SharedWorker(string scriptURL)
        { }
        public SharedWorker(string scriptURL, string name)
        { }

        [Obsolete("https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130812-sharedworker")]
        public SharedWorker(Action<SharedWorkerGlobalScope> yield)
        {

        }
    }
}
