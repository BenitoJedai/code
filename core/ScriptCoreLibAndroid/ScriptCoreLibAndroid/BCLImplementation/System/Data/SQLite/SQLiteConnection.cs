using android.content;
using android.database.sqlite;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    // https://github.com/mono/mono/blob/effa4c07ba850bedbe1ff54b2a5df281c058ebcb/mcs/class/Mono.Data.Sqlite/Mono.Data.Sqlite_2.0/SQLiteConnection.cs
    // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs


    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnection))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteConnection")]
    internal class __SQLiteConnection : __DbConnection
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        //        Implementation not found for type import :
        //type: System.Data.SQLite.SQLiteConnection
        //method: Void set_BusyTimeout(Int32)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        // we aint using this yet? tests needed!
        public int BusyTimeout { get; set; }

        public override global::System.Data.Common.DbCommand CreateDbCommand()
        {
            return (global::System.Data.Common.DbCommand)(object)new __SQLiteCommand("", this);
        }


        private LocalSQLiteOpenHelper h;
        public SQLiteDatabase db;

        bool ForceReadOnly;

        public string InternalConnectionString;

        public bool InternalReadOnly
        {
            get
            {
                return InternalConnectionString.Contains("Read Only=True;");
            }
        }

        public string InternalDatabaseName
        {
            get
            {
                // I/System.Console( 4328): enter InternalDatabaseName { InternalConnectionString = ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite.__SQLiteConnectionStringBuilder@407714e0 }
                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAndroidOrderByThenGroupBy\ApplicationWebService.cs
                Console.WriteLine("enter InternalDatabaseName " + new { InternalConnectionString });

                var prefix = "Data Source=";
                var suffix = ";";

                var i = InternalConnectionString.IndexOf(prefix) + prefix.Length;

                return InternalConnectionString.Substring(
                    i,
                    InternalConnectionString.IndexOf(suffix, i) - i
                );
            }
        }



        public override string ConnectionString
        {
            get;
            set;
        }

        public __SQLiteConnection(string connectionstring)
        {
            this.InternalConnectionString = connectionstring;
            this.ConnectionString = connectionstring;


            //Console.WriteLine(
            //    new
            //    {
            //        InternalConnectionString,
            //        InternalDatabaseName,
            //        InternalReadOnly
            //    }.ToString()
            //);


            //Console.WriteLine(
            //    "InternalConnectionString: " + InternalConnectionString + ", " +
            //    "InternalDatabaseName: " + InternalDatabaseName + ", " +
            //    "InternalReadOnly: " + InternalReadOnly
            //);

            this.h = new LocalSQLiteOpenHelper(
                ThreadLocalContextReference.CurrentContext,
                InternalDatabaseName
            );
        }

        public override void Open()
        {
            if (this.InternalReadOnly)
                db = h.getReadableDatabase();
            else
                db = h.getWritableDatabase();

        }



        public override void Close()
        {
            h.close();
        }


        [Script]
        public class LocalSQLiteOpenHelper : SQLiteOpenHelper
        {

            public LocalSQLiteOpenHelper(Context context, string name,
                SQLiteDatabase.CursorFactory factory = null, int version = 1)
                : base(context, name, factory, version)
            {

            }

            public override void onCreate(SQLiteDatabase db)
            {
                // TODO Auto-generated method stub
                //db.execSQL(SCRIPT_CREATE_DATABASE);
            }

            public override void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
            {
                // TODO Auto-generated method stub

            }




        }


        public override void Dispose(bool e)
        {
            this.Close();
        }


        //  public int LastInsertRowId { get; }
        //public long LastInsertRowId
        public int LastInsertRowId
        {
            get
            {
                // https://groups.google.com/forum/?fromgroups=#!topic/android-developers/r3LHk-fCoNI

                var cursor = this.db.rawQuery(
                    "SELECT last_insert_rowid()",
                    null
                );

                if (cursor.moveToFirst())
                {
                    return cursor.getInt(0);
                }

                return -1;
            }
        }

    }


}
