using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DropFileIntoSQLite.Schema
{
    class Table1 : Table1Queries
    {
        public const string DefaultDataSource = "SQLiteWithDataGridView51.sqlite";

        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public Table1(string DataSource = DefaultDataSource)
        {

            #region abort if in design mode
            if (new StackTrace().ToString().Contains("System.ComponentModel.Design.DesignerHost.System.ComponentModel.Design.IDesignerHost"))
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
                    new Create { }.Command(c).ExecuteNonQuery();
                    new Table1MetaQueries.CreateMeta { }.Command(c).ExecuteNonQuery();

                    //cmd.Dispose();
                }
             );
        }

        // reference to child table?

        public void Delete(Delete value)
        {
            WithConnection(
                c =>
                {
                    var cmd = value.Command(c);
                    cmd.Parameters.AddWithValue(value);
                    cmd.ExecuteNonQuery();
                }
            );
        }

        public void SelectAll(Action<dynamic> yield)
        {
            WithConnection(
              c =>
              {
                  var value = new SelectAll();
                  var cmd = value.Command(c);
                  cmd.ExecuteReaderForEach(yield);
              }
            );
        }

        public void SelectBytes(SelectBytes value, Action<IDataReader> yield)
        {
            WithConnection(
               c =>
               {
                   var cmd = value.Command(c);
                   cmd.Parameters.AddWithValue(value);

                   using (var r = cmd.ExecuteReader())
                   {
                       while (r.Read())
                           yield(r);
                   }
               }
             );
        }

        public void Insert(Insert value, Action<long> yield)
        {
            WithConnection(
             c =>
             {
                 var cmd = value.Command(c);
                 cmd.Parameters.AddWithValue(value);
                 cmd.ExecuteNonQuery();

                 yield(c.LastInsertRowId);
             }
           );
        }

        public void InsertMeta(Table1MetaQueries.InsertMeta value)
        {
            WithConnection(
                c =>
                {
                    var cmd = value.Command(c);

                    cmd.Parameters.AddWithValue(value);

                    cmd.ExecuteNonQuery();
                }
            );
        }
    }




    public static partial class XX
    {


        public static void ExecuteReaderForEach(this SQLiteCommand cmd, Action<dynamic> y)
        {
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
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
