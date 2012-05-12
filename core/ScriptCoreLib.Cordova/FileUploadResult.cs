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
    /// A FileUploadResult object is returned via the success callback of the FileTransfer upload method
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#FileUploadResult
    /// </summary>
    [Script(IsNative = true)]
    public class FileUploadResult
    {
        #region Constructor

        public FileUploadResult()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  The number of bytes sent to the server as part of the upload. (long)
        /// </summary>
        public long bytesSent;

        /// <summary>
        ///  The HTTP response code returned by the server. (long)
        /// </summary>
        public long responseCode;

        /// <summary>
        ///  The HTTP response returned by the server. (DOMString)
        /// </summary>
        public string response;

        #endregion


    }
}
