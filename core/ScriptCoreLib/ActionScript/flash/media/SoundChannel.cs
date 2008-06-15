using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.media
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/media/SoundChannel.html
    [Script(IsNative = true)]
    public class SoundChannel : EventDispatcher
    {
        #region Properties
        /// <summary>
        /// [read-only] The current amplitude (volume) of the left channel, from 0 (silent) to 1 (full amplitude).
        /// </summary>
        public double leftPeak { get; private set; }

        /// <summary>
        /// [read-only] When the sound is playing, the position property indicates the current point that is being played in the sound file.
        /// </summary>
        public double position { get; private set; }

        /// <summary>
        /// [read-only] The current amplitude (volume) of the right channel, from 0 (silent) to 1 (full amplitude).
        /// </summary>
        public double rightPeak { get; private set; }

        /// <summary>
        /// The SoundTransform object assigned to the sound channel.
        /// </summary>
        public SoundTransform soundTransform { get; set; }

        #endregion

        /// <summary>
        /// Stops the sound playing in the channel.
        /// </summary>
        public void stop()
        {
        }
        #region Events
        /// <summary>
        /// Dispatched when a sound has finished playing.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> soundComplete;

        #endregion

    }
}
