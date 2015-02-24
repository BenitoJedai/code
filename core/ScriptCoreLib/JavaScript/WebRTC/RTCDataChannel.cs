using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/RTCDataChannel.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/mediastream/RTCDataChannel.idl

    [Script(HasNoPrototype = true, ExternalTarget = "RTCDataChannel")]
    public class RTCDataChannel : IEventTarget
    {
        // https://groups.google.com/forum/#!topic/discuss-webrtc/Z7hMshWun78

        // http://xsockets.net/docs/3/webrtc
        // https://software.intel.com/en-us/blogs/2014/12/15/meshcentral-webrtc-data-stack-for-native-applications

        // X:\jsc.svn\examples\javascript\p2p\RTCDataChannelExperiment\RTCDataChannelExperiment\Application.cs

        #region event onmessage
        public event Action<MessageEvent> onmessage
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "message");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "message");
            }
        }
        #endregion
    }
}
