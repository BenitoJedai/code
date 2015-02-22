using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/RTCSessionDescription.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/mediastream/RTCSessionDescription.idl

    [Script(HasNoPrototype = true, ExternalTarget = "RTCSessionDescription")]
    public class RTCSessionDescription
    {
        public string sdp;

    }
}
