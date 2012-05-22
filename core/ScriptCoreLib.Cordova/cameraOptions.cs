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
    /// Optional parameters to customize the camera settings
    /// http://docs.phonegap.com/en/1.7.0/cordova_camera_camera.md.html#cameraOptions
    /// </summary>
    [Script(IsNative = true)]
    public class CameraOptions
    {
        #region Constructor

        public CameraOptions()
        {

        }

        /// <summary>
        /// quality: Quality of saved image. Range is [0, 100]. (Number)
        /// </summary>
        public int quality { get; set; }
        
        /// <summary>
        /// destinationType: Choose the format of the return value. Defined in navigator.camera.DestinationType (Number)
        /// </summary>
        public Camera.DestinationType destinationType;


        /// <summary>
        /// sourceType: Set the source of the picture. Defined in nagivator.camera.PictureSourceType (Number)
        /// </summary>
        public int sourceType { get; set; }

        /// <summary>
        /// allowEdit: Allow simple editing of image before selection. (Boolean)
        /// </summary>
        public bool allowEdit;

        /// <summary>
        /// encodingType: Choose the encoding of the returned image file. Defined in navigator.camera.EncodingType (Number)
        /// </summary>
        ///      
        public Camera.EncodingType encodingType;


        
         /// <summary>
        /// targetWidth: Width in pixels to scale image. 
        /// Must be used with targetHeight. Aspect ratio is maintained. (Number)
        /// </summary>
        public int targetWidth { get; set; }


         /// <summary>
        /// targetHeight: Height in pixels to scale image. 
        /// Must be used with targetWidth. Aspect ratio is maintained. (Number)
        /// </summary>
        public int targetHeight { get; set; }


        /// <summary>
        /// mediaType: Set the type of media to select from. 
        /// Only works when PictureSourceType is PHOTOLIBRARY or SAVEDPHOTOALBUM. 
        /// Defined in nagivator.camera.MediaType (Number)
        /// </summary>
        public Camera.MediaType mediaType;

        /// <summary>
        /// correctOrientation: Rotate the image to correct for the orientation of the device during capture. (Boolean)
        /// </summary>
        public bool correctOrientation;


      /// <summary>
        /// saveToPhotoAlbum: Save the image to the photo album on the device after capture. (Boolean)
        /// </summary>
        public int saveToPhotoAlbum { get; set; }

        #endregion

    }
}
