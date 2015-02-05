using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://dan.nea.me/audiolandscape/


    [Script(HasNoPrototype = true)]
    public class AudioWorkerGlobalScope : DedicatedWorkerGlobalScope
    {
        public double sampleRate;

        #region event onaudioprocess
        public event System.Action<IEvent> onaudioprocess
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "audioprocess");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "audioprocess");
            }
        }
        #endregion
    }
}
