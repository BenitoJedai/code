using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Promise.webidl
    // https://code.google.com/p/dart/source/browse/third_party/WebCore/core/dom/Promise.idl?spec=svn26952&r=26952
    // https://chromium.googlesource.com/chromium/blink/+/072dd87acc8859c3a35441f24b1486faa12efe42/Source/core/dom/Promise.idl
    // chosing the prefix of I to prevent intellisense pollution. 
    // this feature in C# is to be available as async/await Task<>
    // this might be what Windows Runtime API def feels within .NET def
    // how does it relate to Nullable<> ?

    [Script(HasNoPrototype = true, ExternalTarget = "Promise")]
    public class IPromise
    {
    }

    [Script(HasNoPrototype = true, ExternalTarget = "Promise")]
    public class IPromise<T> : IPromise
    {
        // https://people.mozilla.org/~jorendorff/es6-draft.html#sec-promise-objects
        // jsc shall translate to IFunction here
        public void then(Action<T> serviceWorker)
        {
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs

        }
    }
}
