using DeltaExperiment.Schema;
using ScriptCoreLib.Shared.Data;
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

        public Delta(string DataSource = "BatchOfAggregatedTimedDeltas02.sqlite")
        {

            #region abort if in design mode
            var s = new StackTrace();

            if (s.ToString().Contains("System.ComponentModel.Design.DesignerHost.System.ComponentModel.Design.IDesignerHost"))
            {
                // Y:\DeltaExperiment.ApplicationWebService\staging.java\web\java\DeltaExperiment\Delta.java:35: variable WithConnection might already have been assigned

                // make javac happy
                this.WithConnection = null;

                return;
            }
            #endregion

            this.WithConnection = DataSource.AsWithConnection();

            WithConnection(
               c =>
               {
                   new Create { }.ExecuteNonQuery(c);
               }
            );
        }



        public void Add(InsertVector value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
             );
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
                    e.ExecuteReader(c).WithEach(yield);
                }
            );
        }

        public void Enumerate(Action<dynamic> yield)
        {
            WithConnection(
                c =>
                {
                    new SelectAll().ExecuteReader(c).WithEach(yield);


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





        public static Action<Action<SQLiteConnection>> AsWithConnection(this string DataSource, int Version = 3)
        {
            //Console.WriteLine("AsWithConnection...");

            return y =>
            {
                //Console.WriteLine("AsWithConnection... invoke");

                using (var c = DataSource.ToConnection(Version))
                {
                    c.Open();

                    try
                    {
                        y(c);
                    }
                    catch (Exception ex)
                    {
                        var message = new { ex.Message, ex.StackTrace };

                        //Console.WriteLine("AsWithConnection... error: " + message);

                        //java
                        //throw new InvalidOperationException(message.ToString());

                        // php
                        throw new Exception(message.ToString());
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
