using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/ProgressEvent.webidl
    // https://dvcs.w3.org/hg/progress/raw-file/tip/Overview.html#progressevent
    // X:\jsc.svn\core\ScriptCoreLib.Redux\ScriptCoreLib.Redux\JavaScript\IDL\progressevents.idl
    [Script(HasNoPrototype = true, ExternalTarget = "ProgressEvent")]
    public class ProgressEvent : IEvent
    {
        // tested by ?

        public readonly bool lengthComputable;
        public readonly long loaded;
        public readonly long total;
    }
}
