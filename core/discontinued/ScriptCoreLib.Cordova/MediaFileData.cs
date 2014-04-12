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
    /// Encapsulates format information about a media file
    /// http://docs.phonegap.com/en/1.7.0/cordova_media_capture_capture.md.html#MediaFileData
    /// </summary>
    [Script(IsNative = true)]
    public class MediaFileData
    {
        #region Constructor

        public MediaFileData()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  The actual format of the audio and video content. (DOMString)
        /// </summary>
        public string codecs;

        /// <summary>
        ///  The average bitrate of the content. In case of an image, this attribute has value 0. (Number)
        /// </summary>
        public int bitrate;

        /// <summary>
        ///  The height of the image or video in pixels. In the case of a sound clip, this attribute has value 0. (Number)
        /// </summary>
        public int height;

        /// <summary>
        ///  The width of the image or video in pixels. In the case of a sound clip, this attribute has value 0. (Number)
        /// </summary>
        public int width;

        /// <summary>
        ///  The length of the video or sound clip in seconds. In the case of an image, this attribute has value 0. (Number)
        /// </summary>
        public int duration;

        #endregion


    }
}
