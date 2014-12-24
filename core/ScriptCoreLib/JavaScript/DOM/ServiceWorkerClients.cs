using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs
    // http://www.w3.org/TR/service-workers/#service-worker-client-interface
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/ServiceWorkerClients.idl


    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerClients")]
    public class ServiceWorkerClients
    {
        // X:\jsc.svn\examples\javascript\test\TestServiceWorkerClient\TestServiceWorkerClient\Application.cs

        //[CallWith=ScriptState] Promise<sequence<ServiceWorkerClient>?> getAll(optional ServiceWorkerClientQueryOptions options);
        public IPromise<ServiceWorkerClient[]> getAll() { return null; }
    }

}
