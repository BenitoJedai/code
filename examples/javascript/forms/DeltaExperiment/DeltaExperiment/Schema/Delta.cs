using DeltaExperiment.Schema;
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
    public class Delta : DeltaQueries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public Delta(string DataSource = "BatchOfAggregatedTimedDeltas.sqlite")
        {
            #region abort if in design mode
            if (new StackTrace().ToString().Contains("System.ComponentModel.Design.DesignerHost.System.ComponentModel.Design.IDesignerHost"))
                return;
            #endregion

            this.WithConnection = DataSource.AsWithConnection();

            new Create { }.ExecuteNonQuery(WithConnection);
        }

        public void Add(object ticks = null, object x = null, object y = null, object z = null)
        {
            Add(
                new InsertVector
                {
                    ticks = ticks,
                    x = x,
                    y = y,
                    z = z
                }
            );
        }

        public void Add(InsertVector value)
        {
            value.ExecuteNonQuery(WithConnection);
        }

        public void Last(Action<long> yield)
        {
            WithConnection(
                c =>
                {


                    using (var reader = new SelectLast().Command(c).ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dynamic r = new DynamicDataReader(reader);

                            long ticks = r.ticks;

                            yield(ticks);
                        }
                        else
                        {
                            yield(0);
                        }
                    }
                }
            );
        }

        public void Sum(SelectSum e, Action<dynamic> yield)
        {
            WithConnection(
                   c =>
                   {
                       var cmd = e.Command(c);

                       cmd.Parameters.AddWithValue(e);


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

        public void Enumerate(Action<dynamic> yield)
        {
            WithConnection(
                c =>
                {
                    var cmd = new SelectAll
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

    [Description("inferred by running create command")]
    class DeltaColumns
    {
        public long id;

        public long ticks;

        public int x;
        public int y;
        public int z;
    }

    [Description("enables dlinq")]
    class DeltaQuery : Delta, IQueryable<DeltaColumns>
    {

        IEnumerator<DeltaColumns> IEnumerable<DeltaColumns>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        Type IQueryable.ElementType
        {
            get { throw new NotImplementedException(); }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { throw new NotImplementedException(); }
        }

        IQueryProvider IQueryable.Provider
        {
            get { throw new NotImplementedException(); }
        }
    }

    public static partial class XX
    {

        public static int ExecuteNonQuery(this DeltaQueries.InsertVector e, string DataSource = "BatchOfAggregatedTimedDeltas.sqlite")
        {
            return e.ExecuteNonQuery(DataSource.AsWithConnection());
        }

        public static int ExecuteNonQuery(this DeltaQueries.InsertVector e, Action<Action<SQLiteConnection>> WithConnection)
        {
            var i = default(int);
            WithConnection(
                c => i = e.ExecuteNonQuery(c)
            );
            return i;
        }

        public static int ExecuteNonQuery(this DeltaQueries.InsertVector e, SQLiteConnection c)
        {
            var cmd = e.Command(c);
            cmd.Parameters.AddWithValue(e);
            return cmd.ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(this DeltaQueries.Create e, Action<Action<SQLiteConnection>> WithConnection)
        {
            var i = default(int);
            WithConnection(
                c => i = e.ExecuteNonQuery(c)
            );
            return i;
        }

        public static int ExecuteNonQuery(this DeltaQueries.Create e, SQLiteConnection c)
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
