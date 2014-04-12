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
    /// The camera object provides access to the device's default camera application
    /// http://docs.phonegap.com/en/1.7.0/cordova_camera_camera.md.html#Camera
    /// </summary>
    [Script(IsNative = true)]
    public class Camera
    {
        #region Constructor

        public Camera()
        {

        }

        #endregion

        #region METHODS

        public void getPicture(Action<object> cameraSuccess, Action<string> cameraError,CameraOptions options=null)
        {

        }

        #endregion

        #region ENUMS

        public enum DestinationType
        {
            DATA_URL = 0,                // Return image as base64 encoded string
            FILE_URI = 1                 // Return image file URI
        };

        
        public enum PictureSourceType
        {
            PHOTOLIBRARY = 0,
            CAMERA = 1,
            SAVEDPHOTOALBUM = 2
        };

        public enum EncodingType
        {
            JPEG = 0,               // Return JPEG encoded image
            PNG = 1                 // Return PNG encoded image
        }

        /// <summary>
        /// mediaType: Set the type of media to select from. 
        /// Only works when PictureSourceType is PHOTOLIBRARY or SAVEDPHOTOALBUM. 
        /// Defined in nagivator.camera.MediaType (Number)
        /// </summary>
        public enum MediaType
        { 
            PICTURE= 0,             // allow selection of still pictures only. DEFAULT. Will return format specified via DestinationType
            VIDEO= 1,               // allow selection of video only, WILL ALWAYS RETURN FILE_URI
            ALLMEDIA = 2            // allow selection from all media types
        }


        #endregion

    }
}
