using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://slightlyoff.github.io/ServiceWorker/spec/service_worker/index.html#cache-objects
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/Cache.idl

    [Script(HasNoPrototype = true, ExternalTarget = "Cache")]
    public class Cache
    {
        // https://twitter.com/andrewsmatt/status/508600185245560833
        // https://code.google.com/p/chromium/issues/detail?id=425426

        // The Cache, however, will not be shared by different ServiceWorkers; each ServiceWorker has its own exclusive caches object.

        // https://jakearchibald.github.io/isserviceworkerready/
        // Only available within ServiceWorkers

        [Obsolete("not yet available")]
        public IPromise addAll(params string[] requests) { return null; }



        // X:\jsc.svn\examples\javascript\test\TestServiceWorkerAssetCache\TestServiceWorkerAssetCache\Application.cs
        public IPromise<object> put(Request request, Response response) { return null; }
    }
}
