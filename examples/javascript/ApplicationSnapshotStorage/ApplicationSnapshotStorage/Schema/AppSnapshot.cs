using ApplicationSnapshotStorage.Schema;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ApplicationSnapshotStorage
{
    public class AppSnapshot : AppSnapshotQueries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public AppSnapshot(string DataSource = "AppSnapshotDatabase.sqlite")
        {

            this.WithConnection = DataSource.AsWithConnection();

            WithConnection(
                c =>
                {
                    new Create { }.Command(c).ExecuteNonQuery();
                }
             );
        }

        public void Insert(string content, Action<string> key)
        {
            WithConnection(
                c =>
                {
                    var value = new Insert { AppSnapshotContent = content };
                    var cmd = value.Command(c);
                    cmd.Parameters.AddWithValue(value);
                    cmd.ExecuteNonQuery();

                    key("" + c.LastInsertRowId);
                }
            );
        }
    }


    public static partial class XX
    {
        public static Action<Action<SQLiteConnection>> AsWithConnection(this string DataSource, int Version = 3)
        {
            Console.WriteLine("AsWithConnection...");

            return y =>
            {
                Console.WriteLine("AsWithConnection... invoke");

                using (var c = DataSource.ToConnection(Version))
                {
                    c.Open();

                    try
                    {
                        y(c);
                    }
                    catch (Exception ex)
                    {
                        var message = new { ex.Message, ex.StackTrace }.ToString();

                        Console.WriteLine("AsWithConnection... error: " + message);

                        throw new InvalidOperationException(message);
                    }
                }
            };
        }

        public static SQLiteConnection ToConnection(this string DataSource, int Version = 3)
        {
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = Version
            };

            var c = new SQLiteConnection(csb.ConnectionString);

            return c;
        }
    }
}
