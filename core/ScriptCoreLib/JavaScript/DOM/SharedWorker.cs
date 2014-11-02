using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/SharedWorker.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/workers/SharedWorker.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/workers/SharedWorker.cpp

    [Script(HasNoPrototype = true, ExternalTarget = "SharedWorker")]
    public class SharedWorker : IEventTarget
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141102
        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\flash\system\Worker.cs

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
