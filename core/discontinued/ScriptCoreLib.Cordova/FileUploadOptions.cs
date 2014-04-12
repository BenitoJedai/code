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
    /// A FileUploadOptions object can be passed to the FileTransfer objects upload method in order to specify additional parameters to the upload script.
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#FileUploadOptions
    /// </summary>
    [Script(IsNative = true)]
    public class FileUploadOptions
    {
        #region Constructor

        public FileUploadOptions(string fileKey, string fileName, string mimeType, object @params)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        ///  The name of the form element. If not set defaults to "file". (DOMString)
        /// </summary>
        public string fileKey;

        /// <summary>
        ///  The file name you want the file to be saved as on the server. If not set defaults to "image.jpg". (DOMString)
        /// </summary>
        public string fileName;

        /// <summary>
        ///  The mime type of the data you are uploading. If not set defaults to "image/jpeg". (DOMString)
        /// </summary>
        public string mimeType;

        /// <summary>
        ///  A set of optional key/value pairs to be passed along in the HTTP request. (Object)
        /// </summary>
        public object @params;

        /// <summary>
        ///  Should the data be uploaded in chunked streaming mode. If not set defaults to "true". (Boolean)
        /// </summary>
        public bool chunkedMode;

        #endregion


    }
}
