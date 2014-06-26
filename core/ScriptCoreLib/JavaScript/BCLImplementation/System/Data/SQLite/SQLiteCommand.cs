using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.SQLite
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteCommand")]
    public class __SQLiteCommand : __DbCommand
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs
        public override string CommandText { get; set; }
        __SQLiteConnection c;

        public __SQLiteCommand(string sql, __SQLiteConnection c)
        {
            this.c = c;
            this.CommandText = sql;
        }


        public override Task<DbDataReader> __DbCommand_ExecuteReaderAsync()
        {
            Console.WriteLine(new { CommandText });

            var x = new TaskCompletionSource<DbDataReader>();

            // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs

            this.c.InternalDatabase.transaction(
                callback:
                     tx =>
                     {
                         tx.executeSql(
                             sqlStatement: this.CommandText,

                             callback:
                                   (SQLTransaction xtx, SQLResultSet r) =>
                                   {
                                       this.c.InternalLastSQLResultSet = null;

                                       __DbDataReader xx = new __SQLiteDataReader { r = r };

                                       x.SetResult(xx);
                                   }
                         );

                     }
            );

            return x.Task;
        }

        // http://msdn.microsoft.com/en-us/library/hh223678(v=vs.110).aspx
        // http://msdn.microsoft.com/en-us/library/system.data.common.dbcommand.executenonquery(v=vs.110).aspx
        public override global::System.Threading.Tasks.Task<int> ExecuteNonQueryAsync()
        {
            Console.WriteLine(new { CommandText });

            var x = new TaskCompletionSource<int>();

            // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs

            this.c.InternalDatabase.transaction(
                callback:
                     tx =>
                     {
                         tx.executeSql(
                             sqlStatement: this.CommandText,

                             callback:
                                   (SQLTransaction xtx, SQLResultSet r) =>
                                   {
                                       this.c.InternalLastSQLResultSet = r;

                                       x.SetResult((int)r.rowsAffected);
                                   }
                         );

                     }
            );

            return x.Task;
        }

        public override int ExecuteNonQuery()
        {
            ExecuteNonQueryAsync();

            return 0;
        }

        public new __SQLiteParameterCollection Parameters { get; set; }

        public override DbParameterCollection DbParameterCollection
        {
            get { return this.Parameters; }
        }

    }
}
