using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    #region referencesource

    // http://www.chromium.org/blink/serviceworker/testing

    // https://developer.mozilla.org/en-US/docs/Mozilla/Projects/Social_API/Service_worker_API_reference

    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/ServiceWorker.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorker.idl


    // https://jakearchibald.github.io/isserviceworkerready/
    // https://github.com/slightlyoff/ServiceWorker
    // http://www.chromestatus.com/features/6561526227927040

    // http://www.w3.org/TR/service-workers/
    // http://www.chromium.org/blink/serviceworker

    #endregion

    // Enabled by default 40
    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorker")]
    [Obsolete("experimental")]
    public class ServiceWorker : Worker
    {
        // http://blog.chromium.org/2014/12/chrome-40-beta-powerful-offline-and.html
        // https://www.igvita.com/2014/12/15/capability-reporting-with-service-worker/

        // https://www.chromium.org/Home/chromium-security/marking-http-as-non-secure

        // https://github.com/GoogleChrome/samples/tree/gh-pages/service-worker
        // https://code.google.com/p/chromium/issues/detail?id=364627

        // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs

        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\flash\system\Worker.cs

        public string scriptURL;
        public string state;

        // http://www.chromium.org/Home/chromium-security/prefer-secure-origins-for-powerful-new-features

        //  those that do will ignore the AppCache and let the service worker take over.

        // https://docs.google.com/document/d/1cAumcmAFqcgVbMVPiHmYGJ5ySHbcxm9FynYJoZ1UE-Y/edit#heading=h.5w8volg6ryf5
        // X:\jsc.svn\examples\javascript\Test\TestNavigatorServiceWorker\TestNavigatorServiceWorker\Application.cs

        // Service workers only run over HTTPS, for security reasons. Having modified network requests wide open to man in the middle attacks would be really bad.
        // https://developer.mozilla.org/en-US/docs/Web/API/ServiceWorker_API

        // https://matthew-andrews.github.io/serviceworker-simple/
        // https://code.google.com/p/chromium/issues/detail?id=365201
        // https://github.com/slightlyoff/ServiceWorker/blob/master/service_worker.ts

        // http://mxr.mozilla.org/mozilla-central/source/dom/workers/ServiceWorker.h

        // ?
        // http://src.chromium.org/viewvc/blink/trunk/Source/core/workers/AbstractWorker.idl

        // how dos this compare to extensions, webviews?




        // in 2007 we had google gears, it got discontiued.
        // lets ait and see if this ServiceWorker feature from chrome delivers as promised 


        // how does it relate to Task.Run, Worker and SharedWorker?
        // this will replace AppCache

        // works only on https? does jsc inline server support SSL?
        // 20141018 now it does.




    }



}
