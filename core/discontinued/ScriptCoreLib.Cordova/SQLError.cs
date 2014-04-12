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
    /// A SQLError object is thrown when an error occurs
    /// http://docs.phonegap.com/en/1.7.0/cordova_storage_storage.md.html#SQLError
    /// </summary>
    [Script(IsNative = true)]
    public class SQLError
    {
        #region Constructor

        public SQLError()
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

        public static int UNKNOWN_ERR;
        public static int DATABASE_ERR;
        public static int VERSION_ERR;
        public static int TOO_LARGE_ERR;
        public static int QUOTA_ERR;
        public static int SYNTAX_ERR;
        public static int CONSTRAINT_ERR;
        public static int TIMEOUT_ERR;

        #endregion

    }
}
