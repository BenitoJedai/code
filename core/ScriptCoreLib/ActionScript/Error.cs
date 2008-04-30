using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/Error.html
    [Script(IsNative = true)]
    public class Error
    {


        #region Properties
        /// <summary>
        /// [read-only] Contains the reference number associated with the specific error message.
        /// </summary>
        public int errorID { get; private set; }

        #endregion


        #region Fields
        /// <summary>
        /// Contains the message associated with the Error object.
        /// </summary>
        public string message;

        /// <summary>
        /// Contains the name of the Error object.
        /// </summary>
        public string name;

        #endregion

        #region Methods
        /// <summary>
        /// Returns the call stack for an error as a string at the time of the error's construction (for the debugger version of Flash Player and the AIR Debug Launcher (ADL) only; returns null if not using the debugger version of Flash Player or the ADL.
        /// </summary>
        public string getStackTrace()
        {
            return default(string);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new Error object.
        /// </summary>
        public Error(string message, int id)
        {
        }

        /// <summary>
        /// Creates a new Error object.
        /// </summary>
        public Error(string message)
        {
        }

        /// <summary>
        /// Creates a new Error object.
        /// </summary>
        public Error()
            : base()
        {
        }

        #endregion


    }
}
