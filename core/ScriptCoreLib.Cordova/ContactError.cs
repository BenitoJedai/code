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
    /// A ContactError object is returned to the contactError callback when an error occurs
    /// http://docs.phonegap.com/en/1.7.0/cordova_contacts_contacts.md.html#ContactError
    /// </summary>
    [Script(IsNative = true)]
    public class ContactError
    {
        #region Constructor

        public ContactError()
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

        public static int UNKNOWN_ERROR = 0;
        public static int INVALID_ARGUMENT_ERROR = 1;
        public static int TIMEOUT_ERROR = 2;
        public static int PENDING_OPERATION_ERROR = 3;
        public static int IO_ERROR = 4;
        public static int NOT_SUPPORTED_ERROR = 5;
        public static int PERMISSION_DENIED_ERROR = 20;

        #endregion

    }
}
