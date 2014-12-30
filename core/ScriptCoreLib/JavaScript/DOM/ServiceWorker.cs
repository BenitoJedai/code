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
    //[Obsolete("experimental")]
    public class ServiceWorker : Worker
    {
        // Pages using SSL can be sure that only pages using SSL that have certificates identifying them as being from the same domain can access their databases.

        // back in the days, gears plugin allowed to go offline

        // http://sssslide.com/www.slideshare.net/cwdoh/serviceworker-new-game-changer-is-coming

        // https://jakearchibald.github.io/isserviceworkerready/

        // ServiceWorker, in contrast, promises to change the way we all develop Web applications. The impact will be significant.
        // Scripts will be automatically updated every 24 hours, and when the browser re-fetches the main script
        // http://www.programmableweb.com/news/serviceworker-caching-solution-holds-great-promise/how-to/2014/05/19

        // chrome apps only have https client api
        // on android tcps server is not yet prototyped too?

        // would service worker
        // be the one to download/fetch the assetslibary paload?

        // service worker could be used
        // to use to sync datasources
        // "X:\jsc.svn\examples\javascript\forms\FormsDualDataSource\FormsDualDataSource.sln"



        // http://www.i-programmer.info/news/87-web-development/7494-serviceworkers-are-coming.html

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





        // X:\jsc.svn\examples\javascript\test\TestServiceWorkerCache\TestServiceWorkerCache\Application.cs
        #region event onstatechange
        public event System.Action<IEvent> onstatechange
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "statechange");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "statechange");
            }
        }
        #endregion

    }



}
