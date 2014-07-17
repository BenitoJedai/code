using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // DOM is not actually the correct namespace, DOM is unavailable within workers isnt it?

    #region referencesource

    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorkerGlobalScope.idl
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\ServiceWorker.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\DedicatedWorkerGlobalScope.cs

    #endregion

    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerGlobalScope")]
    [Obsolete("experimental")]
    public class ServiceWorkerGlobalScope : WorkerGlobalScope
    {
        // http://www.chromium.org/blink/serviceworker/service-worker-faq
        // chrome://serviceworker-internals
        // chrome://inspect/#service-workers

        // how does this relate to UI performance tracker?


        // tested by?

    }
}
