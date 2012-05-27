using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using android.content;
using android.database;
using android.database.sqlite;

namespace ScriptCoreLib.Android
{


    [Script(Implements = typeof(DbConnectionStringBuilder))]
    public class __DbConnectionStringBuilder
    {
        public string ConnectionString
        {
            get
            {
                return InternalGetConnectionString();
            }
        }

        protected virtual string InternalGetConnectionString()
        {
            return null;
        }
    }

    [Script(Implements = typeof(SQLiteConnectionStringBuilder))]
    public class __SQLiteConnectionStringBuilder : __DbConnectionStringBuilder
    {
        public string DataSource { get; set; }
        public int Version { get; set; }
        public bool ReadOnly { get; set; }

        protected override string InternalGetConnectionString()
        {

            var r = "";

            r += "Data Source=" + this.DataSource + ";";
            __SQLiteConnectionHack.MYDATABASE_NAME = "MY_DATABASE.sqlite";


            r += "Version=" + ((object)this.Version).ToString() + ";";

            if (this.ReadOnly)
            {
                r += "Read Only=True;";
                __SQLiteConnectionHack.ForceReadOnly = true;
            }


            return r;
        }
    }

    [Script(Implements = typeof(System.Data.Common.DbCommand))]
    public abstract class __DbCommand
    {
        public abstract int ExecuteNonQuery();

    }

    [Script(Implements = typeof(System.Data.SQLite.SQLiteCommand))]
    public class __SQLiteCommand : __DbCommand
    {
        __SQLiteConnection c;
        string sql;

        public __SQLiteCommand(string sql, SQLiteConnection c)
        {
            this.c = (__SQLiteConnection)(object)c;
            this.sql = sql;
        }

        public override int ExecuteNonQuery()
        {
            c.db.execSQL(sql);
            return 0;
        }

        public __SQLiteDataReader ExecuteReader()
        {
            return new __SQLiteDataReader { cursor = c.db.rawQuery(sql, null) };
        }
    }

    [Script(Implements = typeof(System.Data.Common.DbDataReader))]
    public abstract class __DbDataReaders
    {
        public abstract bool Read();

        public abstract object this[string name] { get; }

    }

    [Script(Implements = typeof(System.Data.SQLite.SQLiteDataReader))]
    public class __SQLiteDataReader : __DbDataReaders
    {
        public Cursor cursor;

        int __state;

        public override bool Read()
        {
            if (__state == 0)
            {
                __state = 1;

                cursor.moveToFirst();
            }
            else
            {
                cursor.moveToNext();
            }

            return !(cursor.isAfterLast());
        }

        public override object this[string name]
        {
            get
            {
                int i = cursor.getColumnIndex(name);

                return cursor.getString(i);
            }
        }
    }

    [Script(Implements = typeof(System.Data.Common.DbConnection))]
    public abstract class __DbConnection : System.IDisposable
    {
        public abstract void Open();
        public abstract void Close();

        public abstract void Dispose();
    }

    public static class __SQLiteConnectionHack
    {
        public static Context Context;

        public static string MYDATABASE_NAME;
        public static bool ForceReadOnly;
    }

    [Script(Implements = typeof(System.Data.SQLite.SQLiteConnection))]
    public class __SQLiteConnection : __DbConnection
    {
        private LocalSQLiteOpenHelper h;
        public SQLiteDatabase db;

        bool ForceReadOnly;

        public __SQLiteConnection(string connectionstring)
        {
            this.h = new LocalSQLiteOpenHelper(__SQLiteConnectionHack.Context, __SQLiteConnectionHack.MYDATABASE_NAME);
        }

        public override void Open()
        {
            if (__SQLiteConnectionHack.ForceReadOnly)
                db = h.getReadableDatabase();
            else
                db = h.getWritableDatabase();

        }



        public override void Close()
        {
            h.close();
        }



        public class LocalSQLiteOpenHelper : SQLiteOpenHelper
        {

            public LocalSQLiteOpenHelper(Context context, string name, android.database.sqlite.SQLiteDatabase.CursorFactory factory = null, int version = 1)
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


        public override void Dispose()
        {
            this.Close();
        }
    }

}
