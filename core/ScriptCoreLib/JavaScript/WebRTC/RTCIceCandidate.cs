using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/RTCIceCandidate.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/mediastream/RTCIceCandidate.idl

    [Script(HasNoPrototype = true, ExternalTarget = "RTCIceCandidate")]
    public class RTCIceCandidate
    {
        public string candidate;
        public string sdpMid;
        public ushort sdpMLineIndex;


    }
}
