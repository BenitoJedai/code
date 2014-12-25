using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    //https://fetch.spec.whatwg.org/#response
    // X:\jsc.svn\examples\javascript\test\TestServiceWorkerCache\TestServiceWorkerCache\Application.cs

    // name clash? name to IResponse?
    [Script(HasNoPrototype = true, ExternalTarget = "Response")]
    public class Response : Body
    {
        // typedef (Blob or BufferSource or FormData or URLSearchParams or USVString) BodyInit;
        public Response(object BodyInit)
        {

        }

    }

}
