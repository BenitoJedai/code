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
        // linqpad wth an extra driver actually opens our sqlite file.
        // and i can just refresh it. sweet

        [STAThread]
        public static void Main(string[] e)
        {
            // http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/

            SQLiteConnection.CreateFile("MyDatabase.sqlite");


            SQLiteConnection m_dbConnection;

            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            #region highscores
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
            #endregion


            Action<string> nonquery = c => new SQLiteCommand(c, m_dbConnection).ExecuteNonQuery();


            #region SELECT * FROM nodes, SELECT * FROM edges
            {
                // http://sqlite.org/datatype3.html



                nonquery("create table nodes (id int, label text)");
                nonquery("insert into nodes (id, label) values (100, 'a')");
                nonquery("insert into nodes (id, label) values (102, 'b')");
                nonquery("insert into nodes (id, label) values (104, 'c')");

                nonquery("create table edges (source int, target int)");
                nonquery("insert into edges (source, target) values (100, 102)");
                nonquery("insert into edges (source, target) values (100, 104)");
                nonquery("insert into edges (source, target) values (102, 104)");

            }
            #endregion

        }
    }
}
