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
    /// Contains methods that allow the user to manipulate the Database
    /// http://docs.phonegap.com/en/1.7.0/cordova_storage_storage.md.html#Database
    /// </summary>
    [Script(IsNative = true)]
    public class Database
    {
        #region Constructor

        public Database()
        {

        }

        #endregion

        #region METHODS

        /// <summary>
        ///  Runs a database transaction.
        /// </summary>
        public void transaction(){}

        /// <summary>
        ///  method allows scripts to atomically verify the version number and change it at the same time as doing a schema update.
        /// </summary>
        public void changeVersion(){}


        #endregion

    }
}
