using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;

namespace FormsMeetsSQLiteForAndroid
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


        public void CountItems(string e, Action<string> y)
        {
        }

        public void EnumerateItems(string e, Action<string> y)
        {
            using (var c = new SQLiteConnection(

               new SQLiteConnectionStringBuilder
               {
                   DataSource = "MY_DATABASE.sqlite",
                   Version = 3,
                   ReadOnly = true,

                   // cannot be set while running under .net debugger
                   // php needs to use defaults

                   //Password = "",
                   //Uri = "localhost"              
               }.ConnectionString
               ))
            {
                // Invalid ConnectionString format for parameter "password"
                c.Open();




                var reader = new SQLiteCommand("select Content from MY_TABLE", c).ExecuteReader();

                //var x = from k in MY_TABLE
                //        select new { k.Content };


                //if (reader == null)
                //    contentRead += "Reader was null";
                //else
                //{
                //var i = 6;

                while (reader.Read())
                {
                    //i--;
                    var Content = (string)reader["Content"];

                    y(Content);

                    //if (i == 0)
                    //    break;
                }
                //}


                c.Close();

            }
        }

        public void AddItem(string e, Action<string> y)
        {
            using (var c = new SQLiteConnection(

             new SQLiteConnectionStringBuilder
             {
                 DataSource = "MY_DATABASE.sqlite",
                 Version = 3
             }.ConnectionString

             ))
            {
                c.Open();

                new SQLiteCommand("create table if not exists MY_TABLE (Content text not null)", c).ExecuteNonQuery();

                //new SQLiteCommand("delete from MY_TABLE", c).ExecuteNonQuery();

                new SQLiteCommand("insert into MY_TABLE (Content) values ('" + e + "')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 2')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 3')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 4')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 5')", c).ExecuteNonQuery();



                c.Close();
            }

            // Send it back to the caller.
            y(e);
        }
    }
}
