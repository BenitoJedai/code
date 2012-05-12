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
    /// Encapsulates the error code resulting from a failed media capture operation
    ///http://docs.phonegap.com/en/1.7.0/cordova_media_capture_capture.md.html#CaptureError
    /// </summary>
    [Script(IsNative = true)]
    public class CaptureError
    {
        #region Constructor

        public CaptureError()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// One of the pre-defined capture error codes listed below
        /// </summary>
        public int code;

        #endregion

        #region CONSTS

        public static int CAPTURE_INTERNAL_ERR=0;        //: Camera or microphone failed to capture image or sound.
        public static int CAPTURE_APPLICATION_BUSY=1;   //: Camera application or audio capture application is currently serving other capture request.
        public static int CAPTURE_INVALID_ARGUMENT=2;   //: Invalid use of the API (e.g. limit parameter has value less than one).
        public static int CAPTURE_NO_MEDIA_FILES=3;     //: User exited camera application or audio capture application before capturing anything.
        public static int CAPTURE_NOT_SUPPORTED = 20;   //: The requested capture operation is not supported.

        #endregion


    }
}
