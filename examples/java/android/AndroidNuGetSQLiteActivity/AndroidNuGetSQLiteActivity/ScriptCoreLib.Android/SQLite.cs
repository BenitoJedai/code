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

    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteConnectionStringBuilder, System.Data.SQLite")]
    public class __SQLiteConnectionStringBuilder : __DbConnectionStringBuilder
    {
        public string DataSource { get; set; }
        public int Version { get; set; }
        public bool ReadOnly { get; set; }

        protected override string InternalGetConnectionString()
        {

            var r = "";

            r += "Data Source=" + this.DataSource + ";";
            __SQLiteConnectionHack.MYDATABASE_NAME = this.DataSource;


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

    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteCommand, System.Data.SQLite")]
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
            android.util.Log.d("__SQLiteCommand", sql);


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
        public abstract void Close();
        public abstract bool Read();

        public abstract object this[string name] { get; }

        public abstract string GetString(int i);
        public abstract int GetInt32(int i);


    }

    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteDataReader, System.Data.SQLite")]
    public class __SQLiteDataReader : __DbDataReaders
    {
        public Cursor cursor;

        int __state;

        public override void Close()
        {
            cursor.close();
        }

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

        public override string GetString(int i)
        {
            return cursor.getString(i);
        }

        public override int GetInt32(int i)
        {
            return cursor.getInt(i);
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

    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteConnection, System.Data.SQLite")]
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








    class InternalSQLiteKeyValueGenericTable
    {
        // CRUD

        public SQLiteConnection Connection { get; set; }
        public string Name { get; set; }

        public bool Exists
        {
            get { return Connection.SQLiteTableExists(Name); }
        }

        public void Create()
        {
            if (this.Exists)
                return;

            // key value table!

            //var sql = "create table if not exists ";
            var sql = "create table ";

            sql += Name;
            sql += " (Key text not null, ValueString text, ValueInt32 integer)";

            new SQLiteCommand(sql, Connection).ExecuteNonQuery();

            // http://www.sqlite.org/datatype3.html
        }

        #region Int32
        public class Int32Indexer
        {
            public InternalSQLiteKeyValueGenericTable Context;

            public int this[string Key]
            {
                set
                {
                    Context.Create();

                    if (Context.Connection.SQLiteCountByColumnName(Context.Name, "Key", Key) == 0)
                    {
                        var sql = "insert into ";
                        sql += Context.Name;
                        sql += " (Key, ValueInt32) values (";
                        sql += "'";
                        sql += Key;
                        sql += "'";
                        sql += ", ";
                        sql += ((object)value).ToString();
                        sql += ")";



                        new SQLiteCommand(sql, Context.Connection).ExecuteNonQuery();

                        return;
                    }

                    #region update
                    {
                        var sql = "update ";
                        sql += Context.Name;
                        sql += " set ValueInt32 = ";
                        sql += ((object)value).ToString();
                        sql += " where Key = ";
                        sql += "'";
                        sql += Key;
                        sql += "'";

                        new SQLiteCommand(sql, Context.Connection).ExecuteNonQuery();
                    }
                    #endregion

                }

                get
                {
                    Context.Create();


                    var sql = "select ValueInt32 from ";
                    sql += Context.Name;
                    sql += " where Key = ";
                    sql += "'";
                    sql += Key;
                    sql += "'";

                    //new SQLiteCommand(sql, Connection).ExecuteScalar();

                    var value = 0;
                    var reader = new SQLiteCommand(sql, Context.Connection).ExecuteReader();

                    if (reader.Read())
                    {
                        value = reader.GetInt32(0);
                    }
                    reader.Close();

                    return value;
                }
            }
        }


        public Int32Indexer Int32
        {
            get
            {
                return new Int32Indexer { Context = this };
            }
        }
        #endregion

        // XElement is next? :)

        #region String
        public class StringIndexer : InternalSQLiteKeyValueGenericTable
        {
            public InternalSQLiteKeyValueGenericTable Context;

            public string this[string Key]
            {
                set
                {
                    Context.Create();

                    if (Context.Connection.SQLiteCountByColumnName(Context.Name, "Key", Key) == 0)
                    {
                        var sql = "insert into ";
                        sql += Context.Name;
                        sql += " (Key, ValueString) values (";
                        sql += "'";
                        sql += Key;
                        sql += "'";
                        sql += ", ";
                        sql += "'";
                        sql += value;
                        sql += "'";
                        sql += ")";



                        new SQLiteCommand(sql, Context.Connection).ExecuteNonQuery();

                        return;
                    }

                    #region update
                    {
                        var sql = "update ";
                        sql += Context.Name;
                        sql += " set ValueString = ";
                        sql += "'";
                        sql += value;
                        sql += "'";
                        sql += " where Key = ";
                        sql += "'";
                        sql += Key;
                        sql += "'";

                        new SQLiteCommand(sql, Context.Connection).ExecuteNonQuery();
                    }
                    #endregion

                }

                get
                {
                    Context.Create();


                    var sql = "select ValueString from ";
                    sql += Context.Name;
                    sql += " where Key = ";
                    sql += "'";
                    sql += Key;
                    sql += "'";

                    //new SQLiteCommand(sql, Connection).ExecuteScalar();

                    var value = default(string);
                    var reader = new SQLiteCommand(sql, Context.Connection).ExecuteReader();

                    if (reader.Read())
                    {
                        value = reader.GetString(0);
                    }
                    reader.Close();

                    return value;
                }
            }
        }


        public StringIndexer String
        {
            get
            {
                return new StringIndexer { Context = this };
            }
        }
        #endregion
    }

    static class InternalSQLiteExtensions
    {

        public static int SQLiteCountByColumnName(this SQLiteConnection Connection, string Name, string ByColumnName, string ValueString)
        {
            var sql = "select count(*) from ";
            sql += Name;
            sql += " where ";
            sql += ByColumnName;
            sql += " = ";
            sql += "'";
            sql += ValueString;
            sql += "'";

            var value = 0;
            var reader = new SQLiteCommand(sql, Connection).ExecuteReader();

            if (reader.Read())
            {
                value = reader.GetInt32(0);
            }

            reader.Close();

            return value;
        }

        public static bool SQLiteTableExists(this SQLiteConnection c, string name)
        {
            var w = "select name from sqlite_master where type='table' and name=";
            w += "'";
            w += name;
            w += "'";


            var reader = new SQLiteCommand(w, c).ExecuteReader();

            var value = reader.Read();

            reader.Close();

            return value;
        }
    }

}
