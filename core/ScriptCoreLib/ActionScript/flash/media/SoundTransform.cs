using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.media
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/media/SoundTransform.html
    [Script(IsNative = true)]
    public class SoundTransform
    {
        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a SoundTransform object.
        /// </summary>
        public SoundTransform(double vol, double panning)
        {
        }

        /// <summary>
        /// Creates a SoundTransform object.
        /// </summary>
        public SoundTransform(double vol)
        {
        }

        /// <summary>
        /// Creates a SoundTransform object.
        /// </summary>
        public SoundTransform()
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// A value, from 0 (none) to 1 (all), specifying how much of the left input is played in the left speaker.
        /// </summary>
        public double leftToLeft { get; set; }

        /// <summary>
        /// A value, from 0 (none) to 1 (all), specifying how much of the left input is played in the right speaker.
        /// </summary>
        public double leftToRight { get; set; }

        /// <summary>
        /// The left-to-right panning of the sound, ranging from -1 (full pan left) to 1 (full pan right).
        /// </summary>
        public double pan { get; set; }

        /// <summary>
        /// A value, from 0 (none) to 1 (all), specifying how much of the right input is played in the left speaker.
        /// </summary>
        public double rightToLeft { get; set; }

        /// <summary>
        /// A value, from 0 (none) to 1 (all), specifying how much of the right input is played in the right speaker.
        /// </summary>
        public double rightToRight { get; set; }

        /// <summary>
        /// The volume, ranging from 0 (silent) to 1 (full volume).
        /// </summary>
        public double volume { get; set; }

        #endregion

    }
}
