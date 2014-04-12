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
    /// http://docs.phonegap.com/en/1.7.0/cordova_media_capture_capture.md.html#CaptureImageOptions
    /// </summary>
    [Script(IsNative = true)]
    public class CaptureImageOptions
    {
        #region Constructor

        public CaptureImageOptions()
        {

        }

      

        #endregion

        #region PROPERTIES


        /// <summary>
        /// limit: The maximum number of images the device user can capture in a single capture operation. 
        /// The value must be greater than or equal to 1 (defaults to 1).
        /// </summary>
        public int limit;


        /// <summary>
        ///mode: The selected image mode. The value must match one of the elements in capture.supportedImageModes.
        /// </summary>
        public object mode;

        #endregion

    }
}
