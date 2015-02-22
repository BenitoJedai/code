using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/RTCPeerConnection.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/mediastream/RTCPeerConnection.idl
    // 20150222 - spec has been updated to promises. when will chrome follow?

    [Script(HasNoPrototype = true, ExternalTarget = "RTCPeerConnection")]
    public class RTCPeerConnection
    {
        // chrome://webrtc-internals/
        // http://www.html5rocks.com/en/tutorials/webrtc/datachannels/
        // http://www.html5rocks.com/en/tutorials/webrtc/basics/#toc-rtcdatachannel

        // https://bloggeek.me/webrtc-data-channel-uses/

        // http://www.w3.org/TR/webrtc/
        // http://xsockets.net/docs/3/webrtc
        // http://stackoverflow.com/questions/15806617/creating-a-webrtc-receiver

        // http://caniuse.com/#feat=rtcpeerconnection
        // http://blog.salemove.com/webrtc-vs-flash-not-much-of-a-competition/

        // http://googletesting.blogspot.se/2014/08/chrome-firefox-webrtc-interop-test-pt-1.html

        // how does this relate to UDP / encrypted UDP

        // can we do LAN to LAN yet?
        // will need it for VR!

        // http://apps.playcanvas.com/max/tanky/colorTanks

        // http://www.tokbox.com/blog/announcing-the-end-of-life-of-the-opentok-1-0-platform/

        // https://bugzilla.mozilla.org/show_bug.cgi?id=922363

        // tested by
        // X:\jsc.svn\examples\javascript\Test\TestPeerConnection\TestPeerConnection\Application.cs
        // X:\jsc.svn\examples\javascript\p2p\RTCPeerIPAddress\RTCPeerIPAddress\Application.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150222


        public void createOffer(Action<RTCSessionDescription> successCallback) { }
    }

    public static class RTCPeerConnectionExtensions
    {
        public static Task<RTCSessionDescription> createOffer(this RTCPeerConnection that)
        {
            var x = new TaskCompletionSource<RTCSessionDescription>();

            that.createOffer(successCallback: x.SetResult);

            return x.Task;
        }
    }
}
