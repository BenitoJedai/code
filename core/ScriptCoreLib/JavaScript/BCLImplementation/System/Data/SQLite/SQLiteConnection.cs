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
        }

        public override void Open()
        {
        }

        public override void Close()
        {
        }

        public override void Dispose()
        {
        }

        //        0200005e System.Data.IDbConnectionExtensions
        //no implementation for System.Data.SQLite.SQLiteConnection 4c618da5-288c-3fe4-a06f-5e12ef83b1d5
        //script: error JSC1000: No implementation found for this native method, please implement [System.Data.SQLite.SQLiteConnection.get_LastInsertRowId()]
        public int LastInsertRowId
        {
            get
            {
                return -1;
            }
        }
    }
}
