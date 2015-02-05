using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://webaudio.github.io/web-audio-api/#idl-def-AudioParam
    // http://dan.nea.me/audiolandscape/

    [Script(HasNoPrototype = true, ExternalTarget = "AudioParam")]
    public class AudioParam
    {
        public double value;
    }
}
