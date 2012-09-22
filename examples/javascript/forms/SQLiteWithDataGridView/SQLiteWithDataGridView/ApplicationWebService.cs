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
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        const string DataSource = "SQLiteWithDataGridView.0.sqlite";

        public void InitializeDatabase(string e, Action<string> y, string TableName = "SQLiteWithDataGridView_0_Table001")
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

                using (var cmd = new SQLiteCommand("create table if not exists " + TableName + " (ContentKey INTEGER PRIMARY KEY, ContentValue text not null, ContentComment text not null)", c))
                {
                    cmd.ExecuteNonQuery();
                }

                // http://stackoverflow.com/questions/5289861/sqlite-android-foreign-key-syntax
                using (var cmd = new SQLiteCommand(
                    "create table if not exists TransactionLog_" + TableName
                    + " ("
                    + " ContentKey INTEGER PRIMARY KEY "
                    + ", ContentReferenceKey INTEGER "
                    + ", ContentComment text not null "
                    + ", FOREIGN KEY(ContentComment) REFERENCES " + TableName + "(ContentKey)"
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

        public void CountTransactionLogItemsFor(string TableName, Action<string> y)
        {
            //Console.WriteLine("CountItems enter");
            using (var c = OpenReadOnlyConnection())
            {
                c.Open();

                using (var reader = new SQLiteCommand("select count(*) from  TransactionLog_" + TableName, c).ExecuteReader())
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

        public void AddItem(string ContentValue, string ContentComment, Action<string> y, string TableName = "SQLiteWithDataGridView_0_Table001")
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


                var cmd = new SQLiteCommand("insert into " + TableName + " (ContentValue, ContentComment) values ('" + ContentValue + "', '" + ContentComment + "')", c);
                cmd.ExecuteNonQuery();

                var ContentReferenceKey = c.LastInsertRowId;
                y(ContentReferenceKey.ToString());

                var cmd1 = new SQLiteCommand("insert into TransactionLog_" + TableName + " (ContentReferenceKey, ContentComment) values (" + ContentReferenceKey + ", 'AddItem')", c);
                cmd1.ExecuteNonQuery();

                c.Close();
            }

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



        public void EnumerateItems(string e, Action<string, string, string> y, string TableName = "SQLiteWithDataGridView_0_Table001", Action done = null)
        {
            InitializeDatabase("", delegate { }, TableName: TableName);

            //Console.WriteLine("EnumerateItems enter");
            using (var c = OpenReadOnlyConnection())
            {
                c.Open();

                using (var reader = new SQLiteCommand("select ContentKey, ContentValue, ContentComment from " + TableName, c).ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var ContentKey = reader.GetInt32(reader.GetOrdinal("ContentKey"));
                        var ContentValue = (string)reader["ContentValue"];
                        var ContentComment = (string)reader["ContentComment"];

                        y(ContentKey.ToString(), ContentValue, ContentComment);

                    }
                }

                c.Close();

            }
            //Console.WriteLine("EnumerateItems exit");

            if (done != null)
                done();
        }

    }
}
