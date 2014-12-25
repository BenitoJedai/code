using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerClient\TestServiceWorkerClient\Application.cs
    // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs
    // http://www.w3.org/TR/service-workers/#service-worker-client-interface
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorkerClient.idl

    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerClient")]
    public class ServiceWorkerClient
    {
        // somewhat like chrome.AppWindow ?

        [Obsolete("not available yet?")]
        public readonly bool focused;


        // can we link it to a notification?
        // how will this work?

        // jsc, when can we return Task<> ?
        public IPromise<bool> focus()
        {
            return null;
        }

        [Obsolete("not available yet?")]
        public readonly string frameType;
        [Obsolete("not available yet?")]
        public readonly string visibilityState;
        [Obsolete("not available yet?")]
        public readonly string url;



        // what about async version?
        public void postMessage(object message, MessagePort[] transfer) { }

    }

}
