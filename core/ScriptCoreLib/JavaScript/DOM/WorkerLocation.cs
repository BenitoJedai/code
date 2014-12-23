using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/WorkerLocation.webidl

    [Script(HasNoPrototype = true)]
    public class WorkerLocation
    {
        public readonly string href;
        public readonly string hash;
    }

}
