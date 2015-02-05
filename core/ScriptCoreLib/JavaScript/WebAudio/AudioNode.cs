using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://webaudio.github.io/web-audio-api/#idl-def-AudioNode
    // http://dan.nea.me/audiolandscape/

    [Script(HasNoPrototype = true, ExternalTarget = "AudioNode")]
    public class AudioNode : IEventTarget
    {
        public readonly AudioContext context;

        public void connect(AudioNode destination, uint output = 0, uint input = 0)
        {
        }

        public void connect(AudioParam destination, uint output = 0)
        {
        }
    }
}
