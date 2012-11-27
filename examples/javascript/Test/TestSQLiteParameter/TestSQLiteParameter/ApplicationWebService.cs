using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;
using TestSQLiteParameter.Tables;

namespace TestSQLiteParameter
{


    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        public const string MyDataSource = "SQLiteWithDataGridView51.sqlite";

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            new Table1(MyDataSource).With(
                Table1 =>
                {
                    Table1.Add(
                        // new Tables.Table1.AddQueryParameters
                        new Tables.Table1.AddQuery
                        {
                            // implicit?
                            ContentValue = e
                        }
                    );

                    Table1.Enumerate(
                        // dynamic until we can actually infer what
                        // fields we are getting
                        reader =>
                        {
                            var data = new { reader.ContentKey, reader.ContentValue };

                            // Send it back to the caller.
                            y(data.ToString());
                        }
                    );
                }
            );
        }

    }


    public static partial class XX
    {
        public static Action<Action<SQLiteConnection>> AsWithConnection(this string DataSource)
        {
            return y =>
            {
                using (var c = DataSource.ToConnection())
                {
                    c.Open();

                    y(c);
                }
            };
        }

        public static SQLiteConnection ToConnection(this string DataSource)
        {
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = 3
            };

            var c = new SQLiteConnection(csb.ConnectionString);

            return c;
        }
    }

    class Table1
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        //public Table1(Action<Action<SQLiteConnection>> WithConnection)
        public Table1(string DataSource)
        {
            this.WithConnection = DataSource.AsWithConnection();

            WithConnection(
                c =>
                {
                    using (var reader = new SQLiteCommand(
                        Tables.Table1.CreateQuery.GetSource()
                        , c).ExecuteReader())
                    {

                    }
                }
            );
        }

        public void Add(Tables.Table1.AddQuery value)
        {
            WithConnection(
                  c =>
                  {
                      var cmd = new SQLiteCommand(
                          Tables.Table1.AddQuery.GetSource()
                      , c);

                      Tables.Table1.AddQueryExtensions.AddWithValue(
                           cmd.Parameters,
                           value
                      );

                      using (var reader = cmd.ExecuteReader())
                      {

                      }
                  }
             );
        }

        public void Enumerate(Action<dynamic> yield)
        {
            WithConnection(
                  c =>
                  {
                      var cmd = new SQLiteCommand(
                          Tables.Table1.EnumerateQuery.GetSource()
                      , c);



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


}
