using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace TestSQLiteFromNuGet
{
    class Program
    {
        // later we need to make it running in java and android and AIR

        [STAThread]
        public static void Main(string[] e)
        {
            // http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/

            SQLiteConnection.CreateFile("MyDatabase.sqlite");


            SQLiteConnection m_dbConnection;

            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            {
                string sql = "create table highscores (name varchar(20), score int)";


                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);


                command.ExecuteNonQuery();

            }
            {
                string sql = "insert into highscores (name, score) values ('Me', 9001)";
            }
            {
                string sql = "insert into highscores (name, score) values ('Me', 3000)";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                sql = "insert into highscores (name, score) values ('Myself', 6000)";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                sql = "insert into highscores (name, score) values ('And I', 9001)";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }
            
            {

                string sql = "select * from highscores order by score desc";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);

            }
        }
    }
}
