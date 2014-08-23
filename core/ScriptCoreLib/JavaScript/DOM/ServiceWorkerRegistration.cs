using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // DOM is not actually the correct namespace, DOM is unavailable within workers isnt it?

    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorkerRegistration.idl


    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerRegistration")]
    [Obsolete("experimental")]
    public class ServiceWorkerRegistration
    {
        // http://www.chromium.org/blink/serviceworker/service-worker-faq
        // chrome://serviceworker-internals
        // chrome://inspect/#service-workers

        public readonly ServiceWorker installing;
        public readonly ServiceWorker waiting;
        // current ?

        public readonly ServiceWorker active;

        public string scope;

    }
}
