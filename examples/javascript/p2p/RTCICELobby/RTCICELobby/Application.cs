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
using RTCICELobby;
using RTCICELobby.Design;
using RTCICELobby.HTML.Pages;

namespace RTCICELobby
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
                    new IHTMLPre { "preparing for the first connection..." }.AttachToDocument();

                    // we also have the crypto private key avaiable if this is https
                    var localPeerConnection = new RTCPeerConnection(null, null);



                    localPeerConnection.createOffer(
                   //x =>
                   //{
                }
            );



        }

    }
}
