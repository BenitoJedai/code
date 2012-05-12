using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System;
using ScriptCoreLib.JavaScript.DOM;


namespace ScriptCoreLib.Cordova
{
    /// <summary>
    /// The Media object provides the ability to record and play back audio files on a device
    /// http://docs.phonegap.com/en/1.7.0/cordova_media_media.md.html#Media
    /// </summary>
    [Script(IsNative = true)]
    public class Media
    {
        #region Constructor

        public Media(string src, Action mediaSuccess, Action<string> mediaError=null, Action<string>mediaStatus=null)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        ///  Returns the current position within an audio file.
        /// </summary>
        /// <param name="mediaSuccess"></param>
        /// <param name="mediaError"></param>
        public void getCurrentPosition(Action<int> mediaSuccess, Action<string> mediaError=null)
        {}
        
        /// <summary>
        ///  Returns the duration of an audio file.
        /// </summary>
        public void getDuration()
        {}

        /// <summary>
        /// Start or resume playing audio file.
        /// </summary>
        public void play()
        {}

        /// <summary>
        /// Pause playing audio file.
        /// </summary>
        public void pause()
        {}

        /// <summary>
        /// Releases the underlying OS'es audio resources.
        /// </summary>
        public void release()
        {}

        /// <summary>
        /// Moves the position within the audio file.
        /// </summary>
        /// <param name="milliseconds"></param>
        public void seekTo(int milliseconds)
        {}

        /// <summary>
        /// Start recording audio file.
        /// </summary>
        public void startRecord()
        {}

        /// <summary>
        /// Stop recording audio file.
        /// </summary>
        public void stopRecord()
        {}

        /// <summary>
        /// Stop playing audio file.
        /// </summary>
        public void stop()
        {}

        #endregion

    }
}
