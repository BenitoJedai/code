using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using android.content;
using android.database;
using android.database.sqlite;
using ScriptCoreLib.Android.BCLImplementation.System.Data.Common;

namespace ScriptCoreLib.Android
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
