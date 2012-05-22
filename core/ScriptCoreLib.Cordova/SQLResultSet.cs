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
    /// When the executeSql method of a SQLTransaction is called it will invoke it's callback with a SQLResultSet.
    /// http://docs.phonegap.com/en/1.7.0/cordova_storage_storage.md.html#SQLResultSet
    /// </summary>
    [Script(IsNative = true)]
    public class SQLResultSet
    {
        #region Constructor

        public SQLResultSet()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///  the row ID of the row that the SQLResultSet object's SQL statement inserted into the database
        /// </summary>
        public int insertId;

        /// <summary>
        ///  the number of rows that were changed by the SQL statement. If the statement did not affect any rows then it is set to 0.
        /// </summary>
        public int rowsAffected;

        /// <summary>
        /// 
        /// </summary>
        public int rows;

        #endregion

        #region METHODS

       

        #endregion

    }
}
