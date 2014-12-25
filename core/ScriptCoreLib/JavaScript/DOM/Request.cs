using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs
    // https://fetch.spec.whatwg.org/#request
    // X:\jsc.svn\examples\javascript\test\TestServiceWorkerClient\TestServiceWorkerClient\Application.cs
    // X:\jsc.svn\examples\javascript\test\TestServiceWorkerCache\TestServiceWorkerCache\Application.cs

    [Script(HasNoPrototype = true, ExternalTarget = "Request")]
    public class Request
    {
        public readonly string url;
        [Obsolete("not available yet?")]
        public readonly string context;
    }

}
