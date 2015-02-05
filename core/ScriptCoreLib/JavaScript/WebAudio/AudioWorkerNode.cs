using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://webaudio.github.io/web-audio-api/#idl-def-AudioWorkerNode
    // http://dan.nea.me/audiolandscape/

    [Script(HasNoPrototype = true, ExternalTarget = "AudioWorkerNode")]
    public class AudioWorkerNode
    {
        public void terminate()
        {
        }
    }
}
