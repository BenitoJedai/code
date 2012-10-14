using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
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
