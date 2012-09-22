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

                //new SQLiteCommand("delete from MY_TABLE", c).ExecuteNonQuery();

                // The database file is locked
                // http://stackoverflow.com/questions/4348860/the-database-file-is-locked-with-system-data-sqlite

                //new SQLiteCommand("insert into SQLiteWithDataGridView_MY_TABLE (Content) values ('" + e + "')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 2')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 3')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 4')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 5')", c).ExecuteNonQuery();



                c.Close();
            }

            // Send it back to the caller.
            y(e);
            //Console.WriteLine("AddItem exit");
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

                var id = cmd.ExecuteNonQuery();


                c.Close();
            }

            // Send it back to the caller.
            y("");
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
