using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://webaudio.github.io/web-audio-api/#idl-def-OscillatorNode
    // http://dan.nea.me/audiolandscape/

    [Script(HasNoPrototype = true, ExternalTarget = "OscillatorNode")]
    public class OscillatorNode : AudioNode
    {
        // X:\jsc.svn\examples\javascript\audio\StandardOscillator\StandardOscillator\Application.cs

        public OscillatorType type;

        public void start(double when = 0) { }
        public void disconnect() { }

        public readonly AudioParam frequency;
    }

    // c# can we get string enums in 2015?
    [Script(IsStringEnum = true)]
    public enum OscillatorType
    {
        sine,
        square,
        sawtooth,
        triangle,
        custom
    }
}
