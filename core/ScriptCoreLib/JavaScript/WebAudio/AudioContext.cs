using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://dan.nea.me/audiolandscape/
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webaudio/WindowWebAudio.idl?sortby=date
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webaudio/AudioContext.idl?sortby=file

    [Script(HasNoPrototype = true, ExternalTarget = "AudioContext")]
    public class AudioContext
    {
        // https://msdn.microsoft.com/en-us/library/aa376846.aspx?f=255&MSPPError=-2147217396

        // http://forestmist.org/share/web-audio-api-demo/
        // http://caniuse.com/#feat=audio-api
        // http://www.w3.org/2011/audio/wiki/Basic-Examples#Looping_Sounds_Without_Gaps

        public readonly AudioDestinationNode destination;

        // https://developer.apple.com/library/iad/documentation/AudioVideo/Conceptual/Using_HTML5_Audio_Video/PlayingandSynthesizingSounds/PlayingandSynthesizingSounds.html
        // http://typedarray.org/from-microphone-to-wav-with-getusermedia-and-web-audio/
        // http://www.sitepoint.com/using-fourier-transforms-web-audio-api/
        // http://webaudio.github.io/web-audio-api/#idl-def-AudioWorkerGlobalScope

        public AudioWorkerNode createAudioWorker(string scriptURL, uint numberOfInputChannels = 2, uint numberOfOutputChannels = 2)
        {
            return default(AudioWorkerNode);
        }

        public OscillatorNode createOscillator()
        {
            return default(OscillatorNode);
        }

        public IPromise close()
        {

            return null;
        }
    }
}
