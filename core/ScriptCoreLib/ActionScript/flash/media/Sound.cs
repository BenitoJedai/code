using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;

namespace ScriptCoreLib.ActionScript.flash.media
{
    [Script(IsNative = true)]
    public class Sound
    {
        public Sound()
        {

        }

        public Sound(URLRequest stream)
        {

        }

        #region play
        /// <summary>
        /// Generates a new SoundChannel object to play back the sound.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="loops"></param>
        /// <param name="sndTransform"></param>
        /// <returns></returns>
        public SoundChannel play(double startTime, int loops, SoundTransform sndTransform)
        {
            return default(SoundChannel);
        }

        public SoundChannel play(double startTime, int loops)
        {
            return default(SoundChannel);
        }

        public SoundChannel play(double startTime)
        {
            return default(SoundChannel);
        }

        public SoundChannel play()
        {
            return default(SoundChannel);
        }
        #endregion

    }
}
