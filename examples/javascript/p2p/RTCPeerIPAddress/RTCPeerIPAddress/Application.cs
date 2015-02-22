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
using RTCPeerIPAddress;
using RTCPeerIPAddress.Design;
using RTCPeerIPAddress.HTML.Pages;

namespace RTCPeerIPAddress
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
            //http://codepen.io/crackerboy/pen/kDnoB?editors=001
            // X:\jsc.svn\examples\javascript\test\TestPeerConnection\TestPeerConnection\Application.cs
            //#region fix RTCPeerConnection
            //var w = Native.window as dynamic;
            //w.RTCPeerConnection = w.webkitRTCPeerConnection;
            //#endregion


            //var rtc = new RTCPeerConnection({ iceServers:[] });
            var rtc = new RTCPeerConnection(null, null);

            // Uncaught TypeError: Failed to construct 'RTCPeerConnection': 1 argument required, but only 0 present.
            //var rtc = new RTCPeerConnection();

            //if (window.mozRTCPeerConnection) {      // FF needs a channel/stream to proceed
            //    rtc.createDataChannel('', {reliable:false});
            //};

            //IFunction<>

            rtc.onicecandidate = new Action<RTCPeerConnectionIceEvent>(
                (RTCPeerConnectionIceEvent e) =>
                {
                    if (e.candidate != null)
                    {
                        new IHTMLPre { "onicecandidate: " + new { e.candidate.candidate } }.AttachToDocument();
                    }
                }
            );

            // await?
            rtc.createOffer(
                (RTCSessionDescription x) =>
                {
                    new IHTMLPre { "after createOffer " + new { x.sdp } }.AttachToDocument();

                    rtc.setLocalDescription(x,
                        new Action(
                            delegate
                            {
                                // // send the offer to a server that can negotiate with a remote client
                                new IHTMLPre { "after setLocalDescription " + new { x.sdp } }.AttachToDocument();
                            }
                        )
                    );

                }
            );

        }

    }
}
