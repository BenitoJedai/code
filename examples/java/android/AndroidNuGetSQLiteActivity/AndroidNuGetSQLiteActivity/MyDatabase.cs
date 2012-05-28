using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace AndroidNuGetSQLiteActivity
{
    public static class MyDatabase
    {
        // http://www.eggheadcafe.com/tutorials/ado/20f7912e-6fa7-40eb-b31b-b6f46d4f2c6a/get-started-with-sqlite-and-visual-studio.aspx

        public static string Read(string contentRead)
        {
            using (var c = new SQLiteConnection(

                new SQLiteConnectionStringBuilder
                {
                    DataSource = "MY_DATABASE.sqlite",
                    Version = 3,
                    ReadOnly = true
                }.ConnectionString
                ))
            {
                c.Open();




                var reader = new SQLiteCommand("select Content from MY_TABLE", c).ExecuteReader();
                while (reader.Read())
                {
                    contentRead += "\n";
                    contentRead += (string)reader["Content"];
                }


                c.Close();

            }
            return contentRead;
        }

        public static void Write()
        {
            ;

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

                new SQLiteCommand("delete from MY_TABLE", c).ExecuteNonQuery();

                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 1')", c).ExecuteNonQuery();
                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 2')", c).ExecuteNonQuery();
                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 3')", c).ExecuteNonQuery();
                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 4')", c).ExecuteNonQuery();
                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 5')", c).ExecuteNonQuery();



                c.Close();
            }
        }
    }

}
