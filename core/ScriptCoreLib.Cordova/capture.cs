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
    /// Provides access to the audio, image, and video capture capabilities of the device
    /// http://docs.phonegap.com/en/1.7.0/cordova_media_capture_capture.md.html#Capture
    /// </summary>
    [Script(IsNative = true)]
    public class Capture
    {
        #region Constructor

        public Capture()
        {

        }

        #endregion

        #region VARS

        public object[] supportedAudioModes;
        public object[] supportedImageModes;
        public object[] supportedVideoModes;

        #endregion

        #region PROPERTIES


        public static class PictureSourceType
        {
            public static object CAMERA;
            public static object PHOTOLIBRARY;
            public static object SAVEDPHOTOALBUM;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Encapsulates audio capture configuration options
        /// </summary>
        /// <param name="captureSuccess"></param>
        /// <param name="captureError"></param>
        /// <param name="options"></param>
        public void captureAudio(Action<MediaFile[]> captureSuccess, Action<CaptureError> captureError,  CaptureAudioOptions options=null)
        {

        }

        /// <summary>
        /// Start the camera application and return information about captured image file(s).
        /// </summary>
        /// <param name="captureSuccess"></param>
        /// <param name="captureError"></param>
        /// <param name="options"></param>
        public void captureImage( Action<MediaFile[]> captureSuccess, Action<CaptureError> captureError, CaptureImageOptions options)
        {
        }

        /// <summary>
        /// Start the video recorder application and return information about captured video clip file(s).
        /// </summary>
        /// <param name="captureSuccess"></param>
        /// <param name="captureError"></param>
        /// <param name="options"></param>
        public void captureVideo(Action<MediaFile[]> captureSuccess, Action<CaptureError> captureError, CaptureVideoOptions options)
        {
        }
        #endregion

    }
}
