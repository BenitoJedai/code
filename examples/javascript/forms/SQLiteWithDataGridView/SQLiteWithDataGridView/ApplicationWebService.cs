using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;

namespace SQLiteWithDataGridView
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {

        const string DataSource = "SQLiteWithDataGridView.4.sqlite";

        public void GridExample_InitializeDatabase(string e, Action<string> y, string TableName)
        {
            //Console.WriteLine("AddItem enter");
            using (var c = new SQLiteConnection(

             new SQLiteConnectionStringBuilder
             {
                 DataSource = DataSource,
                 Version = 3
             }.ConnectionString

             ))
            {
                c.Open();
                // sqlite infers AUTO_INCREMENT while MySQL does not.
                // SQLite error (1): near "AUTO_INCREMENT": syntax error
                // http://www.sqlite.org/autoinc.html

                using (var cmd = new SQLiteCommand(
                    "create table if not exists " + TableName + " ("
                    + "ContentKey INTEGER PRIMARY KEY AUTOINCREMENT"
                    + ", ContentValue text not null"
                    + ", ContentComment text not null"
                    + ", ParentContentKey INTEGER "
                    + ", FOREIGN KEY(ParentContentKey) REFERENCES " + TableName + "(ContentKey)"
                    + ")", c))
                {
                    cmd.ExecuteNonQuery();
                }

                // http://stackoverflow.com/questions/5289861/sqlite-android-foreign-key-syntax
                using (var cmd = new SQLiteCommand(
                    "create table if not exists TransactionLog_" + TableName
                    + " ("
                    + " ContentKey INTEGER PRIMARY KEY AUTOINCREMENT"
                    + ", ContentReferenceKey INTEGER "
                    + ", ContentComment text not null "
                    + ", FOREIGN KEY(ContentReferenceKey) REFERENCES " + TableName + "(ContentKey)"
                    + ")"

                    , c))
                {
                    cmd.ExecuteNonQuery();
                }


                c.Close();
            }

            // Send it back to the caller.
            y(e);
            //Console.WriteLine("AddItem exit");
        }

        public void GridExample_GetTransactionKeyFor(
            string TableName,

            Action<string> y)
        {
            //Console.WriteLine("CountItems enter");
            using (var c = OpenReadOnlyConnection())
            {
                c.Open();

                // http://www.sqlite.org/lang_corefunc.html

                using (var reader = new SQLiteCommand("select coalesce(max(ContentKey), 0) from  TransactionLog_" + TableName, c).ExecuteReader())
                {

                    if (reader.Read())
                    {
                        var Content = (int)reader.GetInt32(0);

                        y("" + Content);

                    }
                }

                c.Close();

            }
            //Console.WriteLine("CountItems exit");
        }

        public void GridExample_AddItem(
            string ContentValue,
            string ContentComment,
            /* int? */ string ParentContentKey,

            Action<string> AtContentReferenceKey,
            string TableName)
        {
            //Console.WriteLine("AddItem enter");
            using (var c = new SQLiteConnection(

             new SQLiteConnectionStringBuilder
             {
                 DataSource = DataSource,
                 Version = 3
             }.ConnectionString

             ))
            {
                c.Open();

                var sql = "insert into " + TableName + " (ContentValue, ContentComment, ParentContentKey) "
                    + "values (";

                sql += "'" + ContentValue + "'";
                sql += ", '" + ContentComment + "'";

                if (ParentContentKey == "")
                    sql += ", null";
                else
                    sql += ", " + ParentContentKey;

                sql += ")";

                var cmd = new SQLiteCommand(sql, c);
                cmd.ExecuteNonQuery();

                var ContentReferenceKeyLong = c.LastInsertRowId;
                // jsc does not yet autobox for java 
                // int cannot be dereferenced
                var ContentReferenceKey = ContentReferenceKeyLong.ToString();
                //var ContentReferenceKey = ((object)ContentReferenceKeyLong).ToString();


                new SQLiteCommand(
                    "insert into TransactionLog_" + TableName + " (ContentReferenceKey, ContentComment) "
                    + "values (" + ContentReferenceKey + ", 'AddItem')"
                    , c).ExecuteNonQuery();

                if (ParentContentKey != "")
                    new SQLiteCommand(
                     "insert into TransactionLog_" + TableName + " (ContentReferenceKey, ContentComment) "
                     + "values (" + ParentContentKey + ", 'ChildAdded')"
                     , c).ExecuteNonQuery();

                AtContentReferenceKey(ContentReferenceKey);

                c.Close();
            }

            // Send it back to the caller.
            //Console.WriteLine("AddItem exit");
        }

        public void GridExample_UpdateItem(
                string TableName,

                string ContentKey,
                string ContentValue,
                string ContentComment,

                 Action<string> AtTransactionKey = null
            )
        {
            //Console.WriteLine("AddItem enter");
            using (var c = new SQLiteConnection(

             new SQLiteConnectionStringBuilder
             {
                 DataSource = DataSource,
                 Version = 3
             }.ConnectionString

             ))
            {
                c.Open();


                var cmd = new SQLiteCommand(
                    "update " + TableName + " set "
                    + " ContentValue = '" + ContentValue + "'"
                    + ", ContentComment = '" + ContentComment + "'"
                    + " where ContentKey = " + ContentKey
                    , c
                );

                cmd.ExecuteNonQuery();


                var cmd1 = new SQLiteCommand("insert into TransactionLog_" + TableName + " (ContentReferenceKey, ContentComment) values (" + ContentKey + ", 'AddItem')", c);
                cmd1.ExecuteNonQuery();


                c.Close();
            }


            if (AtTransactionKey != null)
                GridExample_GetTransactionKeyFor(TableName, AtTransactionKey);
            // Send it back to the caller.
            //Console.WriteLine("AddItem exit");
        }


        SQLiteConnection OpenReadOnlyConnection()
        {
            return new SQLiteConnection(

           new SQLiteConnectionStringBuilder
           {
               DataSource = DataSource,
               Version = 3,
               ReadOnly = true,

           }.ConnectionString
           );
        }

        public void GridExample_EnumerateItemsChangedBetweenTransactions(
            string TableName,
            /* int? */ string ParentContentKey,


            string FromTransaction,
            string ToTransaction,
            Action<string, string, string, string> AtContent,
            Action<string> done
        )
        {
            GridExample_InitializeDatabase("", delegate { }, TableName: TableName);

            using (var c = OpenReadOnlyConnection())
            {
                c.Open();

                // http://www.shokhirev.com/nikolai/abc/sql/joins.html
                // near "TransactionLog_SQLiteWithDataGridView_0_Table003": syntax error



                var sql =
                    "select "
                    + "TransactionLog_" + TableName + ".ContentReferenceKey"
                    + ", " + TableName + ".ContentValue"
                    + ", " + TableName + ".ContentComment  "
                    + ", (select count(*) from " + TableName + " t2 where t2.ParentContentKey = " + TableName + ".ContentKey) as ContentChildren "

                    + " from "
                    + " TransactionLog_" + TableName
                    + ", " + TableName
                    + " where "
                    + " TransactionLog_" + TableName + ".ContentReferenceKey = " + TableName + ".ContentKey"
                    + " and TransactionLog_" + TableName + ".ContentKey > " + FromTransaction
                    + " and TransactionLog_" + TableName + ".ContentKey <= " + ToTransaction;

                if (ParentContentKey == "")
                    sql += " and ParentContentKey is null";
                else
                    sql += " and ParentContentKey = " + ParentContentKey;

                using (var reader = new SQLiteCommand(sql, c).ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var ContentValue = (string)reader["ContentValue"];
                        var ContentComment = (string)reader["ContentComment"];

                        var ContentKeyInt32 = reader.GetInt32(reader.GetOrdinal("ContentReferenceKey"));
                        var ContentKey = ContentKeyInt32.ToString();
                        //var ContentKey = ((object)ContentKeyInt32).ToString();

                        var ContentChildrenInt32 = reader.GetInt32(reader.GetOrdinal("ContentChildren"));
                        var ContentChildren = ContentChildrenInt32.ToString();
                        //var ContentChildren = ((object)ContentChildrenInt32).ToString();

                        AtContent(ContentKey, ContentValue, ContentComment, ContentChildren);
                    }
                }

                c.Close();

            }

            // why does jsc not support parameterless yields?
            done("");
        }




        public void GridExample_EnumerateItems(
            string e,
            Action<string, string, string, string> y,
            string TableName,
            /* int? */ string ParentContentKey,
            Action<string> AtTransactionKey = null
            )
        {
            GridExample_InitializeDatabase("", delegate { }, TableName: TableName);

            //Console.WriteLine("EnumerateItems enter");
            using (var c = OpenReadOnlyConnection())
            {
                c.Open();

                var sql =
                    "select "
                    + "t1.ContentKey"
                    + ", t1.ContentValue"
                    + ", t1.ContentComment"
                    + ", (select count(*) from " + TableName + " t2 where t2.ParentContentKey = t1.ContentKey) as ContentChildren "
                    + " from " + TableName + " t1";


                if (ParentContentKey == "")
                    sql += " where t1.ParentContentKey is null";
                else
                    sql += " where t1.ParentContentKey = " + ParentContentKey;


                using (var reader = new SQLiteCommand(sql, c).ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var ContentValue = (string)reader["ContentValue"];
                        var ContentComment = (string)reader["ContentComment"];

                        var ContentKeyInt32 = reader.GetInt32(reader.GetOrdinal("ContentKey"));
                        var ContentKey = ContentKeyInt32.ToString();
                        //var ContentKey = ((object)ContentKeyInt32).ToString();
       
                        var ContentChildrenInt32 = reader.GetInt32(reader.GetOrdinal("ContentChildren"));
                        var ContentChildren = ContentChildrenInt32.ToString();
                        //var ContentChildren = ((object)ContentChildrenInt32).ToString();

                        y(ContentKey, ContentValue, ContentComment, ContentChildren);

                    }
                }

                c.Close();

            }
            //Console.WriteLine("EnumerateItems exit");

            if (AtTransactionKey != null)
                GridExample_GetTransactionKeyFor(TableName, AtTransactionKey);


        }



    }

    class LINQExample
    {
        static void Invoke()
        {
            // http://www.dotnetperls.com/join

            {
                var db = new
                {
                    TransactionLog_TableName1 = new[] { new { ContentKey = 0, ContentReferenceKey = 0 } },
                    TableName1 = new[] { new { ContentKey = 0, ContentValue = "", ContentComment = "" } }
                };

                var data = from t1 in db.TransactionLog_TableName1
                           join t2 in db.TableName1 on t1.ContentReferenceKey equals t2.ContentKey
                           select new { t1.ContentReferenceKey, t2.ContentValue, t2.ContentComment };

                // jsc when can I do this? :)
                var array = data.ToArray();
            }


        }
    }

    class DLINQExample
    {
        static void Invoke()
        {
            // http://www.dotnetperls.com/join



            {
                var db = new
                {
                    TransactionLog_TableName1 = new[] { new { ContentKey = 0, ContentReferenceKey = 0 } }.AsQueryable(),
                    TableName1 = new[] { new { ContentKey = 0, ContentValue = "", ContentComment = "" } }.AsQueryable()
                };

                // how would this look like on the client side?
                var data = from t1 in db.TransactionLog_TableName1
                           join t2 in db.TableName1 on t1.ContentReferenceKey equals t2.ContentKey
                           select new { t1.ContentReferenceKey, t2.ContentValue, t2.ContentComment };

                // jsc when can I do this? :)
                var array = data.ToArray();
            }
        }
    }
}
