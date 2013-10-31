using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropFileIntoSQLite.Schema
{
    class Table1 : Table1Queries
    {
        //public const string DefaultDataSource = "SQLiteWithDataGridView52.sqlite";
        public const string DefaultDataSource = "SQLiteWithDataGridView53.sqlite";

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
                    new Create { }.ExecuteNonQuery(c);
                    new Table1MetaQueries.CreateMeta { }.ExecuteNonQuery(c);
                }
             );
        }

        // reference to child table?

        public void Delete(Delete value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
            );
        }

        public void SelectAll(Action<dynamic> yield)
        {
            WithConnection(
              c =>
              {
                  new SelectAll().ExecuteReader(c).WithEach(yield);
              }
            );
        }

        public void SelectBytes(SelectBytes value, Action<IDataReader> yield)
        {
            WithConnection(
               c =>
               {
                   value.ExecuteReader(c).WithEachReader(yield);
               }
             );
        }

        public void Insert(Insert value, Action<long> yield)
        {
            WithConnection(
             c =>
             {
                 value.ExecuteNonQuery(c);

                 yield(c.LastInsertRowId);
             }
           );
        }

        public Task<Table1MetaQueries.InsertMeta> InsertMeta(Table1MetaQueries.InsertMeta value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
            );

            return value.ToTaskResult();
        }
    }




    public static partial class XX
    {
        // jsc cannot handle generic that only differ in generic arguments?
        public static void WithEachReader(this SQLiteDataReader reader, Action<IDataReader> y)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
        }

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
                        var message = new { ex.Message, ex.StackTrace }.ToString();

                        //Console.WriteLine("AsWithConnection... error: " + message);

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
