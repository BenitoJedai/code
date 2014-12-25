using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://fetch.spec.whatwg.org/#body
    // X:\jsc.svn\examples\javascript\test\TestServiceWorkerCache\TestServiceWorkerCache\Application.cs

    // name clash? name to IBody?
    [Script(HasNoPrototype = true, ExternalTarget = "Body")]
    public class Body
    {
    }

}
