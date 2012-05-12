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
    /// A MediaError object is returned to the mediaError callback function when an error occurs
    /// http://docs.phonegap.com/en/1.7.0/cordova_media_media.md.html#MediaError
    /// </summary>
    [Script(IsNative = true)]
    public class MediaError
    {
        #region Constructor

        public MediaError()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// One of the pre-defined capture error codes listed below
        /// </summary>
        public int code;

        /// <summary>
        /// message: Error message describing the details of the error.
        /// </summary>
        public string message;

        #endregion

        #region CONSTS

        public static int MEDIA_ERR_ABORTED;
        public static int MEDIA_ERR_NETWORK;
        public static int MEDIA_ERR_DECODE;
        public static int MEDIA_ERR_NONE_SUPPORTED;

        #endregion

    }
}
