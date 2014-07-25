using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/RTCPeerConnection.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/mediastream/RTCPeerConnection.idl

    [Script(HasNoPrototype = true, ExternalTarget = "RTCPeerConnection")]
    public class RTCPeerConnection
    {
        // https://bugzilla.mozilla.org/show_bug.cgi?id=922363

        // tested by
        // X:\jsc.svn\examples\javascript\Test\TestPeerConnection\TestPeerConnection\Application.cs

    }
}
