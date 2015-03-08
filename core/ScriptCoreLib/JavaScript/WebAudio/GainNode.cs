using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
	// http://webaudio.github.io/web-audio-api/
	// http://webaudio.github.io/web-audio-api/#idl-def-GainNode

	[Script(HasNoPrototype = true, ExternalTarget = "GainNode")]
    public class GainNode : AudioNode
    {
		public AudioParam gain;
    }


}
