using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://dvcs.w3.org/hg/progress/raw-file/tip/Overview.html#progressevent
    // X:\jsc.svn\core\ScriptCoreLib.Redux\ScriptCoreLib.Redux\JavaScript\IDL\progressevents.idl
    [Script(HasNoPrototype = true, ExternalTarget = "ProgressEvent")]
    public class ProgressEvent : IEvent
    {
        public readonly bool lengthComputable;
        public readonly long loaded;
        public readonly long total;
    }
}
