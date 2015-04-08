using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/MessageChannel.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/MessageChannel.idl

    // https://bugzilla.mozilla.org/show_bug.cgi?id=677638
    [Script(HasNoPrototype = true, ExternalTarget = "MessageChannel")]
    public class MessageChannel
    {
		// tested by?
		// X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IWindow.postMessage.cs

		public readonly MessagePort port1;
        public readonly MessagePort port2;

    }
}
