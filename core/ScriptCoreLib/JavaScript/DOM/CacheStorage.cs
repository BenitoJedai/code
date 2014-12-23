using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://slightlyoff.github.io/ServiceWorker/spec/service_worker/index.html#cache-objects
    [Script(HasNoPrototype = true, ExternalTarget = "CacheStorage")]
    public class CacheStorage
    {
        // https://jakearchibald.github.io/isserviceworkerready/
        // Only available within ServiceWorkers

        public IPromise<string[]> keys()
        {
            return null;
        }
    }
}
