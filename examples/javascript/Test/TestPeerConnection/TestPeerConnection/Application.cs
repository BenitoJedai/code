using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestPeerConnection;
using TestPeerConnection.Design;
using TestPeerConnection.HTML.Pages;

namespace TestPeerConnection
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://developer.mozilla.org/en-US/docs/Web/API/RTCPeerConnection

            // https://github.com/cjb/serverless-webrtc

            // https://github.com/XSockets/WebRTC


            // jsc when was the last time we tried p2p?
            // var peer = new PeerConnection(iceServers, optional); where iceServers = null this is working without internet
            // http://stackoverflow.com/questions/19675165/whether-stun-server-is-needed-within-lan-for-webrtc

            //var peer = new PeerConnection(iceServers, optional);
            // https://www.webrtc-experiment.com/docs/WebRTC-PeerConnection.html
            // http://stackoverflow.com/questions/12848013/what-is-the-replacement-for-the-deprecated-peerconnection-api
            // http://docs.webplatform.org/wiki/apis/webrtc/RTCPeerConnection
            // http://w3schools.invisionzone.com/index.php?showtopic=46661
            // http://www.html5rocks.com/en/tutorials/webrtc/basics/#toc-rtcpeerconnection



            // IDL dictionary looks like C# PrimaryCnstructor concept does it not
            ////var d = new RTCSessionDescription(
            ////    new
            ////{

            ////}
            ////);


            //    02000002 TestPeerConnection.Application
            //    script: error JSC1000: You tried to instance a class which seems to be marked as native.
            //    script: error JSC1000: type has no callable constructor: [ScriptCoreLib.JavaScript.DOM.RTCPeerConnection]
            //Void.ctor()

            // Uncaught ReferenceError: RTCPeerConnection is not defined
            // wtf?


            // {{ RTCPeerConnection = undefined }}
            //new IHTMLPre { new { w.RTCPeerConnection } }.AttachToDocument();
            // {{ webkitRTCPeerConnection = function RTCPeerConnection() { [native code] } }}
            //new IHTMLPre { new { w.webkitRTCPeerConnection } }.AttachToDocument();

            // wtf chrome? stop prefixing
            var w = Native.window as dynamic;

            Console.WriteLine(new { w.RTCPeerConnection });

            w.RTCPeerConnection = w.webkitRTCPeerConnection;
            // Uncaught TypeError: Failed to construct 'RTCPeerConnection': 1 argument required, but only 0 present.

            // http://stackoverflow.com/questions/22470291/rtcdatachannels-readystate-is-not-open

            // after Chrome 31, you can use SCTP based data channels.
            // http://stackoverflow.com/questions/21585681/send-image-data-over-rtc-data-channel
            // https://code.google.com/p/chromium/issues/detail?id=295771
            // https://gist.github.com/shacharz/9661930



            // http://chimera.labs.oreilly.com/books/1230000000545/ch18.html#_tracking_ice_gathering_and_connectivity_status
            var peer = new RTCPeerConnection(
                new { iceServers = new object[0] },
                null

            // https://groups.google.com/forum/#!topic/discuss-webrtc/y2A97iCByTU

            //constraints: new {
            //    optional = new[]
            //    {
            //        new {  RtpDataChannels = true }
            //    }
            //} 
            );

            // how the hell cann I connect two p2p?
            // i see we need to do data

            //peer.setLocalDescription
            // https://groups.google.com/forum/#!topic/discuss-webrtc/zK_5yUqiqsE
            // X:\jsc.svn\examples\javascript\xml\VBDisplayServerDebuggerPresence\VBDisplayServerDebuggerPresence\ApplicationWebService.vb
            // https://code.google.com/p/webrtc/source/browse/trunk/samples/js/base/adapter.js
            // http://www.webrtc.org/faq-recent-topics

            // http://stackoverflow.com/questions/14134090/how-is-a-webrtc-peer-connection-established

            peer.onicecandidate = new Action<RTCPeerConnectionIceEvent>(
                (RTCPeerConnectionIceEvent e) =>
                {
                    if (e.candidate != null)
                    {
                        new IHTMLPre {
                            "onicecandidate: " + new { e.candidate.candidate }
                        }.AttachToDocument();



                        peer.addIceCandidate(e.candidate,
                                                    new Action(
                                                        delegate
                                                {
                                                    new IHTMLPre { "addIceCandidate" }.AttachToDocument();
                                                }
                                                        ));
                    }
                }
            );

            // http://stackoverflow.com/questions/15484729/why-doesnt-onicecandidate-work
            // http://www.skylinetechnologies.com/Blog/Article/48/Peer-to-Peer-Media-Streaming-with-WebRTC-and-SignalR.aspx

            peer.createOffer(
                new Action<RTCSessionDescription>(
                    (RTCSessionDescription x) =>
                    {

                        new IHTMLPre { "after createOffer " + new { x.sdp } }.AttachToDocument();

                        peer.setLocalDescription(x,
                                                    new Action(
                                                        delegate
                                                {
                                                    // // send the offer to a server that can negotiate with a remote client
                                                    new IHTMLPre { "after setLocalDescription " }.AttachToDocument();
                                                }
                                                    )
                                                );

                        peer.setRemoteDescription(x,
                                new Action(
                                    delegate
                        {
                            // // send the offer to a server that can negotiate with a remote client
                            new IHTMLPre { "after setRemoteDescription " }.AttachToDocument();
                        }
                                )
                            );
                    }
                )
            );



            peer.createAnswer(
                                new Action<RTCSessionDescription>(
                    (RTCSessionDescription x) =>
                    {

                        new IHTMLPre { "after createAnswer " + new { x.sdp } }.AttachToDocument();
                    }
                                    ));


            // https://groups.google.com/forum/#!topic/discuss-webrtc/wbcgYMrIii4
            // https://groups.google.com/forum/#!msg/discuss-webrtc/wbcgYMrIii4/aZ12cENVTxEJ
            // http://blog.printf.net/articles/2013/05/17/webrtc-without-a-signaling-server/

            //peer.onconn

            // https://github.com/cjb/serverless-webrtc/blob/master/js/serverless-webrtc.js
            peer.ondatachannel = new Action<RTCDataChannelEvent>(
                (RTCDataChannelEvent e) =>
                {
                    //Console.WriteLine("ondatachannel");
                    new IHTMLPre { "ondatachannel" }.AttachToDocument();

                    var c = e.channel;

                    c.onmessage = new Action<MessageEvent>(
                                            (MessageEvent ee) =>
                                            {
                                                new IHTMLPre { new { ee.data } }.AttachToDocument();

                                            }
                                        );


                }
            );

            // jsc cant the idl generator understand optinal?
            RTCDataChannel dc = peer.createDataChannel("label1", null);


            // {{ id = 65535, label = label1, readyState = connecting }}
            new IHTMLPre { new { dc.id, dc.label, dc.readyState } }.AttachToDocument();

            new IHTMLButton { "awaiting to open..." }.AttachToDocument().With(
                 button =>
                            {

                                // !!! can our IDL compiler generate events and async at the same time?
                                dc.onopen = new Action<IEvent>(
                                    async ee =>
                                                    {


                                                        button.innerText = "send";

                                                        while (true)
                                                        {
                                                            await button.async.onclick;

                                                            new IHTMLPre { "send" }.AttachToDocument();

                                                            // Failed to execute 'send' on 'RTCDataChannel': RTCDataChannel.readyState is not 'open'
                                                            dc.send("data to send");
                                                        }

                                                    }
                                );


                            }
                        );

            //connection.createOffer
        }

    }
}
