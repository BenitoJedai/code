using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Extensions;
using System.Diagnostics;

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
            var CommonApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var AnalysisFolder = new DirectoryInfo(
                Path.Combine(
                    CommonApplicationData,
                    "jsc/analysis/"
                )
            );


            var Name = "FileSystemDiscovery" + "/db.sqlite";

            var File = new FileInfo(Path.Combine(AnalysisFolder.FullName, Name));



            #region WithConnection
            Action<Action<SQLiteConnection>> WithConnection =
                h =>
                {
                    new SQLiteConnection("Data Source=" + File.FullName + ";Version=3;Read Only=True;").With(
                           c =>
                           {
                               c.Open();
                                   h(c);

                               c.Close();
                           }
                    );
                };
            #endregion

            WithConnection(
                c =>
                {
                    var sw = new Stopwatch();
                    sw.Start();

                    var nodes = new[] { new { id = 0, label = "" } }.ToList();

                    #region SQLiteCommand
                    new SQLiteCommand(@"
SELECT id, label
FROM nodes
where label like '%.dll'
and not(label like '%\x86_64\%')
and not(label like '%\x64\%')
and not(label like '%\amd64\%')
and not(label like '%\windows64\%')
and not(label like '%\tag\%')
and not(label like '%\tags\%')
and not(label like '%\branches\%')
and not(label like '%\obj\%')
and not(label like '%\Interop.%')
order by id

                    ", c).ExecuteReader().With(
                            r =>
                            {
                                while (r.Read())
                                {
                                    var id = (int)r["id"];
                                    var label = (string)r["label"];

                                    nodes.Add(
                                        new { id, label }
                                    );
                                }
                            }
                        );
                    #endregion

                    #region biggest
                    var biggest = nodes.Where(k => !string.IsNullOrEmpty(k.label)).GroupBy(k => k.id).Select(
                        TargetAssemblyGroup =>
                        {
                            Console.Write(".");
                            
                            if (TargetAssemblyGroup.Count() == 1)
                                return new FileInfo(TargetAssemblyGroup.Single().label);

                            var aa = (
                                from a in TargetAssemblyGroup
                                let f = new FileInfo(a.label)
                                let size = f.Length
                                orderby size descending
                                select new { f, size }
                            ).ToArray();


                            return aa.First().f;
                        }
                    ).ToArray();
                    #endregion


                    biggest.AsParallel().ForAll(
                        target =>
                        {
                            Console.Write("!");

                            Process.Start(
                                new ProcessStartInfo(@"c:\util\jsc\bin\jsc.meta.exe", "AnalyzeAssembliesCommand /Target:\"" + target.FullName + "\"")
                                {
                                    CreateNoWindow = false,
                                    UseShellExecute = false,
                                    WorkingDirectory = @"c:\util\jsc\bin\"
                                }
                            ).WaitForExit();
                        }
                    );
                    sw.Stop();
                    Console.WriteLine("done in " + sw.Elapsed);

                    Debugger.Break();
                }
            );

            Test();

        }

        private static void Test()
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
