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
using AndroidRTC;
using AndroidRTC.Design;
using AndroidRTC.HTML.Pages;
using AndroidRTC.HTML.Images.FromAssets;
using System.Diagnostics;

namespace AndroidRTC
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
            // X:\jsc.svn\examples\javascript\p2p\RTCDataChannelExperiment\RTCDataChannelExperiment\Application.cs

            new { }.With(
                async delegate
                {
                    // X:\jsc.svn\examples\javascript\p2p\RTCICELobby\RTCICELobby\Application.cs

                    new IHTMLPre { "preparing..." }.AttachToDocument();

                    // first check if we can just anwser and offer?
                    #region GetOffer
                    await base.GetOffer();

                    if (!string.IsNullOrEmpty(sdpOffer))
                    {
                        new IHTMLPre { "anwsering and offer......" }.AttachToDocument();

                        var remotePeerConnection = new RTCPeerConnection(null, null);

                        #region onicecandidate
                        // onICE: It returns locally generated ICE candidates; so you can share them with other peer(s).
                        remotePeerConnection.onicecandidate += e =>
                        {
                            if (e.candidate == null)
                                return;
                            // onicecandidate: {{ sdpMLineIndex = 0, candidate = candidate:630834096 1 tcp 1518214911 192.168.43.252 0 typ host tcptype active generation 0 }}
                            new IHTMLPre { "remotePeerConnection.onicecandidate: " + new {

                                e.candidate.candidate,
                                e.candidate.sdpMLineIndex,
                                e.candidate.sdpMid
                            } }.AttachToDocument();

                            base.sdpAnwserCandidates.Add(e.candidate.candidate);
                        };
                        #endregion

                        #region ondatachannel
                        remotePeerConnection.ondatachannel = new Action<RTCDataChannelEvent>(
                            (RTCDataChannelEvent e) =>
                            {
                                var mcounter = 0;
                                var data = default(XElement);
                                new IHTMLPre { () => "enter  remotePeerConnection.ondatachannel " + new { e.channel.label, mcounter, data } }.AttachToDocument();

                                var cur = new MyCursor();
                                cur.AttachToDocument();

                                e.channel.onmessage += ee =>
                                {
                                    mcounter++;
                                    data = XElement.Parse("" + ee.data);

                                    var CursorX = (int)data.Attribute(nameof(IEvent.CursorX));
                                    var CursorY = (int)data.Attribute(nameof(IEvent.CursorY));

                                    cur.style.SetLocation(
                                        CursorX,
                                        CursorY
                                       );

                                };


                            }
                        );
                        #endregion

                        var desc = new RTCSessionDescription(new { sdp = sdpOffer, type = "offer" });

                        new IHTMLPre { "setRemoteDescription..." }.AttachToDocument();
                        await remotePeerConnection.setRemoteDescription(desc);

                        new IHTMLPre { "createAnswer..." }.AttachToDocument();
                        var a = await remotePeerConnection.createAnswer();

                        new IHTMLPre { "setLocalDescription..." }.AttachToDocument();
                        await remotePeerConnection.setLocalDescription(a);

                        base.sdpAnwser = a.sdp;

                        new IHTMLPre { () => "awaiting for any sdpAnwserCandidates... " + new { base.sdpAnwserCandidates.Count } }.AttachToDocument();

                        while (!base.sdpAnwserCandidates.Any())
                            await Task.Delay(1000 / 15);

                        new IHTMLPre { "Anwser... " + new { base.sdpAnwserCandidates.Count } }.AttachToDocument();

                        await base.Anwser();

                        new IHTMLPre { "Anwser... done. await ondatachannel?" }.AttachToDocument();

                        await new TaskCompletionSource<object>().Task;
                    }
                    #endregion

                    // we also have the crypto private key avaiable if this is https
                    var localPeerConnection = new RTCPeerConnection(null, null);


                    #region onicecandidate
                    // onICE: It returns locally generated ICE candidates; so you can share them with other peer(s).
                    localPeerConnection.onicecandidate += e =>
                    {
                        if (e.candidate == null)
                            return;

                        // onicecandidate: {{ sdpMLineIndex = 0, candidate = candidate:630834096 1 tcp 1518214911 192.168.43.252 0 typ host tcptype active generation 0 }}
                        new IHTMLPre { "localPeerConnection.onicecandidate: " + new {
                                e.candidate.candidate,
                                e.candidate.sdpMLineIndex,
                                 e.candidate.sdpMid
                            } }.AttachToDocument();

                        base.sdpCandidates.Add(e.candidate.candidate);
                    };
                    #endregion



                    var sendChannel = localPeerConnection.createDataChannel("sendDataChannel", new { reliable = false });

                    // await async.onopen
                    sendChannel.onopen = new Action(
                        async delegate
                        {
                            new IHTMLPre { () => "sendChannel.onopen " + new { sendChannel.label } }.AttachToDocument();

                            //sendChannel.send("hi!");

                            var mmcounter = 0;

                            Native.document.onmousemove +=
                                e =>
                                {
                                    mmcounter++;

                                    sendChannel.send(
                                        new XElement("sendDataChannel",
                                            new XAttribute(nameof(mmcounter), mmcounter),
                                            new XAttribute(nameof(IEvent.CursorX), "" + e.CursorX),
                                            new XAttribute(nameof(IEvent.CursorY), "" + e.CursorY)
                                        ).ToString()

                                    //new { mmcounter, e.CursorX, e.CursorY }.ToString()
                                    );
                                };
                        }
                    );


                    new IHTMLPre { "createOffer..." }.AttachToDocument();
                    var o = await localPeerConnection.createOffer();

                    new IHTMLPre { "setLocalDescription..." }.AttachToDocument();

                    await localPeerConnection.setLocalDescription(o);

                    base.sdp = o.sdp;

                    new IHTMLPre { () => "awaiting for any sdpCandidates... " + new { base.sdpCandidates.Count } }.AttachToDocument();

                    while (!base.sdpCandidates.Any())
                        await Task.Delay(1000 / 15);

                    new IHTMLPre { "letting the server know we made a new offer... " + new { base.sdpCandidates.Count } }.AttachToDocument();

                    await base.Offer();

                    new IHTMLPre { "letting the server know we made a new offer... done. open a new tab!" }.AttachToDocument();


                    var sw = Stopwatch.StartNew();
                    var counter = 0;

                    // lets have a new status line

                    // using
                    new IHTMLPre { () => "awaiting for answer " + new { counter, sw.Elapsed } }.AttachToDocument();

                    // now poll the server, for any other offers?
                    while (string.IsNullOrEmpty(this.sdpAnwser))
                    {
                        await Task.Delay(5000);
                        await base.CheckAnswer();
                        counter++;
                    }

                    // end using

                    new IHTMLPre { "setRemoteDescription..." }.AttachToDocument();

                    await localPeerConnection.setRemoteDescription(new RTCSessionDescription(new { sdp = base.sdpAnwser, type = "answer" }));

                    new IHTMLPre { "addIceCandidate..." }.AttachToDocument();
                    foreach (var candidate in base.sdpAnwserCandidates)
                    {
                        var c = new RTCIceCandidate(new { candidate, sdpMLineIndex = 0, sdpMid = "data" });

                        await localPeerConnection.addIceCandidate(c);
                    }
                    new IHTMLPre { "addIceCandidate... done. sendChannel" }.AttachToDocument();
                }
            );



        }
    }
}
