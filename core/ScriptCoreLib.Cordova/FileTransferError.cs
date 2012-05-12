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
    /// A FileTransferError object is returned via the error callback when an error occurs
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#FileTransferError
    /// </summary>
    [Script(IsNative = true)]
    public class FileTransferError
    {
        #region Constructor

        public FileTransferError()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// One of the pre-defined capture error codes listed below
        /// </summary>
        public int code;

        /// <summary>
        ///  URI to the source (string)
        /// </summary>
        public string source;

        /// <summary>
        ///  URI to the target (string)
        /// </summary>
        public string target;

        #endregion

        #region CONSTS

        public static int FILE_NOT_FOUND_ERR = 1;
        public static int INVALID_URL_ERR = 2;
        public static int CONNECTION_ERR = 3;

        #endregion

}
}
