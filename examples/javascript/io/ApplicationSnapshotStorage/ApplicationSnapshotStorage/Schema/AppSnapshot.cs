using ApplicationSnapshotStorage.Schema;
using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ApplicationSnapshotStorage
{
    static class Trace
    {
        public static void w(string e)
        {
            var now = DateTime.Now;

            Console.WriteLine(now + " " + e);
        }
    }

    public class AppSnapshot : AppSnapshotQueries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public AppSnapshot(string DataSource = "file:AppSnapshotDatabase04.sqlite")
        {
            Trace.w("AppSnapshot");

            this.WithConnection = new SQLiteConnectionStringBuilder { DataSource = DataSource }.AsWithConnection();

            WithConnection(
                c =>
                {
                    new Create { }.ExecuteNonQuery(c);
                }
             );
        }

        public void Insert(Insert content, Action<string> key)
        {
            Trace.w("Insert... " + new { content.AppSnapshotContent.Length });

            // "C:\util\xampp-win32-1.8.0-VC9\xampp\mysql\bin\my.ini"

            //Caused by: java.lang.RuntimeException: Packet for query is too large (1199750 > 1048576). You can change this value on the server by setting the max_allowed_packet' variable.
            //        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteCommand.ExecuteNonQuery(__SQLiteCommand.java:275)
            // Caused by: java.sql.SQLException: Data truncation: Data too long for column 'AppSnapshotContent' at row 1
            // http://stackoverflow.com/questions/8878779/sql-error-1406-data-too-long-for-column

            WithConnection(
                c =>
                {
                    content.ExecuteNonQuery(c);

                    var LastInsertRowId = "" + c.LastInsertRowId;

                    Trace.w("Insert " + new { LastInsertRowId });

                    key(LastInsertRowId);
                }
            );
        }

        internal void Delete(Delete AppSnapshotKey)
        {
            Console.WriteLine("Delete");
            WithConnection(
                c =>
                {
                    AppSnapshotKey.ExecuteNonQuery(c);
                }
            );
        }

        public void SelectAll(Action<string> key)
        {
            Console.WriteLine("SelectAll");
            WithConnection(
                c =>
                {
                    new SelectAll { }.ExecuteReader(c).WithEach(
                        r =>
                        {
                            long AppSnapshotKey = r.AppSnapshotKey;
                            key("" + AppSnapshotKey);
                        }
                    );
                }
            );
        }

        public void SelectBytes(SelectBytes AppSnapshotKey, Action<string> content)
        {
            Console.WriteLine("SelectBytes");
            WithConnection(
                c =>
                {
                    AppSnapshotKey.ExecuteReader(c).WithEach(
                        r =>
                        {
                            string AppSnapshotContent = r.AppSnapshotContent;
                            content(AppSnapshotContent);
                        }
                    );
                }
            );
        }


    }


    public static partial class XX
    {
        public static void WithEach(this SQLiteDataReader reader, Action<dynamic> y)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
        }


        //public static Action<Action<SQLiteConnection>> AsWithConnection(this string DataSource, int Version = 3)
        //{
        //    Console.WriteLine("AsWithConnection...");

        //    return y =>
        //    {
        //        Console.WriteLine("AsWithConnection... invoke");

        //        using (var c = DataSource.ToConnection(Version))
        //        {
        //            c.Open();

        //            try
        //            {
        //                y(c);
        //            }
        //            catch (Exception ex)
        //            {
        //                var message = new { ex.Message, ex.StackTrace }.ToString();

        //                Console.WriteLine("AsWithConnection... error: " + message);

        //                throw new InvalidOperationException(message);
        //            }
        //        }
        //    };
        //}

        //public static SQLiteConnection ToConnection(this string DataSource, int Version = 3)
        //{
        //    var csb = new SQLiteConnectionStringBuilder
        //    {
        //        DataSource = DataSource,
        //        Version = Version
        //    };

        //    var c = new SQLiteConnection(csb.ConnectionString);

        //    return c;
        //}
    }
}
