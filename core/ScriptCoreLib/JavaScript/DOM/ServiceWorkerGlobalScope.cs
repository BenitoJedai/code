using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // DOM is not actually the correct namespace, DOM is unavailable within workers isnt it?

    #region referencesource

    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/ServiceWorkerGlobalScope.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorkerGlobalScope.idl
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\ServiceWorker.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\DedicatedWorkerGlobalScope.cs

    #endregion

    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerGlobalScope")]
    [Obsolete("experimental")]
    public class ServiceWorkerGlobalScope : WorkerGlobalScope
    {
        // X:\jsc.svn\examples\javascript\test\TestServiceWorker\TestServiceWorker\Application.cs
        // chrome just removed it..?
        public readonly string scope;

        // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs

        // http://www.chromium.org/blink/serviceworker/service-worker-faq
        // chrome://serviceworker-internals
        // chrome://inspect/#service-workers

        // how does this relate to UI performance tracker?

        // X:\jsc.svn\examples\javascript\test\TestServiceWorkerClient\TestServiceWorkerClient\Application.cs

        public readonly ServiceWorkerClients clients;

        // tested by?


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


        #region event onfetch
        public event System.Action<FetchEvent> onfetch
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "fetch");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "fetch");
            }
        }
        #endregion
    }
}
