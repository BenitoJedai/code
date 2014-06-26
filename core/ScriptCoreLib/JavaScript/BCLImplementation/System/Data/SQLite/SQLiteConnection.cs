using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnection))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteConnection")]
    public class __SQLiteConnection : __DbConnection
    {
        // X:\jsc.svn\examples\javascript\test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs

        public override string ConnectionString { get; set; }

        public __SQLiteConnection(string connectionstring)
        {
            this.ConnectionString = connectionstring;
        }

        public override global::System.Data.Common.DbCommand CreateDbCommand()
        {
            return (global::System.Data.Common.DbCommand)(object)new __SQLiteCommand("", this);
        }

        public Database db;
        public SQLResultSet InternalLastSQLResultSet;

        public override void Open()
        {
            // ignore connectionstring datasource for now.
            db = Native.window.openDatabase();

        }

        public override void Close()
        {
            // we cant close it can we?
            db = null;
        }

        public override void Dispose()
        {
        }

        public int LastInsertRowId
        {
            get
            {
                return (int)InternalLastSQLResultSet.insertId;
            }
        }
    }
}
