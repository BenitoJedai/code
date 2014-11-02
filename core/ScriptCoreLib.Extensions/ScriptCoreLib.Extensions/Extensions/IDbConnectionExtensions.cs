using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

//namespace ScriptCoreLib.Extensions
namespace System.Data
{
    public static class IDbConnectionExtensions
    {
        public static Func<IDbConnection, string, IDbCommand> VirtualCreateCommand;


        public static int GetLastInsertRowId(this IDbConnection c)
        {
            Console.WriteLine("enter GetLastInsertRowId");

            // tested by
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAndroidInsert\ApplicationWebService.cs

            if (c == null)
            {
                Console.WriteLine("enter GetLastInsertRowId c null");

                return 0;
            }


// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141102
//#if FSQLiteConnection
            var xSQLiteConnection = c as SQLite.SQLiteConnection;

            Console.WriteLine("enter GetLastInsertRowId " + new { xSQLiteConnection });

            if (xSQLiteConnection != null)
            {
                return xSQLiteConnection.LastInsertRowId;
            }
//#endif

            // http://stackoverflow.com/questions/12858588/mysql-last-inserted-row-id
            // http://dev.mysql.com/doc/refman/5.6/en/information-functions.html#function_last-insert-id


            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            // tested by?
            // +		$exception	{"There is already an open DataReader associated with this Connection which must be closed first."}	System.Exception {System.Data.MySQL.MySQLException}
            var value = c.CreateCommand(
                "select LAST_INSERT_ID()").ExecuteScalar();

            if (value is long)
            {
                var i8 = (long)value;
                return (int)i8;

            }

            return (int)value;
        }

        // used by the asset compiler
        public static IDbCommand CreateCommand(this IDbConnection c, string CommandText)
        {
            if (VirtualCreateCommand != null)
                return VirtualCreateCommand(c, CommandText);
            // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Schema\TheGridTable.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140318

            var x = c.CreateCommand();


            x.CommandText = CommandText;

            return x;
        }

        // is this used by the inserts?
        // can we do the type conversion over here?
        public static IDbCommand AddParameter(this IDbCommand x, string ParameterName, object Value)
        {
            // X:\jsc.svn\examples\javascript\linq\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Where.cs


            // X:\jsc.svn\examples\javascript\linq\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs
            // if we have datetimes, are they security timestamped?
            // do they also contain signature of the data at the same time

            // will is work for jvm?
            if (Value is DateTime)
            {
                // in insert we are doing special conversions.
                // for where arguments we need to do the same.
                // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Ultra\Library\StringConversionsForStopwatch.cs

                var i8 = ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertToInt64((DateTime)Value);
                // shouldnt this happen in AddParam anyhow?
                Value = i8;
            }

            //Console.WriteLine("AddParameter enter " + new { x, ParameterName });
            var newParameter = x.CreateParameter();
            //Console.WriteLine("AddParameter " + new { newParameter });

            //java.lang.NullPointerException
            //   at System.Data.IDbConnectionExtensions.AddParameter(IDbConnectionExtensions.java:39)
            //   at SQLiteWithDataGridView.Schema.TheGridTableExtensions.ExecuteReader(TheGridTableExtensions.java:50)
            //   at SQLiteWithDataGridView.Schema.TheGridTable___c__DisplayClassf._SelectContent_b__e(TheGridTable___c__DisplayClassf.java:27)


            newParameter.ParameterName = ParameterName;
            newParameter.Value = Value;

            //  java.lang.NullPointerException
            //at System.Data.IDbConnectionExtensions.AddParameter(IDbConnectionExtensions.java:30)
            //at SQLiteWithDataGridView.Schema.TheGridTableExtensions.ExecuteReader(TheGridTableExtensions.java:55)
            //at

            var Parameters = x.Parameters;

            //Console.WriteLine("AddParameter " + new { Parameters });

            Parameters.Add(newParameter);
            return x;
        }
    }
}
