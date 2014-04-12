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
    public class SQLResultSetList
    {
        #region Constructor

        public SQLResultSetList()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  the number of rows returned by the SQL query
        /// </summary>
        public int length;

        #endregion

        #region METHODS

        /// <summary>
        ///  returns the row at the specified index represented by a JavaScript object.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object item(int index)
        {
            return default(object);
        }

        #endregion

    }
}
