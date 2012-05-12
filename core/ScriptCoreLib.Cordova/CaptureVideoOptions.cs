using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System;


namespace ScriptCoreLib.Cordova
{
    /// <summary>
    /// Encapsulates image capture configuration options.
    /// http://docs.phonegap.com/en/1.7.0/cordova_media_capture_capture.md.html#CaptureVideoOptions
    /// </summary>
    [Script(IsNative = true)]
    public class CaptureVideoOptions
    {
        #region Constructor

        public CaptureVideoOptions()
        {

        }

      

        #endregion

        #region PROPERTIES


        /// <summary>
        /// limit: The maximum number of video clips the device user can capture in a single capture operation. 
        /// The value must be greater than or equal to 1 (defaults to 1)
        /// </summary>
        public int limit;

        /// <summary>
        /// duration: The maximum duration of a video clip, in seconds
        /// </summary>
        public int duration;

        /// <summary>
        /// mode: The selected video capture mode. The value must match one of the elements in capture.supportedVideoModes
        /// </summary>
        public object mode;

        #endregion

    }
}
