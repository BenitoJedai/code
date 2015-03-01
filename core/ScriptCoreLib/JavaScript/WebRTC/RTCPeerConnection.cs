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
    public class RTCPeerConnection : IEventTarget
    {
        // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerVisualizedScreens\TestServiceWorkerVisualizedScreens\Application.cs

        // X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs
        // X:\jsc.svn\examples\javascript\forms\FormsForSecondaryScreen\FormsForSecondaryScreen\Application.cs

        // X:\opensource\other\WebRTC-Stack-Sample\WebRTC C Sample\Microstack\ILibWebRTC.c
        // X:\opensource\other\WebRTC-Stack-Sample\WebRTC C# Sample\MainForm.cs

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


        public void setRemoteDescription(RTCSessionDescription description, IFunction successCallback) { }

        public void createAnswer(Action<RTCSessionDescription> successCallback) { }

        public void setLocalDescription(RTCSessionDescription description, IFunction successCallback) { }

        public void addIceCandidate(RTCIceCandidate candidate, IFunction successCallback, Action<object> failureCallback) { }


        #region event onicecandidate
        public event Action<RTCPeerConnectionIceEvent> onicecandidate
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "icecandidate");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "icecandidate");
            }
        }
        #endregion

        public RTCDataChannel createDataChannel(string label, object dataChannelDict)
        {
            return default(RTCDataChannel);
        }
    }


    [Script]
    public static class RTCPeerConnectionExtensions
    {
        // X:\jsc.svn\examples\javascript\p2p\RTCICELobby\RTCICELobby\Application.cs

        public static Task<RTCSessionDescription> createOffer(this RTCPeerConnection that)
        {
            var x = new TaskCompletionSource<RTCSessionDescription>();
            that.createOffer(successCallback: x.SetResult);
            return x.Task;
        }

        public static Task setRemoteDescription(this RTCPeerConnection that, RTCSessionDescription description)
        {
            var x = new TaskCompletionSource<object>();
            that.setRemoteDescription(description, successCallback: new Action(delegate { x.SetResult(null); }));
            return x.Task;
        }


        public static Task<RTCSessionDescription> createAnswer(this RTCPeerConnection that)
        {
            var x = new TaskCompletionSource<RTCSessionDescription>();
            that.createAnswer(successCallback: x.SetResult);
            return x.Task;
        }

        public static Task setLocalDescription(this RTCPeerConnection that, RTCSessionDescription description)
        {
            var x = new TaskCompletionSource<object>();
            that.setLocalDescription(description, successCallback: new Action(delegate { x.SetResult(null); }));
            return x.Task;
        }

        public static Task addIceCandidate(this RTCPeerConnection that, RTCIceCandidate candidate)
        {
            var x = new TaskCompletionSource<object>();
            that.addIceCandidate(candidate,
                successCallback: new Action(delegate { x.SetResult(null); }),
                failureCallback: err => { throw new Exception(new { err }.ToString()); }
            );

            return x.Task;
        }

        // X:\jsc.svn\examples\javascript\android\AndroidRTC\AndroidRTC\Application.cs
        public static Task<RTCDataChannel> openDataChannel(this RTCPeerConnection that, string label, object dataChannelDict)
        {
            // reliable: true
            // reliable: 0 ?

            //  new { reliable = false }

            var adataChannelDict = new { reliable = false };
            dataChannelDict = adataChannelDict;

            Console.WriteLine("enter openDataChannel " + new { adataChannelDict });
            // does it work?

            var x = new TaskCompletionSource<RTCDataChannel>();

            var s = that.createDataChannel(label, dataChannelDict);


            //readyState: "connecting"
            //reliable: true

            // never called? something wrong with redux rebuild?
            s.onopen = new Action(
                delegate
                {
                    Console.WriteLine("enter openDataChannel onopen");

                    // does it fire?
                    x.SetResult(s);
                }
            );

            Console.WriteLine("exit openDataChannel " + new { s.readyState, s.reliable });
            return x.Task;
        }

    }
}
