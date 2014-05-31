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
            var peer = new RTCPeerConnection();

            // how the hell cann I connect two p2p?
            // i see we need to do data

            //peer.setLocalDescription


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

            new IHTMLButton { "create send channel" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    // jsc cant the idl generator understand optinal?
                    RTCDataChannel dc = peer.createDataChannel("label1", null);

                    button.innerText = "send";

                    while (true)
                    {
                        await button.async.onclick;

                        new IHTMLPre { "send" }.AttachToDocument();

                        dc.send("data to send");
                    }
                }
            );

            //connection.createOffer
        }

    }
}
