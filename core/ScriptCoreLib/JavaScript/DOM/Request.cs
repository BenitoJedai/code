using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs
    // https://fetch.spec.whatwg.org/#request
    // X:\jsc.svn\examples\javascript\test\TestServiceWorkerClient\TestServiceWorkerClient\Application.cs

    [Script(HasNoPrototype = true, ExternalTarget = "Request")]
    public class Request
    {
        public readonly string url;
        public readonly string context;
    }

}
