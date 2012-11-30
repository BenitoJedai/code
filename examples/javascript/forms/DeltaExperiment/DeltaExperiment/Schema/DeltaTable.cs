using DeltaExperiment.Schema.DeltaTable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeltaExperiment
{
    [Description("Auto generated. Server side only.")]
    public class DeltaTable
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public DeltaTable(string DataSource = "BatchOfAggregatedTimedDeltas.sqlite")
        {
            #region abort if in design mode
            if (new StackTrace().ToString().Contains("System.ComponentModel.Design.DesignerHost.System.ComponentModel.Design.IDesignerHost"))
                return;
            #endregion

            this.WithConnection = DataSource.AsWithConnection();

            new CreateQuery { }.ExecuteNonQuery(WithConnection);
        }

        public void Add(object ticks = null, object x = null, object y = null, object z = null)
        {
            Add(
                new AddQuery
                {
                    ticks = ticks,
                    x = x,
                    y = y,
                    z = z
                }
            );
        }

        public void Add(AddQuery value)
        {
            value.ExecuteNonQuery(WithConnection);
        }

        public void Enumerate(Action<dynamic> yield)
        {
            WithConnection(
                c =>
                {
                    var cmd = new EnumerateQuery
                    {

                    }.Command(c);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield(new DynamicDataReader(reader));
                        }
                    }
                }
            );
        }
    }


    public static partial class XX
    {

        public static int ExecuteNonQuery(this AddQuery e, string DataSource = "BatchOfAggregatedTimedDeltas.sqlite")
        {
            return e.ExecuteNonQuery(DataSource.AsWithConnection());
        }

        public static int ExecuteNonQuery(this AddQuery e, Action<Action<SQLiteConnection>> WithConnection)
        {
            var i = default(int);
            WithConnection(
                c => i = e.ExecuteNonQuery(c)
            );
            return i;
        }

        public static int ExecuteNonQuery(this AddQuery e, SQLiteConnection c)
        {
            var cmd = e.Command(c);
            cmd.Parameters.AddWithValue(e);
            return cmd.ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(this CreateQuery e, Action<Action<SQLiteConnection>> WithConnection)
        {
            var i = default(int);
            WithConnection(
                c => i = e.ExecuteNonQuery(c)
            );
            return i;
        }

        public static int ExecuteNonQuery(this CreateQuery e, SQLiteConnection c)
        {
            return e.Command(c).ExecuteNonQuery();
        }

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
