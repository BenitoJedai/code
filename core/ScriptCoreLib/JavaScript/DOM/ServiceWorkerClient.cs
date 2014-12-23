using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs
    // http://www.w3.org/TR/service-workers/#service-worker-client-interface

    [Script(HasNoPrototype = true, ExternalTarget = "ServiceWorkerClient")]
    public class ServiceWorkerClient
    {
        public readonly string url;
    }

}
