using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://www.w3.org/TR/service-workers/
    // http://www.chromium.org/blink/serviceworker
    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorker")]
    [Obsolete("experimental")]
    public class ServiceWorker : Worker
    {
        // how dos this compare to extensions, webviews?




        // in 2007 we had google gears, it got discontiued.
        // lets ait and see if this ServiceWorker feature from chrome delivers as promised 


        // how does it relate to Task.Run, Worker and SharedWorker?
        // this will replace AppCache

        // works only on https? does jsc inline server support SSL?
    }



    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\DedicatedWorkerGlobalScope.cs
    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerGlobalScope")]
    [Obsolete("experimental")]
    public class ServiceWorkerGlobalScope : WorkerGlobalScope
    { 

    }
}
