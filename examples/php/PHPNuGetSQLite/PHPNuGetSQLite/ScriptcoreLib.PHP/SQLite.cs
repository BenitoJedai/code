using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP //.Android
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
        public string Uri { get; set; }
        public string Password { get; set; }


        protected override string InternalGetConnectionString()
        {

            var r = "";

            //r += "Data Source=" + this.DataSource + ";";

            __SQLiteConnectionHack.MYDATABASE_NAME = DataSource; // "MY_DATABASE.sqlite";
            __SQLiteConnectionHack.MyDBLoginInfo.Database = DataSource;     // __SQLiteConnectionHack.MYDATABASE_NAME;
            __SQLiteConnectionHack.MyDBLoginInfo.Host = "localhost";
            __SQLiteConnectionHack.MyDBLoginInfo.User = "root";     //     //"root";
            __SQLiteConnectionHack.MyDBLoginInfo.Pass = "";

            //r += "Version=" + ((object)this.Version)+";";//.ToString() + ";";

            if (this.ReadOnly)
            {
                // r += "Read Only=True;";
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
        object queryResult;

        bool debug = false;

        public __SQLiteCommand(string sql, SQLiteConnection c)
        {
            this.c = (__SQLiteConnection)(object)c;
            this.sql = sql;
        }

        public override int ExecuteNonQuery()
        {
            // c.db.execSQL(sql);

            object o = MySQL.API.mysql_query(sql);  //no NonQuery Native Avail for PHP

            if (debug)
            {
                string s = "<empty>";
                if (o != null)
                    s = (string)o;

                Console.WriteLine("ExecuteNonQuery result:" + s);
            }

            return 0;
        }

        public __SQLiteDataReader ExecuteReader()
        {
            queryResult = MySQL.API.mysql_query(sql);

            if ((queryResult == null))
                return null;

            if (((bool)queryResult == false))
                return null;

            return new __SQLiteDataReader
            {
                queryResult = queryResult
                //c.db.rawQuery(sql, null) };
            };
        }
    }

    [Script(Implements = typeof(System.Data.Common.DbDataReader))]
    public abstract class __DbDataReader
    {
        public abstract void Close();
        public abstract bool Read();

        public abstract object this[string name] { get; }

        public abstract string GetString(int i);
        public abstract int GetInt32(int i);

    }

    [Script(Implements = typeof(System.Data.SQLite.SQLiteDataReader))]
    public class __SQLiteDataReader : __DbDataReader
    {
        public IArray cursor; // Cursor cursor;
        public object queryResult;

        int __state;
        int __index = 0;

        public override void Close()
        {
            // ?
        }

        public override bool Read()
        {
            var x = MySQL.API.mysql_fetch_array(queryResult, MySQL.API.FetchArrayResult.MYSQL_BOTH);

            var e = Expando.Of(x);

            //Console.WriteLine(e.TypeString);

            if (e.IsArray)
            {
                cursor = x;

                if (cursor != null)
                    if (cursor.Length > 0)
                    {

                        return true;
                    }
            }

            return false;

            /*
            if (__state == 0)
            {
                __state = 1;
    
                //cursor.moveToFirst();

                __index = 0; // move to first                
            }
            else
            {
                //cursor.moveToNext();

                __index++; // move to next
            }

            return __index < cursor.Length; //!(cursor.isAfterLast());
             * */
        }

        public override object this[string name]
        {
            get
            {
                // int i = cursor.getColumnIndex(name);

                // return cursor.getString(i);

                var Keys = cursor.Keys;
                if (Keys.Contains(name))
                {
                    var value = cursor[name];
                    return value;
                }

                return null;
            }
        }

        public override string GetString(int i)
        {
            // int i = cursor.getColumnIndex(name);

            // return cursor.getString(i);

            var keys = (object[])cursor.Keys;
            var name = keys[i];

            return (string)cursor[name];
        }

        public override int GetInt32(int i)
        {
            // int i = cursor.getColumnIndex(name);

            // return cursor.getString(i);

            var keys = (object[])cursor.Keys;
            var name = keys[i];

            return (int)cursor[name];
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
        // public static Context Context;

        public static string MYDATABASE_NAME;
        public static bool ForceReadOnly;

        public static MySQL.LoginInfo MyDBLoginInfo = new MySQL.LoginInfo();
    }

    [Script(Implements = typeof(System.Data.SQLite.SQLiteConnection))]
    public class __SQLiteConnection : __DbConnection
    {
        //private LocalSQLiteOpenHelper h;
        public object db; // SQLiteDatabase db;

        bool ForceReadOnly;

        bool debug = false;

        public __SQLiteConnection(string connectionstring)
        {
            //this.h = new LocalSQLiteOpenHelper(__SQLiteConnectionHack.Context, __SQLiteConnectionHack.MYDATABASE_NAME);
        }

        public override void Open()
        {
            db = MySQL.Connect(__SQLiteConnectionHack.MyDBLoginInfo);


            if (db == null)
            {
                Console.WriteLine("Database connect failed: db=" + __SQLiteConnectionHack.MyDBLoginInfo.Database + " ; host=" + __SQLiteConnectionHack.MyDBLoginInfo.Host + " ; user=" + __SQLiteConnectionHack.MyDBLoginInfo.User + " ; psswd=" + __SQLiteConnectionHack.MyDBLoginInfo.Pass);
            }
            else
            {
                if (debug)
                    Console.WriteLine("Database connect success");
            }

            //Console.WriteLine("Trying hard!");
            var r = MySQL.API.mysql_query("CREATE DATABASE IF NOT EXISTS `" + __SQLiteConnectionHack.MYDATABASE_NAME + "`");

            //Console.WriteLine("error: " + MySQL.API.mysql_error());







            if (!MySQL.API.mysql_select_db(__SQLiteConnectionHack.MYDATABASE_NAME))
            {
                Console.WriteLine("Database select failed, db=" + __SQLiteConnectionHack.MYDATABASE_NAME);
            }
            else
            {
                if (debug)
                    Console.WriteLine("Database select success");
            }

            /*
            if (__SQLiteConnectionHack.ForceReadOnly)
                db = h.getReadableDatabase();
            else
                db = h.getWritableDatabase();
            */
        }



        public override void Close()
        {
            // h.close();  

            // no PHP my_sql Close command implemented yet

        }


        /*
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

        */
        public override void Dispose()
        {
            this.Close();
        }
    }

}
