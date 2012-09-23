using ScriptCoreLib.PHP.BCLImplementation.System.Data.Common;
using ScriptCoreLib.PHP.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnection))]
    internal class __SQLiteConnection : __DbConnection
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
            //Console.WriteLine("<-- Connect " + MySQL.API.mysql_error() + " -->");


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
            var r = MySQL.API.mysql_query("CREATE DATABASE IF NOT EXISTS `" + __SQLiteConnectionHack.MyDBLoginInfo.Database + "`");

            //Console.WriteLine("<-- mysql_query " + MySQL.API.mysql_error() + " -->");







            if (!MySQL.API.mysql_select_db(__SQLiteConnectionHack.MyDBLoginInfo.Database))
            {
                Console.WriteLine("Database select failed, db=" + __SQLiteConnectionHack.MyDBLoginInfo.Database);
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


        public long LastInsertRowId
        {
            get
            {
                // http://php.net/manual/en/function.mysql-insert-id.php
                // http://php.net/manual/en/mysqli.insert-id.php


                return MySQL.API.mysql_insert_id();
            }
        }
    }

}
