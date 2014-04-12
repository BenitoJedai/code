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
    /// A 'FileError' object is set when an error occurs in any of the File API methods
    /// http://docs.phonegap.com/en/1.7.0/cordova_file_file.md.html#FileError
    /// </summary>
    [Script(IsNative = true)]
    public class FileError
    {
        #region Constructor

        public FileError()
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

        public static int NOT_FOUND_ERR = 1;
        public static int SECURITY_ERR = 2;
        public static int ABORT_ERR = 3;
        public static int NOT_READABLE_ERR = 4;
        public static int ENCODING_ERR = 5;
        public static int NO_MODIFICATION_ALLOWED_ERR = 6;
        public static int INVALID_STATE_ERR = 7;
        public static int SYNTAX_ERR = 8;
        public static int INVALID_MODIFICATION_ERR = 9;
        public static int QUOTA_EXCEEDED_ERR = 10;
        public static int TYPE_MISMATCH_ERR = 11;
        public static int PATH_EXISTS_ERR = 12;

        #endregion

    }
}
