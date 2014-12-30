using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://slightlyoff.github.io/ServiceWorker/spec/service_worker/index.html#cache-objects
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/CacheStorage.idl

    [Script(HasNoPrototype = true, ExternalTarget = "CacheStorage")]
    public class CacheStorage
    {
        // https://code.google.com/p/chromium/issues/detail?id=425426

        // The Cache, however, will not be shared by different ServiceWorkers; each ServiceWorker has its own exclusive caches object.

        // https://jakearchibald.github.io/isserviceworkerready/
        // Only available within ServiceWorkers

        public IPromise<Response> match(object request)
        {
            return null;
        }

        public IPromise<Cache> open(string cacheName)
        {
            // X:\jsc.svn\examples\javascript\test\TestServiceWorkerAssetCache\TestServiceWorkerAssetCache\Application.cs

            return null;
        }

        public IPromise<string[]> keys()
        {
            return null;
        }
    }
}
