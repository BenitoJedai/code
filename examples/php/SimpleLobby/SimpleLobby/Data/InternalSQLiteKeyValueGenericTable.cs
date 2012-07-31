using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace SimpleLobby
{
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
            sql += " (id text not null, x text, y text)";

            new SQLiteCommand(sql, Connection).ExecuteNonQuery();

            // http://www.sqlite.org/datatype3.html
        }

        public class Point
        {
            public string x;
            public string y;
        }

        #region String
        public class StringIndexer : InternalSQLiteKeyValueGenericTable
        {
            public InternalSQLiteKeyValueGenericTable Context;

            public Point this[string id]
            {
                set
                {
                    Context.Create();

                    if (Context.Connection.SQLiteCountByColumnName(Context.Name, "id", id) == 0)
                    {
                        var sql = "insert into ";
                        sql += Context.Name;
                        sql += " (id, x, y) values (";
                        sql += "'";
                        sql += id;
                        sql += "'";
                        sql += ", ";
                        sql += "'";
                        sql += value.x;
                        sql += "'";

                        sql += ", ";
                        sql += "'";
                        sql += value.y;
                        sql += "'";

                        sql += ")";



                        new SQLiteCommand(sql, Context.Connection).ExecuteNonQuery();

                        return;
                    }

                    #region update
                    {
                        var sql = "update ";
                        sql += Context.Name;

                        sql += " set x = ";
                        sql += "'";
                        sql += value.x;
                        sql += "'";

                        sql += " set y = ";
                        sql += "'";
                        sql += value.y;
                        sql += "'";


                        sql += " where id = ";
                        sql += "'";
                        sql += id;
                        sql += "'";

                        new SQLiteCommand(sql, Context.Connection).ExecuteNonQuery();
                    }
                    #endregion

                }

                get
                {
                    Context.Create();


                    var sql = "select x, y from ";
                    sql += Context.Name;
                    sql += " where id = ";
                    sql += "'";
                    sql += id;
                    sql += "'";

                    //new SQLiteCommand(sql, Connection).ExecuteScalar();

                    var value = default(Point);
                    var reader = new SQLiteCommand(sql, Context.Connection).ExecuteReader();

                    if (reader.Read())
                    {
                        value = new Point { x = reader.GetString(0), y = reader.GetString(1) };
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

        public List<string> GetKeys()
        {
            var sql = "select id from ";
            sql += Name;

            var value = new List<string>();

            var reader = new SQLiteCommand(sql, Connection).ExecuteReader();

            while (reader.Read())
            {
                value.Add(reader.GetString(0));
            }

            reader.Close();

            return value;
        }
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
            // http://www.electrictoolbox.com/check-if-mysql-table-exists/



            //var w = "select name from sqlite_master where type='table' and name=";
            var w = "select table_name from information_schema.tables where table_name=";
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
