using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    #region referencesource


    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/ServiceWorkerContainer.webidl
    //http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorkerContainer.idl

    #endregion

    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerContainer")]
    [Obsolete("experimental, need SSL server?")]
    public class ServiceWorkerContainer
    {
        // http://www.w3.org/TR/service-workers/#service-worker-obj
        // tested by
        // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs

        // https://jakearchibald.github.io/isserviceworkerready/
        // http://mxr.mozilla.org/mozilla-central/source/dom/workers/ServiceWorkerContainer.h


        public readonly ServiceWorker installing;
        public readonly ServiceWorker waiting;
        // current ?

        public readonly ServiceWorker controller;
        public readonly ServiceWorker active;


        // Task ?
        readonly IPromise ready;

        //Promise register(ScalarValueString url, optional Dictionary options);
        // https://code.google.com/p/chromium/issues/detail?id=395928
        // http://www.chromium.org/Home/chromium-security/prefer-secure-origins-for-powerful-new-features
        // Only secure origins are allowed. http://goo.gl/lq4gCo
        // can we upgrade our android, chrome, clr servers to do SSL?
        // https://code.google.com/p/chromium/issues/detail?id=362214
        public IPromise<ServiceWorker> register(string url, object options) { return null; }

        //Promise unregister(optional ScalarValueString scope = "/*");
        public IPromise unregister(string scope = "/*") { return null; }


        // http://www.chromium.org/blink/serviceworker
        // https://github.com/w3c-webmob/ServiceWorkersDemos

    }



}
