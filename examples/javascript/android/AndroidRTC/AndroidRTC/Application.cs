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

            // http://caniuse.com/#feat=rtcpeerconnection
            // http://iswebrtcreadyyet.com/

            new IHTMLPre
            {
                new { flightcheck = "are you using chrome40? open on android first?",
                Native.window.navigator.userAgent
                }
            }.AttachToDocument();

            // X:\jsc.svn\examples\javascript\p2p\RTCDataChannelExperiment\RTCDataChannelExperiment\Application.cs

            new { }.With(
                async delegate
                {
                    // X:\jsc.svn\examples\javascript\p2p\RTCICELobby\RTCICELobby\Application.cs

                    new IHTMLPre { "preparing..." }.AttachToDocument();

                    // first check if we can just anwser and offer?
                    #region GetOffer
                    await base.GetOffer();

                    if (base.sdpOffer != null)
                        if (base.sdpOffer.sdp == null)
                        {
                            new IHTMLPre { "bugcheck. invalid offer? " }.AttachToDocument();
                            base.sdpOffer = null;
                        }

                    if (base.sdpOffer != null)
                    {
                        new IHTMLPre { "anwsering and offer...... " }.AttachToDocument();
                        //new IHTMLPre { "anwsering and offer...... " + new { base.sdpOffer.sdp, base.sdpOffer.sdpCandidates.Count } }.AttachToDocument();
                        new IHTMLPre { "anwsering and offer...... " + new { base.sdpOffer.sdpCandidates.Count } }.AttachToDocument();

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



                            //remotePeerConnection.onicecandidate: { { candidate = candidate:3890619490 1 udp 2122255103 2001::9d38:6abd: 1474:3b35: 3f57:d403 52152 typ host generation 0, sdpMLineIndex = 0, sdpMid = audio } }
                            //remotePeerConnection.onicecandidate: { { candidate = candidate:3890619490 1 udp 2122255103 2001::9d38:6abd: 1474:3b35: 3f57:d403 52152 typ host generation 0, sdpMLineIndex = 1, sdpMid = data } }
                            //remotePeerConnection.onicecandidate: { { candidate = candidate:1796882240 1 udp 2122194687 192.168.43.252 52153 typ host generation 0, sdpMLineIndex = 0, sdpMid = audio } }
                            //remotePeerConnection.onicecandidate: { { candidate = candidate:1796882240 1 udp 2122194687 192.168.43.252 52153 typ host generation 0, sdpMLineIndex = 1, sdpMid = data } }

                            base.sdpAnwserCandidates.Add(
                                // need to send all 3 fields over..
                                new DataRTCIceCandidate
                                {
                                    candidate = e.candidate.candidate,
                                    sdpMLineIndex = e.candidate.sdpMLineIndex,
                                    sdpMid = e.candidate.sdpMid
                                }
                            );
                        };
                        #endregion


                        #region ondatachannel
                        remotePeerConnection.ondatachannel = new Action<RTCDataChannelEvent>(
                            async (RTCDataChannelEvent e) =>
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

                                #region onopen

                                // http://stackoverflow.com/questions/15324500/creating-webrtc-data-channels-after-peerconnection-established

                                new IHTMLPre { "remotePeerConnection.createDataChannel sendDataChannel2...... " }.AttachToDocument();

                                //var sendDataChannel2 = remotePeerConnection.createDataChannel("sendDataChannel2", new { reliable = false });
                                var sendDataChannel2 = await remotePeerConnection.openDataChannel("sendDataChannel2", new { reliable = false });

                                new IHTMLPre { () => "sendDataChannel2.onopen " + new { sendDataChannel2.label } }.AttachToDocument();

                                //sendChannel.send("hi!");

                                var mmmcounter = 0;



                                Native.document.body.ontouchmove +=
                                        re =>
                                        {
                                            var n = new { re.touches[0].clientX, re.touches[0].clientY };

                                            re.preventDefault();

                                            mmmcounter++;

                                            sendDataChannel2.send(
                                                new XElement("sendDataChannel2",
                                                    new XAttribute(nameof(mmmcounter), "" + mmmcounter),
                                                    new XAttribute(nameof(IEvent.CursorX), "" + n.clientX),
                                                    new XAttribute(nameof(IEvent.CursorY), "" + n.clientY)
                                                ).ToString()
                                            );


                                        };


                                Native.document.onmousemove +=
                                    re =>
                                    {
                                        mmmcounter++;

                                        sendDataChannel2.send(
                                            new XElement("sendDataChannel2",
                                                new XAttribute(nameof(mmmcounter), "" + mmmcounter),
                                                new XAttribute(nameof(IEvent.CursorX), "" + re.CursorX),
                                                new XAttribute(nameof(IEvent.CursorY), "" + re.CursorY)
                                            ).ToString()

                                        //new { mmcounter, e.CursorX, e.CursorY }.ToString()
                                        );
                                    };
                                #endregion

                            }
                        );
                        #endregion

                        var desc = new RTCSessionDescription(new { sdp = sdpOffer.sdp, type = "offer" });

                        new IHTMLPre { "remotePeerConnection setRemoteDescription..." }.AttachToDocument();
                        await remotePeerConnection.setRemoteDescription(desc);

                        new IHTMLPre { "createAnswer..." }.AttachToDocument();
                        var a = await remotePeerConnection.createAnswer();

                        new IHTMLPre { "setLocalDescription..." }.AttachToDocument();
                        await remotePeerConnection.setLocalDescription(a);

                        base.sdpAnwser = a.sdp;

                        new IHTMLPre { () => "awaiting for any sdpAnwserCandidates... " + new { base.sdpAnwserCandidates.Count } + " atleast connect to local hotspot" }.AttachToDocument();

                        while (!base.sdpAnwserCandidates.Any())
                            await Task.Delay(1000 / 15);

                        new IHTMLPre { "Anwser... " + new { base.sdpAnwserCandidates.Count } }.AttachToDocument();

                        await base.Anwser();

                        new IHTMLPre { "Anwser... done. await ondatachannel?" }.AttachToDocument();

                        #region addIceCandidate
                        new IHTMLPre { "addIceCandidate..." }.AttachToDocument();
                        foreach (var c in base.sdpOffer.sdpCandidates)
                        {
                            var cc = new RTCIceCandidate(new { c.candidate, c.sdpMLineIndex, c.sdpMid });

                            //if (c.sdpMid == "audio" || c.candidate.Contains("::"))
                            if (c.sdpMid == "audio")
                            {
                                new IHTMLPre { "skip audio remotePeerConnection addIceCandidate... " + new { c.candidate, c.sdpMLineIndex, c.sdpMid } }.AttachToDocument();
                            }
                            else if (c.candidate.Contains("::"))
                            {
                                new IHTMLPre { "skip ip6 remotePeerConnection addIceCandidate... " + new { c.candidate, c.sdpMLineIndex, c.sdpMid } }.AttachToDocument();
                            }
                            else
                            {
                                new IHTMLPre { "remotePeerConnection addIceCandidate... " + new { c.candidate, c.sdpMLineIndex, c.sdpMid } }.AttachToDocument();
                                await remotePeerConnection.addIceCandidate(cc);
                            }

                        }
                        new IHTMLPre { "addIceCandidate... done. awaiting sendChannel.onopen.." }.AttachToDocument();
                        #endregion


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



                        //remotePeerConnection.onicecandidate: { { candidate = candidate:3890619490 1 udp 2122255103 2001::9d38:6abd: 1474:3b35: 3f57:d403 52152 typ host generation 0, sdpMLineIndex = 0, sdpMid = audio } }
                        //remotePeerConnection.onicecandidate: { { candidate = candidate:3890619490 1 udp 2122255103 2001::9d38:6abd: 1474:3b35: 3f57:d403 52152 typ host generation 0, sdpMLineIndex = 1, sdpMid = data } }
                        //remotePeerConnection.onicecandidate: { { candidate = candidate:1796882240 1 udp 2122194687 192.168.43.252 52153 typ host generation 0, sdpMLineIndex = 0, sdpMid = audio } }
                        //remotePeerConnection.onicecandidate: { { candidate = candidate:1796882240 1 udp 2122194687 192.168.43.252 52153 typ host generation 0, sdpMLineIndex = 1, sdpMid = data } }

                        base.sdpCandidates.Add(
                            // need to send all 3 fields over..
                            new DataRTCIceCandidate
                            {
                                candidate = e.candidate.candidate,
                                sdpMLineIndex = e.candidate.sdpMLineIndex,
                                sdpMid = e.candidate.sdpMid
                            }
                        );
                    };
                    #endregion

                    var asendchannel = localPeerConnection.openDataChannel("sendDataChannel", new { reliable = false });

                    #region ondatachannel
                    localPeerConnection.ondatachannel = new Action<RTCDataChannelEvent>(
                        (RTCDataChannelEvent e) =>
                        {
                            var mcounter = 0;
                            var data = default(XElement);
                            new IHTMLPre { () => "enter  localPeerConnection.ondatachannel " + new { e.channel.label, mcounter, data } }.AttachToDocument();

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

                    new IHTMLPre { "createOffer..." }.AttachToDocument();
                    var o = await localPeerConnection.createOffer();

                    new IHTMLPre { "setLocalDescription..." }.AttachToDocument();

                    await localPeerConnection.setLocalDescription(o);

                    base.sdp = o.sdp;

                    new IHTMLPre { () => "awaiting for any sdpCandidates... " + new { base.sdpCandidates.Count } + " (atleast connect to local hotspot, openDataChannel)" }.AttachToDocument();

                    // cannot wait for candidates before we do openDataChannel
                    while (!base.sdpCandidates.Any())
                        await Task.Delay(1000 / 15);

                    new IHTMLPre { "letting the server know we made a new offer... " + new { base.sdpCandidates.Count } }.AttachToDocument();

                    await base.Offer();

                    new IHTMLPre { "letting the server know we made a new offer... done. open a new tab! even on android?" }.AttachToDocument();


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

                    #region addIceCandidate
                    new IHTMLPre { "addIceCandidate..." }.AttachToDocument();
                    foreach (var c in base.sdpAnwserCandidates)
                    {
                        var cc = new RTCIceCandidate(new { c.candidate, c.sdpMLineIndex, c.sdpMid });

                        //if (c.sdpMid == "audio" || c.candidate.Contains("::"))
                        if (c.sdpMid == "audio")
                        {
                            new IHTMLPre { "skip audio localPeerConnection addIceCandidate... " + new { c.candidate, c.sdpMLineIndex, c.sdpMid } }.AttachToDocument();
                        }
                        else if (c.candidate.Contains("::"))
                        {
                            new IHTMLPre { "skip ip6 localPeerConnection addIceCandidate... " + new { c.candidate, c.sdpMLineIndex, c.sdpMid } }.AttachToDocument();
                        }
                        else
                        {
                            new IHTMLPre { "localPeerConnection addIceCandidate... " + new { c.candidate, c.sdpMLineIndex, c.sdpMid } }.AttachToDocument();
                            await localPeerConnection.addIceCandidate(cc);
                        }

                    }
                    new IHTMLPre { "awaiting localPeerConnection.openDataChannel.." }.AttachToDocument();
                    #endregion


                    #region onopen
                    //var sendChannel = localPeerConnection.createDataChannel("sendDataChannel", new { reliable = false });
                    var sendChannel = await asendchannel;

                    // await async.onopen
                    new IHTMLPre { () => "sendChannel.onopen " + new { sendChannel.label } }.AttachToDocument();

                    //sendChannel.send("hi!");

                    var mmcounter = 0;



                    Native.document.body.ontouchmove +=
                            e =>
                            {
                                var n = new { e.touches[0].clientX, e.touches[0].clientY };

                                e.preventDefault();

                                mmcounter++;

                                sendChannel.send(
                                    new XElement("sendDataChannel",
                                        new XAttribute(nameof(mmcounter), mmcounter),
                                        new XAttribute(nameof(IEvent.CursorX), "" + n.clientX),
                                        new XAttribute(nameof(IEvent.CursorY), "" + n.clientY)
                                    ).ToString()
                                );


                            };


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
                    #endregion

                }
            );


        }
    }
}
