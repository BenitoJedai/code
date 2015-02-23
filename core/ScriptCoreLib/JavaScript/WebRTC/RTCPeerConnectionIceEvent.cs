using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/RTCPeerConnectionIceEvent.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/mediastream/RTCPeerConnectionIceEvent.idl

    [Script(HasNoPrototype = true, ExternalTarget = "RTCPeerConnectionIceEvent")]
    public class RTCPeerConnectionIceEvent : IEvent
    {
        public readonly RTCIceCandidate candidate;



    }
}
