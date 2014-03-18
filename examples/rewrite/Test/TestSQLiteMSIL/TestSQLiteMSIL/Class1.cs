//extern alias global2;
//extern alias global3;

using System;
using System.Collections.Generic;
//using System.Data.SQLite;
//using SQLite;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TestSQLiteMSIL
{

    //using SQLiteConnection = global2::Community.CsharpSqlite.SQLiteClient.SqliteConnection;
    //using SQLiteCommand = global3::Community.CsharpSqlite.SQLiteClient.SqliteCommand;
    //using SQLiteCommand = Community.CsharpSqlite.SQLiteClient.SqliteCommand;
    using System.Diagnostics;

    public class Class1
    {
        public static void Main(string[] args)
        {
            // http://stackoverflow.com/questions/9173485/how-can-i-create-an-in-memory-sqlite-database
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140318
            // http://stackoverflow.com/questions/3039096/how-do-i-connect-to-sqlite-db-file-from-c

            //Unable to load DLL 'SQLite.Interop.dll': The specified module could not be found. (Exception from HRESULT: 0x8007007E)

            // X:\jsc.svn\examples\rewrite\Test\TestSetConnectionString\TestSetConnectionString\Class1.cs

            // Invalid connection string: invalid URI
            //var c = new SQLiteConnection("Data Source=:memory:");
            //var c = new SQLiteConnection("Data Source=StressData.s3db");
            var c = new SQLiteConnection("Data Source=/StressData.s3db");

            // X:\jsc.svn\examples\rewrite\Test\TestInitializeArrayForSqlite3\TestInitializeArrayForSqlite3\Class1.cs

            //at System.Runtime.CompilerServices.RuntimeHelpers.InitializeArray(Array array, RuntimeFieldHandle fldHandle)
            //at Community.CsharpSqlite.Sqlite3..cctor()
            try
            {
                c.Open();
            }
            catch (Exception ex)
            {
                var s = new StackTrace(ex.InnerException);
                var ff = s.GetFrame(1);


                ;
            }

            // Error	1	'SQLite.SQLiteCommand' does not contain a constructor that takes 2 arguments	X:\jsc.svn\examples\rewrite\Test\TestSQLiteMSIL\TestSQLiteMSIL\Class1.cs	24	21	TestSQLiteMSIL

            var f = new SQLiteCommand(@"

create table if not exists 

FileStorageLogTable 

(
ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, 
ContentValue text not null 

)

", c).ExecuteNonQuery();


            //var i0 = new SQLiteCommand(@"insert into FileStorageLogTable (ContentValue) values ('text 1')", c).ExecuteNonQuery();
            // public DbCommand CreateCommand();
            var i0c = c.CreateCommand();
            i0c.CommandText = "insert into FileStorageLogTable (ContentValue) values ('text 1')";
            var i0 = i0c.ExecuteNonQuery();

            var i1 = new SQLiteCommand(@"insert into FileStorageLogTable (ContentValue) values ('text 1')", c).ExecuteNonQuery();
            var i2 = new SQLiteCommand(@"insert into FileStorageLogTable (ContentValue) values ('text 1')", c).ExecuteNonQuery();

            var max = new SQLiteCommand(@"
select coalesce(max(ContentKey), 0) as ContentKey /* integer */ from  FileStorageLogTable
", c).ExecuteScalar();

            //max = 0x0000000000000003
        }
    }
}
