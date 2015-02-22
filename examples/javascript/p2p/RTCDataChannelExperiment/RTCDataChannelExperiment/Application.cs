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
using RTCDataChannelExperiment;
using RTCDataChannelExperiment.Design;
using RTCDataChannelExperiment.HTML.Pages;

namespace RTCDataChannelExperiment
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
            // how can we connect VR headset webview and chrome app mouse lock?
            // X:\jsc.svn\examples\javascript\p2p\RTCPeerIPAddress\RTCPeerIPAddress\Application.cs

            // http://www.html5rocks.com/en/tutorials/webrtc/basics/
            // http://simpl.info/rtcdatachannel/
            // http://www.webrtc.org/
            // http://www.html5rocks.com/en/tutorials/webrtc/basics/#toc-rtcdatachannel
            // https://bloggeek.me/webrtc-data-channel-uses/
            // http://www.slideshare.net/victorpascual/webrtc-datachannels-demystified
            // http://simpl.info/rtcdatachannel/js/main.js
            // http://stackoverflow.com/questions/20396160/how-to-transfer-file-between-two-browsers-with-webrtc
            // http://blog.printf.net/articles/2013/05/17/webrtc-without-a-signaling-server/
            // https://www.webrtc-experiment.com/docs/how-file-broadcast-works.html
            // http://lists.w3.org/Archives/Public/public-webrtc/2012Jul/0008.html
            // https://code.google.com/p/webrtc/issues/detail?id=1430
            // http://blog.printf.net/articles/2014/07/01/serverless-webrtc-continued/
            // https://github.com/muaz-khan/WebRTC-Experiment/tree/master/DataChannel
            // https://www.webrtc-experiment.com/docs/how-to-use-rtcdatachannel-and-rtcpeerconnectionjs.html
            // http://omshiv.github.io/impressions/jsfoo/webrtc-talk/#/6
            // https://groups.google.com/forum/#!topic/discuss-webrtc/CNWplOqtV_w
            // https://groups.google.com/forum/#!topic/discuss-webrtc/9zs21EBciNM

            Native.body.Clear();

            new { }.With(
                async delegate
                {
                    var yellow = new IHTMLDiv { }.AttachToDocument();

                    yellow.style.position = IStyle.PositionEnum.absolute;
                    yellow.style.right = "0px";
                    yellow.style.top = "0px";
                    yellow.style.backgroundColor = "cyan";



                    #region remotePeerConnection

                    var remotePeerConnection = new RTCPeerConnection(null,
                         null
                         //new { optional = new[] { new { RtpDataChannels = true } } }
                         );


                    #region onicecandidate
                    // onICE: It returns locally generated ICE candidates; so you can share them with other peer(s).
                    remotePeerConnection.onicecandidate = new Action<RTCPeerConnectionIceEvent>(
                        (RTCPeerConnectionIceEvent e) =>
                        {
                            if (e.candidate == null)
                                return;


                            // onicecandidate: {{ sdpMLineIndex = 0, candidate = candidate:630834096 1 tcp 1518214911 192.168.43.252 0 typ host tcptype active generation 0 }}


                            new IHTMLPre { "remotePeerConnection.onicecandidate: " + new { e.candidate.sdpMLineIndex, e.candidate.sdpMid } }.AttachTo(yellow);

                            new IHTMLTextArea
                            {
                                value = e.candidate.candidate,

                                title = "for local addIceCandidate"

                            }.AttachTo(yellow);

                        }
                    );
                    #endregion

                    #region ondatachannel
                    remotePeerConnection.ondatachannel = new Action<RTCDataChannelEvent>(
                        (RTCDataChannelEvent e) =>
                        {
                            new IHTMLPre { "enter  remotePeerConnection.ondatachannel " }.AttachTo(yellow);


                            var data = default(object);
                            var counter = 0;

                            new IHTMLPre { () => "onmessage: " + new { data, counter } }.AttachTo(yellow);

                            //Native.document.title

                            e.channel.onmessage +=
                                m =>
                                {
                                    counter++;
                                    data = m.data;
                                };

                        }
                    );

                    #endregion


                    #endregion

#if RRTCIceCandidate
                     //remotePeerConnection.addIceCandidate(event.candidate);
                    var xcandidate = new IHTMLTextArea { }.AttachToDocument();

                    await new IHTMLButton { "addIceCandidate" }.AttachToDocument().async.onclick;


                    var candidate = new RTCIceCandidate(new { candidate = xcandidate.value, sdpMLineIndex = 0, sdpMid = "data" });

                    #region   remotePeerConnection.addIceCandidate
                    // http://stackoverflow.com/questions/23325510/not-able-to-add-remote-ice-candidate-in-webrtc
                    // ??? wtf

                    new IHTMLPre { "before remotePeerConnection.addIceCandidate" }.AttachToDocument();

                    // TypeError: Failed to execute 'addIceCandidate' on 'RTCPeerConnection': Valid arities are: [1, 3], but 2 arguments provided.
                    remotePeerConnection.addIceCandidate(
                        candidate,
                        new Action(
                            delegate
                            {
                                new IHTMLPre { "at  remotePeerConnection.addIceCandidate" }.AttachToDocument();

                            }
                        ),
                        new Action<object>(
                            err =>
                            {
                                // All callback-based methods have been moved to a legacy section, and replaced by same-named overloads using Promises instead
                                // err  remotePeerConnection.addIceCandidate {{ err = Error processing ICE candidate }}

                                new IHTMLPre { "err  remotePeerConnection.addIceCandidate " + new { err } }.AttachToDocument();

                            }
                        )
                    );

                    new IHTMLPre { "after remotePeerConnection.addIceCandidate" }.AttachToDocument();
                    #endregion
#endif


                    var x = new IHTMLTextArea { }.AttachTo(yellow);

                    await new IHTMLButton { "setRemoteDescription" }.AttachTo(yellow).async.onclick;

                    //await x.async.onch





                    var desc = new RTCSessionDescription(new { sdp = x.value, type = "offer" });

                    new IHTMLPre { "before  remotePeerConnection.setRemoteDescription" }.AttachTo(yellow);

                    remotePeerConnection.setRemoteDescription(desc,
                        new Action(
                            delegate
                            {

                                new IHTMLPre { "at  remotePeerConnection.setRemoteDescription" }.AttachTo(yellow);


                                // the second parameter to setRemoteDescription is a completion callback.  Call createAnswer from within the callback to avoid a race condition


                                new IHTMLPre { "before  remotePeerConnection.createAnswer" }.AttachTo(yellow);

                                remotePeerConnection.createAnswer(
                                    e =>
                                    {
                                        new IHTMLPre { "at  remotePeerConnection.createAnswer" }.AttachTo(yellow);


                                        remotePeerConnection.setLocalDescription(e,
                                            new Action(
                                                delegate { }
                                            )
                                        );

                                        new IHTMLTextArea { readOnly = true, value = e.sdp, title = "for localPeerConnection.setRemoteDescription" }.AttachTo(yellow);
                                        // localPeerConnection.setRemoteDescription(desc);



                                    }
                                );
                            }
                        )
                    );






                }
            );

            new { }.With(
                async delegate
                {
                    new IHTMLHorizontalRule { }.AttachToDocument();

                    //(await new IHTMLButton { "start" }.AttachToDocument().async.onclick).Element.Orphanize();
                    await new IHTMLButton { "createOffer" }.AttachToDocument().async.onclick;

                    // createConnection


                    // TypeError: Failed to construct 'RTCPeerConnection': Malformed constraints object.
                    var localPeerConnection = new RTCPeerConnection(null,
                        null
                        //new { optional = new[] { new { RtpDataChannels = true } } }
                        );


                    #region onicecandidate
                    // onICE: It returns locally generated ICE candidates; so you can share them with other peer(s).
                    localPeerConnection.onicecandidate = new Action<RTCPeerConnectionIceEvent>(
                        (RTCPeerConnectionIceEvent e) =>
                        {
                            if (e.candidate == null)
                                return;


                            // onicecandidate: {{ sdpMLineIndex = 0, candidate = candidate:630834096 1 tcp 1518214911 192.168.43.252 0 typ host tcptype active generation 0 }}
                            new IHTMLPre { "localPeerConnection.onicecandidate: " + new {



                                e.candidate.candidate,

                                e.candidate.sdpMLineIndex,

                                 e.candidate.sdpMid
                            } }.AttachToDocument();
                        }
                    );
                    #endregion

                    #region sendChannel
                    var sendChannel = localPeerConnection.createDataChannel(
                        "sendDataChannel", new { reliable = false });


                    sendChannel.onopen = new Action(
                        async delegate
                        {
                            new IHTMLPre { "sendChannel.onopen" }.AttachToDocument();


                            sendChannel.send("hi!");

                            var counter = 0;

                            Native.body.onmousemove +=
                                e =>
                                {
                                    if (Native.document.pointerLockElement != Native.body) return;

                                    counter++;

                                    // rpc switchTo
                                    sendChannel.send(
                                        new { counter, e.movementX, e.movementY }.ToString()

                                        );

                                    //await for new message to continue?
                                    // webrtc meets async
                                };


                            var btn = new IHTMLButton("pointerlock").AttachToDocument();


                            while (await btn.async.onclick)
                            {

                                new IHTMLPre { "requestPointerLock" }.AttachToDocument();

                                Native.body.requestPointerLock();
                            }

                        }
                    );

                    sendChannel.onclose = new Action(
                        delegate
                        {

                            new IHTMLPre { "sendChannel.onclose" }.AttachToDocument();
                        }
                    );
                    #endregion


                    localPeerConnection.createOffer(
                        x =>
                        {
                            new IHTMLPre { "enter localPeerConnection.createOffer > setLocalDescription" }.AttachToDocument();

                            new IHTMLTextArea { readOnly = true, value = x.sdp }.AttachToDocument();

                            localPeerConnection.setLocalDescription(x,
                                new Action(
                                    async delegate
                                    {
                                        new IHTMLPre { "enter localPeerConnection.createOffer > setLocalDescription done" }.AttachToDocument();

                                        var z = new IHTMLTextArea { }.AttachToDocument();

                                        await new IHTMLButton { "setRemoteDescription" }.AttachToDocument().async.onclick;


                                        localPeerConnection.setRemoteDescription(
                                            new RTCSessionDescription(new { sdp = z.value, type = "answer" }),
                                            new Action(
                                                async delegate
                                                {
                                                    new IHTMLPre { "? done" }.AttachToDocument();

                                                    //// ncaught InvalidStateError: Failed to execute 'send' on 'RTCDataChannel': RTCDataChannel.readyState is not 'open'
                                                    //sendChannel.send("hi?");


                                                    //remotePeerConnection.addIceCandidate(event.candidate);
                                                    var xcandidate = new IHTMLTextArea { }.AttachToDocument();

                                                    await new IHTMLButton { "addIceCandidate" }.AttachToDocument().async.onclick;


                                                    var candidate = new RTCIceCandidate(new { candidate = xcandidate.value, sdpMLineIndex = 0, sdpMid = "data" });

                                                    #region   remotePeerConnection.addIceCandidate
                                                    // http://stackoverflow.com/questions/23325510/not-able-to-add-remote-ice-candidate-in-webrtc
                                                    // ??? wtf

                                                    new IHTMLPre { "before localPeerConnection.addIceCandidate" }.AttachToDocument();

                                                    // TypeError: Failed to execute 'addIceCandidate' on 'RTCPeerConnection': Valid arities are: [1, 3], but 2 arguments provided.
                                                    localPeerConnection.addIceCandidate(
                                                        candidate,
                                                        new Action(
                                                            delegate
                                                            {
                                                                new IHTMLPre { "at  localPeerConnection.addIceCandidate" }.AttachToDocument();

                                                            }
                                                        ),
                                                        new Action<object>(
                                                            err =>
                                                            {
                                                                // All callback-based methods have been moved to a legacy section, and replaced by same-named overloads using Promises instead
                                                                // err  remotePeerConnection.addIceCandidate {{ err = Error processing ICE candidate }}

                                                                new IHTMLPre { "err  localPeerConnection.addIceCandidate " + new { err } }.AttachToDocument();

                                                            }
                                                        )
                                                    );

                                                    new IHTMLPre { "after localPeerConnection.addIceCandidate" }.AttachToDocument();
                                                    #endregion

                                                }
                                            )
                                        );
                                    }
                                )
                            );
                        }
                    );

                    // The PeerConnection won't start gathering candidates until you call setLocalDescription(); the information supplied to setLocalDescription tells PeerConnection how many candidates need to be gathered. 

                    //  localPeerConnection.createOffer(gotLocalDescription);

                    //new IHTMLPre { "now what? " + new { sendChannel } }.AttachToDocument();

                }
            );



        }
    }
}