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
    /// Contains methods that allow the user to execute SQL statements against the Database
    /// http://docs.phonegap.com/en/1.7.0/cordova_storage_storage.md.html#SQLTransaction
    /// </summary>
    [Script(IsNative = true)]
    public class SQLTransaction
    {
        #region Constructor

        public SQLTransaction()
        {

        }

        #endregion

        #region METHODS

        /// <summary>
        ///  executes a SQL statement
        /// </summary>
        public void executeSql(string tx){  }


        #endregion

    }
}
